using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.DataSyncs;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class GridForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private BackgroundWorker _backgroundWorker2 = null;
        private DataSet _ds_stock_list = null;
        private MarketType _marketType = MarketType.A;
        private AutoCompleteStringCollection _acsc;
        private string _industry = "";
        public GridForm()
        {
            InitializeComponent();

            this.IsMdiContainer = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 1100;
            this.Height = 900;

            WinFormHelper.InitDataGridViewStyle(tbl_datalist);

            Type type = tbl_datalist.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(tbl_datalist, true, null);

            ShowTopPanelSize(false);

            //_backgroundWorker2 = new BackgroundWorker();
            //_backgroundWorker2.DoWork += _backgroundWorker2_DoWork;
            //_backgroundWorker2.RunWorkerCompleted += _backgroundWorker2_RunWorkerCompleted;
            //if (!_backgroundWorker2.IsBusy)
            //    _backgroundWorker2.RunWorkerAsync();

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void _backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void _backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {


        }

        #region 通用

        #region 异步

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                tbl_datalist.DataSource = _ds_stock_list.Tables[0];
                tbl_datalist.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                tbl_datalist.Columns[4].Frozen = true;
                tbl_datalist.Columns[0].Visible = false;

                for (int i = 0; i < this.tbl_datalist.Columns.Count; i++)
                {
                    this.tbl_datalist.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                BindNormalStocksData();
        }

        #endregion

        #region 顶部菜单

        /// <summary>
        /// 普通股列表
        /// </summary>
        private void tsm_a_normal_Click(object sender, EventArgs e)
        {
            _marketType = MarketType.A;
            tbxIndustry.Text = "房地产";
            this.Width = 1100;
            this.CenterToScreen();

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 港股列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsm_hk_common_Click(object sender, EventArgs e)
        {
            _marketType = MarketType.HK;
            tbxIndustry.Text = "地产";
            this.Width = 950;
            this.CenterToScreen();

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 基本数据同步
        /// </summary>
        private void tsmi_datasync_Click(object sender, EventArgs e)
        {
            BasicDataSyncForm wb = new BasicDataSyncForm();
            wb.MdiParent = this;
            wb.Text = "同步基本数据";
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        #endregion

        #region 行业输入框

        private void SelectIndustryValue()
        {
            AutoCompleteNameAndValue info = lstIndustry.SelectedItem as AutoCompleteNameAndValue;
            if (info == null)
                return;

            tbxIndustry.Text = info.Name;
            lstIndustry.Visible = false;

            ShowTopPanelSize(false);
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

                //if (!_backgroundWorker.IsBusy)
                //    _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(tbxIndustry.Text))
                {
                    lstIndustry.Visible = false;
                    ShowTopPanelSize(false);
                    return;
                }

                IList<AutoCompleteNameAndValue> ds = GetIndustriesByKeywords(tbxIndustry.Text.Trim().ToLower());
                if (ds.Count == 0)
                {
                    lstIndustry.Visible = false;
                    ShowTopPanelSize(false);
                    return;
                }

                lstIndustry.DataSource = ds;
                lstIndustry.DisplayMember = "Name";
                lstIndustry.ValueMember = "Value";
                lstIndustry.Visible = true;

                ShowTopPanelSize(true);
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

            //tbxIndustry.Select(tbxIndustry.Text.Length, 1); //光标定位到最后
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

            ShowTopPanelSize(false);
        }

        private void tbxStockName_KeyUp(object sender, KeyEventArgs e)
        {
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

                if (!_backgroundWorker.IsBusy)
                    _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(tbxStockName.Text))
                {
                    lstStockName.Visible = false;
                    ShowTopPanelSize(false);
                    return;
                }

                IList<AutoCompleteNameAndValue> ds = GetStockNamesByKeywords(tbxStockName.Text.Trim().ToLower());
                if (ds.Count == 0)
                {
                    lstStockName.Visible = false;
                    ShowTopPanelSize(false);
                    return;
                }

                lstStockName.DataSource = ds;
                lstStockName.DisplayMember = "Name";
                lstStockName.ValueMember = "Value";
                lstStockName.Visible = true;

                ShowTopPanelSize(true);
            }

            tbxStockName.Select(tbxStockName.Text.Length, 1); //光标定位到文本框最后
        }

        private void tbxStockName_Enter(object sender, EventArgs e)
        {
            lstIndustry.Visible = false;
            lstStockName.Visible = false;
        }

        private void lstStockName_Click(object sender, EventArgs e)
        {
            SelectStockNameValue();

            tbxStockName.Select(tbxStockName.Text.Length, 1); //光标定位到最后
        }

        #endregion

        #region 获取数据

        private void BindNormalStocksData()
        {
            if (_marketType == MarketType.A)
                BindANormalStocksData();
            else if (_marketType == MarketType.HK)
                BindHKNormalStocksData();
        }

        private void BindHKNormalStocksData()
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,
convert_to_yiyuan(s.market_capital) 市值,convert_to_decimal(s.pe_ttm) 市盈率,
m.jqjzcsyl ROE, mll 毛利率,jll 净利率,convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速
from em_hk_mainindex m left JOIN xq_stock s on m.ts_code=s.ts_code
where m.date='2019-12-31' {0} order by s.market_capital desc {1}";

                string where_addition = string.Empty;
                if (!string.IsNullOrEmpty(tbxIndustry.Text))
                    where_addition += string.Format(" and s.sshy='{0}'", tbxIndustry.Text.Trim());

                if (!string.IsNullOrEmpty(tbxStockName.Text))
                    where_addition += string.Format(" and s.name='{0}'", tbxStockName.Text.Trim());

                if (!string.IsNullOrEmpty(tbxRoe.Text))
                    where_addition += string.Format(" and m.jqjzcsyl>={0}", tbxRoe.Text.Trim());

                if (!string.IsNullOrEmpty(tbxKfjlr.Text))
                    where_addition += string.Format(" and m.gsjlr>={0}*100000000", tbxKfjlr.Text.Trim());

                if (!string.IsNullOrEmpty(tbx_kfjlr_zs.Text))
                    where_addition += string.Format(" and m.gsjlrtbzz>={0}", tbx_kfjlr_zs.Text.Trim());

                if (!string.IsNullOrEmpty(where_addition))
                    sql = string.Format(sql, where_addition, "");
                else
                    sql = string.Format(sql, where_addition, " limit 100");

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                _ds_stock_list = ds;
            }
        }

        private void BindANormalStocksData()
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,
#m.date 日期,
convert_to_yiyuan(s.market_capital) 市值,convert_to_decimal(s.pe_ttm) 市盈率,
m.jqjzcsyl ROE, mll 毛利率,jll 净利率,
convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,
#convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速,
convert_to_yiyuan(m.kfjlr) 扣非净利润,m.kfjlrtbzz 扣非净利润增速,
convert_to_yiyuan(ifnull(b.MONETARYFUND,0)+ifnull(b.TRADEFASSET,0)+ifnull(b.otherequity_invest,0)+ifnull(b.othernonlfinasset,0)+ifnull(b.SALEABLEFASSET,0)) 金融资产,
convert_to_yiyuan(b.ACCOUNTREC) 应收账款,convert_to_yiyuan(ic.OPERATEREVE) 主营收,
calc_proportion(b2.ACCOUNTREC,ic.OPERATEREVE) 应收账款占主营比,
convert_to_yiyuan(cf.SUMOPERATEFLOWIN) 回款,convert_to_yiyuan(cf.NETOPERATECASHFLOW) 经营现金流净额,convert_to_yiyuan(m.gsjlr) 归母净利润,
convert_to_yiyuan(ifnull(cf.NETOPERATECASHFLOW,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0)) 经营资产自由现金流,
convert_to_yiyuan(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0)) 预收款,
convert_to_yiyuan(b.FIXEDASSET) 固定资产,calc_proportion(b.FIXEDASSET,b.SUMASSET) 固定资产占比,
convert_to_yiyuan(b.OTHERLASSET) 在建工程,calc_proportion(b.OTHERLASSET,b.SUMASSET) 在建工程占比,
convert_to_yiyuan(b.INTANGIBLEASSET) 无形资产,calc_proportion(b.INTANGIBLEASSET,b.SUMASSET) 无形资产占比,
convert_to_yiyuan(b.GOODWILL) 商誉,calc_proportion(b.GOODWILL,b.SUMASSET) 商誉占比,
convert_to_yiyuan(b.INVENTORY) 存货,calc_proportion(b.INVENTORY,b.SUMASSET) 存货占比,
convert_to_yiyuan(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)) 短期债务,
calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0),b.SUMASSET) 短期债务率,
convert_to_yiyuan(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 长期债务,
calc_proportion(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) 长期债务率,
convert_to_yiyuan(cf.DIVIPROFITORINTPAY) 分红和利息支出,
convert_to_yiyuan(IFNULL(b.billpay,0)+IFNULL(b.accountpay,0)+IFNULL(b.advancereceive,0)+ifnull(b.contractliab,0)+IFNULL(b.salarypay,0)+IFNULL(b.taxpay,0)+IFNULL(b.otherlliab,0)+IFNULL(b.otherpay,0)) 经营性负债,
convert_to_yiyuan(IFNULL(b.stborrow,0)+IFNULL(b.nonlliaboneyear,0)+IFNULL(b.ltborrow,0)+IFNULL(b.bondpay,0)) 金融性负债,
convert_to_yiyuan(IFNULL(b.sharecapital,0)+IFNULL(b.capitalreserve,0)+IFNULL(b.minorityequity,0)) 股东入资,
convert_to_yiyuan(IFNULL(b.surplusreserve,0)+IFNULL(b.retainedearning,0)) 利润积累
from em_a_mainindex m 
LEFT JOIN em_balancesheet_common b on b.SECURITYCODE=m.ts_code and b.REPORTDATE=m.date
LEFT JOIN em_balancesheet_common b2 on b2.SECURITYCODE=m.ts_code and b2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN em_income_common ic on ic.SECURITYCODE=m.ts_code and ic.REPORTDATE=m.date
LEFT JOIN em_cashflow_common cf on cf.SECURITYCODE=m.ts_code and cf.REPORTDATE=m.date
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
where m.date='2019-12-31'
and s.markettype=1
and s.type in (11,82) 
and s.name not like '%b' and s.name not like '%b股'  and s.name not like '%退' and s.name not like '%退市%'
{0}
order by s.market_capital desc {1}";

                string where_addition = string.Empty;
                if (!string.IsNullOrEmpty(tbxIndustry.Text))
                    where_addition += string.Format(" and s.sshy='{0}'", tbxIndustry.Text.Trim());

                if (!string.IsNullOrEmpty(tbxStockName.Text))
                    where_addition += string.Format(" and s.name='{0}'", tbxStockName.Text.Trim());

                if (!string.IsNullOrEmpty(tbxRoe.Text))
                    where_addition += string.Format(" and m.jqjzcsyl>={0}", tbxRoe.Text.Trim());

                if (!string.IsNullOrEmpty(tbxKfjlr.Text))
                    where_addition += string.Format(" and m.kfjlr>={0}*100000000", tbxKfjlr.Text.Trim());

                if (!string.IsNullOrEmpty(tbx_kfjlr_zs.Text))
                    where_addition += string.Format(" and m.kfjlrtbzz>={0}", tbx_kfjlr_zs.Text.Trim());

                if (!string.IsNullOrEmpty(where_addition))
                    sql = string.Format(sql, where_addition, "");
                else
                    sql = string.Format(sql, where_addition, " limit 100");

                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);

                _ds_stock_list = ds;
            }
        }

        private List<AutoCompleteNameAndValue> GetAllIndustries()
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "";
                if (_marketType == MarketType.A)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=1 and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and sshy is not null order by sshy asc";
                else if (_marketType == MarketType.HK)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=2 and sshy<>'--' and sshy is not null order by sshy asc";

                return conn.Query<AutoCompleteNameAndValue>(sql).ToList();
            }
        }

        private List<AutoCompleteNameAndValue> GetIndustriesByKeywords(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
                return new List<AutoCompleteNameAndValue>();

            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "";
                if (_marketType == MarketType.A)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where  markettype=1 and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and sshy is not null and (sshy like '%{0}%' or sshy_py like '%{0}%' or sshy_fullpy like '%{0}%') order by sshy asc";
                else if (_marketType == MarketType.HK)
                    sql = "select distinct sshy Name,sshy Value from xq_stock where markettype=2 and sshy<>'--' and sshy is not null and (sshy like '%{0}%' or sshy_py like '%{0}%' or sshy_fullpy like '%{0}%') order by sshy asc";

                sql = string.Format(sql, keywords);
                return conn.Query<AutoCompleteNameAndValue>(sql).ToList();
            }
        }

        private List<AutoCompleteNameAndValue> GetStockNamesByKeywords(string keywords)
        {
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

        #endregion

        #region A股

        #region 窗体显示

        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void ShowTopPanelSize(bool isMax)
        {
            //if (isMax)
            //    pnl_top.Height = 175;
            //else
            //    pnl_top.Height = 50;
            //this.pnl_top.SendToBack();
            //this.lstIndustry.BringToFront();
            //this.tbl_datalist.SendToBack();
        }

        #endregion

        #region 窗体控件事件

        private void tbl_datalist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.tbl_datalist.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tbl_datalist.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void tbl_datalist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //tbl_datalist.Columns[3].DefaultCellStyle.BackColor = Color.Red;
            //tbl_datalist[3, 3].Style.BackColor = Color.Green;
        }

        private void tbl_datalist_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (tbl_datalist.Rows[e.RowIndex].Selected == false)
                    {
                        tbl_datalist.ClearSelection();
                        tbl_datalist.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (tbl_datalist.SelectedRows.Count == 1 && e.ColumnIndex != -1)
                        tbl_datalist.CurrentCell = tbl_datalist.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    //弹出操作菜单
                    cms_stock_details.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void tbxIndustry_DoubleClick(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void tbxStockName_DoubleClick(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
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
        }

        private void tbxIndustry_MouseDown(object sender, MouseEventArgs e)
        {
            IList<AutoCompleteNameAndValue> ds = GetAllIndustries();

            lstIndustry.DataSource = ds;
            lstIndustry.DisplayMember = "Name";
            lstIndustry.ValueMember = "Value";
            lstIndustry.Visible = true;

            ShowTopPanelSize(true);
        }

        private void tbxIndustry_MouseLeave(object sender, EventArgs e)
        {
            //鼠标离开的时候
        }

        private void tbxIndustry_MouseClick(object sender, MouseEventArgs e)
        {
            tbxIndustry.SelectAll();
            lstIndustry.SelectedValue = tbxIndustry.Text;
        }

        private void tbxStockName_MouseClick(object sender, MouseEventArgs e)
        {
            tbxStockName.SelectAll();
        }

        #endregion

        #region 右键菜单事件

        /// <summary>
        /// 个股详细
        /// </summary>
        private void tsmi_stock_analysis_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var industry = selectedRow.Cells["行业"].Value.ToString();

            StockDetailsForm frmStockDetails = new StockDetailsForm(_marketType, stockName,"", industry);
            frmStockDetails.MdiParent = this;
            frmStockDetails.Text = stockName + "-个股详细财务指标";
            frmStockDetails.Show();

            SetParent((int)frmStockDetails.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 查看财务报表
        /// </summary>
        private void tsmi_em_finreports_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            EmFinReportForm emFinReportForm = new EmFinReportForm(_marketType, stockName, tscode);
            emFinReportForm.MdiParent = this;
            emFinReportForm.Text = stockName + "-财务报表";
            emFinReportForm.Show();

            SetParent((int)emFinReportForm.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 股东人数
        /// </summary>
        private void tsmi_gdrs_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = _marketType == MarketType.A ? StringUtils.ConvertEMToXQSymbol(tscode) : tscode;
            EChartsWebBrowserForm frmEcharts = new EChartsWebBrowserForm(symbol);
            frmEcharts.MdiParent = this;
            frmEcharts.Text = stockName + "-股东人数";
            frmEcharts.Show();

            SetParent((int)frmEcharts.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 历史估值
        /// </summary>
        private void tsmi_lsgz_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = _marketType == MarketType.A ? StringUtils.ConvertEMToXQSymbol(tscode) : tscode;
            HistoryValueWbForm frmEcharts = new HistoryValueWbForm(_marketType, symbol);
            frmEcharts.MdiParent = this;
            frmEcharts.Text = stockName + "-历史估值";
            frmEcharts.Show();

            SetParent((int)frmEcharts.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 右键菜单：机构持股
        /// </summary>
        private void tsmi_jgcc_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = tscode.Substring(0, 6);
            ChromeWebBrowser wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 800,
                Height = 650,
                Symbol = symbol,
                Url = "eastmoney.html?symbol=" + symbol
            });
            wb.MdiParent = this;
            wb.Text = stockName + "-机构持仓";
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 郭氏财务分析模型
        /// </summary>
        private void tsmi_gsmx_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();
            var stockName = selectedRow.Cells["名称"].Value.ToString();

            GenerateAssAndCapExcelForm frmAss = new GenerateAssAndCapExcelForm(tscode);
            frmAss.MdiParent = this;
            frmAss.Text = stockName + "-郭氏财务分析模型";
            frmAss.Show();

            SetParent((int)frmAss.Handle, (int)this.Handle);
        }

        #endregion

        private void cms_stock_details_Opening(object sender, CancelEventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var industry = selectedRow.Cells["行业"].Value.ToString();

            if (_marketType == MarketType.A)
            {
                if ((new string[] { "银行", "保险", "证券" }).Contains(industry))
                    (sender as ContextMenuStrip).Items[5].Visible = false;
                else
                    (sender as ContextMenuStrip).Items[5].Visible = true;

                (sender as ContextMenuStrip).Items[1].Visible = true;
                (sender as ContextMenuStrip).Items[4].Visible = true;
            }
            else if (_marketType == MarketType.HK)
            {
                (sender as ContextMenuStrip).Items[1].Visible = false;
                (sender as ContextMenuStrip).Items[4].Visible = false;
                (sender as ContextMenuStrip).Items[5].Visible = false;
            }
        }

        #endregion

        private void cbxIndustries_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbxIndustry_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxIndustry_TextUpdate(object sender, EventArgs e)
        {
            //清空combobox
            this.cbxIndustry.Items.Clear();

            var listNew = new List<string>();
            //清空listNew
            listNew.Clear();

            var listOnit = GetIndustriesByKeywords(tbxIndustry.Text.Trim().ToLower());
            //遍历全部备查数据
            foreach (var item in listOnit)
            {
                listNew.Add(item.Name);
            }
            //combobox添加已经查到的关键词
            this.cbxIndustry.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.cbxIndustry.SelectionStart = this.cbxIndustry.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            //自动弹出下拉框
            this.cbxIndustry.DroppedDown = true;
        }
    }

}
