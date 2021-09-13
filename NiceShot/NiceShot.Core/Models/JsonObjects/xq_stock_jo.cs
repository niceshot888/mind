using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class xq_stock_list_jo
    {
        public int error_code { get; set; }
        public string error_description { get; set; }
        public xq_stock_data data { get; set; }
    }

    public class xq_stock_data
    {
        public xq_stock_data()
        {
            list = new List<xq_stock_jo>();
        }
        public int count { get; set; }
        public List<xq_stock_jo> list { get; set; }
    }

    public class xq_stock_jo
    {
        public string symbol { get; set; }
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
        public long? issue_date_ts { get; set; }
        public decimal? main_net_inflows { get; set; }
        public long? volume { get; set; }
        public decimal? volume_ratio { get; set; }
        public decimal? pb { get; set; }
        public int? followers { get; set; }
        public decimal? turnover_rate { get; set; }
        public decimal? first_percent { get; set; }
        public string name { get; set; }
        public decimal? pe_ttm { get; set; }
        public long? total_shares { get; set; }
    }
}
