using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasLifeSpanHandler : ILifeSpanHandler
    {
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName,
            WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo,
            IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;

            return false;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
            
        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
            //关闭弹出页面后，重新刷新登录页
            if (browserControl.Address.IndexOf("login.html", System.StringComparison.Ordinal) != -1)
            {
                //browserControl.Load(Sr.CefClientIndexUrl_V0.ToAppDomainPath());
            }
        }
    }
}