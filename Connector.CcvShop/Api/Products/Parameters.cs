using System.Web;

namespace Connector.CcvShop.Api.Products
{
    public class Parameters : Base.ParameterBase
    {
        /// <summary>
        /// Number of the product
        /// </summary>
        public void SetProductNumber(string productNumber) => SetParam("productnumber", productNumber);

        /// <summary>
        /// Specific part (min 3 char.) of productname with a LIKE matching method. Encode according to RFC 3986.
        /// </summary>
        public void SetProductName(string productName)
        {
            if(productName.Length < 3)
            {
                string errorMessage = "Specific part of productname MUST have at least 3 chars.";
                Error.ErrorLogger.ErrorOccurred(errorMessage);
                throw new System.NotSupportedException(errorMessage);
            }

            var encodedProductName = HttpUtility.UrlEncode(productName);

            SetParam("productname", encodedProductName);
        }

        /// <summary>
        /// Specific stock of the products in the result.
        /// </summary>
        public void SetStock(int amount) => SetParam("stock", amount);

        /// <summary>
        /// Minimal stock of the products in the result.
        /// </summary>
        public void SetMinstock(int amount) => SetParam("minstock", amount);

        /// <summary>
        /// Maximal stock of the products in the result.
        /// </summary>
        public void SetMaxstock(int amount) => SetParam("maxstock", amount);
    }
}
