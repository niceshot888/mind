using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{

    [JsonObject]
    public class em_major_business_scope_jo
    {
        public List<em_major_business_scope_zygcfx> zygcfx { get; set; }
    }

    public class em_major_business_scope_zygcfx
    {
        public List<em_major_business_scope_item> cp { get; set; }
        public List<em_major_business_scope_item> hy { get; set; }
    }

    //public class em_major_business_scope_zygcfx_cp{}

    public class em_major_business_scope_item
    {
        public string zygc { get; set; }
        public string zysr { get; set; }
        public string srbl { get; set; }
        public string zycb { get; set; }
        public string cbbl { get; set; }
        public string zylr { get; set; }
        public string lrbl { get; set; }
        public string mll { get; set; }
        public string rq { get; set; }
    }
}
