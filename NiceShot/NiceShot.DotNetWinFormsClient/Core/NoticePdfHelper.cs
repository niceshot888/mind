using Newtonsoft.Json;
using NiceShot.DotNetWinFormsClient.JsonObjects;
using System;
using System.Diagnostics;
using System.Linq;

namespace NiceShot.DotNetWinFormsClient.Core
{
    public class NoticePdfHelper
    {
        public static string em_notices_url = "http://np-anotice-stock.eastmoney.com/api/security/ann?sr=-1&page_size=500&page_index=1&ann_type=A&client_source=web&stock_list={0}&f_node=1&s_node=1";

        public static string GetReportTitleKeywords(DateTime date)
        {
            var keywords = string.Empty;
            if (date.Month == 3)
                keywords = string.Format("{0}年第一季度报告全文,{0}年第一季度报告,{0}年第一季度季报", date.Year);
            else if (date.Month == 6)
                keywords = string.Format("{0}年半年度报告,{0}年半年度报告,{0}年半年报", date.Year);
            else if (date.Month == 9)
                keywords = string.Format("{0}年第三季度报告全文,{0}年第三季度报告,{0}年第三季度季报", date.Year);
            else if (date.Month == 12)
                keywords = string.Format("{0}年年度报告,{0}年度报告,{0}年年报", date.Year);

            return keywords;
        }

        public static void ShowPdfViewer(string jsonStr, DateTime date)
        {
            var noticelist = JsonConvert.DeserializeObject<em_notice_list_jo>(jsonStr);
            var keywords = GetReportTitleKeywords(date);

            em_notice_data_item notice;
            var keywords_arr = keywords.Split(',');
            if (keywords_arr.Length > 1)
                notice = GetNoticeDataItem(keywords_arr, noticelist); 
            else
                notice = noticelist.data.list.FirstOrDefault(s => s.title.Contains(keywords) && !s.title.Contains("摘要") && !s.title.Contains("英文") && !s.title.Contains("报告及") && !s.title.Contains("报告正文") && !s.title.Contains("报告附件"));
            if (notice != null)
            {
                var link = string.Format("https://pdf.dfcfw.com/pdf/H2_{0}_1.PDF", notice.art_code);

                var p = new Process();
                p.StartInfo.FileName = link;
                p.Start();

                /*var wb = new ChromeWebBrowser(new ChromeWebBrowserCriteria
                {
                    Width = 1300,
                    Height = 750,
                    IsExternalLink = true,
                    Url = link
                });
                wb.Text = stockName + "-" + keywords_arr[0];
                wb.Show();*/
            }
        }

        public static em_notice_data_item GetNoticeDataItem(string[] keywords_arr, em_notice_list_jo noticelist)
        {
            foreach (var keywords in keywords_arr)
            {
                var notice = noticelist.data.list.FirstOrDefault(s => s.title.Contains(keywords) && !s.title.Contains("摘要") && !s.title.Contains("英文") && !s.title.Contains("报告及") && !s.title.Contains("报告正文") && !s.title.Contains("报告附件"));
                if (notice != null)
                    return notice;
            }
            return null;
        }
    }
}
