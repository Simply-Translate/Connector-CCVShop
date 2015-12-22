using Newtonsoft.Json;

namespace Connector.CcvShop.Api.Execute
{
    internal class ExecuteResult
    {
        public bool Success { get; set; }
        public string Response { get; set; }

        public T ConvertToData<T>() => Success ? JsonConvert.DeserializeObject<T>(Response) : default(T);

        public ExecuteResult(bool success, string response)
        {
            Success = success;
            Response = response;
        }
    }
}
