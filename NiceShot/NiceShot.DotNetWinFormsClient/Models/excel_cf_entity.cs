using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.Models
{
   public  class excel_cf_entity
    {
        public string name { get; set; }
        public DateTime? report_date { get; set; }

        public decimal? sales_services { get; set; }
        public decimal? deposit_interbank_add { get; set; }
        public decimal? receive_interest_commission { get; set; }
        public decimal? receive_other_operate { get; set; }
        public decimal? total_operate_inflow { get; set; }
        public decimal? buy_services { get; set; }
        public decimal? loan_advance_add { get; set; }
        public decimal? pbc_interbank_add { get; set; }
        public decimal? pay_interest_commission { get; set; }
        public decimal? pay_staff_cash { get; set; }
        public decimal? pay_all_tax { get; set; }
        public decimal? pay_other_operate { get; set; }
        public decimal? operate_outflow_other { get; set; }
        public decimal? total_operate_outflow { get; set; }
        public decimal? netcash_operate { get; set; }
        public decimal? withdraw_invest { get; set; }
        public decimal? receive_invest_income { get; set; }
        public decimal? disposal_long_asset { get; set; }
        public decimal? disposal_subsidiary_other { get; set; }
        public decimal? receive_other_invest { get; set; }
        public decimal? total_invest_inflow { get; set; }
        public decimal? construct_long_asset { get; set; }
        public decimal? invest_pay_cash { get; set; }
        public decimal? pay_other_invest { get; set; }
        public decimal? total_invest_outflow { get; set; }
        public decimal? netcash_invest { get; set; }
        public decimal? accept_invest_cash { get; set; }
        public decimal? subsidiary_accept_invest { get; set; }
        public decimal? total_finance_inflow { get; set; }
        public decimal? pay_debt_cash { get; set; }
        public decimal? assign_dividend_porfit { get; set; }
        public decimal? subsidiary_pay_dividend { get; set; }
        public decimal? pay_other_finance { get; set; }
        public decimal? total_finance_outflow { get; set; }
        public decimal? netcash_finance { get; set; }
        public decimal? rate_change_effect { get; set; }
        public decimal? cce_add { get; set; }
        public decimal? begin_cce { get; set; }
        public decimal? end_cce { get; set; }
        public decimal? netprofit { get; set; }
        public decimal? asset_impairment { get; set; }
        public decimal? fa_ir_depr { get; set; }
        public decimal? oilgas_biology_depr { get; set; }
        public decimal? ir_depr { get; set; }
        public decimal? ia_amortize { get; set; }
        public decimal? lpe_amortize { get; set; }
        public decimal? disposal_longasset_loss { get; set; }
        public decimal? fa_scrap_loss { get; set; }
        public decimal? fairvalue_change_loss { get; set; }
        public decimal? invest_loss { get; set; }
        public decimal? defer_tax { get; set; }
        public decimal? dt_asset_reduce { get; set; }
        public decimal? dt_liab_add { get; set; }
        public decimal? inventory_reduce { get; set; }
        public decimal? operate_rece_reduce { get; set; }
        public decimal? operate_payable_add { get; set; }
        public decimal? netcash_operatenote { get; set; }
        public decimal? end_cash { get; set; }
        public decimal? begin_cash { get; set; }
        public decimal? cce_addnote { get; set; }

        public decimal? receive_loan_cash { get; set; }
        public decimal? issue_bond { get; set; }
        public decimal? receive_other_finance { get; set; }
    }
}
