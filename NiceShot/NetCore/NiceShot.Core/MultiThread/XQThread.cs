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
    public class XQThread
    {

        public static void SyncMacdData(string cookie)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = stock.ts_code, Cookie = cookie });
                taskList.Add(taskFactory.StartNew(em.CrawlMacdData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球十大股东数据完毕");
        }

        public static void SyncXQTopHoldersData(string cookie, TopHolderType topHolderType)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = stock.ts_code, Cookie = cookie, TopHolderType = topHolderType });
                taskList.Add(taskFactory.StartNew(em.CrawlTopHoldersData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球十大股东数据完毕");
        }

        /// <summary>
        /// 同步雪球A股主要财务指标
        /// </summary>
        public static void SyncXQAMainIndexData(string cookie,string nodata_date)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQNoDataXQStockListByDate(MarketType.A, nodata_date);
            foreach (var stock in stockList)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = stock.ts_code, Cookie = cookie });
                taskList.Add(taskFactory.StartNew(em.CrawlAMainIndexData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球a主要财务指标数据完毕");
        }

        public static void SyncXQForeignDebtData(string cookie)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var dataList = XQDataAccess.GetForeighDebtList();
            foreach (var data in dataList)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = data.code, Cookie = cookie, Keywords = data.name });
                taskList.Add(taskFactory.StartNew(em.CrawlForeignDebt));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球搜索数据完毕");
        }

        public static void SyncXQFinBalanceSheetData(string cookie)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQANormalStockList();
            foreach (var stock in stockList)
            {
                XQCrawler xq = new XQCrawler(new XQCrawlerCriteria { SecurityCode = stock.ts_code, Cookie = cookie });
                taskList.Add(taskFactory.StartNew(xq.CrawlXQFinBalanceData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球资产负债表数据完毕");
        }

        /// <summary>
        /// 同步A股数据
        /// </summary>
        public static void SyncXQAStockList()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            XQCrawler xq = new XQCrawler();

            var pages = xq.GetXQAStockListTotalPages();
            for (int page = 1; page <= pages; page++)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { Page = page });
                taskList.Add(taskFactory.StartNew(em.CrawlXQAStockListData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球A股股票列表数据完毕");
        }

        /// <summary>
        /// 同步港股数据
        /// </summary>
        public static void SyncXQHKStockList()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            XQCrawler xq = new XQCrawler();

            var pages = xq.GetXQHKStockListTotalPages();
            for (int page = 1; page <= pages; page++)
            {
                XQCrawler em = new XQCrawler(new XQCrawlerCriteria { Page = page });
                taskList.Add(taskFactory.StartNew(em.CrawlXQHKStockListData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球HK股股票列表数据完毕");
        }

        public static void SyncXQHKStockTradeType(string cookie)
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQANormalStockList();
            foreach (var stock in stockList)
            {
                XQCrawler xq = new XQCrawler(new XQCrawlerCriteria { SecurityCode = stock.ts_code, Cookie = cookie, MarketType = MarketType.HK });
                taskList.Add(taskFactory.StartNew(xq.CrawlXQHKStockTradeType));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取雪球HK股港股通交易类型完毕");
        }

    }
}
