namespace HtmlMinifier
{
    using System.Text.RegularExpressions;
    using System.Web.Configuration;
    using System.Web.Mvc.Razor;
    using System.Web.Razor;
    using System.Web.Razor.Parser.SyntaxTree;
    using System.Web.WebPages.Razor;
    
    internal class MinifiedCSharpRazorCodeGenerator : MvcCSharpRazorCodeGenerator
    {
        public MinifiedCSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
            : base(className, rootNamespaceName, sourceFileName, host)
        {
        }

        public override void VisitSpan(Span span)
        {
            if (span.Kind == SpanKind.Markup && !IsDebuggingEnabled())
            {
                string content = span.Content;
                
                content = Regex.Replace(content, @"\s*\n\s*", "", RegexOptions.Multiline);
                content = Regex.Replace(content, @"\s{2,}", " ", RegexOptions.Multiline);

                span.Content = content;
            }
       
            base.VisitSpan(span);
        }

        private bool IsDebuggingEnabled()
        {
            if (System.Web.HttpContext.Current != null)
                return System.Web.HttpContext.Current.IsDebuggingEnabled;

            string virtualPath = ((WebPageRazorHost)Host).VirtualPath;
            return ((CompilationSection)WebConfigurationManager.GetSection("system.web/compilation", virtualPath)).Debug;
        }
    }
}
