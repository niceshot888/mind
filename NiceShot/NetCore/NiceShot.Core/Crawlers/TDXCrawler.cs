using NiceShot.Core.DataAccess;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using System.Text;

namespace NiceShot.Core.Crawlers
{
    public class TDXCrawler
    {
        private string tdx_secuinfo_url = "http://page.tdx.com.cn:7615/TQLEX?Entry=CWServ.SecuInfo";
        private string simple_code = "";
        private string securitycode = "";
        public TDXCrawler(string ts_code)
        {
            simple_code = ts_code.Substring(0,6);
            securitycode = ts_code;
        }

        public void CrawlForeRoeAndNP()
        {
            Logger.Info(string.Format("craw tdx fore roe and net profit:{0}", securitycode));

            //{"CallName":"f10_gg_jzfx","Params":["601318","ylyctj"]}
            //{"CallName":"f10_gg_jzfx","Params":["000002","ylyctj"]}
            //{"CallName":"f10_gg_jzfx","Params":["601318","ylyctj"]}
            var jsonResult = HttpHelper.DownloadUrl(tdx_secuinfo_url, Encoding.UTF8, "{\"CallName\":\"f10_gg_jzfx\",\"Params\":[\"" + simple_code + "\",\"ylyctj\"]}");

            var dr = JsonHelper.JsonDeserialize<TdxDataResult>(jsonResult);
            if (jsonResult == null)
                return;

            var resultSets = dr.ResultSets;

            if (resultSets == null || resultSets.Count == 0)
                return;

            var rs = resultSets[0];
            var dataArr = rs.Content[0];

            if (dataArr == null)
                return;

            //TDXDataAccess.UpdateForeRoeAndNP(this.securitycode, dataArr);
        }

        public void CrawlStockIndustryName()
        {
            Logger.Info(string.Format("抓取通达信股票所属行业:{0}", securitycode));

            var jsonResult = HttpHelper.DownloadUrl(tdx_secuinfo_url, Encoding.UTF8, "{\"CallName\":\"f10_gg_gsgk\",\"Params\":[\"0\",\"" + simple_code + "\",\"\"]}");

            var tdxCompanyObj = JsonHelper.JsonDeserialize<TdxDataResult>(jsonResult);
            if (jsonResult == null)
                return;

            var resultSets = tdxCompanyObj.ResultSets;
            var data = resultSets[0];
            var content = data.Content;

            if (content == null)
                return;

            string tdxIndustry = content[0][37];//通达信行业
            string zjhIndustry = content[0][38];//证监会行业

            TDXDataAccess.UpdateStockIndustryName(this.securitycode, tdxIndustry, zjhIndustry);
        }

    }
}
