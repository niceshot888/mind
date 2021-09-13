using CefSharp;
using CefSharp.WinForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class ChromeWebBrowser : Form
    {
        private string _xqsymbol = string.Empty;

        public ChromeWebBrowser(ChromeWebBrowserCriteria criteria)
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
            this.Width = criteria.Width;
            this.Height = criteria.Height;

            _xqsymbol = criteria.Symbol;

            //"gdrs.html?symbol=" + _xqsymbol
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html5", criteria.Url);
            if (criteria.IsExternalLink)
                fileName = criteria.Url;

            ResizeBegin += (s, e) => SuspendLayout();

            ChromiumWebBrowser wb = new ChromiumWebBrowser(fileName)
            {
                Dock = DockStyle.Fill,
                AllowDrop = true, 
                BrowserSettings = new BrowserSettings
                {
                    FileAccessFromFileUrls = CefState.Enabled,
                    UniversalAccessFromFileUrls = CefState.Enabled,
                    ApplicationCache = CefState.Enabled
                },
                LifeSpanHandler = new SasLifeSpanHandler(),
                JsDialogHandler = new SasJsDialogHandler(),
                DownloadHandler = new SasDownloadHandler(),
                MenuHandler = new SasContextMenuHandler(),
                LoadHandler = new SasLoadHandler()
            };

            if (fileName.Contains("bizmodel.html"))
            {
                //CefSharpSettings.ConcurrentTaskExecution = true;
                //wb.JavascriptObjectRepository. = true;
                //wb.JavascriptObjectRepository.Register("tooltip", new BizModelJsObject() { Content = criteria.Content }, isAsync: true, options: BindingOptions.DefaultBinder);
            }
                //wb.RegisterJsObject("tooltip", new BizModelJsObject() { Content=criteria.Content }, new BindingOptions() { CamelCaseJavascriptNames = false });
            //wb.ExecuteScriptAsync("var content='"+ criteria.Content + "'");
            //wb.ExecuteScriptAsync("document.getElementById('bizmodel').innerHTML='" + criteria.Content + "'");

            wb.FrameLoadStart += (s, argsi) =>
            {
                var b = (ChromiumWebBrowser)s;
                if (argsi.Frame.IsMain)
                {
                    b.SetZoomLevel(0.0);
                }
            };
            wb.FrameLoadEnd += Wb_FrameLoadEnd;
            this.Controls.Add(wb);

            ResizeEnd += (s, e) => ResumeLayout(true);
        }

        private void Wb_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            var cookieManager = Cef.GetGlobalCookieManager();

            CookieVisitor visitor = new CookieVisitor(all_cookies => {
                
            });
            cookieManager.VisitAllCookies(visitor);
        }
    }
}
