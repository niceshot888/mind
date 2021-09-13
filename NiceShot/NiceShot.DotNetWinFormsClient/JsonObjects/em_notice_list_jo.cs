using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.JsonObjects
{
    [JsonObject]
    public class em_notice_list_jo
    {
        public em_notice_data data { get; set; }
    }

    public class em_notice_data
    {
        public List<em_notice_data_item> list { get; set; }
    }

    public class em_notice_data_item
    {
        public DateTime notice_date { get; set; }
        public string title { get; set; }
        public string art_code { get; set; }
    }
}
