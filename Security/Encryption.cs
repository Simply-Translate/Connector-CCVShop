using System.Security.Cryptography;
using System.Text;

namespace Connector.CcvShop.Security
{
    internal class Encryption
    {
        internal static string ComputeHmacSha512(string key, string data)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);

            using (var sha512 = new HMACSHA512(keyBytes))
            {
                return sha512.ComputeHashForString(data).Replace("-", "").ToLower();
            }
        }
    }
}
