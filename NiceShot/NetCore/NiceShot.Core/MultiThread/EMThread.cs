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
        public static void SyncEMAMainIndexData(bool includeUpdate)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code, IncludeUpdate = includeUpdate });
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
        /// 同步东财主营构成
        /// </summary>
        public static void SyncEMMajorBusinessScopeData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                var em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlMajorBusinessData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财主营构成数据完毕");
        }

        /// <summary>
        /// 同步核心题材
        /// </summary>
        public static void SyncEMCoreConceptData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                var em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlCoreConceptData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("同步核心题材数据完毕");
        }

        /// <summary>
        /// 同步港股公司概况信息
        /// </summary>
        public static void SyncEMHKCompanyProfile(MarketType marketType)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(marketType);
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
        public static void SyncEMACompanySurveyData(MarketType marketType)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(marketType);
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
        /// 同步A股公司陆股通数据
        /// </summary>
        public static void SyncEMALuGuTongData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(em.CrawlLuGuTongData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取东财A股公司陆股通数据完毕");
        }

        /// <summary>
        /// 同步东财三张财务报表数据
        /// </summary>
        public static void SyncFinIndustryData(bool includeUpdate)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetFinanceIndustriesStockList();
            foreach (var stock in stockList)
            {
                EMCrawler em = new EMCrawler(new EMCrawlerCriteria { SecurityCode = stock.ts_code, Industry = stock.sshy, IncludeUpdate = includeUpdate });
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
