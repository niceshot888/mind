using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasStringVisitor : TaskStringVisitor
    {
        private string html;


        public string Html
        {
            get { return html; }
            set { html = value; }
        }
    }
}