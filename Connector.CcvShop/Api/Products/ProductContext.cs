using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Products
{
    public class ProductContext : Base.ContextBase
    {
        public async Task<ProductResult> GetById(IConnectionCcvShop connection, int id, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var p = new ExecuteParams(connection, lan)
            {
                Uri = $"/api/rest/v1/products/{id}",
            };
            return await Get<ProductResult>(p);
        }

        public async Task<MultipleProductsResult> Get(IConnectionCcvShop connection = null, Parameters parameters = null, string lan = null)
        {
            //todo: add counter, max 150 per minute

            string uri = $"/api/rest/v1/products/";
            var p = new ExecuteParams(connection, lan);
            p.SetUri(uri, parameters);
            return await Get<MultipleProductsResult>(p);
        }
        
        public async Task<bool> Patch(IConnectionCcvShop connection, int id, ProductResult original, string lan = null)
        {
            //todo: add counter, max 100 per minute
            
            var p = new ExecuteParams(connection, lan)
            {
                Uri = $"/api/rest/v1/products/{id}",
                Data = Core.Compare.GetChanges(original)
            };

            return (await Patch(p)).Success;
        }
    }
}
