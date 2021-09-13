using NiceShot.MultiThread.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NiceShot.MultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            TheadMethod2();
             //EmDataAccess.get_a_stock_list();
        }

        public static void TheadMethod2()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Task> taskList = new List<Task>();

            var stockList = EmDataAccess.get_a_stock_list();
            foreach (var stock in stockList)
            {
                EMDataSync em = new EMDataSync(stock.ts_code);
                taskList.Add(taskFactory.StartNew(em.SyncMainIndexData));
                if (taskList.Count >= 10)
                {
                    Task.WaitAny(taskList.ToArray());
                    taskList = taskList.Where(t => !t.IsCanceled && !t.IsCompleted && !t.IsFaulted).ToList();
                }
            }
            Task.WaitAll(taskList.ToArray());
            Console.WriteLine("数据全部抓取完毕");
        }

        public static void TheadMethod1()
        {
            TaskFactory taskFactory = Task.Factory;
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 10000; i++)
            {
                EMDataSync em = new EMDataSync("symbol-" + i);
                actions.Add(new Action(em.SyncMainIndexData));
            }
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 10;
            Parallel.Invoke(options, actions.ToArray());
        }

    }
}
