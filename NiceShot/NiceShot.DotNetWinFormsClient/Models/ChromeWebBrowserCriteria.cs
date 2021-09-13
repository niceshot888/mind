namespace NiceShot.DotNetWinFormsClient.Models
{
    public class ChromeWebBrowserCriteria
    {
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 窗体高度
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 网页链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否为外部连接
        /// </summary>
        public bool IsExternalLink { get; set; }

        public string Content { get; set; }
    }
}
