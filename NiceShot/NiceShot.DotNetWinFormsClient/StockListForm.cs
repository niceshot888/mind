using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.DotNetWinFormsClient.ChildForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.DataSyncs;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class StockListForm : Form
    {
        public BackgroundWorker _backgroundWorker = null;
        private DataSet _ds_stock_list = null;
        public MarketType _marketType = MarketType.A;

        //private readonly LongOperation _longOperation;
        public StockListForm()
        {
            InitializeComponent();

            this.IsMdiContainer = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Width = 1460; this.Height = 800;

            this.AcceptButton = this.scp_toppanel.btnSearch;

            WinFormHelper.InitDataGridViewStyle(tbl_datalist);

            Type type = tbl_datalist.GetType();
            var pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(tbl_datalist, true, null);

            ShowTopPanelSize(false);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        #region 异步

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tbl_datalist.DataSource = _ds_stock_list.Tables[0];
            tbl_datalist.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            tbl_datalist.Columns[4].Frozen = true;
            tbl_datalist.Columns[0].Visible = false;
            tbl_datalist.Columns[1].Visible = false;

            for (int colIndex = 0; colIndex < this.tbl_datalist.Columns.Count; colIndex++)
            {
                this.tbl_datalist.Columns[colIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int rowIndex = 0; rowIndex < this.tbl_datalist.Rows.Count; rowIndex++)
            {
                var markedas = tbl_datalist.Rows[rowIndex].Cells[1].Value.ToString();

                if (!string.IsNullOrEmpty(markedas))
                {
                    var markedasType = (MarkedAsType)int.Parse(markedas);
                    if (markedasType == MarkedAsType.Normal)
                        tbl_datalist.Rows[rowIndex].DefaultCellStyle.BackColor = tbl_datalist.RowsDefaultCellStyle.BackColor;
                    else
                        tbl_datalist.Rows[rowIndex].DefaultCellStyle.BackColor = MarkedAsTypeHelper.ConverToColor(markedasType);
                }
            }

            //tbl_datalist.Rows[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindNormalStocksData();
        }

        #endregion

        #region 窗体控件事件

        private void tbl_datalist_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var sb = new SolidBrush(this.tbl_datalist.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tbl_datalist.DefaultCellStyle.Font, sb, e.RowBounds.Location.X + 12, e.RowBounds.Location.Y + 4);
        }

        private void tbl_datalist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //tbl_datalist.Columns[3].DefaultCellStyle.BackColor = Color.Red;
            //tbl_datalist[3, 3].Style.BackColor = Color.Green;
            //e.RowIndex
        }

        private void tbl_datalist_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (tbl_datalist.Columns[e.ColumnIndex].Name.Equals("核心题材"))
            {
                e.Value = e.Value != null ? (e.Value.ToString().Length >= 20 ? e.Value.ToString().Substring(0, 20) + "..." : e.Value.ToString()) : string.Empty;
            }
        }

        private void tbl_datalist_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (tbl_datalist.Columns[e.ColumnIndex].Name.Equals("核心题材"))
            {
                e.ToolTipText = string.Format("{0}", tbl_datalist.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void tbl_datalist_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            var content = tbl_datalist.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            MessageBox.Show(content, tbl_datalist.Rows[e.RowIndex].Cells[3].Value + "-核心题材");
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

        private void cms_stock_details_Opening(object sender, CancelEventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var industry = selectedRow.Cells["行业"].Value.ToString();
            var is_follow = selectedRow.Cells["是否关注"].Value.ToString();
            var cms = (sender as ContextMenuStrip);
            if (cms == null)
                return;

            ((ToolStripMenuItem)cms.Items["tsmi_markedas"]).DropDownItems["tsmi_is_follow"].Text = is_follow == "已关注" ? "取消关注" : "关注";

            if (_marketType == MarketType.A)
            {
                //tsmi_gsmx

                if ((new string[] { "银行", "保险", "证券" }).Contains(industry))
                    ((ToolStripMenuItem)cms.Items["tsmi_bizmodel_ana"]).DropDownItems["tsmi_gsmx"].Visible = false;
                else
                    ((ToolStripMenuItem)cms.Items["tsmi_bizmodel_ana"]).DropDownItems["tsmi_gsmx"].Visible = true;

                /*cms.Items["tsmi_gdrs"].Visible = true;
                cms.Items["tsmi_jgcc"].Visible = true;

                cms.Items["tsmi_supply_findata"].Visible = true;
                cms.Items["tsmi_tdx"].Visible = true;
                cms.Items["tsmi_bizmodel"].Visible = true;*/

            }
            else if (_marketType == MarketType.HK)
            {
                cms.Items["tsmi_gdrs"].Visible = false;
                cms.Items["tsmi_jgcc"].Visible = false;
                cms.Items["tsmi_gsmx"].Visible = false;

                //cms.Items["tsmi_supply_findata"].Visible = false;
                cms.Items["tsmi_tdx"].Visible = false;
                cms.Items["tsmi_bizmodel"].Visible = false;
            }
        }

        private void pnl_top_MouseClick(object sender, MouseEventArgs e)
        {
            this.scp_toppanel.lstIndustry.Visible = false;
            this.scp_toppanel.lstStockName.Visible = false;
            ShowTopPanelSize(false);
        }

        #endregion

        #region 右键菜单事件

        private void tsmi_tdx_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = tscode.Substring(0, 6);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1300,
                Height = 750,
                Symbol = symbol,
                Url = "tdx.html?symbol=" + symbol
            });
            wb.MdiParent = this;
            wb.Text = stockName + "-通达信";
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_zhanganaysis_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            var frmFinData = new ZhangAanalysisForm(ts_code);
            frmFinData.MdiParent = this;
            frmFinData.Text = stockName + "-张氏财务报表分析";
            frmFinData.Show();

            SetParent((int)frmFinData.Handle, (int)this.Handle);
        }

        private void tsmi_supply_findata_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            SupplyFinDataForm frmFinData = new SupplyFinDataForm(ts_code);
            frmFinData.MdiParent = this;
            frmFinData.Text = stockName + "-财务报表补充数据";
            frmFinData.Show();

            SetParent((int)frmFinData.Handle, (int)this.Handle);
        }
        /*
        private void tsmi_supply_findata_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            SupplyFinDataForm frmFinData = new SupplyFinDataForm(ts_code);
            frmFinData.MdiParent = this;
            frmFinData.Text = stockName + "-财务报表补充数据";
            frmFinData.Show();

            SetParent((int)frmFinData.Handle, (int)this.Handle);
        }
         */

        private void tsmi_is_follow_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(ts_code, _marketType);
            if (stockInfo == null)
                return;

            try
            {
                string sql = "update xq_stock set is_follow=@is_follow where id=@id";

                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    conn.Execute(sql, new
                    {
                        id = stockInfo.id,
                        is_follow = stockInfo.is_follow.HasValue && stockInfo.is_follow.Value == 1 ? 0 : 1
                    });

                    selectedRow.Cells["是否关注"].Value = selectedRow.Cells["是否关注"].Value.ToString() == "已关注" ? "" : "已关注";
                    //if (!_backgroundWorker.IsBusy)
                    //    _backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("关注失败");
            }
        }

        private void tsmi_topholders_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            TopHoldersForm frmTopHolders = new TopHoldersForm(ts_code);
            frmTopHolders.MdiParent = this;
            frmTopHolders.Text = stockName + "-十大股东情况";
            frmTopHolders.Show();

            SetParent((int)frmTopHolders.Handle, (int)this.Handle);
        }

        /// <summary>
        /// 个股详细
        /// </summary>
        private void tsmi_stock_analysis_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var industry = selectedRow.Cells["行业"].Value.ToString();
            var pb = selectedRow.Cells["PB"].Value.ToString();
            var pettm = selectedRow.Cells["PE"].Value.ToString();
            var roe = selectedRow.Cells["ROE"].Value.ToString();
            var mc = selectedRow.Cells["市值"].Value.ToString();
            var dividend_yield_ratio = selectedRow.Cells["股息率"].Value.ToString();
            var reportType = ((AutoCompleteNameAndValue)this.scp_toppanel.cbxReportType.SelectedItem).Value;

            var frmStockDetails = new FinDetailsForm(_marketType, stockName, reportType, industry);
            frmStockDetails.MdiParent = this;
            frmStockDetails.Text = stockName + string.Format("：个股详细财务指标（市值={0}，PB={1}，PE-TTM={2}，ROE={3}，股息率={4}%）", mc, pb, pettm, roe, dividend_yield_ratio);
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
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 850,
                Height = 700,
                Symbol = symbol,
                Url = "jgcc.html?symbol=" + symbol
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

            //GenerateAssAndCapExcelForm frmAss = new GenerateAssAndCapExcelForm(tscode);
            var frmAss = new ExcelAnalysisForm(tscode);
            frmAss.MdiParent = this;
            frmAss.Text = stockName + "-郭氏财务分析模型";
            frmAss.Show();

            SetParent((int)frmAss.Handle, (int)this.Handle);
        }

        private void tsmi_bizmodel_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            //var symbol = _marketType == MarketType.A ? StringUtils.ConvertEMToXQSymbol(tscode) : tscode;
            //var form = new BizModelGeneration(tscode);
            var form = new BizModelGenerationForm(tscode);
            form.MdiParent = this;
            form.Text = stockName + "-商业模式画布";
            form.Show();

            SetParent((int)form.Handle, (int)this.Handle);
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

        private void BindANormalStocksData()
        {
            var condition = this.scp_toppanel.GetSearchStockCondition();

            using (var con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = @"SELECT s.ts_code,s.markedas,m.date 报表日期,
#primary_industry 一级行业,secondary_industry 二级行业,
s.sshy 行业,s.name 名称,board_name 核心题材,
(case s.is_follow when 1 then '已关注' else '' end) 是否关注,
s.current_year_percent 年初至今涨跌幅,
#(s.chg720) `涨跌幅720天`,
#(s.chg360) `涨跌幅360天`,
#pb1yearsago,pb4yearsago,calc_growth(s.pb1yearsago, s.pb) '一年间PB涨幅',calc_growth(s.pb4yearsago, s.pb) '四年间PB涨幅',
convert_to_decimal(s.pb) PB,
convert_to_yiyuan(b.total_parent_equity) 归母净资产,convert_to_yiyuan(s.market_capital) 市值,
TIMESTAMPDIFF(year,s.issue_date,CURDATE()) 上市时间,convert_to_decimal(s.pe_ttm) PE,
divide(b.total_assets, b.total_parent_equity) 财务杠杆,m.jqjzcsyl ROE,
$q4comment$calc_proportion(ic.deduct_parent_netprofit,(b.total_parent_equity+b2.total_parent_equity)/2) 扣非ROE,
convert_to_yiyuan(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0)) 预收款,
calc_proportion(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0),ic.operate_income) 预收占主营比,
convert_to_yiyuan(s.latest_bonus) 分红2021,s.dividend_yield_ratio 股息率,
$q4comment$convert_to_yiyuan(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0)) 自由现金流,
$q4comment$calc_proportion(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0),ic.operate_income) 自由现金流占营收比,
m.mll 毛利率,m.jll 净利率,convert_to_yiyuan(ic.operate_income) 营收,
calc_growth(ic2.operate_income,ic.operate_income) 营收增速,
convert_to_yiyuan(ic.deduct_parent_netprofit) 扣非净利润,
calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit) 扣非净利润增速,
#divide(cf.netcash_operatenote,ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.fe_interest_expense,0)-ifnull(ic.asset_impairment_income,0)) 核心利润获现率,
$q4comment$convert_to_yiyuan(hd.rdspendsum) 研发投入,hd.rdspendsum_ratio 研发投入占比,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0),b.total_assets) 有息负债率,
convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) 金融资产,
convert_to_yiyuan(b.accounts_rece) 应收账款,convert_to_yiyuan(ic.operate_income) 主营收,
$q4comment$calc_proportion(b.accounts_rece,ic.operate_income) 应收账款占主营比,
#convert_to_yiyuan(ic.INVESTINCOME) 投资收益,
convert_to_yiyuan(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) 核心利润,
calc_proportion(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0),ic.operate_income) 核心利润率,

convert_to_yiyuan(b.fixed_asset) 固定资产,calc_proportion(b.fixed_asset,b.total_assets) 固定资产占比,
convert_to_yiyuan(b.cip) 在建工程,calc_proportion(b.cip,b.total_assets) 在建工程占比,
convert_to_yiyuan(b.intangible_asset) 无形资产,calc_proportion(b.intangible_asset,b.total_assets) 无形资产占比,
convert_to_yiyuan(b.goodwill) 商誉,calc_proportion(b.goodwill,b.total_assets) 商誉占比,
convert_to_yiyuan(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)) 存货,calc_proportion(ifnull(b.inventory,0)+ifnull(b.contract_asset,0),b.total_assets) 存货占比,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短期债务,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0),b.total_assets) 短期债务率,
convert_to_yiyuan(b.accounts_payable) 应付账款,
calc_proportion(b.accounts_payable,b.total_assets) 应付账款占比,
convert_to_yiyuan(b.note_payable) 应付票据,
calc_proportion(b.note_payable,b.total_assets) 应付票据占比,
convert_to_yiyuan(cf.assign_dividend_porfit) `分配股利、偿付利息等`,
convert_to_yiyuan(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)+ifnull(ic.fe_interest_expense,0)) 三费,
calc_proportion(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)+ifnull(ic.fe_interest_expense,0),ic.operate_income) 三费占比

from em_a_mainindex m 
LEFT JOIN em_bal_common_v1 b on b.secucode=m.ts_code and b.report_date=m.date
LEFT JOIN em_bal_common_v1 b2 on b2.secucode=m.ts_code and b2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN em_inc_common_v1 ic on ic.secucode=m.ts_code and ic.report_date=m.date
LEFT JOIN em_inc_common_v1 ic2 on ic2.secucode=m.ts_code and ic2.report_date=DATE_ADD(m.date,INTERVAL '-2' YEAR)
LEFT JOIN em_cf_common_v1 cf on cf.secucode=m.ts_code and cf.report_date=m.date
LEFT JOIN em_cf_common_v1 cf2 on cf2.secucode=m.ts_code and cf2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN ths_hd hd on hd.ts_code=m.ts_code and hd.reportdate=m.date
LEFT JOIN static_mainindex sr on sr.ts_code=m.ts_code
where 1=1
#and m.date='2019-12-31'
#and TIMESTAMPDIFF(year,s.issue_date,CURDATE()) >=10
#and (IFNULL(b.surplusreserve,0)+IFNULL(b.retainedearning,0))>0 
and s.markettype=1
and s.type in (11,82) 
and s.name not like '%b' and s.name not like '%b股'  and s.name not like '%退' and s.name not like '%退市%'
{0}
order by $orderby$ desc {1}";
                //s.market_capital s.current_year_percent

                if (condition.Industry == "房地产2")
                {
                    sql = @"SELECT s.ts_code,s.markedas,
m.date 报表日期,
s.sshy 行业,s.name 名称,board_name 核心题材,
(case s.is_follow when 1 then '已关注' else '' end) 是否关注,
(s.chg720) `涨跌幅720天`,
TIMESTAMPDIFF(year,s.issue_date,CURDATE()) 上市时间,
convert_to_decimal(s.pe_ttm) PE,convert_to_decimal(s.pb) PB,m.jqjzcsyl ROE, 
calc_proportion(ic.deduct_parent_netprofit,(b.total_parent_equity+b2.total_parent_equity)/2) 扣非ROE,
convert_to_yiyuan(b.total_parent_equity) 归母净资产,
convert_to_yiyuan(s.market_capital) 市值,
convert_to_yiyuan(s.latest_bonus) 分红2021,calc_proportion(s.latest_bonus,s.market_capital) 股息率,
#convert_to_yiyuan(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0)) 自由现金流,
#calc_proportion(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0),ic.operate_income) 自由现金流占营收比,
m.mll 毛利率,m.jll 净利率,
convert_to_yiyuan(ic.operate_income) 营收,
calc_growth(ic2.operate_income,ic.operate_income) 营收增速,
convert_to_yiyuan(ic.deduct_parent_netprofit) 扣非净利润,
calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit) 扣非净利润增速,
#divide(cf.netcash_operatenote,ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.fe_interest_expense,0)-ifnull(ic.asset_impairment_income,0)) 核心利润获现率,
#convert_to_yiyuan(hd.rdspendsum) 研发投入,hd.rdspendsum_ratio 研发投入占比,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0),b.total_assets) 有息负债率,
convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) 金融资产,
convert_to_yiyuan(b.accounts_rece) 应收账款,convert_to_yiyuan(ic.operate_income) 主营收,
calc_proportion(b.accounts_rece,ic.operate_income) 应收账款占主营比,
-- convert_to_yiyuan(ic.INVESTINCOME) 投资收益,
convert_to_yiyuan(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) 核心利润,
calc_proportion(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0),ic.operate_income) 核心利润率,
convert_to_yiyuan(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0)) 预收款,
convert_to_yiyuan(b.fixed_asset) 固定资产,calc_proportion(b.fixed_asset,b.total_assets) 固定资产占比,
convert_to_yiyuan(b.cip) 在建工程,calc_proportion(b.cip,b.total_assets) 在建工程占比,
convert_to_yiyuan(b.intangible_asset) 无形资产,calc_proportion(b.intangible_asset,b.total_assets) 无形资产占比,
convert_to_yiyuan(b.goodwill) 商誉,calc_proportion(b.goodwill,b.total_assets) 商誉占比,
convert_to_yiyuan(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)) 存货,calc_proportion(ifnull(b.inventory,0)+ifnull(b.contract_asset,0),b.total_assets) 存货占比,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短期债务,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0),b.total_assets) 短期债务率
from em_a_mainindex m 
LEFT JOIN em_bal_common_v1 b on b.secucode=m.ts_code and b.report_date=m.date
LEFT JOIN em_bal_common_v1 b2 on b2.secucode=m.ts_code and b2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN em_inc_common_v1 ic on ic.secucode=m.ts_code and ic.report_date=m.date
LEFT JOIN em_inc_common_v1 ic2 on ic2.secucode=m.ts_code and ic2.report_date=DATE_ADD(m.date,INTERVAL '-2' YEAR)
LEFT JOIN em_cf_common_v1 cf on cf.secucode=m.ts_code and cf.report_date=m.date
LEFT JOIN em_cf_common_v1 cf2 on cf2.secucode=m.ts_code and cf2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN ths_hd hd on hd.ts_code=m.ts_code and hd.reportdate=m.date
LEFT JOIN static_mainindex sr on sr.ts_code=m.ts_code
where 1=1
#and m.date='2019-12-31'
#and TIMESTAMPDIFF(year,s.issue_date,CURDATE()) >=10
#and (IFNULL(b.surplusreserve,0)+IFNULL(b.retainedearning,0))>0 
and s.markettype=1
and s.type in (11,82) 
and s.name not like '%b' and s.name not like '%b股'  and s.name not like '%退' and s.name not like '%退市%'
{0}
order by s.market_capital desc {1}";
                }

                string where_addition = string.Empty;
                if (!string.IsNullOrEmpty(condition.Industry))
                    where_addition += string.Format(" and (s.sshy='{0}' or s.sshy_py like '%{0}%' or s.sshy_fullpy like '%{0}%')", condition.Industry);

                //where_addition += string.Format(" and (s.secondary_industry='{0}' or s.sshy_py like '%{0}%' or s.sshy_fullpy like '%{0}%')", condition.Industry);
                //where_addition += string.Format(" and (s.board_name like '%{0}%' or s.board_name_pinyin like '%{0}%' or s.board_name_fullpinyin like '%{0}%')", condition.Industry);

                if (!string.IsNullOrEmpty(condition.StockName))
                    where_addition += string.Format(" and (s.name='{0}' or s.pinyin like '%{0}%' or s.fullpinyin like '%{0}%' or s.ts_code like '%{0}%')", condition.StockName);

                if (!string.IsNullOrEmpty(condition.Roe))
                    where_addition += string.Format(" and m.jqjzcsyl>={0}", condition.Roe);
                //where_addition += string.Format(" and calc_proportion(ic.deduct_parent_netprofit,(b.total_parent_equity+b2.total_parent_equity)/2)>={0}", condition.Roe);

                if (!string.IsNullOrEmpty(condition.Kfjlr))
                {
                    if (condition.Kfjlr.Contains(","))
                        where_addition += string.Format(" and m.kfjlr>={0}*100000000 and m.kfjlr<={1}*100000000 ", condition.Kfjlr.Split(',')[0], condition.Kfjlr.Split(',')[1]);
                    else
                        where_addition += string.Format(" and m.kfjlr>={0}*100000000 ", condition.Kfjlr);
                }

                if (!string.IsNullOrEmpty(condition.MarketCapital))
                {
                    if (condition.MarketCapital.Contains(","))
                        where_addition += string.Format(" and s.market_capital>={0}*100000000 and s.market_capital<={1}*100000000 ", condition.MarketCapital.Split(',')[0], condition.MarketCapital.Split(',')[1]);
                    else
                        where_addition += string.Format(" and s.market_capital>={0}*100000000 ", condition.MarketCapital);
                }

                if (!string.IsNullOrEmpty(condition.SumParentEquity))
                {
                    if (condition.SumParentEquity.Contains(","))
                        where_addition += string.Format(" and b.total_parent_equity>={0}*100000000 and b.total_parent_equity<={1}*100000000 ", condition.SumParentEquity.Split(',')[0], condition.SumParentEquity.Split(',')[1]);
                    else
                        where_addition += string.Format(" and b.total_parent_equity>={0}*100000000 ", condition.SumParentEquity);
                }

                if (!string.IsNullOrEmpty(condition.ContractLiab))
                {
                    if (condition.ContractLiab.Contains(","))
                        where_addition += string.Format(" and (IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0))>={0}*100000000 and (IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0))<={1}*100000000 ", condition.SumParentEquity.Split(',')[0], condition.ContractLiab.Split(',')[1]);
                    else
                        where_addition += string.Format(" and (IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0))>={0}*100000000 ", condition.ContractLiab);
                }


                if (!string.IsNullOrEmpty(condition.Kfjlr_zs))
                {
                    if (condition.Kfjlr_zs.Contains(","))
                        where_addition += string.Format(" and s.latest_np_yoy>=10 and calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit)>={0} and calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit)<={1} ", condition.Kfjlr_zs.Split(',')[0], condition.Kfjlr_zs.Split(',')[1]);
                    else
                        where_addition += string.Format(" and s.latest_np_yoy>=10 and calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit)>={0}", condition.Kfjlr_zs);
                }

                if (!string.IsNullOrEmpty(condition.Pe))
                {
                    if (condition.Pe.Contains(","))
                        where_addition += string.Format(" and s.pe_ttm>={0} and s.pe_ttm<={1} ", condition.Pe.Split(',')[0], condition.Pe.Split(',')[1]);
                    else
                        where_addition += string.Format(" and s.pe_ttm<={0} ", condition.Pe);
                }

                if (!string.IsNullOrEmpty(condition.Pb))
                {
                    var pb_arr = condition.Pb.Split(',');
                    if (condition.Pb.Contains(","))
                        where_addition += string.Format(" and s.pb>={0} and s.pb<={1} ", pb_arr[0], pb_arr[1]);
                    else
                        where_addition += string.Format(" and s.pb<={0} ", condition.Pb);
                }

                if (!string.IsNullOrEmpty(condition.CurrentYearPercent))
                    where_addition += string.Format(" and s.chg720<={0}", condition.CurrentYearPercent);
                //where_addition += string.Format(" and s.current_year_percent<={0}", condition.CurrentYearPercent);

                if (!string.IsNullOrEmpty(condition.IssueDate))
                    where_addition += string.Format(" and TIMESTAMPDIFF(year,s.issue_date,CURDATE())>={0}", condition.IssueDate);

                if (!condition.IncludeFinIndustry)
                    where_addition += " and s.sshy not in ('多元金融','券商信托','保险','银行')";

                if (!string.IsNullOrEmpty(condition.CoreConcept))
                    where_addition += string.Format(" and (s.board_name like '%{0}%' or s.board_name_pinyin like '%{0}%' or s.board_name_fullpinyin like '%{0}%')", condition.CoreConcept);

                /*if (condition.RoeType == "1")
                    where_addition += " and sr.roe2019>=15 and TIMESTAMPDIFF(year,s.issue_date,CURDATE())>=2 and convert_to_yiyuan(m.kfjlr)>=1 and ((calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve)>=10 and convert_to_yiyuan(s.market_capital)<=50) or convert_to_yiyuan(s.market_capital)>50)";
                else if (condition.RoeType == "2")
                    where_addition += " and sr.roe2017>=15 and sr.roe2018>=15 and sr.roe2019>=15 and TIMESTAMPDIFF(year,s.issue_date,CURDATE())>=2 and convert_to_yiyuan(m.kfjlr)>=1 and ((calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve)>=10 and convert_to_yiyuan(s.market_capital)<=50) or convert_to_yiyuan(s.market_capital)>50)";
                else if (condition.RoeType == "3")
                    where_addition += " and sr.roe2019>=20 and TIMESTAMPDIFF(year,s.issue_date,CURDATE())>=2 and convert_to_yiyuan(m.kfjlr)>=1 and ((calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve)>=10 and convert_to_yiyuan(s.market_capital)<=50) or convert_to_yiyuan(s.market_capital)>50)";
                else if (condition.RoeType == "4")
                    where_addition += " and sr.roe2017>=20 and sr.roe2018>=20 and sr.roe2019>=20 and TIMESTAMPDIFF(year,s.issue_date,CURDATE())>=2 and convert_to_yiyuan(m.kfjlr)>=1 and ((calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve)>=10 and convert_to_yiyuan(s.market_capital)<=50) or convert_to_yiyuan(s.market_capital)>50)";*/

                if (condition.BestCompanyType == "roe_and_hd_cf_adv")
                {
                    where_addition += " and ((convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=1 and hd.rdspendsum_ratio>=3 and convert_to_yiyuan(hd.rdspendsum)>=1 and (convert_to_yiyuan(s.market_capital)>=100 and sr.roe2019<15 and sr.roe2020>0) or (convert_to_yiyuan(s.market_capital)<100 and sr.roe2019>=15 and sr.roe2020>5)) or ( convert_to_yiyuan(m.kfjlr)>=1 and (sr.roe2019-sr.roe2018)>=-2 and sr.roe2019>=15 and sr.roe2020>=0) or (convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=3 and sr.roe2019>=10 and sr.roe2020>=-10) or (convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=3 and calc_proportion(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0),m.yyzsr)>=3) )";
                }
                else if (condition.BestCompanyType == "hd")
                {
                    where_addition += " and (convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=1 and hd.rdspendsum_ratio>=3 and convert_to_yiyuan(hd.rdspendsum)>=1 and (convert_to_yiyuan(s.market_capital)>=100 and sr.roe2019<15 and sr.roe2020>0) or (convert_to_yiyuan(s.market_capital)<100 and sr.roe2019>=15 and sr.roe2020>5))";
                }
                else if (condition.BestCompanyType == "roe")
                {
                    where_addition += " and ( convert_to_yiyuan(m.kfjlr)>=1 and (sr.roe2019-sr.roe2018)>=-2 and sr.roe2019>=15 and sr.roe2020>=0)";
                }
                else if (condition.BestCompanyType == "roe4years")
                {
                    //and sr.roe2017>=15 and sr.roe2019>=15
                    where_addition += " and (sr.roe2020>=15 and (sr.roe2018>=15 or sr.roe2019>=15) and (cf.netcash_operate/ic.parent_netprofit)>=0.7)";
                }
                else if (condition.BestCompanyType == "cf")
                {
                    where_addition += " and (convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=3 and sr.roe2019>=10 and sr.roe2020>=-10)";
                }
                else if (condition.BestCompanyType == "adv")
                {
                    where_addition += " and (convert_to_yiyuan(cf2.netcash_operate)>=1 and convert_to_yiyuan(cf.netcash_operate)>=3 and calc_proportion(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0),m.yyzsr)>=3)";
                }

                if (condition.IsFollow)
                    where_addition += " and s.is_follow=1";

                if (condition.ReportType == "3")
                    where_addition += " and m.date='2021-3-31'";
                else if (condition.ReportType == "6")
                    where_addition += " and m.date='2021-6-30'";
                else if (condition.ReportType == "9")
                    where_addition += " and m.date='2020-9-30'";
                else if (condition.ReportType == "12")
                    where_addition += (condition.BestCompanyType == "preyear") ? " and m.date='2019-12-31'" : " and m.date='2020-12-31'";

                if (!string.IsNullOrEmpty(where_addition))
                    sql = string.Format(sql, where_addition, "");
                else
                    sql = string.Format(sql, where_addition, "");

                if (condition.ReportType != "12")
                    sql = sql.Replace("$q4comment$", "#");
                else
                    sql = sql.Replace("$q4comment$", "");

                if (!string.IsNullOrEmpty(condition.OrderBy))
                    sql = sql.Replace("$orderby$", condition.OrderBy);

                var da = new MySqlDataAdapter(sql, con);
                var ds = new DataSet();
                da.Fill(ds);

                _ds_stock_list = ds;
            }
        }

        private void BindHKNormalStocksData()
        {
            var condition = this.scp_toppanel.GetSearchStockCondition();

            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = @"SELECT s.ts_code,
m.date 报表日期,
s.sshy 行业,s.name 名称,(case s.is_follow when 1 then '已关注' else '' end) 是否关注,
s.current_year_percent 年初至今涨跌幅,
TIMESTAMPDIFF(year,s.issue_date,CURDATE()) 上市时间,
convert_to_yiyuan(s.market_capital) 市值,convert_to_decimal(s.pe_ttm) PE,convert_to_decimal(s.pb) PB,
m.jqjzcsyl ROE, mll 毛利率,jll 净利率,convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速
from em_hk_mainindex m left JOIN xq_stock s on m.ts_code=s.ts_code
where m.date='2019-12-31' and s.tradetype='港股通' {0} order by s.market_capital desc {1}";

                string where_addition = string.Empty;
                if (!string.IsNullOrEmpty(condition.Industry))
                    where_addition += string.Format(" and s.sshy='{0}'", condition.Industry);

                if (!string.IsNullOrEmpty(condition.StockName))
                    where_addition += string.Format(" and s.name='{0}'", condition.StockName);

                if (!string.IsNullOrEmpty(condition.Roe))
                    where_addition += string.Format(" and m.jqjzcsyl>={0}", condition.Roe);

                if (!string.IsNullOrEmpty(condition.Kfjlr))
                    where_addition += string.Format(" and m.gsjlr>={0}*100000000", condition.Kfjlr);

                if (!string.IsNullOrEmpty(condition.Kfjlr_zs))
                    where_addition += string.Format(" and m.gsjlrtbzz>={0}", condition.Kfjlr_zs);

                if (!string.IsNullOrEmpty(condition.Pe))
                    where_addition += string.Format(" and s.pe_ttm<={0}", condition.Pe);

                if (!string.IsNullOrEmpty(condition.Pb))
                    where_addition += string.Format(" and m.pb<={0}", condition.Pb);

                if (condition.IsFollow)
                    where_addition += " and s.is_follow=1";

                if (!string.IsNullOrEmpty(where_addition))
                    sql = string.Format(sql, where_addition, "");
                else
                    sql = string.Format(sql, where_addition, "");// " limit 100"

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

        #endregion

        #region 窗体显示

        [DllImport("user32", EntryPoint = "SetParent")]
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

        public void ShowTopPanelSize(bool isMax)
        {
            if (isMax)
                pnl_top.Height = 150;
            else
                pnl_top.Height = 85;
        }

        #endregion

        private void tsmi_copyseccode_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            //var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();
            Clipboard.SetDataObject(tscode);
        }

        private void tsmi_thscookie_Click(object sender, EventArgs e)
        {
            GetThsCookieForm wb = new GetThsCookieForm();
            wb.MdiParent = this;
            wb.Text = "获取同花顺Cookie";
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_fin_add_details_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = tscode.Substring(0, 6);
            var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
            {
                Width = 1350,
                Height = 900,
                Symbol = symbol,
                IsExternalLink = true,
                Url = "https://robo.datayes.com/v2/stock/" + symbol + "/finance#FINANCIAL_NOTE"
            });
            wb.MdiParent = this;
            wb.Text = stockName + "-财务附注明细";
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_view_major_biz_scope_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var childForm = new MajorBusinessScopeForm(stockName, tscode);
            childForm.MdiParent = this;
            childForm.Text = stockName + "-主营构成";
            childForm.Show();

            SetParent((int)childForm.Handle, (int)this.Handle);
        }

        private void tsmi_isbestbiz_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            if (selectedRow == null)
                return;
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            UpdateRowColorInDb(ts_code, (int)MarkedAsType.IsBestBiz);
            selectedRow.DefaultCellStyle.BackColor = MarkedAsTypeHelper.ConverToColor(MarkedAsType.IsBestBiz);
        }

        private void tsmi_pendingresearch_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            if (selectedRow == null)
                return;
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            UpdateRowColorInDb(ts_code, (int)MarkedAsType.PendingResearch);
            selectedRow.DefaultCellStyle.BackColor = MarkedAsTypeHelper.ConverToColor(MarkedAsType.PendingResearch);
        }

        private void tsmi_followup_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            if (selectedRow == null)
                return;
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            UpdateRowColorInDb(ts_code, (int)MarkedAsType.FollowUp);
            selectedRow.DefaultCellStyle.BackColor = MarkedAsTypeHelper.ConverToColor(MarkedAsType.FollowUp);
        }

        private void tsmi_normal_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            if (selectedRow == null)
                return;
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            UpdateRowColorInDb(ts_code, (int)MarkedAsType.Normal);
            selectedRow.DefaultCellStyle.BackColor = tbl_datalist.RowsDefaultCellStyle.BackColor;
        }

        private void UpdateRowColorInDb(string ts_code, int color)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sqlUpdate = "update xq_stock set markedas={1} where ts_code='{0}'";
                sqlUpdate = string.Format(sqlUpdate, ts_code, color);
                conn.Execute(sqlUpdate);
            }
        }

        private void tsmi_measuringthemoat_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();

            var wb = new MeasuringTheMoatForm(ts_code);
            wb.MdiParent = this;
            wb.Text = "丈量护城河-" + stockName;
            wb.Show();

            SetParent((int)wb.Handle, (int)this.Handle);
        }

        private void tsmi_bond_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var childForm = new SinaBondForm(stockName, ts_code);
            childForm.MdiParent = this;
            childForm.Text = stockName + "-债券信息";
            childForm.Show();

            SetParent((int)childForm.Handle, (int)this.Handle);
        }

        private void tsmi_recentreport_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var simplecode = ts_code.Substring(0, 6);
            var date = StringUtils.ConvertToDate("2021-6-30").GetValueOrDefault();
            var jsonStr = HttpHelper.DownloadUrl(string.Format(NoticePdfHelper.em_notices_url, simplecode), Encoding.GetEncoding("utf-8"));

            NoticePdfHelper.ShowPdfViewer(jsonStr, date);
        }

        /// <summary>
        /// 问董秘
        /// </summary>
        private void tsmi_askdm_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = tscode.Substring(0, 6);

            var url = "https://guba.eastmoney.com/qa/search?type=1&code=" + symbol;
            WinFormHelper.OpenPageInChrome(url);

        }

        private void tsmi_ltgd_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = StringUtils.ConvertEMToXQSymbol(tscode);

            var url = "https://xueqiu.com/snowman/S/" + symbol + "/detail#/LTGD";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_srgc_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var simplecode = tscode.Substring(0, 6);

            var url = "http://stockpage.10jqka.com.cn/"+simplecode+"/operate/#analysis";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_quant_fx_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_datalist.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var industry = selectedRow.Cells["行业"].Value.ToString();
            var pb = selectedRow.Cells["PB"].Value.ToString();
            var pettm = selectedRow.Cells["PE"].Value.ToString();
            var roe = selectedRow.Cells["ROE"].Value.ToString();
            var mc = selectedRow.Cells["市值"].Value.ToString();
            var dividend_yield_ratio = selectedRow.Cells["股息率"].Value.ToString();
            var reportType = ((AutoCompleteNameAndValue)this.scp_toppanel.cbxReportType.SelectedItem).Value;

            var frmQuantFx = new QuantFxForm(_marketType, stockName, reportType, industry);
            frmQuantFx.MdiParent = this;
            frmQuantFx.Text = stockName + string.Format("：个股详细财务指标（市值={0}，PB={1}，PE-TTM={2}，ROE={3}，股息率={4}%）", mc, pb, pettm, roe, dividend_yield_ratio);
            frmQuantFx.Show();

            SetParent((int)frmQuantFx.Handle, (int)this.Handle);
        }
    }

}
