using Connector.CcvShop.Api.Execute;
using Connector.CcvShop.Interface;
using System.Threading.Tasks;

namespace Connector.CcvShop.Api.Apps
{
    public class AppsContext : Base.ContextBase
    {
        public async Task<bool> Verify(IConnectionCcvShop connection = null)
        {
            var p = new ExecuteParams(connection)
            {
                Uri = $"/api/rest/v1/apps/{AppInformation.Instance.AppId}",
                Data = new { is_installed = true }
            };
            var result = await Patch(p);
            return result.Success;
        }
    }
}
