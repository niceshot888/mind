using Newtonsoft.Json;
using NiceShot.Core.Criterias;
using NiceShot.Core.DataAccess;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.Core.Models.JsonObjects;
using NiceShot.Core.Models.Structs;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace NiceShot.Core.Crawlers
{
    public class EMCrawlerV1
    {
        private string xqsymbol = "";//SZ000961格式
        private string securitycode = "";//000961.SZ格式
        private string industry = "";
        //private ReportType reporttype;
        private CompanyType companytype;
        private MarketType marketType;
        private DateTime latest_sync_date = new DateTime(2020, 10, 30);
        //private DateTime latest_sync_date = new DateTime(1970, 10, 30);
        private string initDate;
        private int quarterNums;
        private bool includeUpdate;
        public EMCrawlerV1()
        {

        }

        public EMCrawlerV1(EMCrawlerV1Criteria criteria)
        {
            this.securitycode = criteria.SecurityCode;

            if (marketType == MarketType.A)
                this.xqsymbol = StringUtils.ConvertEMToXQSymbol(criteria.SecurityCode);
            else
                this.xqsymbol = criteria.SecurityCode;

            this.industry = string.IsNullOrEmpty(criteria.Industry) ? "区域地产" : criteria.Industry;
            this.marketType = criteria.MarketType;

            this.initDate = criteria.InitDate;
            this.quarterNums = criteria.QuarterNums;
            this.includeUpdate = criteria.IncludeUpdate;
        }

        #region 抓取三张财务报表

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initDates">格式为：2020-12-31,2020-09-30,2020-06-30,2020-03-31,2019-12-31</param>
        public void CrawlFinReportData()
        {
            var initDates = UtilsHelper.GetConsecutiveQuarterDatesIncludeSelf(DateTime.Parse(initDate), quarterNums);

            SyncReportDataByDates(initDates);
            //SyncReportDataByDates(ReportType.Income, initDates);
            //SyncReportDataByDates(ReportType.Cashflow, initDates);
        }

        private void SyncReportDataByDates(string initDates)
        {
            companytype = industry.ConvertToCompanyType();

            var returnDate = SyncReportDataBySpecDates(ReportType.BalanceSheet, initDates);
            while (returnDate.HasValue)
            {
                if (returnDate <= this.latest_sync_date)
                    break;

                //同步其他两张表
                SyncReportDataBySpecDates(ReportType.Income, initDates);
                SyncReportDataBySpecDates(ReportType.Cashflow, initDates);

                //根据returnDate获取specDates
                var specdates = UtilsHelper.GetConsecutiveQuarterDatesNotIncludeSelf(returnDate.Value, quarterNums);
                returnDate = SyncReportDataBySpecDates(ReportType.BalanceSheet, specdates);
            }
        }

        private DateTime? SyncReportDataBySpecDates(ReportType reportType, string specdates)
        {
            Logger.Info(string.Format("同步{0}的{1}数据", this.securitycode, reportType.ToReportTypeName()));

            DateTime? lastesReportDate = null;

            string report_api_url = string.Format(GetEMFinReportApiUrl(reportType), (int)companytype, specdates, this.xqsymbol);

            string content = HttpHelper.DownloadJson(report_api_url);
            if (!string.IsNullOrEmpty(content))
                content = content.Replace("\\", "").Trim('"');

            if (reportType == ReportType.BalanceSheet)
            {
                var jsonList = JsonConvert.DeserializeObject<em_bal_common_v1_jo_list>(content);
                if (jsonList == null || jsonList.data == null)
                    return null;

                foreach (var jo in jsonList.data)
                {
                    var date = jo.report_date.ConvertToDate().GetValueOrDefault();
                    if (DateTime.Compare(date, latest_sync_date) < 0)
                        break;
                    var rs = EMDataAccessV1.oper_em_bal_common_v1_data(jo, this.includeUpdate);

                    lastesReportDate = rs && !includeUpdate ? null : jo.report_date.ConvertToDate();
                }
            }
            else if (reportType == ReportType.Income)
            {
                var jsonList = JsonConvert.DeserializeObject<em_inc_common_v1_jo_list>(content);
                if (jsonList == null || jsonList.data == null)
                    return null;

                foreach (var jo in jsonList.data)
                {
                    var date = jo.report_date.ConvertToDate().GetValueOrDefault();
                    if (DateTime.Compare(date, latest_sync_date) < 0)
                        break;
                    var rs = EMDataAccessV1.oper_em_inc_common_v1_data(jo, this.includeUpdate);

                    lastesReportDate = rs && !includeUpdate ? null : jo.report_date.ConvertToDate();
                }
            }
            else if (reportType == ReportType.Cashflow)
            {
                var jsonList = JsonConvert.DeserializeObject<em_cf_common_v1_jo_list>(content);
                if (jsonList == null || jsonList.data == null)
                    return null;

                foreach (var jo in jsonList.data)
                {
                    var date = jo.report_date.ConvertToDate().GetValueOrDefault();
                    if (DateTime.Compare(date, latest_sync_date) < 0)
                        break;
                    var rs = EMDataAccessV1.oper_em_cf_common_v1_data(jo, this.includeUpdate);

                    lastesReportDate = rs && !includeUpdate ? null : jo.report_date.ConvertToDate();
                }
            }

            return lastesReportDate;
        }

        private string GetEMFinReportApiUrl(ReportType reportType)
        {
            string apiurl = "";
            if (reportType == ReportType.BalanceSheet)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/zcfzbAjaxNew?companyType={0}&reportDateType=0&reportType=1&dates={1}&code={2}";
            else if (reportType == ReportType.Income)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/lrbAjaxNew?companyType={0}&reportDateType=0&reportType=1&dates={1}&code={2}";
            else if (reportType == ReportType.Cashflow)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/xjllbAjaxNew?companyType={0}&reportDateType=0&reportType=1&dates={1}&code={2}";
            return apiurl;
        }

        #endregion
    }
}
