using CefSharp;
using CefSharp.WinForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class GetThsCookieForm : Form
    {
        private List<string> _cookienames_dics;
        private string _cookies = string.Empty;

        public GetThsCookieForm()
        {
            InitializeComponent();

            _cookies = string.Empty;
            _cookienames_dics = new List<string>();
            ChromiumWebBrowser wb = new ChromiumWebBrowser("http://basic.10jqka.com.cn/api/stock/finance/601155_cash.json?rnd=" + new Random().Next(0, 1000000))
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
                LoadHandler = new SasLoadHandler(),
            };
            wb.FrameLoadStart += (s, argsi) =>
            {
                _cookienames_dics = new List<string>();
                _cookies = string.Empty;

                var b = (ChromiumWebBrowser)s;
                if (argsi.Frame.IsMain)
                {
                    b.SetZoomLevel(0.0);
                }
            };
            wb.FrameLoadEnd += cwbrowser_FrameLoadEnd;

            this.Controls.Add(wb);

            ResizeEnd += (s, e) => ResumeLayout(true);

            WinFormHelper.InitChildWindowStyle(this);
        }

        private void cwbrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            var cookieManager = CefSharp.Cef.GetGlobalCookieManager();
            CookieVisitor visitor = new CookieVisitor(all_cookies =>
            {
                var dicNames = new List<string> { "v", "vvvv" };
                foreach (var obj in all_cookies)
                {
                    if (!_cookienames_dics.Contains(obj.Item1) && dicNames.Contains(obj.Item1))
                    {
                        _cookies += obj.Item1 + "=" + obj.Item2 + ";";
                        _cookienames_dics.Add(obj.Item1);
                    }
                }
            });
            //visitor.SendCookie += visitor_SendCookie;
            cookieManager.VisitAllCookies(visitor);
        }

        private void visitor_SendCookie(CefSharp.Cookie obj)
        {
            var dicNames = new List<string> { "v", "vvvv" };
            if (!_cookienames_dics.Contains(obj.Name) && dicNames.Contains(obj.Name))
            {
                _cookies += obj.Name + "=" + obj.Value + ";";
                _cookienames_dics.Add(obj.Name);
            }

            Clipboard.SetDataObject("reviewJump=nojump; searchGuide=sg; Hm_lvt_78c58f01938e4d85eaf619eae71b4ed1=1604383080,1604975213,1605507846,1605802152; Hm_lpvt_78c58f01938e4d85eaf619eae71b4ed1=1606799035;" + _cookies);
        }

        private void btnGetCookie_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("reviewJump=nojump; searchGuide=sg; Hm_lvt_78c58f01938e4d85eaf619eae71b4ed1=1604383080,1604975213,1605507846,1605802152; historystock=600000%7C*%7C000997%7C*%7C000651%7C*%7CHK0960%7C*%7C603707; Hm_lpvt_78c58f01938e4d85eaf619eae71b4ed1=1606799035;" + cookies);
            //Clipboard.SetDataObject("reviewJump=nojump; searchGuide=sg; Hm_lvt_78c58f01938e4d85eaf619eae71b4ed1=1604383080,1604975213,1605507846,1605802152; historystock=600000%7C*%7C000997%7C*%7C000651%7C*%7CHK0960%7C*%7C603707; Hm_lpvt_78c58f01938e4d85eaf619eae71b4ed1=1606799035;" + cookies);
        }
    }
}
