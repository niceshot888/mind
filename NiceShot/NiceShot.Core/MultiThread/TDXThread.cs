using NiceShot.Core.Crawlers;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceShot.Core.MultiThread
{
    public class TDXThread
    {
        public static void SyncStockIndustryName()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                TDXCrawler tdx = new TDXCrawler(stock.ts_code);
                taskList.Add(taskFactory.StartNew(tdx.CrawlStockIndustryName));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取通达信行业数据完毕");
        }

        public static void SyncForeRoeAndNP()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                TDXCrawler tdx = new TDXCrawler(stock.ts_code);
                taskList.Add(taskFactory.StartNew(tdx.CrawlForeRoeAndNP));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取通达信未来业绩预期数据完毕");
        }

    }
}
