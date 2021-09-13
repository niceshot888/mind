using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.DotNetWinFormsClient.JsonObjects
{
    [JsonObject]
    public class xq_top_holders_jo
    {
        public xq_top_holders_data data { get; set; }
    }

    public class xq_top_holders_data
    {
        public List<xq_top_holders_data_item> quit { get; set; }
        public List<xq_top_holders_data_item> items { get; set; }
    }

    public class xq_top_holders_data_item
    {
        public decimal? chg { get; set; }
        public decimal? held_num { get; set; }
        public decimal? held_ratio { get; set; }
        public string holder_name { get; set; }
    }
}
