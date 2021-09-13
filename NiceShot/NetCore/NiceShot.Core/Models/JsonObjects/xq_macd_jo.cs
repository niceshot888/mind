using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.Core.Models.JsonObjects
{
    public class xq_macd_jo
    {
        public xq_macd_data data { get; set; }
    }
    public class xq_macd_data
    {
        public string symbol { get; set; }
        public List<decimal?[]> item { get; set; }
    }
}
