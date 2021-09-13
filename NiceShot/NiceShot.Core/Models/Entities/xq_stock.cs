using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.Core.Models.Entities
{
    public class xq_stock
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string ts_code { get; set; }
        public decimal? net_profit_cagr { get; set; }
        public decimal? ps { get; set; }
        public int? type { get; set; }
        public decimal? percent { get; set; }
        public bool? has_follow { get; set; }
        public long? float_shares { get; set; }
        public decimal? current { get; set; }
        public decimal? amplitude { get; set; }
        public decimal? pcf { get; set; }
        public decimal? current_year_percent { get; set; }
        public long? float_market_capital { get; set; }
        public long? market_capital { get; set; }
        public decimal? dividend_yield { get; set; }
        public decimal? roe_ttm { get; set; }
        public decimal? total_percent { get; set; }
        public decimal? percent5m { get; set; }
        public decimal? income_cagr { get; set; }
        public decimal? amount { get; set; }
        public decimal? chg { get; set; }
        public DateTime? issue_date { get; set; }
        public decimal? main_net_inflows { get; set; }
        public long? volume { get; set; }
        public decimal? volume_ratio { get; set; }
        public decimal? pb { get; set; }
        public int? followers { get; set; }
        public decimal? turnover_rate { get; set; }
        public decimal? first_percent { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
        public string fullpinyin { get; set; }
        public string tdxindustry { get; set; }
        public string zjhindustry { get; set; }
        public decimal? pe_ttm { get; set; }
        public long? total_shares { get; set; }

        public int? markettype { get; set; }
        public string tradetype { get; set; }

        public byte? is_follow { get; set; }
    }
}
