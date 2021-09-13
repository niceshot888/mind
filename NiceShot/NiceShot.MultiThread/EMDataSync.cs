using Newtonsoft.Json;
using NiceShot.MultiThread.Helper;
using NiceShot.MultiThread.JsonObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.MultiThread
{
    public class EMDataSync
    {
        private string symbol_fmt_pre = "";//SZ000961格式
        private string em_mainindex_url = "http://emweb.securities.eastmoney.com/NewFinanceAnalysis/MainTargetAjax?type=1&code={0}";

        public EMDataSync(string symbol)
        {
            this.symbol_fmt_pre = symbol.Substring(7)+symbol.Substring(0,6);
        }

        public void SyncMainIndexData()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            //将000961.SZ转换成SZ000961
            
            string stock_em_mainindex_url = string.Format(em_mainindex_url, symbol_fmt_pre);

            Console.WriteLine("开始同步数据:" + stock_em_mainindex_url);
            
            string json_str = HttpHelper.DownloadJson(stock_em_mainindex_url);
            List<em_mainindex_jo> jolist = JsonConvert.DeserializeObject<List<em_mainindex_jo>>(json_str);
            foreach(var jo in jolist)
            {
                Console.WriteLine(jo.date);
            }
            
            //Console.WriteLine(jo.date+","+ "," + jo.kfjlr);
        }
    }
}
