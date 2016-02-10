using Connector.CcvShop.Api.Execute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Base
{
    public abstract class ContextBase
    {
        //TODO: Do something with the rate limit here
                
        internal async Task<T> Get<T>(ExecuteParams param) => (await Get(param)).ConvertToData<T>();
        internal async Task<List<T>> GetAll<T>(ExecuteParams param) where T : MultipleResultBase 
        {
            Uri uri = new Uri(param.Uri, UriKind.Relative);
            List<T> resultList = new List<T>();
            do
            {
                param.SetUri(uri.ToString());
                var result = await Get(param);
                var multipleResultBase = result.ConvertToData<MultipleResultBase>();
                resultList.Add(result.ConvertToData<T>());
                var tempUri = multipleResultBase.next.ToString();
                var index = tempUri.IndexOf("/api");
                if (index == -1)
                    break;
                uri = new Uri(tempUri.Substring(index), UriKind.Relative);
            } while (uri != null);

            return resultList;
        }

        internal async Task<ExecuteResult> Get(ExecuteParams param)
        {
            param.Method = Methods.Get;
            return await ExecuteCall(param);
        }

        internal async Task<T> Patch<T>(ExecuteParams param) => (await Patch(param)).ConvertToData<T>();

        internal async Task<ExecuteResult> Patch(ExecuteParams param)
        {
            param.Method = Methods.Patch;
            return await ExecuteCall(param);
        }

        private async Task<T> Execute<T>(ExecuteParams param)
        {
            var callResult = await ExecuteCall(param);
            if(callResult.Success)
                return JsonConvert.DeserializeObject<T>(callResult.Response);
            return default(T);
        }

        private async Task<ExecuteResult> ExecuteCall(ExecuteParams param)
        {
            var connection = param.Connection;
            
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss+00:00");
            var hash = GenerateHash(param, timeStamp);

            using (var client = new HttpClient())
            {
                var request = ConstructRequest(param, client, timeStamp, hash);

                var httpResponse = client.SendAsync(request).Result;
                var response = await httpResponse.Content.ReadAsStringAsync();
                bool success = httpResponse.IsSuccessStatusCode;
                if (!success)
                {
                    var errorMessage = GetResponseError(param, timeStamp, hash, response);
                    Error.ErrorLogger.ErrorOccurred(errorMessage);
                }

                return new ExecuteResult(success, response);
            }
        }

        private HttpRequestMessage ConstructRequest(ExecuteParams param, HttpClient client, string timeStamp, string hash)
        {
            var httpMethod = new HttpMethod(param.Method);

            SetHeaders(param, timeStamp, hash, client);

            var request = new HttpRequestMessage(httpMethod, $"{param.Connection.ApiRoot}{param.Uri}");
            if (param.HasData)
                request.Content = new StringContent(param.DataJson);
            return request;
        }

        private static void SetHeaders(ExecuteParams param, string timeStamp, string hash, HttpClient client)
        {
            string publicKey = param.Connection.ApiPublic;

            if (!string.IsNullOrEmpty(param.TargetLanguage))
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", param.TargetLanguage);

            client.DefaultRequestHeaders.Add("x-date", timeStamp);
            client.DefaultRequestHeaders.Add("x-hash", hash);
            client.DefaultRequestHeaders.Add("x-public", publicKey);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=UTF-8");
        }

        private string GetResponseError(ExecuteParams param, string timeStamp, string hash, string responseContent)
        {
            return $@"Sending CCVShop request. This went totaly wrong!
<br />
<br /><b>Request:</b>
<br />PublicKey: {param.Connection.ApiPublic}
<br />SecretKey: {param.Connection.ApiSecret}
<br />Method: {param.Method}
<br />Uri: {param.Uri}
<br />ApiRoot: {param.Connection.ApiRoot}
<br />DataJson: {param.DataJson}
<br />TimeStamp: {timeStamp}
<br />Hash: {hash}
<br />
<br /><b>Response:</b>
<br />{responseContent}";
        }

        private string GenerateHash(ExecuteParams param, string timeStamp)
        {
            var secretKey = param.Connection.ApiSecret;
            var publicKey = param.Connection.ApiPublic;
            var method = param.Method;
            var uri = param.Uri;
            var dataJson = param.DataJson;

            List<string> aDataToHash = new List<string>();
            aDataToHash.Add(publicKey);
            aDataToHash.Add(method);
            aDataToHash.Add(uri);
            aDataToHash.Add(dataJson);
            aDataToHash.Add(timeStamp);
            var sStringToHash = string.Join("|", aDataToHash);
            var sHash = Security.Encryption.ComputeHmacSha512(secretKey, sStringToHash);
            sHash = sHash.ToLower();
            return sHash;
        }
    }
}
