using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceShot.EduWeb.Models
{
    public class MusicInfo
    {
        public string Name { get; set; }

        /// <summary>
        /// 缩写
        /// </summary>
        public string Abbr { get; set; }

        public string Url { get; set; }
        
        public int Sort { get; set; }
    }
}