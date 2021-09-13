using Newtonsoft.Json;
using System.Collections.Generic;

namespace NiceShot.DotNetWinFormsClient.JsonObjects
{
    [JsonObject]
    public class em_companynews_jo
    {
        public em_companynews_gsxw gsxw { get; set; }
        public em_companynews_gsgg gsgg { get; set; }
    }

    public class em_companynews_gsxw
    {

    }

    public class em_companynews_gsgg
    {
        public em_companynews_gsgg_data data { get; set; }
    }

    public class em_companynews_gsgg_data
    {
        public em_companynews_gsgg_data()
        {
            items = new List<em_companynews_gsgg_data_item>();
        }
        public int pageTotal { get; set; }

        public List<em_companynews_gsgg_data_item> items { get; set; }
    }

    public class em_companynews_gsgg_data_item
    {
        public string infoCode { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
    }
}
