using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class xq_fin_indicator_jo
    {
        public xq_fin_indicator_data data { get; set; }
    }

    public class xq_fin_indicator_data
    {
        public xq_fin_indicator_data()
        {
            list = new List<xq_fin_indicator_data_item>();
        }
        public string quote_name { get; set; }
        public int org_type { get; set; }

        public List<xq_fin_indicator_data_item> list { get; set; }
    }

    public class xq_fin_indicator_data_item
    {
        public long? report_date { get; set; }
        public decimal?[] avg_roe { get; set; }
        public decimal?[] ore_dlt { get; set; }
        
        public decimal?[] net_selling_rate { get; set; }
        public decimal?[] gross_selling_rate { get; set; }

        public decimal?[] total_revenue { get; set; }
        public decimal?[] operating_income_yoy { get; set; }
        public decimal?[] net_profit_atsopc { get; set; }
        public decimal?[] net_profit_atsopc_yoy { get; set; }
        public decimal?[] net_profit_after_nrgal_atsolc { get; set; }
        public decimal?[] np_atsopc_nrgal_yoy { get; set; }
        public decimal?[] net_interest_of_total_assets { get; set; }
    }
}
