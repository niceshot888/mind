using System.Collections.Generic;
using CefSharp;

namespace NiceShot.DotNetWinFormsClient.Handler
{
    public class SasDialogHandler : IDialogHandler
    {
        public bool OnFileDialog(IWebBrowser browserControl, IBrowser browser, CefFileDialogMode mode, string title,
            string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
        {
            return true;
        }

        public bool OnFileDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
        {
            throw new System.NotImplementedException();
        }
    }
}