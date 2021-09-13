using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NiceShot.MultiThread.Helper
{
    public static class HttpHelper
    {
        public static string DownloadJson(string url)
        {
            return DownloadUrl(url,Encoding.UTF8);
        }

        public static string DownloadUrl(string url, Encoding encode)
        {
            string html = "";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 30 * 1000;//设置30秒超时
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36";
            request.ContentType = "application/json;charset=utf-8";

            request.CookieContainer = new CookieContainer();
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine(string.Format("抓取{0}地址返回失败,response.StatusCode为{1}", url, response.StatusCode));
                }
                else
                {
                    try
                    {
                        StreamReader sr = new StreamReader(response.GetResponseStream(), encode);
                        html = sr.ReadToEnd();
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("读取地址{0}失败，详细信息：{1}", url, ex.Message));
                    }
                }
            }

            return html;
        }
    }
}
