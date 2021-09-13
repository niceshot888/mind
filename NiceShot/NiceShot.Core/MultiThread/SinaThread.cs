using Newtonsoft.Json;
using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceShot.Core.MultiThread
{
    public class SinaThread
    {
        private static string sina_bond_url = "http://vip.stock.finance.sina.com.cn/quotes_service/api/json_v2.php/Market_Center.getHQNodeDataSimple?page=1&num=400000&sort=pricechange&asc=1&node=hs_z&_s_r_a=page";

        public static void SyncSinaBondData()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            string json_str = HttpHelper.DownloadJson(sina_bond_url);
            var jolist = JsonConvert.DeserializeObject<List<sina_bond_jo>>(json_str);

            foreach (var jo in jolist)
            {
                SinaCrawler em = new SinaCrawler(jo);
                taskList.Add(taskFactory.StartNew(em.CrawlSinaBondData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());

            Logger.Info("抓取SINA BOND数据完毕");
        }
    }
}
