using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace NiceShot.Core.Models.JsonObjects
{
    public class em_cf_common_v1_jo_list
    {
        public List<em_cf_common_v1_jo> data { get; set; }
    }

    [JsonObject]
    public class em_cf_common_v1_jo
    {
        public string secucode { get; set; }
        public string security_code { get; set; }
        public string report_date { get; set; }
        public string notice_date { get; set; }
        public string update_date { get; set; }
        public string sales_services { get; set; }
        public string deposit_interbank_add { get; set; }
        public string loan_pbc_add { get; set; }
        public string ofi_bf_add { get; set; }
        public string receive_origic_premium { get; set; }
        public string receive_reinsure_net { get; set; }
        public string insured_invest_add { get; set; }
        public string disposal_tfa_add { get; set; }
        public string receive_interest_commission { get; set; }
        public string borrow_fund_add { get; set; }
        public string loan_advance_reduce { get; set; }
        public string repo_business_add { get; set; }
        public string receive_tax_refund { get; set; }
        public string receive_other_operate { get; set; }
        public string operate_inflow_other { get; set; }
        public string operate_inflow_balance { get; set; }
        public string total_operate_inflow { get; set; }
        public string buy_services { get; set; }
        public string loan_advance_add { get; set; }
        public string pbc_interbank_add { get; set; }
        public string pay_origic_compensate { get; set; }
        public string pay_interest_commission { get; set; }
        public string pay_policy_bonus { get; set; }
        public string pay_staff_cash { get; set; }
        public string pay_all_tax { get; set; }
        public string pay_other_operate { get; set; }
        public string operate_outflow_other { get; set; }
        public string operate_outflow_balance { get; set; }
        public string total_operate_outflow { get; set; }
        public string operate_netcash_other { get; set; }
        public string operate_netcash_balance { get; set; }
        public string netcash_operate { get; set; }
        public string withdraw_invest { get; set; }
        public string receive_invest_income { get; set; }
        public string disposal_long_asset { get; set; }
        public string disposal_subsidiary_other { get; set; }
        public string reduce_pledge_timedeposits { get; set; }
        public string receive_other_invest { get; set; }
        public string invest_inflow_other { get; set; }
        public string invest_inflow_balance { get; set; }
        public string total_invest_inflow { get; set; }
        public string construct_long_asset { get; set; }
        public string invest_pay_cash { get; set; }
        public string pledge_loan_add { get; set; }
        public string obtain_subsidiary_other { get; set; }
        public string add_pledge_timedeposits { get; set; }
        public string pay_other_invest { get; set; }
        public string invest_outflow_other { get; set; }
        public string invest_outflow_balance { get; set; }
        public string total_invest_outflow { get; set; }
        public string invest_netcash_other { get; set; }
        public string invest_netcash_balance { get; set; }
        public string netcash_invest { get; set; }
        public string accept_invest_cash { get; set; }
        public string subsidiary_accept_invest { get; set; }
        public string receive_loan_cash { get; set; }
        public string issue_bond { get; set; }
        public string receive_other_finance { get; set; }
        public string finance_inflow_other { get; set; }
        public string finance_inflow_balance { get; set; }
        public string total_finance_inflow { get; set; }
        public string pay_debt_cash { get; set; }
        public string assign_dividend_porfit { get; set; }
        public string subsidiary_pay_dividend { get; set; }
        public string buy_subsidiary_equity { get; set; }
        public string pay_other_finance { get; set; }
        public string subsidiary_reduce_cash { get; set; }
        public string finance_outflow_other { get; set; }
        public string finance_outflow_balance { get; set; }
        public string total_finance_outflow { get; set; }
        public string finance_netcash_other { get; set; }
        public string finance_netcash_balance { get; set; }
        public string netcash_finance { get; set; }
        public string rate_change_effect { get; set; }
        public string cce_add_other { get; set; }
        public string cce_add_balance { get; set; }
        public string cce_add { get; set; }
        public string begin_cce { get; set; }
        public string end_cce_other { get; set; }
        public string end_cce_balance { get; set; }
        public string end_cce { get; set; }
        public string netprofit { get; set; }
        public string asset_impairment { get; set; }
        public string fa_ir_depr { get; set; }
        public string oilgas_biology_depr { get; set; }
        public string ir_depr { get; set; }
        public string ia_amortize { get; set; }
        public string lpe_amortize { get; set; }
        public string defer_income_amortize { get; set; }
        public string prepaid_expense_reduce { get; set; }
        public string accrued_expense_add { get; set; }
        public string disposal_longasset_loss { get; set; }
        public string fa_scrap_loss { get; set; }
        public string fairvalue_change_loss { get; set; }
        public string finance_expense { get; set; }
        public string invest_loss { get; set; }
        public string defer_tax { get; set; }
        public string dt_asset_reduce { get; set; }
        public string dt_liab_add { get; set; }
        public string predict_liab_add { get; set; }
        public string inventory_reduce { get; set; }
        public string operate_rece_reduce { get; set; }
        public string operate_payable_add { get; set; }
        public string other { get; set; }
        public string operate_netcash_othernote { get; set; }
        public string operate_netcash_balancenote { get; set; }
        public string netcash_operatenote { get; set; }
        public string debt_transfer_capital { get; set; }
        public string convert_bond_1year { get; set; }
        public string finlease_obtain_fa { get; set; }
        public string uninvolve_investfin_other { get; set; }
        public string end_cash { get; set; }
        public string begin_cash { get; set; }
        public string end_cash_equivalents { get; set; }
        public string begin_cash_equivalents { get; set; }
        public string cce_add_othernote { get; set; }
        public string cce_add_balancenote { get; set; }
        public string cce_addnote { get; set; }
        public string opinion_type { get; set; }
        public string osopinion_type { get; set; }
        public string minority_interest { get; set; }
    }
}
