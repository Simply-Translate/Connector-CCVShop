namespace Connector.CcvShop
{
    public class AppInformation
    {
        public string HandshakeUrl { get; set; } 
        public string InstallUrl { get; set; } 
        public string UninstallUrl { get; set; }
        public int AppId { get; set; }
        public string SecretKey { get; set; }


        private static AppInformation instance;

        private AppInformation() { }

        public static AppInformation Instance
        {
            get
            {
                if (instance == null)
                    instance = new AppInformation();
                return instance;
            }
        }

    }
}
