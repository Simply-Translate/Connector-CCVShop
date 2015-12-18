using System;

namespace Connector.CcvShop.Error
{
    public class ErrorLogger
    {
        public static Action<string> ErrorOccurred { get; set; } = (val) => Console.WriteLine(val);
    }
}
