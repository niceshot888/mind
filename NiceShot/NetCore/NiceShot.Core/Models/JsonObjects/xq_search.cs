using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.Core.Models.JsonObjects
{
    public class xq_search_list
    {
        public List<xq_search> list { get; set; }
    }

    public class xq_search
    {
        public long created_at { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
}
