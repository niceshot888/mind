using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class xq_balancesheet_common_jo
    {
        public string error_code { get; set; }

        public xq_balancesheet_common_data data { get; set; }
    }

    public class xq_balancesheet_common_data
    {
        public xq_balancesheet_common_data()
        {
            list = new List<xq_balancesheet_common_item>();
        }
        public string quote_name { get; set; }

        public List<xq_balancesheet_common_item> list { get; set; }
    }

    public class xq_balancesheet_common_item
    {
        public xq_balancesheet_common_item()
        {
            tradable_fnncl_assets = new List<decimal?>();
            contract_liabilities = new List<decimal?>();
            other_eq_ins_invest = new List<decimal?>();
            other_illiquid_fnncl_assets = new List<decimal?>();
            contractual_assets = new List<decimal?>();
        }
        public long? report_date { get; set; }

        public List<decimal?> tradable_fnncl_assets { get; set; }

        public List<decimal?> contract_liabilities { get; set; }
        public List<decimal?> other_eq_ins_invest { get; set; }
        public List<decimal?> other_illiquid_fnncl_assets { get; set; }
        public List<decimal?> contractual_assets { get; set; }
        

    }

}
