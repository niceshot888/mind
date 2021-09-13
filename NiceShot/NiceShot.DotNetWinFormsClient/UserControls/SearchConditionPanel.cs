using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.UserControls
{
    public partial class SearchConditionPanel : UserControl
    {
        public SearchConditionPanel()
        {
            InitializeComponent();

            this.lstIndustry.BringToFront();
            this.lstStockName.BringToFront();

            //tbxIndustry.Text = "房地产";
            //tbxIssueDate.Text = "3";
            //tbxKfjlr.Text = "3,5000";
            //cbx_is_follow.Checked = true;

            //tbxRoe.Text = "15";
            //tbx_contract_liab.Text = "1";

            cbxReportType.Items.Clear();
            cbxReportType.DisplayMember = "Name";
            cbxReportType.ValueMember = "Value";
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "一季报", Value = "3" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "中报", Value = "6" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "三季报", Value = "9" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "年报", Value = "12" });
            cbxReportType.SelectedIndex = 3;

            cbxBestCompany.Items.Clear();
            cbxBestCompany.DisplayMember = "Name";
            cbxBestCompany.ValueMember = "Value";
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "全部", Value = "" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "高ROE+研发+现金流+预收", Value = "roe_and_hd_cf_adv" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "高ROE", Value = "roe" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "连续4年高ROE", Value = "roe4years" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "高研发", Value = "hd" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "高现金流", Value = "cf" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "高预收款", Value = "adv" });
            cbxBestCompany.Items.Add(new AutoCompleteNameAndValue { Name = "上一年", Value = "preyear" });
            cbxBestCompany.SelectedIndex = 0;


            cbxOrderBy.Items.Clear();
            cbxOrderBy.DisplayMember = "Name";
            cbxOrderBy.ValueMember = "Value";
            cbxOrderBy.Items.Add(new AutoCompleteNameAndValue { Name = "市值", Value = "s.market_capital" });
            cbxOrderBy.Items.Add(new AutoCompleteNameAndValue { Name = "预收款", Value = "(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0))" });
            cbxOrderBy.Items.Add(new AutoCompleteNameAndValue { Name = "存货", Value = "(ifnull(b.inventory,0)+ifnull(b.contract_asset,0))" });
            cbxOrderBy.SelectedIndex = 0;

            Control.CheckForIllegalCrossThreadCalls = false;

            SetDefaultCondition();

        }

        #region 行业输入框

        private void SelectIndustryValue()
        {
            AutoCompleteNameAndValue info = lstIndustry.SelectedItem as AutoCompleteNameAndValue;
            if (info == null)
                return;

            tbxIndustry.Text = info.Name;
            lstIndustry.Visible = false;

            ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
        }

        private void tbxIndustry_KeyUp(object sender, KeyEventArgs e)
        {
            //上下左右
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (lstIndustry.SelectedIndex > 0)
                    lstIndustry.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstIndustry.SelectedIndex < lstIndustry.Items.Count - 1)
                    lstIndustry.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                SelectIndustryValue();

                if (!string.IsNullOrEmpty(tbxIndustry.Text))
                    tbxStockName.Text = string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(tbxIndustry.Text))
                {
                    lstIndustry.Visible = false;
                    ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
                    return;
                }

                IList<AutoCompleteNameAndValue> ds = GetIndustriesByKeywords(tbxIndustry.Text.Trim().ToLower());
                if (ds.Count == 0)
                {
                    lstIndustry.Visible = false;
                    ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
                    return;
                }

                lstIndustry.DataSource = ds;
                lstIndustry.DisplayMember = "Name";
                lstIndustry.ValueMember = "Value";
                lstIndustry.Visible = true;

                ((StockListForm)this.ParentForm).ShowTopPanelSize(true);
            }

            //tbxIndustry.Select(tbxIndustry.Text.Length, 1); //光标定位到文本框最后
        }

        private void tbxIndustry_Enter(object sender, EventArgs e)
        {
            lstIndustry.Visible = false;
            lstStockName.Visible = false;
        }

        private void lstIndustry_Click(object sender, EventArgs e)
        {
            SelectIndustryValue();
            tbxStockName.Text = string.Empty;
            tbxIndustry.Select(tbxIndustry.Text.Length, 1); //光标定位到最后
        }

        private void tbxIndustry_MouseDown(object sender, MouseEventArgs e)
        {
            IList<AutoCompleteNameAndValue> ds = GetAllIndustries();

            lstIndustry.DataSource = ds;
            lstIndustry.DisplayMember = "Name";
            lstIndustry.ValueMember = "Value";
            lstIndustry.Visible = true;

            ((StockListForm)this.ParentForm).ShowTopPanelSize(true);
        }

        private void tbxIndustry_MouseLeave(object sender, EventArgs e)
        {
        }

        private void tbxIndustry_DoubleClick(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void tbxIndustry_MouseClick(object sender, MouseEventArgs e)
        {
            //tbxIndustry.SelectAll();
            lstIndustry.SelectedValue = tbxIndustry.Text;
        }

        #endregion

        #region 股票名称输入框

        private void SelectStockNameValue()
        {
            AutoCompleteNameAndValue info = lstStockName.SelectedItem as AutoCompleteNameAndValue;
            if (info == null)
                return;

            tbxStockName.Text = info.Name;
            lstStockName.Visible = false;

            ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
        }

        private void tbxStockName_KeyUp(object sender, KeyEventArgs e)
        {
            var _backgroundWorker = ((StockListForm)this.ParentForm)._backgroundWorker;

            //上下左右
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (lstStockName.SelectedIndex > 0)
                    lstStockName.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (lstStockName.SelectedIndex < lstStockName.Items.Count - 1)
                    lstStockName.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                SelectStockNameValue();

                if (!string.IsNullOrEmpty(tbxStockName.Text))
                    tbxIndustry.Text = string.Empty;

                if (!_backgroundWorker.IsBusy)
                    _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(tbxStockName.Text))
                {
                    lstStockName.Visible = false;
                    ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
                    return;
                }

                IList<AutoCompleteNameAndValue> ds = GetStockNamesByKeywords(tbxStockName.Text.Trim().ToLower());
                if (ds.Count == 0)
                {
                    lstStockName.Visible = false;
                    ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
                    return;
                }

                lstStockName.DataSource = ds;
                lstStockName.DisplayMember = "Name";
                lstStockName.ValueMember = "Value";
                lstStockName.Visible = true;

                ((StockListForm)this.ParentForm).ShowTopPanelSize(true);
            }

            tbxStockName.Select(tbxStockName.Text.Length, 1); //光标定位到文本框最后
        }

        private void tbxStockName_Enter(object sender, EventArgs e)
        {
            lstIndustry.Visible = false;
            lstStockName.Visible = false;

            ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
        }

        private void lstStockName_Click(object sender, EventArgs e)
        {
            SelectStockNameValue();
            tbxIndustry.Text = string.Empty;
            tbxStockName.Select(tbxStockName.Text.Length, 1); //光标定位到最后
        }

        private void tbxStockName_DoubleClick(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void tbxStockName_MouseClick(object sender, MouseEventArgs e)
        {
            tbxStockName.SelectAll();
        }

        #endregion

        #region 控件事件

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var _backgroundWorker = ((StockListForm)this.ParentForm)._backgroundWorker;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxIndustry.Clear();
            tbxStockName.Clear();
            tbxRoe.Clear();
            tbxKfjlr.Clear();
            tbx_kfjlr_zs.Clear();
            tbxPE.Clear();
            tbxPB.Clear();
            cbx_is_follow.Checked = false;
            cbxIncludeFinIndustry.Checked = false;
            tbxIssueDate.Clear();
            tbx_current_year_percent.Clear();
            cbxBestCompany.SelectedIndex = 0;
            cbxOrderBy.SelectedIndex = 0;
            tbxsumparentequity.Clear();
            tbxCoreConcept.Clear();
            tbx_contract_liab.Clear();
            tbxMarketCapital.Clear();
            //cbxRoe.SelectedIndex = 0;
        }

        #endregion

        #region 共有方法

        public SearchStockCondition GetSearchStockCondition()
        {
            return new SearchStockCondition
            {
                Industry = tbxIndustry.Text.Trim(),
                StockName = tbxStockName.Text.Trim(),
                Kfjlr = tbxKfjlr.Text.Trim(),
                Kfjlr_zs = tbx_kfjlr_zs.Text.Trim(),
                Roe = tbxRoe.Text.Trim(),
                Pe = tbxPE.Text.Trim(),
                Pb = tbxPB.Text.Trim(),
                CurrentYearPercent = tbx_current_year_percent.Text.Trim(),
                IssueDate = tbxIssueDate.Text.Trim(),
                ReportType = ((AutoCompleteNameAndValue)cbxReportType.SelectedItem).Value,
                IsFollow = cbx_is_follow.Checked,
                IncludeFinIndustry = cbxIncludeFinIndustry.Checked,
                //RoeType = ((AutoCompleteNameAndValue)cbxRoe.SelectedItem).Value,
                BestCompanyType = ((AutoCompleteNameAndValue)cbxBestCompany.SelectedItem).Value,
                OrderBy = ((AutoCompleteNameAndValue)cbxOrderBy.SelectedItem).Value,
                SumParentEquity = tbxsumparentequity.Text.Trim(),
                CoreConcept = tbxCoreConcept.Text.Trim(),
                ContractLiab = tbx_contract_liab.Text.Trim(),
                MarketCapital = tbxMarketCapital.Text.Trim(),
            };
        }

        #endregion

        #region 获取数据库数据

        private List<AutoCompleteNameAndValue> GetAllIndustries()
        {
            var _marketType = ((StockListForm)this.ParentForm)._marketType;

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "";
                if (_marketType == MarketType.A)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=1 and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and sshy is not null GROUP BY sshy order by SUM(market_capital) desc";
                else if (_marketType == MarketType.HK)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=2 and sshy<>'--' and sshy is not null order by sshy asc";

                return conn.Query<AutoCompleteNameAndValue>(sql).ToList();
            }
        }

        private List<AutoCompleteNameAndValue> GetIndustriesByKeywords(string keywords)
        {
            var _marketType = ((StockListForm)this.ParentForm)._marketType;

            if (string.IsNullOrEmpty(keywords))
                return new List<AutoCompleteNameAndValue>();

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "";
                if (_marketType == MarketType.A)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=1 and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and sshy is not null and (sshy like '%{0}%' or sshy_py like '%{0}%' or sshy_fullpy like '%{0}%') order by sshy asc";
                else if (_marketType == MarketType.HK)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=2 and sshy<>'--' and sshy is not null and (sshy like '%{0}%' or sshy_py like '%{0}%' or sshy_fullpy like '%{0}%') order by sshy asc";

                sql = string.Format(sql, keywords);
                return conn.Query<AutoCompleteNameAndValue>(sql).ToList();
            }
        }

        private List<AutoCompleteNameAndValue> GetStockNamesByKeywords(string keywords)
        {
            var _marketType = ((StockListForm)this.ParentForm)._marketType;

            if (string.IsNullOrEmpty(keywords))
                return new List<AutoCompleteNameAndValue>();

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "";
                if (_marketType == MarketType.A)
                    sql = "select distinct name Name,ts_code Value from xq_stock where  markettype=1 and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and (name like '%{0}%' or pinyin like '%{0}%' or fullpinyin like '%{0}%') order by name asc";
                else if (_marketType == MarketType.HK)
                    sql = "select distinct name Name,ts_code Value from xq_stock where markettype=2 and (name like '%{0}%' or pinyin like '%{0}%' or fullpinyin like '%{0}%') order by name asc";

                sql = string.Format(sql, keywords);
                return conn.Query<AutoCompleteNameAndValue>(sql).ToList();
            }
        }

        #endregion

        private void btnDefault_Click(object sender, EventArgs e)
        {
            SetDefaultCondition();
        }

        private void SetDefaultCondition()
        {
            /*this.tbxPE.Text = "20";
            this.tbxRoe.Text = "10";
            this.tbxKfjlr.Text = "1";
            this.tbx_kfjlr_zs.Text = "10";
            this.cbxReportType.SelectedIndex = 3;
            this.cbxBestCompany.SelectedIndex = 0;*/

            /*tbxStockName.Text = "";
            tbxIssueDate.Text = "3";
            //tbx_current_year_percent.Text = "20";
            //tbxPE.Text = "20";
            cbx_is_follow.Checked = true;
            cbxIncludeFinIndustry.Checked = false;
            cbxReportType.SelectedIndex = 3;
            //cbxRoe.SelectedIndex = 0;
           
            cbxOrderBy.SelectedIndex = 0;
            tbxKfjlr.Text = "3,5000";
            tbxsumparentequity.Text = "0";
            tbxCoreConcept.Text = "";
            tbx_contract_liab.Text = "";
            tbxMarketCapital.Text = "";*/

            tbxStockName.Text = "";
            tbxIssueDate.Text = "";
            //tbx_current_year_percent.Text = "20";
            //tbxPE.Text = "20";
            cbx_is_follow.Checked = true;
            cbxIncludeFinIndustry.Checked = false;
            cbxReportType.SelectedIndex = 3;
            cbxBestCompany.SelectedIndex = 0;

            //tbxRoe.Text = "15";
            //tbxPE.Text = "30";
            //tbxPB.Text = "3";
            //tbxMarketCapital.Text = "100";

            cbxOrderBy.SelectedIndex = 0;
            tbxKfjlr.Text = "";
            //tbxsumparentequity.Text = "0";
            tbxCoreConcept.Text = "";
            tbx_contract_liab.Text = "";
            
        }

        private void tbxCoreConcept_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxIndustry.Text))
            {
                lstIndustry.Visible = false;
                ((StockListForm)this.ParentForm).ShowTopPanelSize(false);
                return;
            }
        }
    }
}
