using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasContextMenuHandler : IContextMenuHandler
    {
        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            IMenuModel model)
        {
            model.Clear();
          
            model.AddItem(CefMenuCommand.Back, "返回");
            model.AddItem(CefMenuCommand.Reload, "重新加载");
            model.AddItem((CefMenuCommand)ShowDevTools, "开发者工具");
            model.AddItem(CefMenuCommand.Copy,"复制");
            model.AddItem(CefMenuCommand.Paste, "粘贴");
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            if (commandId == (CefMenuCommand)ShowDevTools)
            {
                browser.ShowDevTools();
            }
            else if (commandId == (CefMenuCommand)CloseDevTools)
            {
                browser.CloseDevTools();
            }

            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters,
            IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
}