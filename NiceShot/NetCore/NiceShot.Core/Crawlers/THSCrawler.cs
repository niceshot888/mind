using Newtonsoft.Json;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.JsonObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NiceShot.Core.Crawlers
{
    public class THSCrawler
    {
        private string symbol = "";
        private string securitycode = "";

        private string ths_cashflow_url = "http://basic.10jqka.com.cn/api/stock/finance/{0}_cash.json";
        private string ths_cookie = string.Empty;

        private object[] flashdata_titles;
        private object[] flashdata_report;
        public THSCrawler()
        {

        }

        public THSCrawler(THSCrawlerCriteria criteria)
        {
            this.securitycode = criteria.SecurityCode;
            this.symbol = criteria.SecurityCode.Substring(0, 6);
            this.ths_cookie = criteria.Cookie;
        }




        public void CrawlCashflowAdditionalData()
        {
            Logger.Info(string.Format("crawl ths cashflow additional data:{0}", string.Format(ths_cashflow_url, this.symbol)));

            string content = HttpHelper.DownloadUrl(string.Format(ths_cashflow_url, this.symbol), Encoding.UTF8, ths_cookie);

            content = content.Replace("\"{", "{").Replace("}\"", "}").Replace("\\\\", "\\");
            content = Regex.Unescape(content).Replace("\\", "");
            content = content.Replace("\"科目时间\"", "[\"科目时间\",\"元\",0,true,false]");

            if (content == "null" || content.Contains("parameter error"))
                return;
            var json = JsonHelper.JsonDeserialize<ths_cashflow_jo>(content);

            flashdata_titles = json.flashData.title;
            flashdata_report = json.flashData.report;
            object[] dates = (object[])flashdata_report[0];

            int asseimpa_idx = GetTitleIndex("加：资产减值准备");
            int assedepr_idx = GetTitleIndex("固定资产折旧、油气资产折耗、生产性生物资产折旧");
            int intaasseamor_idx = GetTitleIndex("无形资产摊销");
            int realestadep_idx = GetTitleIndex("投资性房地产折旧、摊销");
            int longdefeexpenamor_idx = GetTitleIndex("长期待摊费用摊销");
            int dispfixedassetloss_idx = GetTitleIndex("处置固定资产、无形资产和其他长期资产的损失");
            int fixedassescraloss_idx = GetTitleIndex("固定资产报废损失");
            int valuechgloss_idx = GetTitleIndex("公允价值变动损失");
            int inveredu_idx = GetTitleIndex("存货的减少");
            int receredu_idx = GetTitleIndex("经营性应收项目的减少");
            int payaincr_idx = GetTitleIndex("经营性应付项目的增加");

            int finexpe_idx = GetTitleIndex("财务费用");

            for (int date_idx = 0; date_idx < dates.Length; date_idx++)
            {
                StringBuilder sbSql = new StringBuilder();

                string date = dates[date_idx].ToString();
                if (asseimpa_idx > 0)
                {
                    object obj = ((object[])flashdata_report[asseimpa_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("asseimpa={0},", val);
                    }
                }
                if (assedepr_idx > 0)
                {
                    object obj = ((object[])flashdata_report[assedepr_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("assedepr={0},", val);
                    }
                }
                if (intaasseamor_idx > 0)
                {
                    object obj = ((object[])flashdata_report[intaasseamor_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("intaasseamor={0},", val);
                    }
                }
                if (realestadep_idx > 0)
                {
                    object obj = ((object[])flashdata_report[realestadep_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("realestadep={0},", val);
                    }
                }
                if (longdefeexpenamor_idx > 0)
                {
                    object obj = ((object[])flashdata_report[longdefeexpenamor_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("longdefeexpenamor={0},", val.ToString());
                    }
                }
                if (dispfixedassetloss_idx > 0)
                {
                    object obj = ((object[])flashdata_report[dispfixedassetloss_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("dispfixedassetloss={0},", val);
                    }
                }
                if (fixedassescraloss_idx > 0)
                {
                    object obj = ((object[])flashdata_report[fixedassescraloss_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("fixedassescraloss={0},", val);
                    }
                }
                if (valuechgloss_idx > 0)
                {
                    object obj = ((object[])flashdata_report[valuechgloss_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("valuechgloss={0},", val);
                    }
                }
                
                if (inveredu_idx > 0)
                {
                    object obj = ((object[])flashdata_report[inveredu_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("inveredu={0},", val);
                    }
                }
                if (receredu_idx > 0)
                {
                    object obj = ((object[])flashdata_report[receredu_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("receredu={0},", val);
                    }
                }
                if (payaincr_idx > 0)
                {
                    object obj = ((object[])flashdata_report[payaincr_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("payaincr={0},", val);
                    }
                }
                if (finexpe_idx > 0)
                {
                    object obj = ((object[])flashdata_report[finexpe_idx])[date_idx];
                    if (!(obj is bool))
                    {
                        decimal? val = StringUtils.ConvertToUnitYuan(obj.ToString());
                        sbSql.AppendFormat("finexpe={0},", val);
                    }
                }

                if (!string.IsNullOrEmpty(sbSql.ToString()))
                {
                    string updateSql = string.Format("UPDATE em_cashflow_common SET {0} WHERE SECURITYCODE='{1}' AND REPORTDATE='{2}';", sbSql.ToString().TrimEnd(','), this.securitycode, date);

                    THSDataAccess.OperateThsCashflowAdditionalData(updateSql);
                }

            }
        }

        private int GetTitleIndex(string titlename)
        {
            int title_idx = 0;
            foreach (var title in flashdata_titles)
            {
                string first_title = ((object[])title)[0].ToString();
                if (first_title == titlename)
                {
                    title_idx = Array.IndexOf(flashdata_titles, title);
                    break;
                }
            }

            return title_idx;
        }

    }
}
