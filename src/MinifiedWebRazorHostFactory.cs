namespace HtmlMinifier
{
    using System.Web.Mvc;
    using System.Web.WebPages.Razor;
    
    public class MinifiedWebRazorHostFactory : MvcWebRazorHostFactory 
    {
        public override WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
        {
            WebPageRazorHost host = base.CreateHost(virtualPath, physicalPath);

            if (host.IsSpecialPage)
                return host;

            return new MinifiedWebPageRazorHost(virtualPath, physicalPath);            
        }
    }
}
