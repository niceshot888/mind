using System.Collections.Generic;

namespace NiceShot.Core.Models.JsonObjects
{
    public class xq_quote_jo
    {
        public xq_quote_data data { get; set; }
    }

    public class xq_quote_data
    {
        public List<xq_quote_data_tag_item> tags { get; set; }
    }

    //public class xq_quote_data_tag
    //{
    //    public List<xq_quote_data_tag_item> items { get; set; }
    //}

    public class xq_quote_data_tag_item
    {
        public string description { get; set; }
        public string value { get; set; }
    }
}
