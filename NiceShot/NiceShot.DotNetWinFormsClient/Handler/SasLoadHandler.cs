
using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasLoadHandler : ILoadHandler
    {

        public void OnLoadingStateChange(IWebBrowser browserControl, LoadingStateChangedEventArgs e)
        {
        }

        public void OnFrameLoadStart(IWebBrowser browserControl, FrameLoadStartEventArgs e)
        {

            var frame = e.Frame;
            if (frame != null)
            {
                var url = frame.Url;
                if (frame.Url.Contains("tdx.com.cn"))
                {
                    if (!string.IsNullOrEmpty(url) && url.Contains("undefined"))
                    {
                        url = frame.Url.Replace("undefined", "");

                        frame.LoadUrl(url);
                    }
                }
            }
        }

        public async void OnFrameLoadEnd(IWebBrowser browserControl, FrameLoadEndEventArgs e)
        {

        }

        public void OnLoadError(IWebBrowser browserControl, LoadErrorEventArgs loadErrorArgs)
        {

        }
    }
}