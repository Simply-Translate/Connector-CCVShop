using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Products
{
    public class ProductContext : Base.ContextBase
    {
        public async Task<ProductResult> GetById(int id, IConnectionCcvShop connection = null)
        {
            var p = new ExecuteParams(connection)
            {
                Uri = $"/api/rest/v1/products/{id}",
            };
            return await Get<ProductResult>(p);
        }

        public async Task<MultipleProductsResult> Get(Parameters parameters, IConnectionCcvShop connection = null)
        {
            string uri = $"/api/rest/v1/products/";
            var p = new ExecuteParams(connection);
            p.SetUri(uri, parameters);
            return await Get<MultipleProductsResult>(p);
        }
    }
}
