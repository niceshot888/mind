using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace NiceShot.Core.Helper
{
    public class HttpHelper
    {
        public static string DownloadJson(string url)
        {
            return DownloadUrl(url, Encoding.UTF8);
        }

        public static string DownloadUrl(string url, Encoding encode, string headers_cookie = "", string content="")
        {
            string html = "";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 30 * 1000;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36";
            request.ContentType = "application/json;charset=utf-8";

            if (!string.IsNullOrEmpty(content))
            {
                byte[] bs = Encoding.UTF8.GetBytes(content);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bs.Length;

                using (var reqStream = request.GetRequestStream()) {
                    reqStream.Write(bs, 0, bs.Length);
                }
            }

            if (!string.IsNullOrEmpty(headers_cookie))
            {
                request.Headers.Add("Cookie", headers_cookie);
            }

            request.CookieContainer = new CookieContainer();
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Logger.Error(string.Format("抓取{0}地址返回失败,response.StatusCode为{1}", url, response.StatusCode));
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
                        Logger.Error(string.Format("读取地址{0}失败，详细信息：{1}", url, ex.Message));
                    }
                }
            }

            return html;
        }


        #region test

        private static string cookiestr = "";
        public static string SendRequest(string url, Encoding encoding)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            webRequest.CookieContainer = new CookieContainer();
            //webRequest.Headers.Add("Cookie", "JSESSIONID=AFA73CF257CDD40D6AFB83CF1BA6F3D4; _USER_NAME=21010037");
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            //cookie.Add(webResponse.Cookies);
            cookiestr = webResponse.Headers.Get("Set-Cookie");//获取Cookie
            StreamReader sr = new StreamReader(webResponse.GetResponseStream(), encoding);
            string str = sr.ReadToEnd();
            Logger.Info(str);
            return str;
        }

        public static string SendRequest1(string url, Encoding encoding)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            //webRequest.CookieContainer = cookie;
            webRequest.Headers.Add("Cookie", cookiestr);//携带Cookie
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream(), encoding);
            string str = sr.ReadToEnd();
            Logger.Info(str);
            return str;
        }

        #endregion
    }
}
