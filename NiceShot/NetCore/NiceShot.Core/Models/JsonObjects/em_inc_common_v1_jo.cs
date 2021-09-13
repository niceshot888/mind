using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace NiceShot.Core.Models.JsonObjects
{
    public class em_inc_common_v1_jo_list
    {
        public List<em_inc_common_v1_jo> data { get; set; }
    }

    [JsonObject]
    public class em_inc_common_v1_jo
    {
        public string secucode { get; set; }
        public string security_code { get; set; }
        public string report_date { get; set; }
        public string notice_date { get; set; }
        public string update_date { get; set; }
        public string total_operate_income { get; set; }
        public string operate_income { get; set; }
        public string interest_income { get; set; }
        public string earned_premium { get; set; }
        public string fee_commission_income { get; set; }
        public string other_business_income { get; set; }
        public string toi_other { get; set; }
        public string total_operate_cost { get; set; }
        public string operate_cost { get; set; }
        public string interest_expense { get; set; }
        public string fee_commission_expense { get; set; }
        public string research_expense { get; set; }
        public string surrender_value { get; set; }
        public string net_compensate_expense { get; set; }
        public string net_contract_reserve { get; set; }
        public string policy_bonus_expense { get; set; }
        public string reinsure_expense { get; set; }
        public string other_business_cost { get; set; }
        public string operate_tax_add { get; set; }
        public string sale_expense { get; set; }
        public string manage_expense { get; set; }
        public string me_research_expense { get; set; }
        public string finance_expense { get; set; }
        public string fe_interest_expense { get; set; }
        public string fe_interest_income { get; set; }
        public string asset_impairment_loss { get; set; }
        public string credit_impairment_loss { get; set; }
        public string toc_other { get; set; }
        public string fairvalue_change_income { get; set; }
        public string invest_income { get; set; }
        public string invest_joint_income { get; set; }
        public string net_exposure_income { get; set; }
        public string exchange_income { get; set; }
        public string asset_disposal_income { get; set; }
        public string asset_impairment_income { get; set; }
        public string credit_impairment_income { get; set; }
        public string other_income { get; set; }
        public string operate_profit_other { get; set; }
        public string operate_profit_balance { get; set; }
        public string operate_profit { get; set; }
        public string nonbusiness_income { get; set; }
        public string noncurrent_disposal_income { get; set; }
        public string nonbusiness_expense { get; set; }
        public string noncurrent_disposal_loss { get; set; }
        public string effect_tp_other { get; set; }
        public string total_profit_balance { get; set; }
        public string total_profit { get; set; }
        public string income_tax { get; set; }
        public string effect_netprofit_other { get; set; }
        public string effect_netprofit_balance { get; set; }
        public string unconfirm_invest_loss { get; set; }
        public string netprofit { get; set; }
        public string precombine_profit { get; set; }
        public string continued_netprofit { get; set; }
        public string discontinued_netprofit { get; set; }
        public string parent_netprofit { get; set; }
        public string minority_interest { get; set; }
        public string deduct_parent_netprofit { get; set; }
        public string netprofit_other { get; set; }
        public string netprofit_balance { get; set; }
        public string basic_eps { get; set; }
        public string diluted_eps { get; set; }
        public string other_compre_income { get; set; }
        public string parent_oci { get; set; }
        public string minority_oci { get; set; }
        public string parent_oci_other { get; set; }
        public string parent_oci_balance { get; set; }
        public string unable_oci { get; set; }
        public string creditrisk_fairvalue_change { get; set; }
        public string otherright_fairvalue_change { get; set; }
        public string setup_profit_change { get; set; }
        public string rightlaw_unable_oci { get; set; }
        public string unable_oci_other { get; set; }
        public string unable_oci_balance { get; set; }
        public string able_oci { get; set; }
        public string rightlaw_able_oci { get; set; }
        public string afa_fairvalue_change { get; set; }
        public string hmi_afa { get; set; }
        public string cashflow_hedge_valid { get; set; }
        public string creditor_fairvalue_change { get; set; }
        public string creditor_impairment_reserve { get; set; }
        public string finance_oci_amt { get; set; }
        public string convert_diff { get; set; }
        public string able_oci_other { get; set; }
        public string able_oci_balance { get; set; }
        public string oci_other { get; set; }
        public string oci_balance { get; set; }
        public string total_compre_income { get; set; }
        public string parent_tci { get; set; }
        public string minority_tci { get; set; }
        public string precombine_tci { get; set; }
        public string effect_tci_balance { get; set; }
        public string tci_other { get; set; }
        public string tci_balance { get; set; }
        public string acf_end_income { get; set; }
        public string opinion_type { get; set; }
    }
}
