using CefSharp;
using CefSharp.WinForms;
using NiceShot.Core.MultiThread;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Handler;
using System;
using System.IO;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.DataSyncs
{
    public partial class XQBasicDataSyncForm : Form
    {
        public XQBasicDataSyncForm()
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "html5", "openxueqiuapi.html");

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
            this.pnlChrome.Controls.Add(wb);
        }

        private void btnSyncBasicData_Click(object sender, EventArgs e)
        {
            btnSyncBasicData.Enabled = false;
            btnSyncBalData.Enabled = false;

            XQThread.SyncXQAStockList();
            MessageBox.Show("数据同步成功！");

            btnSyncBasicData.Enabled = true;
            btnSyncBalData.Enabled = true;
        }

        private void btnSyncBalData_Click(object sender, EventArgs e)
        {
            var cookie = tbxCookie.Text;
            if (string.IsNullOrEmpty(cookie))
            {
                MessageBox.Show("请输入Cookie");
                return;
            }

            btnSyncBasicData.Enabled = false;
            btnSyncBalData.Enabled = false;

            XQThread.SyncXQFinBalanceSheetData();
            MessageBox.Show("数据同步成功！");

            btnSyncBasicData.Enabled = true;
            btnSyncBalData.Enabled = true;
        }
    }
}
