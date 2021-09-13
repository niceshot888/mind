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
    public partial class FinDetailsForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private MarketType _marketType;
        private string _stockName;
        private string _industry;
        private string _reportType;
        private DataSet _dataTable;

        public FinDetailsForm(MarketType marketType, string stockName, string reportType, string industry)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            this.Width = 1300;
            this.Height = 600;

            cbxReportType.Items.Clear();
            cbxReportType.DisplayMember = "Name";
            cbxReportType.ValueMember = "Value";
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "一季报", Value = "3" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "中报", Value = "6" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "三季报", Value = "9" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "年报", Value = "12" });
            cbxReportType.SelectedIndex = 1;

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
            else if (_marketType == MarketType.HK)
                BindHKNormalStocksData(stockName);
        }

        private void BindANormalStocksData(string stockName)
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                //$q4comment$
                string sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,m.date 日期,
mll 毛利率,jll 净利率,calc_proportion(ic.deduct_parent_netprofit,b.total_assets) 资产收益率,
divide(b.total_assets, b.total_parent_equity) 财务杠杆,m.jqjzcsyl ROE,
#calc_proportion(ic.deduct_parent_netprofit,(b.total_parent_equity+b2.total_parent_equity)/2) 扣非ROE,
convert_to_yiyuan(ic.total_profit) 利润总额,
convert_to_yiyuan(cf.total_operate_inflow) 表内回款,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) 有息负债,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0),b.total_assets) 有息负债率,
convert_to_yiyuan(cf.netcash_operate) 经营现金流净额,
convert_to_yiyuan(cf.asset_impairment) 资产减值准备,
convert_to_yiyuan(cf.fa_ir_depr) 固定资产折旧等,
convert_to_yiyuan(cf.ia_amortize) 无形资产摊销,
convert_to_yiyuan(cf.lpe_amortize) 长期待摊费用摊销,
$q4comment$convert_to_yiyuan(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0)) 自由现金流,
$q4comment$calc_proportion(ifnull(cf.netcash_operate,0)-ifnull(cf.asset_impairment,0)-ifnull(cf.oilgas_biology_depr,0)-ifnull(cf.ia_amortize,0)-ifnull(cf.lpe_amortize,0)-ifnull(cf.disposal_longasset_loss,0)-ifnull(cf.ir_depr,0)-ifnull(cf.fa_scrap_loss,0)-ifnull(cf.fairvalue_change_loss,0),ic.operate_income) 自由现金流占营收比,
convert_to_yiyuan(ifnull(cf.assign_dividend_porfit,0)-ifnull(m.bonus,0)) `利息支出`,
convert_to_yiyuan(ic.parent_netprofit) 归母净利润,calc_proportion(ic.parent_netprofit,ic.operate_income) 归母净利率,calc_growth(ic2.parent_netprofit,ic.parent_netprofit) 归母净利润增速,
convert_to_yiyuan(ic.deduct_parent_netprofit) 扣非净利润,
calc_proportion(ic.deduct_parent_netprofit,ic.operate_income) 扣非净利率,
calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit) 扣非净利润增速,
#convert_to_yiyuan(ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0))) EBIT,
#convert_to_yiyuan((ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0))*(1-0.25)) NOPAT,
#convert_to_yiyuan((ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)))*(1-0.25)) NOPLAT,
#convert_to_yiyuan(ifnull(b.total_equity,0)+ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) IC,
#calc_proportion( (ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)))*(1-0.25), ifnull(b.total_equity,0)+ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) ROIC,

convert_to_yiyuan(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) 核心利润,
calc_proportion(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0),ic.operate_income) 核心利润率,
convert_to_yiyuan(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0)) 预收款,
$q4comment$calc_proportion(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0),ic.operate_income) 预收占主营比,
convert_to_yiyuan(ic.operate_income) 营收,calc_growth(ic2.operate_income,ic.operate_income) 营收增速,
convert_to_yiyuan(ifnull(ic.sale_expense,0)) 销售费用,convert_to_yiyuan(ifnull(ic.manage_expense,0)) 管理费用,
convert_to_yiyuan(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)+ifnull(ic.fe_interest_expense,0)) 三费,
calc_proportion(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)+ifnull(ic.fe_interest_expense,0),ic.operate_income) 三费占比,
convert_to_yiyuan(ic.invest_income) 投资收益,
convert_to_yiyuan(ic.asset_disposal_income) 资产处置收益,
convert_to_yiyuan(ic.other_income) 其他收益,
#$hd$

$q4comment$convert_to_yiyuan(hd.rdspendsum) 研发投入,hd.rdspendsum_ratio 研发投入占比,

#calc_proportion(ic.OPERATEREVE, b.SUMASSET) 资产周转率,
#divide(b.SUMASSET, b.sumparentequity) 财务杠杆比率,
convert_to_yiyuan(b.total_parent_equity) 归母净资产,calc_growth(b2.total_parent_equity, b.total_parent_equity) 归母净资产同比,
#convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) 金融资产,
convert_to_yiyuan(ic.fe_interest_expense) 利息费用,
convert_to_yiyuan(cf.assign_dividend_porfit) `分配股利、偿付利息等`,
convert_to_yiyuan(cf.subsidiary_pay_dividend) `减:少数股东的股利等`,
convert_to_yiyuan(m.bonus) `减:分红`,
convert_to_yiyuan(b.total_assets) 总资产,
convert_to_yiyuan(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0)) 低效资产,
calc_proportion(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0),b.total_assets) 低效资产占比,
convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) 金融资产,
calc_proportion(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0),b.total_assets) 金融资产占比,
convert_to_yiyuan(b.fixed_asset) 固定资产,calc_proportion(b.fixed_asset,b.total_assets) 固定资产占比,
convert_to_yiyuan(b.cip) 在建工程,calc_proportion(b.cip,b.total_assets) 在建工程占比,
convert_to_yiyuan(b.intangible_asset) 无形资产,calc_proportion(b.intangible_asset,b.total_assets) 无形资产占比,
convert_to_yiyuan(b.goodwill) 商誉,calc_proportion(b.goodwill,b.total_assets) 商誉占比,
convert_to_yiyuan(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)) 存货,calc_proportion(ifnull(b.inventory,0)+ifnull(b.contract_asset,0),b.total_assets) 存货占比,
#(case when month(b.report_date)=12 then divide(ic.operate_cost,(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)+ifnull(b2.inventory,0)+ifnull(b2.contract_asset,0))/2) else null end) 存货周转率,
#divide(360,ic.operateexp/((ifnull(b.INVENTORY,0)+ifnull(b2.INVENTORY,0))/2)) 存货周转天数,
convert_to_yiyuan(b.prepayment) 预付款项,
calc_proportion(b.prepayment,b.total_assets) 预付款项占比,
convert_to_yiyuan(b.accounts_rece) 应收账款,
calc_proportion(b.accounts_rece,ic.operate_income) 应收账款占主营比,
convert_to_yiyuan(b.note_rece) 应收票据,
calc_proportion(b.note_rece,b.total_assets) 应收票据占比,
convert_to_yiyuan(b.contract_asset) 合同资产,
calc_proportion(b.contract_asset,ic.operate_income) 合同资产占主营比,
convert_to_yiyuan(b.total_other_rece) 其他应收款,
calc_proportion(b.total_other_rece,b.total_assets) 其他应收款占比,
convert_to_yiyuan(b.long_rece) 长期应收款,
calc_proportion(b.long_rece,b.total_assets) 长期应收款占比,
convert_to_yiyuan(b.other_noncurrent_asset) 其他非流动资产,
calc_proportion(b.other_noncurrent_asset,b.total_assets) 其他非流动资产占比,
convert_to_yiyuan(b.other_noncurrent_finasset) 其他非流动金融资产,
calc_proportion(b.other_noncurrent_finasset,b.total_assets) 其他非流动金融资产占比,
convert_to_yiyuan(b.invest_realestate) 投资性房地产,
calc_proportion(b.invest_realestate,b.total_assets) 投资性房地产占比,
convert_to_yiyuan(b.long_equity_invest) 长期股权投资,
calc_proportion(b.long_equity_invest,b.total_assets) 长期股权投资占比,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短期债务,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0),b.total_assets) 短期债务率,
convert_to_yiyuan(b.accounts_payable) 应付账款,
calc_proportion(b.accounts_payable,b.total_assets) 应付账款占比,
convert_to_yiyuan(b.note_payable) 应付票据,
calc_proportion(b.note_payable,b.total_assets) 应付票据占比
from em_a_mainindex m 
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN em_bal_common_v1 b on b.secucode=m.ts_code and b.report_date=m.date
LEFT JOIN em_bal_common_v1 b2 on b2.secucode=m.ts_code and b2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN em_inc_common_v1 ic on ic.secucode=m.ts_code and ic.report_date=m.date
LEFT JOIN em_inc_common_v1 ic2 on ic2.secucode=m.ts_code and ic2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
#LEFT JOIN em_inc_common_v1 ic3 on ic3.secucode=m.ts_code and ic3.report_date=DATE_ADD(m.date,INTERVAL '-2' YEAR)
LEFT JOIN em_cf_common_v1 cf on cf.secucode=m.ts_code and cf.report_date=m.date
LEFT JOIN ths_hd hd on hd.ts_code=b.secucode and hd.reportdate=m.date
where 1=1 
#and (m.date='2020-6-30' or MONTH(m.date)=12)
{0}
and s.name = '{1}'
and s.markettype=1
order by m.date desc";

                if (_industry == "房地产")
                {
                    sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,m.date 日期,
divide(b.total_assets, b.total_parent_equity) 财务杠杆,m.jqjzcsyl ROE, 
#calc_proportion(ic.deduct_parent_netprofit,(b.total_parent_equity+b2.total_parent_equity)/2) 扣非ROE,
convert_to_yiyuan(ic.total_profit) 利润总额,
convert_to_yiyuan(cf.total_operate_inflow) 表内回款,calc_growth(cf2.total_operate_inflow, cf.total_operate_inflow) 表内回款同比,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) 有息负债,
calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0),b.total_assets) 有息负债率,

#convert_to_yiyuan(ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0))) EBIT,
#convert_to_yiyuan((ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0))*(1-0.25)) NOPAT,
#convert_to_yiyuan((ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)))*(1-0.25)) NOPLAT,
#convert_to_yiyuan(ifnull(b.total_equity,0)+ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) IC,
#calc_proportion( (ifnull(ic.parent_netprofit,0)+ifnull(ic.income_tax,0)+(ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)))*(1-0.25), ifnull(b.total_equity,0)+ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) ROIC,

convert_to_yiyuan(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0)) 低效资产,
calc_proportion(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0),b.total_assets) 低效资产占比,

#convert_to_yiyuan(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) 核心利润,
#calc_proportion(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0),ic.operate_income) 核心利润率,
convert_to_yiyuan(ic.operate_income) 营收,calc_growth(ic2.operate_income,ic.operate_income) 营收增速,
convert_to_yiyuan(ifnull(ic.sale_expense,0)) 销售费用,
convert_to_yiyuan(ifnull(ic.manage_expense,0)) 管理费用,
convert_to_yiyuan(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)) 两费,
calc_growth(ifnull(ic2.sale_expense,0)+ifnull(ic2.manage_expense,0),ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)) 两费增速,
calc_proportion(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0),ic.operate_income) 两费占比,
convert_to_yiyuan(ifnull(cf.assign_dividend_porfit,0)-ifnull(m.bonus,0)) `利息支出`,
convert_to_yiyuan(ic.parent_netprofit) 归母净利润,calc_proportion(ic.parent_netprofit,ic.operate_income) 归母净利率,calc_growth(ic2.parent_netprofit,ic.parent_netprofit) 归母净利润增速,
convert_to_yiyuan(ic.deduct_parent_netprofit) 扣非净利润,calc_proportion(ic.deduct_parent_netprofit,ic.operate_income) 扣非净利率,calc_growth(ic2.deduct_parent_netprofit,ic.deduct_parent_netprofit) 扣非净利润增速,
convert_to_yiyuan(b.total_parent_equity) 归母净资产,calc_growth(b2.total_parent_equity, b.total_parent_equity) 归母净资产同比,
convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) 金融资产,
convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短期债务,
calc_growth(ifnull(b2.short_loan,0)+ifnull(b2.noncurrent_liab_1year,0)+ifnull(b2.trade_finliab_notfvtpl,0), ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短债同比,
convert_to_yiyuan(ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) 长期债务,
calc_growth(ifnull(b2.long_loan,0)+ifnull(b2.bond_payable,0)+ifnull(b2.long_payable,0)+ifnull(b2.perpetual_bond,0), ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0)) 长债同比,
convert_to_yiyuan(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)) 存货,calc_proportion(ifnull(b.inventory,0)+ifnull(b.contract_asset,0),b.total_assets) 存货占比,
#convert_to_yiyuan(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)) 短期债务,
#calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0),b.total_assets) 短期债务率,
convert_to_yiyuan(ic.operate_income) 主营收,
convert_to_yiyuan(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0)) 预收款,
#calc_proportion(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0),ic.operate_income) 预收占主营比,
convert_to_yiyuan(b.prepayment) 预付款项,
calc_proportion(b.prepayment,b.total_assets) 预付款项占比,
convert_to_yiyuan(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)) 应收账款,
#calc_proportion(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0),ic.operate_income) 应收账款占主营比,
#convert_to_yiyuan(cf.assedepr) 固定资产折旧等,
#convert_to_yiyuan(cf.intaasseamor) 无形资产摊销,
#convert_to_yiyuan(cf.longdefeexpenamor) 长期待摊费用摊销,
#convert_to_yiyuan(cf.buyfilassetpay) 购买固定资产等,
#convert_to_yiyuan(cf.inveredu) 存货的减少,
#convert_to_yiyuan(cf.receredu) 经营性应收项目的减少,
#convert_to_yiyuan(cf.payaincr) 经营性应付项目的增加,
#ic.sales_eq 销售权益额,
#ic.fore_netprofit 预测两年后净利润,
#convert_to_yiyuan(cf.asseimpa) 资产减值准备,
convert_to_yiyuan(cf.assign_dividend_porfit) `分配股利、偿付利息等`,
#convert_to_yiyuan(ifnull(ic.sale_expense,0)+ifnull(ic.manage_expense,0)) 两费,
convert_to_yiyuan(cf.subsidiary_pay_dividend) `减:少数股东的股利等`,
convert_to_yiyuan(m.bonus) `减:分红`,
convert_to_yiyuan(ic.invest_income) 投资收益,
mll 毛利率,jll 净利率,
convert_to_yiyuan(b.total_assets) 总资产,
convert_to_yiyuan(b.fixed_asset) 固定资产,calc_proportion(b.fixed_asset,b.total_assets) 固定资产占比,
convert_to_yiyuan(b.cip) 在建工程,calc_proportion(b.cip,b.total_assets) 在建工程占比,
convert_to_yiyuan(b.intangible_asset) 无形资产,calc_proportion(b.intangible_asset,b.total_assets) 无形资产占比,
convert_to_yiyuan(b.goodwill) 商誉,calc_proportion(b.goodwill,b.total_assets) 商誉占比,
(case when month(b.report_date)=12 then divide(ic.operate_cost,(ifnull(b.inventory,0)+ifnull(b.contract_asset,0)+ifnull(b2.inventory,0)+ifnull(b2.contract_asset,0))/2) else null end) 存货周转率
from em_a_mainindex m 
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN em_bal_common_v1 b on b.secucode=m.ts_code and b.report_date=m.date
LEFT JOIN em_bal_common_v1 b2 on b.secucode=b2.secucode and b2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN em_inc_common_v1 ic on ic.secucode=b.secucode and ic.report_date=m.date
LEFT JOIN em_inc_common_v1 ic2 on b.secucode=ic2.secucode and ic2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN em_cf_common_v1 cf on cf.secucode=b.secucode and cf.report_date=m.date
LEFT JOIN em_cf_common_v1 cf2 on b.secucode=cf2.secucode and cf2.report_date=DATE_ADD(m.date,INTERVAL '-1' YEAR)
LEFT JOIN ths_hd hd on hd.ts_code=b.secucode and hd.reportdate=m.date
where 1=1 
#and (m.date='2020-6-30' or MONTH(m.date)=12)
{0}
and s.name = '{1}'
and s.markettype=1
order by m.date desc";
                }

                string cond_rep_type = "";
                if (_reportType == "3")
                    cond_rep_type = "and MONTH(m.date)=3";
                else if (_reportType == "6")
                    cond_rep_type = "and MONTH(m.date)=6";
                else if (_reportType == "9")
                    cond_rep_type = "and MONTH(m.date)=9";
                else if (_reportType == "12")
                    cond_rep_type = "and ((MONTH(m.date)=12) or m.date='2021-6-30')";
                //cond_rep_type = "and (m.date='2020-9-30' or MONTH(m.date)=12)";

                //sql = sql.Replace("$hd$", _reportType == "12" ? "convert_to_yiyuan(hd.rdspendsum) 研发投入,hd.rdspendsum_ratio 研发投入占比," : string.Empty);

                if (_reportType != "12")
                    sql = sql.Replace("$q4comment$", "#");
                else
                    sql = sql.Replace("$q4comment$", "");

                sql = string.Format(sql, cond_rep_type, stockName);

                var da = new MySqlDataAdapter(sql, con);
                _dataTable = new DataSet();
                da.Fill(_dataTable);
            }
        }

        private void BindHKNormalStocksData(string stockName)
        {
            using (MySqlConnection con = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                //m.date='2020-3-31' or
                string sql = @"SELECT s.sshy 行业,s.name 名称,m.date 日期,
convert_to_yiyuan(s.market_capital) 市值,s.pe_ttm 市盈率,
m.jqjzcsyl ROE, mll 毛利率,jll 净利率,
convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,
convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速
from em_hk_mainindex m 
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
where ( m.date='2020-6-30' or MONTH(m.date)=12)
and s.name = '{0}'
and s.markettype=2
order by m.date desc";

                sql = string.Format(sql, stockName);
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
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
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var date = StringUtils.ConvertToDate(selectedRow.Cells["日期"].Value.ToString()).GetValueOrDefault().ToString("yyyy-MM-dd");

            var frmFinData = new AddHDForm(stockName, ts_code, date);
            //frmFinData.MdiParent = this;
            frmFinData.Text = stockName + "-补充研发投入数据";
            frmFinData.Show();

            SetParent((int)frmFinData.Handle, (int)this.Handle);
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
            _reportType = ((AutoCompleteNameAndValue)cbxReportType.SelectedItem).Value;

            //if (_marketType == MarketType.A)
            //    BindANormalStocksData(_stockName);
            //else if (_marketType == MarketType.HK)
            //    BindHKNormalStocksData(_stockName);

            if (_backgroundWorker !=null && !_backgroundWorker.IsBusy)
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
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var childForm = new MajorBusinessScopeForm(stockName, tscode);
            //childForm.MdiParent = this;
            childForm.Text = stockName + "-主营构成";
            childForm.ShowDialog();

            //SetParent((int)childForm.Handle, (int)this.Handle);
        }

        private void tsmi_lsgz_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = _marketType == MarketType.A ? StringUtils.ConvertEMToXQSymbol(tscode) : tscode;
            var frmEcharts = new HistoryValueWbForm(_marketType, symbol);
            frmEcharts.Text = stockName + "-历史估值";
            frmEcharts.ShowDialog();
        }

        private void tsmi_gdqk_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = _marketType == MarketType.A ? StringUtils.ConvertEMToXQSymbol(tscode) : tscode;
            var frmEcharts = new EChartsWebBrowserForm(symbol);
            frmEcharts.Text = stockName + "-股东人数";
            frmEcharts.ShowDialog();
        }

        private void tsmi_ltgd_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var symbol = StringUtils.ConvertEMToXQSymbol(tscode);

            var url = "https://xueqiu.com/snowman/S/" + symbol + "/detail#/LTGD";
            WinFormHelper.OpenPageInChrome(url);
        }

        private void tsmi_srgc_Click(object sender, EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var tscode = selectedRow.Cells["ts_code"].Value.ToString();

            var simplecode = tscode.Substring(0, 6);

            var url = "http://stockpage.10jqka.com.cn/" + simplecode + "/operate/#analysis";
            WinFormHelper.OpenPageInChrome(url);
        }
    }
}
