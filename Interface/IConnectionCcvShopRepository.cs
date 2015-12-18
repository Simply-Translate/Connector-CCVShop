namespace Connector.CcvShop.Interface
{
    public interface IConnectionCcvShopRepository
    {
        IConnectionCcvShop GetForApiPublic(string api_public);
        void AddConnection(string api_public, string api_secret, string api_root, string return_url);
    }
}
