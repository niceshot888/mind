using System.Windows.Forms;
using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasJsDialogHandler : IJsDialogHandler
    {
        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType,
            string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            const string caption = "友情提示";
            switch (dialogType)
            {
                case CefJsDialogType.Alert:
                    MessageBox.Show(messageText, caption);
                    suppressMessage = true;
                    return false;
                case CefJsDialogType.Confirm:
                    {
                        var dr = MessageBox.Show(messageText, caption, MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            callback.Continue(true, string.Empty);
                            suppressMessage = false;
                            return true;
                        }

                        callback.Continue(false, string.Empty);
                        suppressMessage = false;
                        return true;
                    }
            }

            return false;
        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload,
            IJsDialogCallback callback)
        {

            return true;
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {

        }

        public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {
            
        }

        public bool OnBeforeUnloadDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, string messageText, bool isReload, IJsDialogCallback callback)
        {
            throw new System.NotImplementedException();
        }
    }
}