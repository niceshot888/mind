using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Newtonsoft.Json;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NiceShot.Core.Crawlers
{
    public class XQCrawler
    {
        private string xqsymbol = string.Empty;//SZ000961格式
        private string securitycode = string.Empty;//000961.SZ格式
        private string keywords = string.Empty;
        private TopHolderType top_holders_type;
        //private string stockname = "";

        private int page = 1;//分页
        private string xq_a_stock_list_url = "https://xueqiu.com/service/v5/stock/screener/quote/list?page={0}&size=90&order=desc&orderby=percent&order_by=percent&market=CN&type=sh_sz";
        private string xq_hk_stock_list_url = "https://xueqiu.com/service/v5/stock/screener/quote/list?page={0}&size=90&order=desc&orderby=percent&order_by=percent&market=HK&type=hk&is_delay=true";
        private string xq_fin_balance_url = "https://stock.xueqiu.com/v5/stock/finance/cn/balance.json?symbol={0}&type=all&is_detail=true&count=10";

        private string xq_kline_url = "https://stock.xueqiu.com/v5/stock/chart/kline.json?symbol={0}&begin={1}&period=day&type=before&count=-1&indicator=kline,macd,cci,pb";
        private string xq_quote_url = "https://stock.xueqiu.com/v5/stock/quote.json?symbol={0}&extend=detail";
        private string xq_fin_indicator_url = "https://stock.xueqiu.com/v5/stock/finance/cn/indicator.json?symbol={0}&type=all&is_detail=true&count=2";
        private string xq_top_holders_url = "https://stock.xueqiu.com/v5/stock/f10/cn/top_holders.json?symbol={0}&circula={1}";
        private string xq_search_url = "https://xueqiu.com/query/v1/user/status/search?q={0}&page={1}&uid=4357540281&sort=time&comment=0";
        private string xq_v5_cookie = string.Empty;
        private DateTime latest_sync_date = new DateTime(2020, 10, 30);

        public XQCrawler()
        {
        }

        public XQCrawler(XQCrawlerCriteria criteria)
        {
            this.page = criteria.Page;
            if (criteria.MarketType != MarketType.HK)
                this.xqsymbol = StringUtils.ConvertEMToXQSymbol(criteria.SecurityCode);
            this.securitycode = criteria.SecurityCode;
            this.xq_v5_cookie = criteria.Cookie;
            this.keywords = criteria.Keywords;

            this.top_holders_type = criteria.TopHolderType;
        }

        public void CrawlXQFinBalanceData()
        {
            Logger.Info(string.Format("crawl xueqiu finance balancesheet data:{0}", string.Format(xq_fin_balance_url, this.xqsymbol)));

            string json_str = HttpHelper.DownloadUrl(string.Format(xq_fin_balance_url, this.xqsymbol), Encoding.UTF8, xq_v5_cookie);
            xq_balancesheet_common_jo jo = JsonConvert.DeserializeObject<xq_balancesheet_common_jo>(json_str);
            if (jo == null || jo.data == null || jo.data.list == null)
                return;
            var itemList = jo.data.list;
            foreach (var item in itemList)
            {
                var date = item.report_date.ToLocalTimeDateByMilliseconds().GetValueOrDefault();
                if (DateTime.Compare(date, latest_sync_date) < 0)
                    break;

                XQDataAccess.OperateBalanceSheetData(this.securitycode, item);
            }
        }

        public void CrawlXQAStockListData()
        {
            Logger.Info(string.Format("crawl xueqiu stocklist data:{0}", string.Format(xq_a_stock_list_url, this.page)));

            string json_str = HttpHelper.DownloadJson(string.Format(xq_a_stock_list_url, this.page));
            xq_stock_list_jo stocks = JsonConvert.DeserializeObject<xq_stock_list_jo>(json_str);
            if (stocks == null || stocks.data == null)
                return;

            foreach (var stockJo in stocks.data.list)
            {
                XQDataAccess.OperateXQAStock(stockJo);
            }
        }
        public int GetXQAStockListTotalPages()
        {
            string json_str = HttpHelper.DownloadJson(string.Format(xq_a_stock_list_url, 1));
            xq_stock_list_jo stocks = JsonConvert.DeserializeObject<xq_stock_list_jo>(json_str);

            int totalNum = stocks.data.count;
            int pages = (int)Math.Ceiling((decimal)totalNum / 90);

            return pages;
        }

        /// <summary>
        /// 抓取港股通交易类型
        /// </summary>
        public void CrawlXQHKStockTradeType()
        {
            Logger.Info(string.Format("crawl xueqiu hk stocks trade type:{0}", string.Format(xq_quote_url, this.page)));

            string json_str = HttpHelper.DownloadUrl(string.Format(xq_quote_url, this.securitycode), Encoding.UTF8, xq_v5_cookie);
            xq_quote_jo quote = JsonConvert.DeserializeObject<xq_quote_jo>(json_str);
            if (quote == null || quote.data == null || quote.data.tags == null || quote.data.tags.Count == 0)
                return;

            var tradeType = quote.data.tags[0].description;
            XQDataAccess.OperateXQHKTradeType(this.securitycode, tradeType);
        }

        public void CrawlXQHKStockListData()
        {
            Logger.Info(string.Format("crawl xueqiu stocklist data:{0}", string.Format(xq_a_stock_list_url, this.page)));

            string json_str = HttpHelper.DownloadJson(string.Format(xq_hk_stock_list_url, this.page));
            xq_stock_list_jo stocks = JsonConvert.DeserializeObject<xq_stock_list_jo>(json_str);
            if (stocks == null || stocks.data == null)
                return;

            foreach (var stockJo in stocks.data.list)
            {
                XQDataAccess.OperateXQHKStock(stockJo);
            }
        }

        public int GetXQHKStockListTotalPages()
        {
            string json_str = HttpHelper.DownloadJson(string.Format(xq_hk_stock_list_url, 1));
            xq_stock_list_jo stocks = JsonConvert.DeserializeObject<xq_stock_list_jo>(json_str);

            int totalNum = stocks.data.count;
            int pages = (int)Math.Ceiling((decimal)totalNum / 90);

            return pages;
        }

        /// <summary>
        /// 抓取a主要财务数据
        /// </summary>
        public void CrawlAMainIndexData()
        {
            //抓取季报数据
            Logger.Info(string.Format("抓取雪球主要财务指标数据:{0}", xqsymbol));

            var json_str = HttpHelper.DownloadUrl(string.Format(xq_fin_indicator_url, xqsymbol), Encoding.UTF8, xq_v5_cookie);
            var indicator = JsonConvert.DeserializeObject<xq_fin_indicator_jo>(json_str);
            if (indicator == null || indicator.data == null)
                return;
            foreach (var jo in indicator.data.list)
            {
                var date = jo.report_date.ToLocalTimeDateByMilliseconds().GetValueOrDefault();
                if (DateTime.Compare(date, latest_sync_date) < 0)
                    break;
                XQDataAccess.OperateAMainIndexData(securitycode, jo);
            }
        }

        /// <summary>
        /// 抓取MACD数据
        /// </summary>
        public void CrawlMacdData()
        {
            Logger.Info(string.Format("抓取雪球MACD数据:{0}", string.Format(xq_kline_url, xqsymbol, DateTime.Now.ToUnixTimestampByMilliseconds())));

            var json_str = HttpHelper.DownloadUrl(string.Format(xq_kline_url, xqsymbol, DateTime.Now.ToUnixTimestampByMilliseconds()), Encoding.UTF8, xq_v5_cookie);
            if (!string.IsNullOrEmpty(json_str))
            {
                var data = JsonConvert.DeserializeObject<xq_macd_jo>(json_str);
                if (data != null && data.data != null)
                {
                    var item = data.data.item[0];
                    XQDataAccess.OperateMacdData(securitycode, item[12], item[13], item[14], item[15], item[16]);
                }
            }
        }

        public void CrawlTopHoldersData()
        {
            //抓取季报数据
            Logger.Info(string.Format("抓取雪球十大股东数据:{0}", string.Format(xq_top_holders_url, xqsymbol, (int)top_holders_type)));

            var json_str = HttpHelper.DownloadUrl(string.Format(xq_top_holders_url, xqsymbol, (int)top_holders_type), Encoding.UTF8, xq_v5_cookie);
            if (!string.IsNullOrEmpty(json_str))
                XQDataAccess.OperateTopHoldersData(top_holders_type, securitycode, json_str);
        }

        public void CrawlForeignDebt()
        {
            //抓取季报数据
            Logger.Info(string.Format("抓取外债数据:{0}", securitycode));

            var json_str = HttpHelper.DownloadUrl(string.Format(xq_search_url, keywords, 1), Encoding.UTF8, xq_v5_cookie);
            var datalist = JsonConvert.DeserializeObject<xq_search_list>(json_str);
            if (datalist == null || datalist.list == null)
                return;
            foreach (var jo in datalist.list)
            {
                var date = StringUtils.ToLocalTimeDateByMilliseconds(jo.created_at);
                if (DateTime.Compare(date.GetValueOrDefault(), DateTime.Parse("2021-1-1")) <= 0)
                    continue;

                var title = jo.title.Replace("<span class='highlight'>", "").Replace("</span>", "");

                if (title.Contains(string.Format("{0}({1})发行", keywords, securitycode)))
                {
                    //Console.WriteLine(date.ToShortDateString() + jo.text);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(jo.text);
                    var p_list = doc.QuerySelectorAll("p");

                    var p_text_list = new List<string>();
                    foreach (var p in p_list)
                    {
                        var p_text = p.InnerText.Replace("&nbsp;", "");
                        p_text_list.Add(p_text);

                        XQDataAccess.OperateForeighDebt(date.GetValueOrDefault(), securitycode, title, jo.text, p_text_list);
                    }

                    break;
                }
            }
        }


    }
}
