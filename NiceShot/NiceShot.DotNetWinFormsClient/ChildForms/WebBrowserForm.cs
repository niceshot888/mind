using CefSharp;
using CefSharp.WinForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using System;
using System.IO;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class WebBrowserForm : Form
    {
        private string _tscode { get; set; }
        public WebBrowserForm(string tscode, string title, string art_code)
        {
            InitializeComponent();

            _tscode = tscode;
            this.Text = title;

            WinFormHelper.InitChildWindowStyle(this);
            this.MaximizeBox = true;

            this.Width = 1300;
            this.Height = 900;

            //if (!_webBrowser.IsDisposed)
            //    _webBrowser.Dispose();

            //_webBrowser = new WebBrowser();
            //panel2.Controls.Add(_webBrowser);
            //_webBrowser.Dock = DockStyle.Fill;
            //_webBrowser.Show();
            //_webBrowser.Navigate(_link);

            //this.wbPdfViewer.Navigate(string.Format("https://pdf.dfcfw.com/pdf/H2_{0}_1.PDF", art_code));

            var link = string.Format("https://pdf.dfcfw.com/pdf/H2_{0}_1.PDF", art_code);

            //var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            //{
            //    Width = 1300,
            //    Height = 750,
            //    IsExternalLink = true,
            //    Url = "http://www.chinaclear.cn/zdjs/gpzyshg/center_mzzbhg.shtml"
            //});
            //wb.MdiParent = this;
            //wb.Text = "中登股权质押查询";
            //wb.Show();

            //OpenWebBrowserForm(ref wbBrowser, link);
        }

        private void OpenWebBrowserForm(ref ChromeWebBrowser _webBrowser, string _link)
        {
            if (!_webBrowser.IsDisposed)
                _webBrowser.Dispose();

            //_webBrowser = new ChromeWebBrowser();
            //pnlWebBrowserForm.Controls.Add(_webBrowser);
            //_webBrowser.Dock = DockStyle.Fill;
            //_webBrowser.Show();
            //_webBrowser.Navigate(_link);
        }

        private void InitChromeBrowser(string url)
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html5", "pdfviewer.html?pdfurl=" + url);

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

            this.Controls.Add(wb);

            ResizeEnd += (s, e) => ResumeLayout(true);
        }

    }
}
