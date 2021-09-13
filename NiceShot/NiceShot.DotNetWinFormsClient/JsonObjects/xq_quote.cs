using System.Collections.Generic;

namespace NiceShot.DotNetWinFormsClient.JsonObjects
{
    public class xq_quote_jo
    {
        public xq_quote_data data { get; set; }
    }

    public class xq_quote_data
    {
        public xq_quote_data_tag tags { get; set; }
    }

    public class xq_quote_data_tag
    {
        public List<xq_quote_data_tag_item> items { get; set; }
    }

    public class xq_quote_data_tag_item
    {
        public string data { get; set; }
    }
}
