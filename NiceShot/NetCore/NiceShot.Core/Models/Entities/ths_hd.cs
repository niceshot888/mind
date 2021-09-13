using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.Core.Models.Entities
{
    public class ths_hd
    {
        public int id { get; set; }
        public string ts_code { get; set; }
        public DateTime? reportdate { get; set; }
        public decimal? rdspendsum { get; set; }
        public decimal? rdspendsum_ratio { get; set; }
    }
}
