using NiceShot.Core.Enums;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.DataSyncs;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.UserControls
{
    public partial class TopMenu : UserControl
    {
        public TopMenu()
        {
            InitializeComponent();
        }

        private void tsm_a_normal_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            if (!parentForm._backgroundWorker.IsBusy)
                parentForm._backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 港股列表
        /// </summary>
        private void tsm_hk_common_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            parentForm._marketType = MarketType.HK;

            if (!parentForm._backgroundWorker.IsBusy)
                parentForm._backgroundWorker.RunWorkerAsync();
        }

        private void tsmi_market_datasync_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);

            var wb = new BasicDataSyncForm();
            //wb.MdiParent = parentForm;
            wb.Text = "同步市场数据";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_ths_cashflow_add_datasync_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);

            var wb = new GetThsCookieForm();
            ////wb.MdiParent = parentForm;
            wb.Text = "获取同花顺Cookie";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_zdgqzycx_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);

            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "http://www.chinaclear.cn/zdjs/gpzyshg/center_mzzbhg.shtml"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "中登股权质押查询";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_hsgt_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "http://data.eastmoney.com/hsgtcg/StockHdStatistics.aspx?stock=000961"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "东方财富-沪深港通数据查询";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_lbty_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "https://robo.datayes.com/v2/search?query="
            });
            //wb.MdiParent = parentForm;
            wb.Text = "萝卜投研数据查询";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_ths_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "http://stockpage.10jqka.com.cn/000961/finance/"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "同花顺数据查询";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_gdzjc_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "http://data.eastmoney.com/executive/gdzjc/000961-jjc.html"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "东方财富-股东增减持";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_money_supply_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1000,
                Height = 600,
                Url = "moneysupply.html"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "货币供应量";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsm_a_insur_Click(object sender, EventArgs e)
        {

        }



        private void tsmi_generatezoniaxy_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new GenerateXYPositionForm();
            //wb.MdiParent = parentForm;
            wb.Text = "转换拿地所在城市经纬度坐标";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_bdi_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                IsExternalLink = true,
                Url = "http://www.96369.net/indices/77"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "东方财富-股东增减持";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_moat_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1000,
                Height = 600,
                Url = "whatismoat.html"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "什么是护城河？";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_rulesforsuccess_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1000,
                Height = 600,
                Url = "rulesforsuccess.html"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "什么是护城河？";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_makefriendwithtime_Click(object sender, EventArgs e)
        {
            var parentForm = ((StockListForm)this.ParentForm);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1000,
                Height = 600,
                Url = "makefriendwithtime.html"
            });
            //wb.MdiParent = parentForm;
            wb.Text = "丈量护城河，做时间的朋友";
            wb.ShowDialog();

            //StockListForm.SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_unclock_basic_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/basic", 
                IsExternalLink=true
            });
            wb.Text = "UNLOCK2_L0_基础级(preA1)";
            wb.Show();
        }

        private void tsmi_unlock_level1_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/l1",
                IsExternalLink = true
            });
            wb.Text = "UNLOCK2_L1_第一级(A1)";
            wb.Show();
        }

        private void tsmi_unlock_level2_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/l2",
                IsExternalLink = true
            });
            wb.Text = "UNLOCK2_L2_第二级(A2)";
            wb.Show();
        }

        private void tsmi_unlock_level3_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/l3",
                IsExternalLink = true
            });
            wb.Text = "UNLOCK2_L3_第三级(A3)";
            wb.Show();
        }

        private void tsmi_unlock_level4_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/l4",
                IsExternalLink = true
            });
            wb.Text = "UNLOCK2_L4_第四级(A4)";
            wb.Show();
        }

        private void tsmi_unlock_level5_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "http://localhost:8081/unlock/l5",
                IsExternalLink = true
            });
            wb.Text = "UNLOCK2_L5_第五级(A5)";
            wb.Show();
        }

        private void tsmi_pu_l1_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up P+ Level 1\Power Up P+ Level 1.exe";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_pu_l2_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up Level 2 P+\Power Up Level 2 P+.exe";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_pu_l3_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up P+ Level 3\Power Up P+ Level 3.exe";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_pu_l4_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up 4\Power Up 4.exe";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_pu_l5_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up Level 5 P+";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_pu_l6_Click(object sender, EventArgs e)
        {
            var filePath = @"E:\Programs\programfiles\Power Up 6\Power Up 6.exe";
            System.Diagnostics.Process.Start(filePath);
        }

        private void tsmi_googlemap_Click(object sender, EventArgs e)
        {
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 900,
                Url = "https://earth.google.com/static/",
                IsExternalLink = true
            });
            wb.Text = "谷歌地图";
            wb.Show();
        }

        private void tsmi_douban_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:8081/douban/booklist#//douban/search";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_unlock_media_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:8081/douban/booklist#//unlock/basic";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_ul_basic_literacy_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\Basic\Unlock 2e Basic Literacy.pdf");
        }

        private void tsmi_ul_basic_skills_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\Basic\Unlock 2e Basic Skills.pdf");
        }

        private void tsmi_ul_l1_rw_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\RW\Unlock 1 RW\【全彩】UNLOCK-1-读写.pdf");
        }

        private void tsmi_ul_l1_ls_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\LS\Unlock 1 LS\【全彩】UNLOCK-1-听口.pdf");
        }

        private void tsmi_ul_l2_rw_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\RW\Unlock 2 RW\【全彩】UNLOCK-2-读写.pdf");
        }

        private void tsmi_ul_l2_ls_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\LS\Unlock 2 LS\【全彩】UNLOCK-2-听口.pdf");
        }

        private void tsmi_ul_l3_rw_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\RW\Unlock 3 RW\【全彩】UNLOCK-3-读写.pdf");
        }

        private void tsmi_ul_l3_ls_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\LS\Unlock 3 LS\【全彩】UNLOCK-3-听口.pdf");
        }

        private void tsmi_ul_l4_rw_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\RW\Unlock 4 RW\【全彩】UNLOCK-4-读写.pdf");
        }

        private void tsmi_ul_l4_ls_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\LS\Unlock 4 LS\【全彩】UNLOCK-4-听口.pdf");
        }

        private void tsmi_ul_l5_rw_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\RW\Unlock 5 RW\【全彩】UNLOCK-5-读写.pdf");
        }

        private void tsmi_ul_l5_ls_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenPdfInAdobeAcrobat(@"G:\selfstudy\learnenglish\Unlock2019\LS\Unlock 5 LS\【全彩】UNLOCK-5-听口.pdf");
        }

        private void tsmi_pwup_l1_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up P+ Level 1\Power Up P+ Level 1.exe");
        }

        private void tsmi_pwup_l2_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up Level 2 P+\Power Up Level 2 P+.exe");
        }

        private void tsmi_pwup_l3_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up P+ Level 3\Power Up P+ Level 3.exe");
        }

        private void tsmi_pwup_l4_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up 4\Power Up 4.exe");
        }

        private void tsmi_pwup_l5_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up Level 5 P+\Power Up Level 5 P+.exe");
        }

        private void tsmi_pwup_l6_Click(object sender, EventArgs e)
        {
            WinFormHelper.OpenExeProgram(@"E:\Programs\programfiles\Power Up 6\Power Up 6.exe");
        }
    }
}
