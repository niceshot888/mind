using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.MultiThread;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace NiceShot.ConsoleApp
{
    public class Program
    {

        //https://stock.xueqiu.com/v5/stock/finance/cn/indicator.json?symbol=SZ000961&type=all&is_detail=true&count=5
        private static string xq_v5_cookie = "s=cv11hmboue; device_id=faee55f3422c95cf1ebdedc40a1dfb83; bid=aae471e1c9766f681d9300f260c39d5c_kf8bd5rl; snbim_minify=true; Hm_lvt_fe218c11eab60b6ab1b6f84fb38bcc4a=1623987322; Hm_lpvt_fe218c11eab60b6ab1b6f84fb38bcc4a=1624973673; xq_is_login=1; u=6406301181; xq_a_token=521556373a4f185a84f37c88f34003949a6d2d94; xqat=521556373a4f185a84f37c88f34003949a6d2d94; xq_id_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJ1aWQiOjY0MDYzMDExODEsImlzcyI6InVjIiwiZXhwIjoxNjMzMTQyOTY2LCJjdG0iOjE2MzA1NTA5NjY1MzgsImNpZCI6ImQ5ZDBuNEFadXAifQ.T3wXkiNyrKooxIw65ZdqUypdfvkTA01r2Rn0BCIuTRypMApSwpJqcP7KsC4pSfUTviPm3S6m0uB2YsQ1MlmkaApPmXdmJwBaR4NG5OkmXCpGbB4mUSfvo3qyPaUwDOrxcBsJzoXHUB6GqJxATeFMxNtLAIGBa8cknp_u9U4bmTNzfnKXgDOlLr4iO22rijfzMHDxJfhkXRV9sww1L2_DmX8snjZVywT51X8vBvCXKfSnyeFeCvsaGYhLSYdt6bkdVmcoIvYUy2C0k-Ur6xxzSeSJmUTxxJsOFRD-DaoDwxhZMIcWk-8-OihQnRE4BdTGqby3_nvcPmNHx0tHYRqYwg; xq_r_token=e78622d0e29efa1e771f2cc9df3bf4740133df0c; Hm_lvt_1db88642e346389874251b5a1eded6e3=1630218857,1630337393,1630917610,1630918457; is_overseas=0; Hm_lpvt_1db88642e346389874251b5a1eded6e3=1631004725";

        static void Main(string[] args)
        {
            //XQThread.SyncXQAStockList();
            //EMThread.SyncEMALuGuTongData();
            //EMThread.SyncEMAMainIndexData(true);

            //EMThread.SyncFinIndustryData(true);
            //EMThreadV1.SyncEMThreeReportData("2021-6-30", 1, true, "2021-6-30");
            //XQThread.SyncXQAMainIndexData(xq_v5_cookie, "2021-6-30");
            
            XQThread.SyncMacdData(xq_v5_cookie);

            //EMThread.SyncEMMajorBusinessScopeData();
            //EMThread.SyncEMCoreConceptData();

            //THSDataAccess.SyncBonusByYear(2020);

            //EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = "601398.SH", Industry = "银行", IncludeUpdate = true });
            //em.CrawlFinIndustryData();

            //EMThreadV1.SyncEMThreeReportData("2021-3-31", 3, true);

            //EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = "000961.SZ" }); em.CrawlAMainIndexData();

            //var em = new EMCrawlerV1(new EMCrawlerV1Criteria { SecurityCode = "600519.SH", Industry = "房地产", InitDate = "2021-3-31", QuarterNums = 5,IncludeUpdate=false }); em.CrawlFinReportData();
            //var em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = "601398.SH", Industry = "银行", IncludeUpdate = true }); em.CrawlFinReportData();


            //XQThread.SyncXQFinBalanceSheetData(xq_v5_cookie);

            //new THSCrawler(new THSCrawlerCriteria { SecurityCode = "600048.SH", Cookie=ths_cookie }).CrawlCashflowAdditionalData();

            //new XQCrawler(new XQCrawlerCriteria { SecurityCode = "600048.SH", Cookie = xq_v5_cookie }).CrawlXQFinBalanceData();

            //THSThread.SyncThsCashflowAdditionalData();

            //EMThread.SyncEMCompanySurveyData();

            //XQThread.SyncXQHKStockTradeType(xq_v5_cookie);

            //THSDataAccess.SyncYftr();//同花顺研发投入
            //THSDataAccess.Sync10YearsBonus();

            //TDXThread.SyncForeRoeAndNP();

            //XQThread.SyncXQTopHoldersData(xq_v5_cookie, TopHolderType.Sdltgd);

            //EMThread.SyncEMHKMainIndexData();

            //HttpHelper.SendRequest("http://basic.10jqka.com.cn/api/stock/finance/601155_cash.json", Encoding.UTF8);

            //new THSCrawler(new THSCrawlerCriteria { SecurityCode = "600929.SH" }).CrawlCashflowAdditionalData();
            //new XQCrawler(new XQCrawlerCriteria { SecurityCode = "000002.SZ" }).CrawlXQFinBalanceData();

            //TDXThread.SyncStockIndustryName();

            //EMDataAccess.DeleteAllTruncatedData();
            //EMDataAccess.BuildOperReportSQLCode();

            //new EMCrawler(new EMCrawlerCriteria { Industry="",SecurityCode= "300859.SZ" }).CrawlFinReportData();

            //EMDataAccess.BuildOperReportSQLCode(ReportType.BalanceSheet, CompanyType.Common, new string[] { }, new string[] { });

            //MySqlDbHelper.BuildEntityBatchedCode("em_mainindex",SeparatedType.Comma,new string[] { "id"}, new string[] { "id", "ts_code", "date" });
            //MySqlDbHelper.BuildEntityBatchedCode("xq_stock", SeparatedType.Comma, new string[] { "id" }, new string[] { "id","symbol" });

            //var em = new EMCrawlerV1(new EMCrawlerV1Criteria{SecurityCode = "600519.SH",Industry = "房地产",InitDate = "2021-3-31",QuarterNums = 5});em.CrawlFinReportData();

            //EMDataAccessV1.BuildTableSQL();
            //EMDataAccessV1.BuildAllJsonObjectCodes();
            //EMDataAccessV1.BuildAllTableEntityCodes();

            //EMDataAccessV1.BuildOperTableSQLCode();

            //CrawErrorDatas();
            //BuildMeasuringTheMoatCode();

            //XQThread.SyncXQForeignDebtData(xq_v5_cookie);
            //var em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = "000961.SZ", Cookie = xq_v5_cookie, Keywords = "中南建设" });
            //em.CrawlForeignDebt();

            //THSDataAccess.SyncStockPriceChangePercent(720);
            //THSDataAccess.SyncStockPriceChangePercent(360);

            //THSDataAccess.SyncBeforeYearsPB(1);
            //THSDataAccess.SyncBeforeYearsPB(4);
        }

        private static void CrawErrorDatas()
        {
            var log_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.log");
            var lines = File.ReadAllLines(log_file_path);
            var seccodelist = new List<string>();
            foreach (var line in lines)
            {
                if (line.Contains("sync em report data occurs error"))
                {
                    var err_idx = line.IndexOf("error:");

                    var str = line.Substring(err_idx + 7);
                    var firstDotIdx = str.IndexOf(",");
                    str = str.Substring(0, firstDotIdx);

                    var arr = str.Split(';');
                    var secucode = arr[0].Split('=')[1];
                    var date = arr[1].Split('=')[1].Split(' ')[0];

                    if (!seccodelist.Contains(secucode))
                        seccodelist.Add(secucode);

                    //var em = new EMCrawlerV1(new EMCrawlerV1Criteria{SecurityCode = secucode,Industry = "房地产",InitDate = "2021-3-31",QuarterNums = 5});
                    //em.CrawlFinReportData();

                }
            }

            foreach (var secucode in seccodelist)
            {
                var em = new EMCrawlerV1(new EMCrawlerV1Criteria { SecurityCode = secucode, Industry = "房地产", InitDate = "2021-3-31", QuarterNums = 5 });
                em.CrawlFinReportData();
            }

        }

        private static void BuildMeasuringTheMoatCode()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "moat.txt");
            var lines = File.ReadAllLines(filePath);
            var sbBindControls = new StringBuilder();
            var sbUpdate = new StringBuilder();
            foreach (var line in lines)
            {
                var field_name = string.Empty;

                if (line.Contains("string"))
                {
                    field_name = line.Replace("public string", "").Replace("{ get; set; }", "").Trim();
                    sbBindControls.AppendLine(string.Format("tbx_{0}.Text = _measuring_the_moat.{0};", field_name));
                    sbUpdate.AppendLine(string.Format("var {0} = tbx_{0}.Text;", field_name));
                    sbUpdate.AppendLine(string.Format("if (!string.IsNullOrEmpty({0}))", field_name));
                    sbUpdate.AppendLine("sbUpdateFields.AppendFormat(\"" + field_name + " = '{0}',\", " + field_name + ");");
                }
                else if (line.Contains("bool?"))
                {
                    field_name = line.Replace("public bool?", "").Replace("{ get; set; }", "").Trim();
                    sbBindControls.AppendLine(string.Format("cb_{0}.Checked = _measuring_the_moat.{0}.GetValueOrDefault();", field_name));
                    sbUpdate.AppendLine(string.Format("var {0} = cb_{0}.Checked;", field_name));
                    sbUpdate.AppendLine("sbUpdateFields.AppendFormat(\"" + field_name + " ={0},\", " + field_name + " ? 1 : 0);");
                }


            }
            Logger.Info(sbBindControls.ToString());
            Logger.Info(sbUpdate.ToString());
        }


    }

    public class ErrorStock
    {
        public string secucode { get; set; }
        public string date { get; set; }
    }

}
