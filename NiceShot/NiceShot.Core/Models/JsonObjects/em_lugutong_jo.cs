using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.Core.Models.JsonObjects
{
    [JsonObject]
    public class em_lugutong_jo
    {
        public decimal? SHARESRATE { get; set; }
        public decimal? SHAREHOLDPRICE { get; set; }
    }
}
