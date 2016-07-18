using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Webshops
{
    public class WebshopsContext : Base.ContextBase
    {
        public async Task<MultipleWebshopsResult> Get(IConnectionCcvShop connection = null, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var uri = "/api/rest/v1/webshops/";
            var p = new ExecuteParams(connection, lan);
            p.SetUri(uri);
            
            return await Get<MultipleWebshopsResult>(p);
        }
    }
}
