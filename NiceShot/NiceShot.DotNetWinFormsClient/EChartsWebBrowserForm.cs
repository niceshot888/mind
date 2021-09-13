using CefSharp;
using CefSharp.WinForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using System;
using System.IO;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class EChartsWebBrowserForm : Form
    {
        private string _xqsymbol = string.Empty;

        public EChartsWebBrowserForm(string symbol)
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
            this.Width = 900;
            this.Height = 700;

            _xqsymbol = symbol;

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html5", "gdrs.html?symbol=" + _xqsymbol);

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
