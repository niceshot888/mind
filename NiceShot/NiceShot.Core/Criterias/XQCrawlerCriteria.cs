using NiceShot.Core.Enums;

namespace NiceShot.Core.Criterias
{
    public class XQCrawlerCriteria
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; }

        public string SecurityCode { get; set; }

        public string Cookie { get; set; }

        public MarketType MarketType { get; set; }

        public TopHolderType TopHolderType { get; set; }

    }
}
