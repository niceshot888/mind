namespace NiceShot.DotNetWinFormsClient.Models
{
    public class SearchStockCondition
    {
        public string Industry { get; set; }
        public string StockName { get; set; }
        public string Roe { get; set; }
        public string Kfjlr { get; set; }
        public string Kfjlr_zs { get; set; }
        public string Pe { get; set; }
        public string Pb { get; set; }
        public string CurrentYearPercent { get; set; }
        public string IssueDate { get; set; }
        public string ReportType { get; set; }
        public bool IsFollow { get; set; }
        public bool IncludeFinIndustry { get; set; }
        
        public  string RoeType { get; set; }
        public string BestCompanyType { get; set; }

        public string SumParentEquity { get; set; }
        public string OrderBy { get; set; }
        public string CoreConcept { get; set; }
        public string ContractLiab { get; set; }
        public string MarketCapital { get; set; }
    }
}
