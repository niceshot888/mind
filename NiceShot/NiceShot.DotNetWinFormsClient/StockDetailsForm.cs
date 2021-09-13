using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.ChildForms;
using NiceShot.DotNetWinFormsClient.Core;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient
{
    public partial class StockDetailsForm : Form
    {
        private BackgroundWorker _backgroundWorker = null;
        private MarketType _marketType;
        private string _stockName;
        private string _industry;
        private string _reportType;
        private DataSet _dataTable;
        public StockDetailsForm(MarketType marketType, string stockName, string reportType, string industry)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            this.Width = 1850;

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
                string sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,m.date 日期,
divide(b.SUMASSET, b.sumparentequity) 财务杠杆,m.jqjzcsyl ROE, 
#calc_proportion(ic.KCFJCXSYJLR,(b.sumparentequity+b2.sumparentequity)/2) 扣非ROE,
convert_to_yiyuan(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0)) 低效资产,
calc_proportion(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0),b.total_assets) 低效资产占比,
convert_to_yiyuan(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0)) 核心利润,
calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve) 核心利润率,
$hd$
convert_to_yiyuan(b.GOODWILL) 商誉,calc_proportion(b.GOODWILL,b.SUMASSET) 商誉占比,
mll 毛利率,jll 净利率,
#calc_proportion(ic.KCFJCXSYJLR, ic.OPERATEREVE) 销售净利率,
#calc_proportion(ic.OPERATEREVE, b.SUMASSET) 资产周转率,
convert_to_yiyuan(b.sumparentequity) 归母净资产,
calc_growth(b2.sumparentequity, b.sumparentequity) 归母净资产同比,
convert_to_yiyuan(ifnull(b.MONETARYFUND,0)+ifnull(b.TRADEFASSET,0)+ifnull(b.otherequity_invest,0)+ifnull(b.othernonlfinasset,0)+ifnull(b.SALEABLEFASSET,0)) `金融资产`,

convert_to_yiyuan(ifnull(cf.NETOPERATECASHFLOW,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0)) 自由现金流,
calc_proportion(ifnull(cf.NETOPERATECASHFLOW,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0),ic.OPERATEREVE) 自由现金流占营收比,

convert_to_yiyuan(cf.NETOPERATECASHFLOW) 经营现金流净额,
convert_to_yiyuan(ic.netprofit) 净利润,
convert_to_yiyuan(m.kfjlr) 扣非净利润,
convert_to_yiyuan(ic.INVESTINCOME) 投资收益,
#convert_to_yiyuan(cf.asseimpa) 资产减值准备,
#convert_to_yiyuan(cf.assedepr) 固定资产折旧等,
#convert_to_yiyuan(cf.intaasseamor) 无形资产摊销,
#convert_to_yiyuan(cf.longdefeexpenamor) 长期待摊费用摊销,
convert_to_yiyuan(ifnull(b.SUMASSET,0)-ifnull(b2.SUMASSET,0)) 总资产增加,
convert_to_yiyuan(ifnull(ic.operatereve,0)-ifnull(ic2.operatereve,0)) 主营收增加,
#convert_to_yiyuan(cf.inveredu) 存货的减少,
#convert_to_yiyuan(cf.receredu) 经营性应收项目的减少,
#convert_to_yiyuan(ifnull(b2.billrec,0)-ifnull(b.billrec,0)) 应收票据的减少,
#calc_proportion(-cf.receredu,ifnull(ic.operatereve,0)-ifnull(ic2.operatereve,0)) 应收款增加占比,
#convert_to_yiyuan(cf.payaincr) 经营性应付项目的增加,
convert_to_yiyuan(IFNULL(b.stborrow,0)+IFNULL(b.nonlliaboneyear,0)+IFNULL(b.ltborrow,0)+IFNULL(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 有息负债,
calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)+ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) 有息负债率,
convert_to_yiyuan(cf.finexpe) 财务费用,
#convert_to_yiyuan(ifnull(ic.netprofit,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0)+ifnull(cf.inveredu,0)+ifnull(cf.receredu,0)) 真实自由现金流,
convert_to_yiyuan(cf.buyfilassetpay) 购买固定资产和无形资产等,
#convert_to_yiyuan(cf.dispfilassetrec) 处置固定资产和无形资产等,
convert_to_yiyuan(cf.diviprofitorintpay) `分配股利、偿付利息等`,
convert_to_yiyuan(m.bonus) 分红,
#calc_proportion(ifnull(cf.NETOPERATECASHFLOW,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0),s.market_capital+(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0))-ifnull(cf.cashequiending,0) ) 现金收益率,

convert_to_yiyuan(b.SUMASSET) 总资产,
convert_to_yiyuan(b.FIXEDASSET) 固定资产,calc_proportion(b.FIXEDASSET,b.SUMASSET) 固定资产占比,
convert_to_yiyuan(b.constructionprogress) 在建工程,calc_proportion(b.constructionprogress,b.SUMASSET) 在建工程占比,
convert_to_yiyuan(b.INTANGIBLEASSET) 无形资产,calc_proportion(b.INTANGIBLEASSET,b.SUMASSET) 无形资产占比,

convert_to_yiyuan(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0)) 存货,calc_proportion(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0),b.SUMASSET) 存货占比,
(case when month(b.reportdate)=12 then divide(ic.operateexp,(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0)+ifnull(b2.INVENTORY,0)+ifnull(b2.contractasset,0))/2) else null end) 存货周转率,
#divide(360,ic.operateexp/((ifnull(b.INVENTORY,0)+ifnull(b2.INVENTORY,0))/2)) 存货周转天数,
convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,
convert_to_yiyuan(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0)) 预收款,calc_proportion(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0),m.yyzsr) 预收占主营比,
convert_to_yiyuan(b.ACCOUNTREC) 应收账款,calc_proportion(b.ACCOUNTREC,ic.OPERATEREVE) 应收账款占主营比,
#convert_to_yiyuan(ic.INVESTINCOME) 投资收益,
convert_to_yiyuan(m.kfjlr) 扣非净利润,m.kfjlrtbzz 扣非净利润增速,
convert_to_yiyuan(ifnull(ic.saleexp,0)+ifnull(ic.manageexp,0)) 三费,
calc_proportion(ifnull(ic.saleexp,0)+ifnull(ic.manageexp,0)+ifnull(ic.rdexp,0),ic.operatereve) 三费占比,
convert_to_yiyuan(cf.SUMOPERATEFLOWIN) 表内回款,
#convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速,
convert_to_yiyuan(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)) 短期负债,
calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0),b.SUMASSET) 短期负债率,
convert_to_yiyuan(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 长期负债,
calc_proportion(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) 长期负债率,
convert_to_yiyuan(IFNULL(b.billpay,0)+IFNULL(b.accountpay,0)+IFNULL(b.advancereceive,0)+ifnull(b.contractliab,0)+IFNULL(b.salarypay,0)+IFNULL(b.taxpay,0)+IFNULL(b.otherlliab,0)+IFNULL(b.otherpay,0)) 经营性负债,
convert_to_yiyuan(IFNULL(b.sharecapital,0)+IFNULL(b.capitalreserve,0)+IFNULL(b.minorityequity,0)) 股东入资,
convert_to_yiyuan(IFNULL(b.surplusreserve,0)+IFNULL(b.retainedearning,0)) 利润积累,
convert_to_yiyuan(cf.buygoodsservicepay) 购买商品接受劳务支付的现金,
convert_to_yiyuan(cf.acceptinvrec) 吸收投资收到的现金,
convert_to_yiyuan(cf.loanrec) 取得借款收到的现金,
convert_to_yiyuan(cf.repaydebtpay) `偿还债务支付的现金`
from em_a_mainindex m 
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN em_balancesheet_common b on b.SECURITYCODE=m.ts_code and b.REPORTDATE=m.date
LEFT JOIN em_balancesheet_common b2 on b.SECURITYCODE=b2.SECURITYCODE and b2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN em_income_common ic on ic.SECURITYCODE=b.SECURITYCODE and ic.REPORTDATE=b.REPORTDATE
LEFT JOIN em_income_common ic2 on b.SECURITYCODE=ic2.SECURITYCODE and ic2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN em_cashflow_common cf on cf.SECURITYCODE=b.SECURITYCODE and cf.REPORTDATE=b.REPORTDATE
LEFT JOIN ths_hd hd on hd.ts_code=b.SECURITYCODE and hd.reportdate=b.reportdate
where 1=1 
#and (m.date='2020-6-30' or MONTH(m.date)=12)
{0}
and s.name = '{1}'
and s.markettype=1
order by m.date desc";

                if (_industry == "房地产")
                {
                    sql = @"SELECT s.ts_code,s.sshy 行业,s.name 名称,m.date 日期,
divide(b.SUMASSET, b.sumparentequity) 财务杠杆,m.jqjzcsyl ROE, 
#calc_proportion(ic.KCFJCXSYJLR,(b.sumparentequity+b2.sumparentequity)/2) 扣非ROE,
convert_to_yiyuan(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0)) 低效资产,
calc_proportion(ifnull(b.accounts_rece,0)+ifnull(b.contract_asset,0)+ifnull(b.long_rece,0)+ifnull(b.other_noncurrent_asset,0),b.total_assets) 低效资产占比,
#divide(b.SUMASSET, b.sumparentequity) 财务杠杆比率,
convert_to_yiyuan(b.sumparentequity) 归母净资产,
calc_growth(b2.sumparentequity, b.sumparentequity) 归母净资产同比,
convert_to_yiyuan(ifnull(b.MONETARYFUND,0)+ifnull(b.TRADEFASSET,0)+ifnull(b.otherequity_invest,0)+ifnull(b.othernonlfinasset,0)+ifnull(b.SALEABLEFASSET,0)) 金融资产,
convert_to_yiyuan(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)) 短期负债,
calc_growth(ifnull(b2.stborrow,0)+ifnull(b2.nonlliaboneyear,0)+ifnull(b2.tradefliab,0), ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)) 短债同比,
convert_to_yiyuan(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 长期债务,
calc_growth(ifnull(b2.ltborrow,0)+ifnull(b2.bondpay,0)+ifnull(b2.ltaccountpay,0)+ifnull(b2.sustainabledebt,0), ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 长债同比,
convert_to_yiyuan(cf.SUMOPERATEFLOWIN) 表内回款,
calc_growth(cf2.SUMOPERATEFLOWIN, cf.SUMOPERATEFLOWIN) 表内回款同比,
convert_to_yiyuan(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0)) 存货,calc_proportion(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0),b.SUMASSET) 存货占比,
convert_to_yiyuan(IFNULL(b.stborrow,0)+IFNULL(b.nonlliaboneyear,0)+IFNULL(b.ltborrow,0)+IFNULL(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0)) 有息负债,
calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)+ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) 有息负债率,
convert_to_yiyuan(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0)) 预收款,calc_proportion(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0),b.SUMASSET) 预收款占比,
convert_to_yiyuan(b.ACCOUNTREC) 应收账款,convert_to_yiyuan(ic.OPERATEREVE) 主营收,
#calc_proportion(b.ACCOUNTREC,ic.OPERATEREVE) 应收账款占主营比,
convert_to_yiyuan(b.FIXEDASSET) 固定资产,calc_proportion(b.FIXEDASSET,b.SUMASSET) 固定资产占比,
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
convert_to_yiyuan(cf.diviprofitorintpay) `分配股利、偿付利息等`,
convert_to_yiyuan(m.bonus) 分红,
convert_to_yiyuan(ifnull(ic.saleexp,0)+ifnull(ic.manageexp,0)) 三费,calc_proportion(ifnull(ic.saleexp,0)+ifnull(ic.manageexp,0)+ifnull(ic.rdexp,0),ic.operatereve) 三费占比,
convert_to_yiyuan(ic.INVESTINCOME) 投资收益,
mll 毛利率,jll 净利率,
convert_to_yiyuan(b.SUMASSET) 总资产,
convert_to_yiyuan(m.yyzsr) 营业收入,m.yyzsrtbzz 营收增速,
convert_to_yiyuan(m.kfjlr) 扣非净利润,m.kfjlrtbzz 扣非净利润增速,

convert_to_yiyuan(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0)) 核心利润,
calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0),ic.operatereve) 核心利润率,
#convert_to_yiyuan(m.gsjlr) 归母净利润,m.gsjlrtbzz 归母净利润增速,
#convert_to_yiyuan(ifnull(cf.NETOPERATECASHFLOW,0)-ifnull(cf.asseimpa,0)-ifnull(cf.assedepr,0)-ifnull(cf.intaasseamor,0)-ifnull(cf.longdefeexpenamor,0)-ifnull(cf.dispfixedassetloss,0)-ifnull(cf.realestadep,0)-ifnull(cf.fixedassescraloss,0)-ifnull(cf.valuechgloss,0)) 经营资产自由现金流,

convert_to_yiyuan(IFNULL(b.billpay,0)+IFNULL(b.accountpay,0)+IFNULL(b.advancereceive,0)+ifnull(b.contractliab,0)+IFNULL(b.salarypay,0)+IFNULL(b.taxpay,0)+IFNULL(b.otherlliab,0)+IFNULL(b.otherpay,0)) 经营性负债,
convert_to_yiyuan(IFNULL(b.sharecapital,0)+IFNULL(b.capitalreserve,0)+IFNULL(b.minorityequity,0)) 股东入资,
convert_to_yiyuan(IFNULL(b.surplusreserve,0)+IFNULL(b.retainedearning,0)) 利润积累,
#convert_to_yiyuan(ifnull(b.MONETARYFUND,0)+ifnull(b.TRADEFASSET,0)+ifnull(b.otherequity_invest,0)+ifnull(b.othernonlfinasset,0)+ifnull(b.SALEABLEFASSET,0)) 金融资产,
#convert_to_yiyuan(b.ACCOUNTREC) 应收账款,convert_to_yiyuan(ic.OPERATEREVE) 主营收,calc_proportion(b.ACCOUNTREC,ic.OPERATEREVE) 应收账款占主营比,
#convert_to_yiyuan(b.FIXEDASSET) 固定资产,calc_proportion(b.FIXEDASSET,b.SUMASSET) 固定资产占比,
convert_to_yiyuan(b.constructionprogress) 在建工程,calc_proportion(b.constructionprogress,b.SUMASSET) 在建工程占比,
convert_to_yiyuan(b.INTANGIBLEASSET) 无形资产,calc_proportion(b.INTANGIBLEASSET,b.SUMASSET) 无形资产占比,
convert_to_yiyuan(b.GOODWILL) 商誉,calc_proportion(b.GOODWILL,b.SUMASSET) 商誉占比,
(case when month(b.reportdate)=12 then divide(ic.operateexp,(ifnull(b.INVENTORY,0)+ifnull(b.contractasset,0)+ifnull(b2.INVENTORY,0)+ifnull(b2.contractasset,0))/2) else null end) 存货周转率,
#divide(360,ic.operateexp/((ifnull(b.INVENTORY,0)+ifnull(b2.INVENTORY,0))/2)) 存货周转天数,
calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0),b.SUMASSET) 短期债务率,
calc_proportion(ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) 长期债务率,
convert_to_yiyuan(cf.DIVIPROFITORINTPAY) 分红和利息支出,
convert_to_yiyuan(cf.salegoodsservicerec) 销售商品提供劳务收到的现金,
convert_to_yiyuan(cf.otheroperaterec) 收到其他与经营活动有关的现金,
convert_to_yiyuan(cf.buygoodsservicepay) 购买商品接受劳务支付的现金,
convert_to_yiyuan(cf.acceptinvrec) 吸收投资收到的现金,
convert_to_yiyuan(cf.loanrec) 取得借款收到的现金,
convert_to_yiyuan(cf.repaydebtpay) `偿还债务支付的现金`,
convert_to_yiyuan(cf.diviprofitorintpay) `分配股利、利润或偿付利息支付的现金`
from em_a_mainindex m 
LEFT JOIN xq_stock s on s.ts_code=m.ts_code
LEFT JOIN em_balancesheet_common b on b.SECURITYCODE=m.ts_code and b.REPORTDATE=m.date
LEFT JOIN em_balancesheet_common b2 on b.SECURITYCODE=b2.SECURITYCODE and b2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN em_income_common ic on ic.SECURITYCODE=b.SECURITYCODE and ic.REPORTDATE=b.REPORTDATE
LEFT JOIN em_income_common ic2 on b.SECURITYCODE=ic2.SECURITYCODE and ic2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN em_cashflow_common cf on cf.SECURITYCODE=b.SECURITYCODE and cf.REPORTDATE=b.REPORTDATE
LEFT JOIN em_cashflow_common cf2 on b.SECURITYCODE=cf2.SECURITYCODE and cf2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
LEFT JOIN ths_hd hd on hd.ts_code=b.SECURITYCODE and hd.reportdate=b.reportdate
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
                    cond_rep_type = "and (MONTH(m.date)=12)";
                    //cond_rep_type = "and (m.date='2020-9-30' or MONTH(m.date)=12)";

                sql = sql.Replace("$hd$", _reportType == "12" ? "convert_to_yiyuan(hd.rdspendsum) 研发投入,hd.rdspendsum_ratio 研发投入占比," : string.Empty);

                sql = string.Format(sql, cond_rep_type, stockName);
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
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
            SolidBrush b = new SolidBrush(this.tbl_stock_details.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.tbl_stock_details.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void tsmi_addhd_Click(object sender, System.EventArgs e)
        {
            var selectedRow = tbl_stock_details.SelectedRows[0];
            var stockName = selectedRow.Cells["名称"].Value.ToString();
            var ts_code = selectedRow.Cells["ts_code"].Value.ToString();
            var date = StringUtils.ConvertToDate(selectedRow.Cells["日期"].Value.ToString()).GetValueOrDefault().ToString("yyyy-MM-dd");

            var frmFinData = new AddHDForm(stockName,ts_code,date);
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

    }
}
