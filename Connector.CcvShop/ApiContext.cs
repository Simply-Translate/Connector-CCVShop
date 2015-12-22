using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector.CcvShop
{
    public class ApiContext
    {
        public Api.Products.ProductContext Products => new Api.Products.ProductContext();

        /// <summary>
        /// Keep in mind that this will only work single server
        /// If you are using multiple servers, please store this rate in a database and read it from there.
        /// </summary>
        public int CurrentRate { get; set; } = 0;
        
        private static volatile ApiContext instance;
        private static object syncRoot = new object();

        private ApiContext() { }

        public static ApiContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ApiContext();
                    }
                }

                return instance;
            }
        }
    }
}
