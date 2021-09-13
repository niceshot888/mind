namespace NiceShot.Core.Enums
{
    public enum ReportType
    {
        BalanceSheet = 1,
        Income = 2,
        Cashflow = 3
    }

    public static class ReportTypeHelper
    {
        public static string ToReportTypeName(this ReportType reportType)
        {
            string repTypeName = string.Empty;
            switch (reportType)
            {
                case ReportType.BalanceSheet:
                    repTypeName = "BalanceSheet".ToLower();
                    break;
                case ReportType.Income:
                    repTypeName = "Income".ToLower();
                    break;
                case ReportType.Cashflow:
                    repTypeName = "Cashflow".ToLower();
                    break;
            }

            return repTypeName;
        }

        public static string ToReportTypeCnName(this ReportType reportType)
        {
            string repTypeName = string.Empty;
            switch (reportType)
            {
                case ReportType.BalanceSheet:
                    repTypeName = "资产负债表";
                    break;
                case ReportType.Income:
                    repTypeName = "利润表";
                    break;
                case ReportType.Cashflow:
                    repTypeName = "现金流量表";
                    break;
            }

            return repTypeName;
        }

        public static ReportType ConvertToReportType(this string repTypeName)
        {
            ReportType reportType = ReportType.BalanceSheet;
            if (repTypeName == "资产负债表")
                reportType = ReportType.BalanceSheet;
            else if (repTypeName == "利润表")
                reportType = ReportType.Income;
            else if (repTypeName == "现金流量表")
                reportType = ReportType.Cashflow;
            return reportType;
        }
    }

}
