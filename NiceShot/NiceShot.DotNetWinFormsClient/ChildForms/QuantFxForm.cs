using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.ChildForms;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using NiceShot.DotNetWinFormsClient.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class QuantFxForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private MarketType _marketType;
        private string _stockName;
        private string _industry;
        private string _reportType;
        private DataSet _dataTable;

        public QuantFxForm(MarketType marketType, string stockName, string reportType, string industry)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            this.Width = 1380;
            this.Height = 200;

            this._marketType = marketType;
            this._stockName = stockName;
            this._reportType = reportType;
            this._industry = industry;

            WinFormHelper.InitDataGridViewStyle(tbl_stock_details);
            WinFormHelper.InitChildWindowStyle(this);

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BindNormalStocksData(_stockName);
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            tbl_stock_details.Columns.Clear();

            tbl_stock_details.DataSource = _dataTable.Tables[0];
            tbl_stock_details.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            tbl_stock_details.Columns[5].Frozen = true;
            tbl_stock_details.Columns[0].Visible = false;

            for (int i = 0; i < this.tbl_stock_details.Columns.Count; i++)
            {
                this.tbl_stock_details.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BindNormalStocksData(string stockName)
        {
            if (_marketType == MarketType.A)
                BindANormalStocksData(stockName);
        }

        private void BindANormalStocksData(string stockName)
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                //$q4comment$
                string sql = @"select s.ts_code,inc.report_date 日期,divide(bal.total_assets, bal.total_parent_equity) 财务杠杆,m.jqjzcsyl ROE,m.mll 毛利率,m.jll 净利率,m.yyzsrtbzz 营收增速,m.gsjlrtbzz 归母净利润增速,
convert_to_yiyuan(cf.total_operate_inflow) 销售回款,
convert_to_yiyuan(IFNULL(bal.contract_liab,0)+IFNULL(bal.advance_receivables,0)) 预收款,
convert_to_yiyuan(inc.total_profit) 利润总额,calc_proportion(bal.total_liabilities, bal.total_assets) 资产负债率,
calc_proportion(ifnull(bal.short_loan,0)+ifnull(bal.noncurrent_liab_1year,0)+ifnull(bal.trade_finliab_notfvtpl,0)+ifnull(bal.long_loan,0)+ifnull(bal.bond_payable,0)+ifnull(bal.long_payable,0)+ifnull(bal.perpetual_bond,0),bal.total_assets) 有息负债率,
convert_to_yiyuan(ifnull(bal.monetaryfunds,0)+ifnull(bal.trade_finasset_notfvtpl,0)+ifnull(bal.other_equity_invest,0)+ifnull(bal.other_noncurrent_finasset,0)+ifnull(bal.available_sale_finasset,0)+ifnull(bal.lend_fund,0)) 金融资产,
convert_to_yiyuan(ifnull(bal.monetaryfunds,0)+ifnull(bal.lend_fund,0)) 货币资金,
convert_to_yiyuan(ifnull(bal.short_loan,0)+ifnull(bal.noncurrent_liab_1year,0)+ifnull(bal.trade_finliab_notfvtpl,0)+ifnull(bal.long_loan,0)+ifnull(bal.bond_payable,0)+ifnull(bal.long_payable,0)+ifnull(bal.perpetual_bond,0)) 有息负债,
convert_to_yiyuan(ifnull(bal.short_loan,0)+ifnull(bal.noncurrent_liab_1year,0)+ifnull(bal.trade_finliab_notfvtpl,0)) 短期债务,
convert_to_yiyuan(ifnull(cf.asset_impairment,0)+ifnull(cf.oilgas_biology_depr,0)+ifnull(cf.ia_amortize,0)+ifnull(cf.lpe_amortize,0)+ifnull(cf.disposal_longasset_loss,0)+ifnull(cf.ir_depr,0)+ifnull(cf.fa_scrap_loss,0)+ifnull(cf.fairvalue_change_loss,0)) 保全性资本支出,
convert_to_yiyuan(cf.netcash_operate) 经营现金流净额,convert_to_yiyuan(inc.parent_netprofit) 归母净利润,
convert_to_yiyuan(inc.deduct_parent_netprofit) 扣非归母净利润,convert_to_yiyuan(cf.assign_dividend_porfit) 利息支出及分红等,
convert_to_yiyuan(m.bonus) 当年分红,convert_to_yiyuan(cf.subsidiary_pay_dividend) `子公司支付给少数股东的股利及利润`,
convert_to_yiyuan(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)) `利息支出`
from xq_stock s
left join em_inc_common_v1 inc on s.ts_code=inc.secucode
left join em_bal_common_v1 bal on s.ts_code=bal.secucode and bal.report_date=inc.report_date
left join em_cf_common_v1 cf on s.ts_code=cf.secucode and cf.report_date=inc.report_date
LEFT JOIN em_a_mainindex m on m.ts_code=s.ts_code and m.date=inc.report_date
where 1=1 and inc.report_date >='2016-12-31' and month(inc.report_date)=12
and s.`name`='{0}'
order by inc.report_date desc";

                sql = string.Format(sql, stockName);

                var da = new MySqlDataAdapter(sql, con);
                _dataTable = new DataSet();
                da.Fill(_dataTable);
            }
        }

        private void tbl_stock_details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var b = new SolidBrush(this.tbl_stock_details.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tbl_stock_details.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void tsmi_addhd_Click(object sender, System.EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var date = StringUtils.ConvertToDate(selectedRow.Cells["日期"].Value.ToString()).GetValueOrDefault().ToString("yyyy-MM-dd");

            var frmFinData = new AddHDForm(_stockName, ts_code, date);
            frmFinData.Text = _stockName + "-补充研发投入数据";
            frmFinData.ShowDialog();
        }

        private void tbl_stock_details_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    //若行已是选中状态就不再进行设置
                    if (tbl_stock_details.Rows[e.RowIndex].Selected == false)
                    {
                        tbl_stock_details.ClearSelection();
                        tbl_stock_details.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (tbl_stock_details.SelectedRows.Count == 1 && e.ColumnIndex != -1)
                        tbl_stock_details.CurrentCell = tbl_stock_details.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    //弹出操作菜单
                    cms_stockdetails.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        [DllImport("user32")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        private void tsmi_view_report_Click(object sender, System.EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var simplecode = ts_code.Substring(0, 6);
            var date = StringUtils.ConvertToDate(selectedRow.Cells["日期"].Value.ToString()).GetValueOrDefault();
            var jsonStr = HttpHelper.DownloadUrl(string.Format(NoticePdfHelper.em_notices_url, simplecode), Encoding.GetEncoding("utf-8"));

            NoticePdfHelper.ShowPdfViewer(jsonStr, date);
        }

        private void cbxReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_backgroundWorker != null && !_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        private void tsmi_wdm_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var simplecode = ts_code.Substring(0, 6);

            var url = "https://guba.eastmoney.com/qa/search?type=1&code=" + simplecode;
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_mainbiz_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var childForm = new MajorBusinessScopeForm(_stockName, tscode);
            //childForm.MdiParent = this;
            childForm.Text = _stockName + "-主营构成";
            childForm.ShowDialog();

            //SetParent((int)childForm.Handle, (int)this.Handle);
        }

        private void tsmi_lsgz_Click(object sender, EventArgs e)
        {

        }

        private void tsmi_gdqk_Click(object sender, EventArgs e)
        {

        }

        private void tsmi_ltgd_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = StringUtils.ConvertEMToXQSymbol(tscode);

            var url = "https://xueqiu.com/snowman/S/" + symbol + "/detail#/LTGD";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_srgc_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var simplecode = tscode.Substring(0, 6);

            var url = "http://stockpage.10jqka.com.cn/" + simplecode + "/operate/#analysis";
            WinFormHelper.OpenPageInChrome(url);
        }
    }
}
