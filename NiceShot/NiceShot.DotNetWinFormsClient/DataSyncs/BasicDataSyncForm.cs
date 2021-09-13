using Newtonsoft.Json;
using NiceShot.Core.Crawlers;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using NiceShot.Core.MultiThread;
using NiceShot.DotNetWinFormsClient.Core;
using System;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.DataSyncs
{
    public partial class BasicDataSyncForm : Form
    {
        private string tdx_secuinfo_url = "http://page.tdx.com.cn:7615/TQLEX?Entry=CWServ.SecuInfo";

        public BasicDataSyncForm()
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
        }

        private void btnSyncXQABSD_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            XQThread.SyncXQAStockList();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void btnSyncXQHKBSD_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            XQThread.SyncXQHKStockList();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void btnSyncEMABSD_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            EMThread.SyncEMACompanySurveyData();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void btnSyncEMHKBSD_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            EMThread.SyncEMHKCompanyProfile();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void EnableButtons(bool enabled)
        {
            btnSyncXQABSD.Enabled = enabled;
            btnSyncEMABSD.Enabled = enabled;
            btnSyncXQHKBSD.Enabled = enabled;
            btnSyncEMHKBSD.Enabled = enabled;
            btnSyncTdxForeEarn.Enabled = enabled;
            btnSyncSinaBonds.Enabled = enabled;
            //btnSyncHkTradeType.Enabled = enabled;
        }

        private void btnSyncHkTradeType_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            XQThread.SyncXQHKStockTradeType();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void btnSyncTdxForeEarn_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            TDXThread.SyncForeRoeAndNP();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }

        private void btnSyncSinaBonds_Click(object sender, EventArgs e)
        {
            EnableButtons(false);

            SinaThread.SyncSinaBondData();
            MessageBox.Show("数据同步成功！");

            EnableButtons(true);
        }
    }
}
