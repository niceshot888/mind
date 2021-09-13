using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class em_hk_mainindex_jo
    {
        public em_hk_mainindex_jo()
        {
            data = new em_hk_mainindex_data();
        }
        public int status { get; set; }
        public string msg { get; set; }

        public em_hk_mainindex_data data { get; set; }
    }

    public class em_hk_mainindex_data
    {
        public em_hk_mainindex_data()
        {
            zyzb_abgq = new List<string[]>();
            zyzb_an = new List<string[]>();
        }
        public List<string[]> zyzb_abgq { get; set; }

        public List<string[]> zyzb_an { get; set; }
    }

    public class em_hk_mainindex_item_jo
    {
        public string yyzsr { get; set; }
        public string gsjlr { get; set; }
        public string kfjlr { get; set; }
        public string yyzsrtbzz { get; set; }
        public string gsjlrtbzz { get; set; }
        public string kfjlrtbzz { get; set; }
        public string jqjzcsyl { get; set; }
        public string tbjzcsyl { get; set; }
        public string tbzzcsyl { get; set; }
        public string mll { get; set; }
        public string jll { get; set; }
        public string date { get; set; }
    }
}
