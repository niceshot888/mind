using NiceShot.Core.Enums;

namespace NiceShot.Core.Criterias
{
    public class EMCrawlerCriteria
    {
        public string SecurityCode { get; set; }

        public string Industry { get; set; }

        public MarketType MarketType { get; set; }
        public bool IncludeUpdate { get; set; }


    }

    public class EMCrawlerV1Criteria
    {
        public string SecurityCode { get; set; }

        public string Industry { get; set; }

        public MarketType MarketType { get; set; }

        public string InitDate { get; set; }
        public int QuarterNums { get; set; }

        public bool IncludeUpdate { get; set; }
    }

}
