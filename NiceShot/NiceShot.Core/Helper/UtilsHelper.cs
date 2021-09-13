using NiceShot.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceShot.Core.Helper
{
    public class UtilsHelper
    {
        /// <summary>
        /// 获取EXCEL表格名称
        /// </summary>
        /// <param name="reportType">报表类型</param>
        public static string GetWorkSheetName(ReportType reportType)
        {
            string sheetName = "";
            switch (reportType)
            {
                case ReportType.BalanceSheet:
                    sheetName = "资产负债表";
                    break;
                case ReportType.Income:
                    sheetName = "利润表";
                    break;
                case ReportType.Cashflow:
                    sheetName = "现金流量表";
                    break;
            }
            return sheetName;
        }

        public static string GetConsecutiveQuarterDatesIncludeSelf(DateTime date, int quarterNums)
        {
            var sbDate = new StringBuilder();

            for (var num = 1; num <= quarterNums; num++)
            {
                sbDate.AppendFormat("{0},", date.ToString("yyyy-MM-dd"));
                date = GetLastQuarterDate(date);
            }

            return sbDate.ToString().TrimEnd(',');
        }

        public static string GetConsecutiveQuarterDatesNotIncludeSelf(DateTime date, int quarterNums)
        {
            var sbDate = new StringBuilder();

            for (var num = 1; num <= quarterNums; num++)
            {
                date = GetLastQuarterDate(date);
                sbDate.AppendFormat("{0},", date.ToString("yyyy-MM-dd"));
            }

            return sbDate.ToString().TrimEnd(',');
        }

        public static DateTime GetLastQuarterDate(DateTime now)
        {
            var date = string.Empty;
            if (now.Month == 12)
                date = string.Format("{0}-{1}-{2}", now.Year, 9, 30);
            else if (now.Month == 9)
                date = string.Format("{0}-{1}-{2}", now.Year, 6, 30);
            else if (now.Month == 6)
                date = string.Format("{0}-{1}-{2}", now.Year, 3, 31);
            else if (now.Month == 3)
                date = string.Format("{0}-{1}-{2}", now.Year - 1, 12, 31);

            return DateTime.Parse(date);
        }

    }
}
