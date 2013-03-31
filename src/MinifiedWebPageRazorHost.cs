namespace HtmlMinifier
{
    using System.Web.Mvc.Razor;
    using System.Web.Razor.Generator;

    internal class MinifiedWebPageRazorHost : MvcWebPageRazorHost
    {
        public MinifiedWebPageRazorHost(string virtualPath, string physicalPath)
            : base(virtualPath, physicalPath)
        {
        }

        public override RazorCodeGenerator DecorateCodeGenerator(RazorCodeGenerator incomingCodeGenerator)
        {
            if (incomingCodeGenerator is CSharpRazorCodeGenerator)
            {
                return new MinifiedCSharpRazorCodeGenerator(
                            incomingCodeGenerator.ClassName, 
                            incomingCodeGenerator.RootNamespaceName, 
                            incomingCodeGenerator.SourceFileName, 
                            incomingCodeGenerator.Host);
            }

            if (incomingCodeGenerator is VBRazorCodeGenerator)
            {
                return new MinifiedVBRazorCodeGenerator(
                            incomingCodeGenerator.ClassName,
                            incomingCodeGenerator.RootNamespaceName,
                            incomingCodeGenerator.SourceFileName,
                            incomingCodeGenerator.Host);
            }

            return base.DecorateCodeGenerator(incomingCodeGenerator);
        }
    }
}
