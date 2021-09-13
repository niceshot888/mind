using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.JsonObjects
{
    public class em_notice_v1_jo
    {
        public em_notice_data_v1 data { get; set; }
    }

    public class em_notice_data_v1
    {
        public List<em_notice_data_item_v1> list { get; set; }
    }

    public class em_notice_data_item_v1
    {
        public List<em_notice_data_item_v1> list { get; set; }
    }
}
