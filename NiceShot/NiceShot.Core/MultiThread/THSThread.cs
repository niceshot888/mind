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
    public class THSThread
    {
        public static void SyncThsCashflowAdditionalData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = NiceShotDataAccess.GetXQNormalStockList(MarketType.A);
            foreach (var stock in stockList)
            {
                THSCrawler ths = new THSCrawler(new THSCrawlerCriteria { SecurityCode = stock.ts_code });
                taskList.Add(taskFactory.StartNew(ths.CrawlCashflowAdditionalData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取同花顺现金流量表补充资料完毕");
        }
    }
}
