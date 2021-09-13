using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceShot.Core.MultiThread
{
    public class EMThread
    {
        /// <summary>
        /// 同步东财HK股主要财务指标
        /// </summary>
        public static void SyncEMHKMainIndexData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.HK);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlHKMainIndexData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财hk主要财务指标数据完毕");
        }

        /// <summary>
        /// 同步东财A股主要财务指标
        /// </summary>
        public static void SyncEMAMainIndexData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlAMainIndexData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财a主要财务指标数据完毕");
        }

        /// <summary>
        /// 同步港股公司概况信息
        /// </summary>
        public static void SyncEMHKCompanyProfile()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.HK);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlHKCompanyProfile));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财A股公司概况数据完毕");
        }

        /// <summary>
        /// 同步A股公司概况信息
        /// </summary>
        public static void SyncEMACompanySurveyData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlACompanySurveyData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财A股公司概况数据完毕");
        }

        /// <summary>
        /// 同步东财三张财务报表数据
        /// </summary>
        public static void SyncEMThreeReportData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code, Industry = stock.tdxindustry });
                taskList.Add(taskFactory.StartNew(em.CrawlFinIndustryData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财三张财务报表数据完毕");
        }

    }
}
