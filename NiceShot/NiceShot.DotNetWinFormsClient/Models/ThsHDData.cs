using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.DotNetWinFormsClient.Models
{
    public class ThsHDData
    {
        public DateTime reportdate { get; set; }
        
        public decimal? rdspendsum { get; set; }
        public decimal? rdspendsum_ratio { get; set; }
        public decimal? rdinvest { get; set; }
        public decimal? rdinvest_ratio { get; set; }
    }

}
