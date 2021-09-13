using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.MultiThread;
using System;
using System.Text;

namespace NiceShot.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //EMThread.SyncEMMainIndex();

            //EMThread.SyncEMThreeReportData();

            //XQThread.SyncXQFinBalanceSheerData();
            //THSThread.SyncThsCashflowAdditionalData();

            HttpHelper.SendRequest("http://basic.10jqka.com.cn/api/stock/finance/601155_cash.json", Encoding.UTF8);

            //new THSCrawler(new THSCrawlerCriteria { SecurityCode = "600519.SH" }).CrawlCashflowAdditionalData();
            //new XQCrawler(new XQCrawlerCriteria { SecurityCode = "000002.SZ" }).CrawlXQFinBalanceData();

            //EMThread.SyncEMCompanySurveyData();

            //XQThread.SyncXQStockList();
            //TDXThread.SyncStockIndustryName();

            //EMDataAccess.DeleteAllTruncatedData();
            //EMDataAccess.BuildOperReportSQLCode();

            //new EMCrawler(new EMCrawlerCriteria { Industry="",SecurityCode= "300859.SZ" }).CrawlFinReportData();

            //EMDataAccess.BuildOperReportSQLCode(ReportType.BalanceSheet, CompanyType.Common, new string[] { }, new string[] { });

            //MySqlDbHelper.BuildEntityBatchedCode("em_mainindex",SeparatedType.Comma,new string[] { "id"}, new string[] { "id", "ts_code", "date" });
            //MySqlDbHelper.BuildEntityBatchedCode("xq_a_stock", SeparatedType.Comma, new string[] { "id" }, new string[] { "id","symbol" });

        }

        private static void CrawlMainIndexData(string ts_code)
        {
            EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = ts_code});
            em.CrawlMainIndexData();
        }

        private static void CrawlXQStockListData()
        {
            XQCrawler xq = new XQCrawler();
            xq.CrawlXQStockListData();
        }
    }
}
