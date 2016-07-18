using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Webshops.Settings
{
    public class SettingsContext : Base.ContextBase
    {
        public async Task<SettingsResult> GetByWebshopId(IConnectionCcvShop connection, int id, string lan = null)
        {
            //todo: add counter, max 150 per minute

            var p = new ExecuteParams(connection, lan)
            {
                Uri = $"/api/rest/v1/webshops/{id}/settings/",
            };
            return await Get<SettingsResult>(p);
        }

        public async Task<SettingsResult> GetByFirstWebshop(IConnectionCcvShop connection, string lan = null)
        {
            var context = new WebshopsContext();
            var result = await context.Get(connection, lan: lan);

            var firstShop = result?.items.FirstOrDefault();
            if(firstShop == null)
                return null;

            return await GetByWebshopId(connection, firstShop.id);
        }
    }
}
