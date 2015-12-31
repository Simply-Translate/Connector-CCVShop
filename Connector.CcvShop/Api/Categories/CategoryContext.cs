using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Categories
{
    public class CategoryContext : Base.ContextBase
    {
        public async Task<CategoryResult> GetById(IConnectionCcvShop connection, int id, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var p = new ExecuteParams(connection, lan)
            {
                Uri = $"/api/rest/v1/categories/{id}",
            };
            return await Get<CategoryResult>(p);
        }

        public async Task<MultipleCategoriesResult> Get(IConnectionCcvShop connection = null, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var uri = "/api/rest/v1/categories/";
            var p = new ExecuteParams(connection, lan);
            p.SetUri(uri);

            return await Get<MultipleCategoriesResult>(p);
        }

        public async Task<List<MultipleCategoriesResult>> GetAll(IConnectionCcvShop connection = null, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var uri = "/api/rest/v1/categories/";
            var p = new ExecuteParams(connection, lan);
            p.SetUri(uri);

            return await GetAll<MultipleCategoriesResult>(p);
        }
        
        public async Task<bool> Patch(IConnectionCcvShop connection, int id, CategoryResult original, string lan = null)
        {
            //todo: add counter, max 100 per minute
            
            var p = new ExecuteParams(connection, lan)
            {
                Uri = $"/api/rest/v1/categories/{id}",
                Data = Core.Compare.GetChanges(original)
            };

            return (await Patch(p)).Success;
        }
    }
}
