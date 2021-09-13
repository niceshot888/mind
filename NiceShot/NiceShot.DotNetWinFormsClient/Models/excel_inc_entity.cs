using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.Models
{
  public  class excel_inc_entity
    {
        public DateTime? report_date { get; set; }
        public string name { get; set; }

        public decimal? total_operate_income { get; set; }
        public decimal? operate_income { get; set; }
        public decimal? interest_income { get; set; }
        public decimal? fee_commission_income { get; set; }
        public decimal? total_operate_cost { get; set; }
        public decimal? operate_cost { get; set; }
        public decimal? interest_expense { get; set; }
        public decimal? fee_commission_expense { get; set; }
        public decimal? research_expense { get; set; }
        public decimal? operate_tax_add { get; set; }
        public decimal? sale_expense { get; set; }
        public decimal? manage_expense { get; set; }
        public decimal? finance_expense { get; set; }
        public decimal? fe_interest_income { get; set; }
        public decimal? fe_interest_expense { get; set; }
        public decimal? fairvalue_change_income { get; set; }
        public decimal? invest_income { get; set; }
        public decimal? asset_disposal_income { get; set; }
        public decimal? asset_impairment_income { get; set; }
        public decimal? credit_impairment_income { get; set; }
        public decimal? other_income { get; set; }
        public decimal? operate_profit { get; set; }
        public decimal? nonbusiness_income { get; set; }
        public decimal? nonbusiness_expense { get; set; }
        public decimal? total_profit { get; set; }
        public decimal? income_tax { get; set; }
        public decimal? netprofit { get; set; }
        public decimal? continued_netprofit { get; set; }
        public decimal? parent_netprofit { get; set; }
        public decimal? minority_interest { get; set; }
        public decimal? deduct_parent_netprofit { get; set; }
        public decimal? basic_eps { get; set; }
        public decimal? diluted_eps { get; set; }
        public decimal? other_compre_income { get; set; }
        public decimal? parent_oci { get; set; }
        public decimal? total_compre_income { get; set; }
        public decimal? parent_tci { get; set; }
        public decimal? minority_tci { get; set; }

    }
}
