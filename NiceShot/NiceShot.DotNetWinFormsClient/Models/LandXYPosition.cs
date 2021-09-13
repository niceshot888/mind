using System;

namespace NiceShot.DotNetWinFormsClient.Models
{
    public class LandXYPosition
    {
        public string name { get; set; }
        public string city { get; set; }
        public string position { get; set; }

        /// <summary>
        /// 楼板价
        /// </summary>
        public decimal? floorprice { get; set; }

        public int month { get; set; }

        public double lng { get; set; }
        public double lat { get; set; }
    }
}
