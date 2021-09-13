using System;

namespace NiceShot.Core.Models.Entities
{
    public class stock_basic
    {
        public int id { get; set; }
        public string ts_code { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string area { get; set; }
        public string industry { get; set; }
        public string market { get; set; }
        public DateTime? list_date { get; set; }
        public decimal? market_capital { get; set; }
        public decimal? dividend { get; set; }
        public decimal? dividend_yield { get; set; }
        public decimal? float_market_capital { get; set; }
        public decimal? high52w { get; set; }
        public decimal? low52w { get; set; }
        public decimal? navps { get; set; }
        public decimal? eps { get; set; }
        public decimal? pb { get; set; }
        public decimal? pe_ttm { get; set; }
        public int? total_dividends { get; set; }
        public decimal? sum_dividends { get; set; }
        public int? total_ipo { get; set; }
        public decimal? sum_ipo { get; set; }
        public int? total_pp { get; set; }
        public decimal? sum_pp { get; set; }
        public int? total_ri { get; set; }
        public decimal? sum_ri { get; set; }
        public string name_py { get; set; }
        public bool? is_spec { get; set; }
        public decimal? ths_fhze { get; set; }
        public decimal? ths_rzhj { get; set; }
        public int? ths_ljfhcs { get; set; }
        public decimal? ths_rdspendsum { get; set; }
        public decimal? ths_rdspendsum_ratio { get; set; }
        public decimal? ths_rdinvest { get; set; }
        public decimal? ths_rdinvest_ratio { get; set; }
        public string xq_industry { get; set; }
        public int? ths_staffs_num { get; set; }
        public decimal? interest_exchange { get; set; }
        public decimal? interest_income { get; set; }
        public decimal? kggdcgbl { get; set; }
        public decimal? kggdzyzzgb { get; set; }
        public decimal? gsczrljzybl { get; set; }
        public decimal? sjkgr_cgbl { get; set; }
        public string sjkgr { get; set; }
        public decimal? fore_np { get; set; }
        public string fore_type { get; set; }
        public decimal? fore_chg { get; set; }
        public string fore_reason { get; set; }
        public string fore_summary { get; set; }
        public decimal? mll_avg { get; set; }
        public decimal? mll_fc { get; set; }
        public decimal? net_oper_cf_avg { get; set; }
        public int? net_oper_cf_gt0_count { get; set; }
        public int? net_fin_cf_gt0_count { get; set; }
        public int? roe_gt10_count { get; set; }
        public int? roe_gt15_count { get; set; }
        public decimal? current { get; set; }
        public decimal? percent { get; set; }
        public decimal? pledge_ratio { get; set; }
        public decimal? goodwill_in_net_assets { get; set; }
        public decimal? turnover_rate { get; set; }

    }
}
