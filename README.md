# Connector CCVShop
.NET API connector for the CCVShop
<br />CCVShop API documentation (https://www.ccvshop.nl/mogelijkheden/api)
<br />Created by Simply Translate (http://www.simply-translate.nl/)
<br />Created by Dennis Rosenbaum, any questions, please e-mail me at dennis.rosenbaum@outlook.com

# Get Started
## 1.0 Setup
<ol>
  <li>Install</li>
  <li>Initialize</li>
</ol>
<p>
  <b>1. Install</b>
  <br />Install the code with NuGet or pull the code and create the library yourself.
  <br />
  <br /><b>Install with NuGet</b>
  <br /><img height="30" src="http://tool.simplytranslate.nl/Content/Images/OpenSource/InstallConnectorCcvShop.png" />
  <br />or
  <br /><img height="100" src="http://tool.simplytranslate.nl/Content/Images/OpenSource/InstallConnectorCcvShopGui.png" />
</p>
<p>
  <b>2. Initialize</b>
  <br />You have to initialize some core values. In web projects this can be done within the global.asax. In other projects you can put it in the startup code.
  <br />
  <br /><img height="150" src="http://tool.simplytranslate.nl/Content/Images/OpenSource/CCVShopInitialize.png" />
  <br />
  <br />Implement the urls that you also use within the CCVShop backend. <emp>Your website MUST be https in order for CCVShop to accept the requests.</emp>
  <pre><code>info.HandshakeUrl = "[your CCVShop handshake url]";
info.InstallUrl = "[your CCVShop install url]";
info.UninstallUrl = "[your CCVShop uninstall url]";</code></pre>

  <br />The AppId and SecretKey can also be found within the CCVShop backend. You can find this at the 'Developer App Center'.
  <pre><code>info.AppId = "[your AppId]";
info.SecretKey = "[your SecretKey]";</code></pre>
  
  <br /><img height="200" src="http://tool.simplytranslate.nl/Content/Images/OpenSource/CCVShopInfo.png" />
  <br />
  <br /><i>Optional:</i> If you would like to add your own error handling, you can override the ErrorOccurred Action.
  <br /><code>Connector.CcvShop.Error.ErrorLogger.ErrorOccurred = (error) => [your function or code];</code>
  <br />
  <br />In order for the connector to work, you must save the api public credentials and values in your own database. Because there are many ways to store the values (SQL database, Table Storage, Session based) you have to implement your own <code>Connector.CcvShop.Interface.IConnectionCcvShopRepository</code>.
  <br /><img height="70" src="http://tool.simplytranslate.nl/Content/Images/OpenSource/CCVShopIConnectorRepository.png" />
  <br />
  <br />The AddConnection is used when the Handshake Url is called. The values should be stored in your database.
  <br />The GetForApiPublic should return an IConnectionCcvShop that is stored in your database.
</p>

## 1.1 Install/handshake process
<p>
  In order for the Connector to work, you must add some Handlers (Handshake, Uninstall, Install). Most of the handling can be done by the CCVShop Connector.
  <br />
  <br /><b>Handshake</b></i>
  <pre><code>public ActionResult Handshake(Connector.CcvShop.Model.HandshakeModel model)
{
    var api = new Connector.CcvShop.ApiInstall();
    HttpStatusCode statusCode = api.StartHandshake(Request, model);
    return new HttpStatusCodeResult(statusCode);
}</code></pre>

  <br /><b>Install</b></i>
  <pre><code>public async Task&lt;ActionResult&gt; Install(Connector.CcvShop.Model.InstallModel model)
{
    var connection = Connector.CcvShop.RepositoryContainer.ConnectionRepo.GetForApiPublic(model.api_public);

    var apiHandshake = new Connector.CcvShop.ApiInstall();
    bool succes = await apiHandshake.VerifyInstall(connection);

    if (succes)
    {
        // Connection is made and install is done, now you can access the API
    }
}</code></pre>

  <br /><b>Uninstall</b>
  <pre><code>public ActionResult Uninstall(Connector.CcvShop.Model.UninstallModel model)
{
  // add your uninstall logic
}</code></pre>
</p>

## 1.2 Using the API
Well done, you have initialized your and connected your shop within the app. Now it is time to retrieve, update, delete stuff with the API. At the moment of writing, it is onlypossible to retrieve and update products and categories. 
<br />
<br /><i>Note:</i> An instance of the <code>Connector.CcvShop.Interface.IConnectionCcvShop</code> is neccesary to make calls. It is the responsibility of your code to retrieve the correct connection.

<ul>
  <li>Retrieve</li>
  <li>Update/Patch</li>
</ul>

<p>
  <b>Retrieve</b>
  <br />Code to retrieve products
  <pre><code>var context = new Connector.CcvShop.Api.Products.ProductContext();
var result = await context.Get(connection);
var products = result.items;</code></pre>
  
  Adding parameters and target language:
  <pre><code>var productParams = new Connector.CcvShop.Api.Products.Parameters();
productParams.SetProductName("shoe");
productParams.SetMinstock(20);
var acceptLanguage = "nl";
var result = await context.Get(connectionCcvShop, parameters: productParams, lan: acceptLanguage);</code></pre>

  The difference between <code>context.Get()</code> and <code>context.GetAll()</code> is that the GetAll() will iterate through ALL the items, Get() will only do a single call. For instance: if there are 500 items and only 150 are returned per call, the Get() method will return 150 items, GetAll() will iterate 4 times and will return 500 items.
</p>

<p>
  <b>Update/Patch</b>
  <br />When patching, the properties in the item will be read out (with reflection) and if the property does not have a default value, it is added to the update list. This makes the calls as slim as possible.
  <br />
  <br />Code to update products
  <pre><code>int productId = 12345;
var context = new Connector.CcvShop.Api.Products.ProductContext();
var updateItem = new Connector.CcvShop.Api.Products.ProductResult();
updateItem.description = "Some here";
updateItem.page_title = "And some here";
var success = await context.Patch(connection, productId, updateItem, lan: "en");</code></pre>
</p>

## 2.0 Contribute code
If you would like to contribute to this project, please note:
<br />
<br /><b>Note 1: Create result class</b>
<br />If you would like to create a Result class (like Connector.CcvShop.Api.Products.ProductResult), you can run the Tools project. You can enter the url provided in the API documentation (it's called 'Content-Type') in the textbox and the code will be generated for you. There is even a handy Copy to Clipboard button.
<br />
<br /><b>Note 2: The code is stateless</b>
<br />
<br /><b>Note 3: RateLimit</b>
<br />Every url in the api has a ratelimit, this is not yet included. If you are going to include this, please keep stateless programming in mind.
<br />
<br /><b>Note 4: Structure</b>
<br />The idea behind the structure is that every type (products, attributes, categories, etc.) will get their own folder within the Api folder.
