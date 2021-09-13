using CefSharp;
using CefSharp.WinForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using System;
using System.IO;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class ViewPdfForm : Form
    {

        private string _tscode { get; set; }
        public ViewPdfForm(string tscode,string title, string art_code)
        {
            InitializeComponent();

            _tscode = tscode;
            this.Text = title;

            WinFormHelper.InitChildWindowStyle(this);
            this.MaximizeBox = true;
            
            this.Width = 1300;
            this.Height = 900;

            this.wbPdfViewer.Navigate(string.Format("https://pdf.dfcfw.com/pdf/H2_{0}_1.PDF",art_code));
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
