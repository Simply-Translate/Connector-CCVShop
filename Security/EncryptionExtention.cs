using System;
using System.Security.Cryptography;
using System.Text;

namespace Connector.CcvShop.Security
{
    internal static class EncryptionExtention
    {
        internal static string ComputeHashForString(this HashAlgorithm md5, string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var computeHash = md5.ComputeHash(bytes);
            return BitConverter.ToString(computeHash);
        }
    }
}
