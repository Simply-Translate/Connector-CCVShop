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
            Api.Apps.AppsContext apps = new Api.Apps.AppsContext();
            return await apps.Verify(connection);
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
