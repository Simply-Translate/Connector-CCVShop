using Connector.CcvShop.Model;
using System.IO;
using System.Net;
using System;
using System.Threading.Tasks;
using Connector.CcvShop.Interface;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Connector.CcvShop
{
    public class ApiInstall
    {
        public ApiInstall()
        {
        }

        private bool HashIsOkHandshake(string hash, string jsonData) => Security.Hash.HashIsOk(AppInformation.Instance.HandshakeUrl, jsonData, hash);
        private bool HashIsOkUninstall(string hash, string jsonData) => Security.Hash.HashIsOk(AppInformation.Instance.UninstallUrl, jsonData, hash);
        
        public HttpStatusCode StartHandshake(System.Web.HttpRequestBase request, HandshakeModel model)
        {
            request.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonData = new StreamReader(request.InputStream).ReadToEnd();

            string hash = request.Headers["x-hash"];
            if (!HashIsOkHandshake(hash, jsonData))
            {
                var myHash = Security.Hash.GenerateHash(AppInformation.Instance.HandshakeUrl, jsonData);
                string errorMessage = GetErrorHandshakeHashNotOk(model, jsonData, hash, myHash);
                Error.ErrorLogger.ErrorOccurred(errorMessage);

                return HttpStatusCode.ServiceUnavailable;
            }

            var repo = RepositoryContainer.ConnectionRepo;
            var connection = repo.GetForApiPublic(model.api_public);
            if (connection != null)
            {
                string errorMessage = $"Trying to do a handshake with {model.api_public}, but there is already a connection for this exact api_public.";

                Error.ErrorLogger.ErrorOccurred(errorMessage);

                return HttpStatusCode.ServiceUnavailable;
            }

            repo.AddConnection(model.api_public, model.api_secret, model.api_root, model.return_url);

            return HttpStatusCode.OK;
        }

        public async Task<bool> VerifyInstall(IConnectionCcvShop connection)
        {
            string apiPublic = connection.ApiPublic;
            var sPublicKey = apiPublic;
            var sSecretKey = connection.ApiSecret;
            var sMethod = "PATCH";
            var sUri = $"/api/rest/v1/apps/{AppInformation.Instance.AppId}";
            var sApiRoot = connection.ApiRoot;
            var requestUri = $"{sApiRoot}{sUri}";

            var sTimeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss+00:00");

            var aData = new { is_installed = true };
            var aDataJson = JsonConvert.SerializeObject(aData);
            List<string> aDataToHash = new List<string>();
            aDataToHash.Add(sPublicKey);
            aDataToHash.Add(sMethod);
            aDataToHash.Add(sUri);
            aDataToHash.Add(aDataJson);
            aDataToHash.Add(sTimeStamp);
            var sStringToHash = string.Join("|", aDataToHash);
            var sHash = Security.Encryption.ComputeHmacSha512(sSecretKey, sStringToHash);
            sHash = sHash.ToLower();

            using (var client = new HttpClient())
            {
                var method = new HttpMethod(sMethod);
                client.DefaultRequestHeaders.Add("x-date", sTimeStamp);
                client.DefaultRequestHeaders.Add("x-hash", sHash);
                client.DefaultRequestHeaders.Add("x-public", sPublicKey);

                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=UTF-8");

                var request = new HttpRequestMessage(method, requestUri)
                {
                    Content = new StringContent(aDataJson)
                };

                HttpResponseMessage response = new HttpResponseMessage();
                // In case you want to set a timeout
                //CancellationToken cancellationToken = new CancellationTokenSource(60).Token;

                response = client.SendAsync(request).Result;
                bool success = response.IsSuccessStatusCode;
                if (!success)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    string errorMessage = GetErrorVerifyInstallResponseNoSuccess(sPublicKey, sSecretKey, sMethod, sUri, sApiRoot, requestUri, sTimeStamp, aDataJson, sStringToHash, sHash, responseContent);

                    Error.ErrorLogger.ErrorOccurred(errorMessage);
                }
                return success;
            }
        }

        private static string GetErrorVerifyInstallResponseNoSuccess(string sPublicKey, string sSecretKey, string sMethod, string sUri, string sApiRoot, string requestUri, string sTimeStamp, string aDataJson, string sStringToHash, string sHash, string responseContent)
        {
            return $@"Sending request to verify Install. This went totaly wrong!
<br />
<br /><b>Request:</b>
<br />sPublicKey: {sPublicKey}
<br />sSecretKey: {sSecretKey}
<br />sMethod: {sMethod}
<br />sUri: {sUri}
<br />sApiRoot: {sApiRoot}
<br />requestUri: {requestUri}
<br />aDataJson: {aDataJson}
<br />sTimeStamp: {sTimeStamp}
<br />dataToHash: {sStringToHash}
<br />hash: {sHash}
<br />
<br /><b>Response:</b>
<br />{responseContent}";
        }

        private static string GetErrorHandshakeHashNotOk(HandshakeModel model, string jsonData, string hash, string myHash)
        {
            return $@"Trying to do a handshake with api_public ({model.api_public}), but it went wrong.
<br />Api public: {model.api_public}
<br />Api secret: {model.api_secret}
<br />Api root: {model.api_root}
<br />Return url: {model.return_url}
<br />X-Hash: {hash}
<br />Handshake url: {AppInformation.Instance.HandshakeUrl}
<br />My hash: {myHash}
<br />Json data: {jsonData}";
        }
    }
}
