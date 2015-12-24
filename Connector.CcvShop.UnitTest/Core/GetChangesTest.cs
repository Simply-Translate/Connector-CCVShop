using Microsoft.VisualStudio.TestTools.UnitTesting;
using Connector.CcvShop.Api.Products;
using Newtonsoft.Json;

namespace Connector.CcvShop.UnitTest.Core
{
    [TestClass]
    public class GetChangesTest
    {
        [TestMethod]
        public void TestProductResultChanges()
        {
            ProductResult original = new ProductResult()
            {
                name = "Name",
                description = "This is my product<br />Very nice and neat.",
                shortdescription = "This is the short description",
                page_title = "Page Title",
                meta_keywords = "Keyword 1, Keyword 2",
                meta_description = "meta Descr"
            };

            var changes = CcvShop.Core.Compare.GetChanges(original);
            
            var changesJson = JsonConvert.SerializeObject(changes);
            string expectedChanges = "{\"name\":\"Name\",\"shortdescription\":\"This is the short description\",\"description\":\"This is my product<br />Very nice and neat.\",\"meta_description\":\"meta Descr\",\"meta_keywords\":\"Keyword 1, Keyword 2\",\"page_title\":\"Page Title\"}";
            Assert.AreEqual(expectedChanges, changesJson);

        }
    }
}
