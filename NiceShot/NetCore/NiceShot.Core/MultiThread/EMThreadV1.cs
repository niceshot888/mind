using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace NiceShot.Core.MultiThread
{
    public class EMThreadV1
    {

        /// <summary>
        /// 同步东财三张财务报表数据
        /// </summary>
        public static void SyncEMThreeReportData(string initDate, int quarterNums, bool includeUpdate, string notdata_date)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetEMNoDataXQStockListByDate(MarketType.A, notdata_date);
            foreach (var stock in stockList)
            {
                EMCrawlerV1 em = new EMCrawlerV1(new EMCrawlerV1Criteria
                {
                    SecurityCode = stock.ts_code,
                    Industry = stock.tdxindustry,
                    InitDate = initDate,
                    QuarterNums = quarterNums,
                    IncludeUpdate = includeUpdate
                });
                taskList.Add(taskFactory.StartNew(em.CrawlFinReportData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }

            /*var log_file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.log");
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
                }
            }

            foreach (var secucode in seccodelist)
            {
                EMCrawlerV1 em = new EMCrawlerV1(new EMCrawlerV1Criteria
                {
                    SecurityCode = secucode,
                    Industry = "房地产",
                    InitDate = initDate,
                    QuarterNums = quarterNums
                });
                taskList.Add(taskFactory.StartNew(em.CrawlFinReportData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }*/

            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财三张财务报表数据完毕");
        }
    }
}
