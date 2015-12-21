namespace Connector.CcvShop.Security
{
    internal class Hash
    {
        internal static bool HashIsOk(string url, string jsonData, string hash) => GenerateHash(url, jsonData) == hash.ToLower();

        internal static string GenerateHash(string url, string jsonData)
        {
            string dataToHash = $"{url}|{jsonData}";
            return Encryption.ComputeHmacSha512(AppInformation.Instance.SecretKey, $"{url}|{jsonData}");
        }
    }
}
