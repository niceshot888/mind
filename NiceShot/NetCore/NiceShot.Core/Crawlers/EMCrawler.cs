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
    public class EMCrawler
    {
        private string xqsymbol = "";//SZ000961格式
        private string securitycode = "";//000961.SZ格式
        private string industry = "";
        private bool includeUpdate;
        //private ReportType reporttype;
        private CompanyType companytype;
        private MarketType marketType;
        private DateTime latest_sync_date = new DateTime(2020, 10, 30);
        //private DateTime latest_sync_date = new DateTime(1990, 3, 31);

        private string em_hk_mainindex_url = "http://emweb.securities.eastmoney.com/PC_HKF10/NewFinancialAnalysis/GetZYZB?code={0}";

        private string em_mainindex_year_url = "http://emweb.securities.eastmoney.com/NewFinanceAnalysis/MainTargetAjax?type=1&code={0}";
        private string em_mainindex_latest_url = "http://emweb.securities.eastmoney.com/NewFinanceAnalysis/MainTargetAjax?type=0&code={0}";
        private string em_company_survey_url = "http://f10.eastmoney.com/CompanySurvey/CompanySurveyAjax?code={0}";

        private string em_main_biz_scope_url = "http://f10.eastmoney.com/BusinessAnalysis/BusinessAnalysisAjax?code={0}";
        private string em_core_concept_url = "http://f10.eastmoney.com/CoreConception/PageAjax?code={0}";

        private string em_hk_company_profile_url = "http://emweb.securities.eastmoney.com/PC_HKF10/CompanyProfile/PageAjax?code={0}";
        private string em_lugutong_url = "http://dcfm.eastmoney.com//em_mutisvcexpandinterface/api/js/get?type=HSGTHDSTA&token=70f12f2f4f091e459a279469fe49eca5&filter=(SCODE=%27{0}%27)&st=HDDATE&sr=-1&p=1&ps=50";

        public EMCrawler()
        {

        }

        public EMCrawler(EMCrawlerCriteria criteria)
        {
            this.securitycode = criteria.SecurityCode;

            if (marketType == MarketType.A)
                this.xqsymbol = StringUtils.ConvertEMToXQSymbol(criteria.SecurityCode);
            else
                this.xqsymbol = criteria.SecurityCode;

            this.industry = string.IsNullOrEmpty(criteria.Industry) ? "区域地产" : criteria.Industry;
            this.marketType = criteria.MarketType;
            this.includeUpdate = criteria.IncludeUpdate;
            //this.reporttype = criteria.ReportType;
        }

        #region 抓取三张财务报表数据

        public void CrawlFinIndustryData()
        {
            SyncReportDataByAllDate();
            //SyncReportDataByAllDate(ReportType.Income);
            //SyncReportDataByAllDate(ReportType.Cashflow);
        }

        private void SyncReportDataByAllDate()
        {
            //Logger.Info(string.Format("同比{0}的{1}数据", this.securitycode, reportType.ToReportTypeName()));

            companytype = industry.ConvertToCompanyType();

            DateTime? defaultDate = null;
            var specdate = SyncReportDataBySpecDate(ReportType.BalanceSheet, defaultDate);
            while (specdate.HasValue)
            {
                if (specdate <= this.latest_sync_date)
                    break;

                //SyncReportDataBySpecDate(ReportType.Income, specdate);
                //SyncReportDataBySpecDate(ReportType.Cashflow, specdate);

                SyncReportDataBySpecDate(ReportType.Income, defaultDate == null ? null : specdate);
                SyncReportDataBySpecDate(ReportType.Cashflow, defaultDate == null ? null : specdate);

                specdate = SyncReportDataBySpecDate(ReportType.BalanceSheet, specdate);
                defaultDate = specdate;
            }
        }

        private DateTime? SyncReportDataBySpecDate(ReportType reportType, DateTime? specdate)
        {
            Logger.Info(string.Format("同步{0}的{1}数据", this.securitycode, reportType.ToReportTypeName()));

            DateTime? lastesReportDate = null;

            string report_api_url = string.Format(GetEMReportApiUrl(reportType), (int)companytype, specdate, this.xqsymbol);

            string content = HttpHelper.DownloadJson(report_api_url);
            if (!string.IsNullOrEmpty(content))
                content = content.Replace("\\", "").Trim('"');

            if (companytype == CompanyType.Broker)
            {
                if (reportType == ReportType.BalanceSheet)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_balancesheet_broker_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_balancesheet_broker_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Income)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_income_broker_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_income_broker_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Cashflow)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_cashflow_broker_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_cashflow_broker_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
            }
            else if (companytype == CompanyType.Insurance)
            {
                if (reportType == ReportType.BalanceSheet)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_balancesheet_insurance_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_balancesheet_insurance_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Income)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_income_insurance_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_income_insurance_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Cashflow)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_cashflow_insurance_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_cashflow_insurance_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
            }
            else if (companytype == CompanyType.Bank)
            {
                if (reportType == ReportType.BalanceSheet)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_balancesheet_bank_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_balancesheet_bank_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Income)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_income_bank_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_income_bank_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Cashflow)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_cashflow_bank_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        var rs = EMDataAccess.oper_em_cashflow_bank_data(jo, includeUpdate);

                        lastesReportDate = rs && !includeUpdate ? null : jo.REPORTDATE.ConvertToDate();
                    }
                }
            }
            else if (companytype == CompanyType.Common)
            {
                if (reportType == ReportType.BalanceSheet)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_balancesheet_common_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        EMDataAccess.oper_em_balancesheet_common_data(jo);

                        lastesReportDate = jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Income)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_income_common_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        EMDataAccess.oper_em_income_common_data(jo);

                        lastesReportDate = jo.REPORTDATE.ConvertToDate();
                    }
                }
                else if (reportType == ReportType.Cashflow)
                {
                    var jsonList = JsonConvert.DeserializeObject<List<em_cashflow_common_jo>>(content);
                    if (jsonList == null)
                        return null;

                    foreach (var jo in jsonList)
                    {
                        var date = jo.REPORTDATE.ConvertToDate().GetValueOrDefault();
                        if (DateTime.Compare(date, latest_sync_date) < 0)
                            break;
                        EMDataAccess.oper_em_cashflow_common_data(jo);

                        lastesReportDate = jo.REPORTDATE.ConvertToDate();
                    }
                }
            }

            //Logger.Info(string.Format("上次同步日期：" + lastesReportDate.GetValueOrDefault().ToShortDateString()));

            return lastesReportDate;
        }

        private string GetEMReportApiUrl(ReportType reportType)
        {
            //http://f10.eastmoney.com/NewFinanceAnalysis/zcfzbAjax?companyType=1&reportDateType=0&reportType=1&endDate=2015-12-31&code=SZ000961
            string apiurl = "";
            if (reportType == ReportType.BalanceSheet)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/zcfzbAjax?companyType={0}&reportDateType=0&reportType=1&endDate={1}&code={2}";
            else if (reportType == ReportType.Income)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/lrbAjax?companyType={0}&reportDateType=0&reportType=1&endDate={1}&code={2}";
            else if (reportType == ReportType.Cashflow)
                apiurl = "http://f10.eastmoney.com/NewFinanceAnalysis/xjllbAjax?companyType={0}&reportDateType=0&reportType=1&endDate={1}&code={2}";
            return apiurl;
        }

        private string GetEMFinReportApiUrl_v1(ReportType reportType)
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

        #region 抓取公司概况信息

        /// <summary>
        /// 抓取港股公司概况信息
        /// </summary>
        public void CrawlHKCompanyProfile()
        {
            Logger.Info(string.Format("抓取EM 港股公司信息:{0}", string.Format(em_hk_company_profile_url, securitycode)));

            string json_str = HttpHelper.DownloadJson(string.Format(em_hk_company_profile_url, securitycode));
            em_hk_company_profile_jo jo = JsonConvert.DeserializeObject<em_hk_company_profile_jo>(json_str);
            if (jo == null || jo.gszl == null)
                return;

            EMDataAccess.OperateHKCompanySurveyData(securitycode, jo);
        }

        /// <summary>
        /// 抓取A股公司概况信息
        /// </summary>
        public void CrawlACompanySurveyData()
        {
            Logger.Info(string.Format("抓取EM A股公司信息:{0}", string.Format(em_company_survey_url, xqsymbol)));

            string json_str = HttpHelper.DownloadJson(string.Format(em_company_survey_url, xqsymbol));
            em_company_survey_jo jo = JsonConvert.DeserializeObject<em_company_survey_jo>(json_str);
            if (jo == null || jo.jbzl == null)
                return;

            EMDataAccess.OperateACompanySurveyData(jo);
        }

        #endregion

        #region 抓取资金数据

        /// <summary>
        /// 抓取A股公司陆股通资金数据
        /// </summary>
        public void CrawlLuGuTongData()
        {
            Logger.Info(string.Format("抓取A股公司陆股通资金数据:{0}", string.Format(em_lugutong_url, xqsymbol)));

            string json_str = HttpHelper.DownloadJson(string.Format(em_lugutong_url, this.securitycode.Substring(0, 6)));
            var jo = JsonConvert.DeserializeObject<List<em_lugutong_jo>>(json_str);
            if (jo == null || jo.Count == 0)
                return;

            EMDataAccess.OperateLuGuTongZBData(this.securitycode, jo[0]);
        }

        #endregion

        #region 抓取主要财务数据

        /// <summary>
        /// 抓取hk主要财务数据
        /// </summary>
        public void CrawlHKMainIndexData()
        {
            //抓取季报数据
            Logger.Info(string.Format("抓取EM HK主要财务指标数据:{0}", string.Format(em_hk_mainindex_url, securitycode)));

            var json_str = HttpHelper.DownloadJson(string.Format(em_hk_mainindex_url, securitycode));

            var jsonObj = JsonConvert.DeserializeObject<em_hk_mainindex_jo>(json_str);
            if (jsonObj.status == -1)
                return;

            //季报
            int count = 0;
            foreach (var jo in jsonObj.data.zyzb_abgq)
            {
                count++;
                if (count == 1)
                    continue;
                var jo_item = new em_hk_mainindex_item_jo()
                {
                    yyzsr = jo[8],
                    gsjlr = jo[10],
                    kfjlr = "",
                    yyzsrtbzz = jo[11],
                    gsjlrtbzz = jo[13],
                    kfjlrtbzz = "",
                    jqjzcsyl = jo[18],
                    tbjzcsyl = jo[19],
                    tbzzcsyl = jo[20],
                    mll = jo[21],
                    jll = jo[22],
                    date = "20" + jo[0],
                };
                EMDataAccess.OperateHKMainIndexData(securitycode, jo_item);
            }
            //年报
            count = 0;
            foreach (var jo in jsonObj.data.zyzb_an)
            {
                count++;
                if (count == 1)
                    continue;
                var jo_item = new em_hk_mainindex_item_jo()
                {
                    yyzsr = jo[8],
                    gsjlr = jo[10],
                    kfjlr = "",
                    yyzsrtbzz = jo[11],
                    gsjlrtbzz = jo[13],
                    kfjlrtbzz = "",
                    jqjzcsyl = jo[18],
                    tbjzcsyl = jo[19],
                    tbzzcsyl = jo[20],
                    mll = jo[21],
                    jll = jo[22],
                    date = "20" + jo[0],
                };
                EMDataAccess.OperateHKMainIndexData(securitycode, jo_item);
            }
        }

        /// <summary>
        /// 抓取a主要财务数据
        /// </summary>
        public void CrawlAMainIndexData()
        {
            //抓取季报数据
            Logger.Info(string.Format("同步{0}的数据", xqsymbol));

            var json_str = HttpHelper.DownloadJson(string.Format(em_mainindex_latest_url, xqsymbol));
            var joList = JsonConvert.DeserializeObject<List<em_mainindex_jo>>(json_str);

            foreach (var jo in joList)
            {
                var date = jo.date.ConvertToDate().GetValueOrDefault();
                if (DateTime.Compare(date, latest_sync_date) < 0)
                    break;

                EMDataAccess.OperateAMainIndexData(securitycode, jo, includeUpdate);
            }
        }

        #endregion

        #region 抓取EM主营构成、核心题材

        /// <summary>
        /// 抓取主营构成
        /// </summary>
        public void CrawlMajorBusinessData()
        {
            //抓取季报数据
            Logger.Info(string.Format("同步{0}的数据", xqsymbol));

            var json_str = HttpHelper.DownloadJson(string.Format(em_main_biz_scope_url, xqsymbol));
            var joList = JsonConvert.DeserializeObject<em_major_business_scope_jo>(json_str);
            if (joList == null || joList.zygcfx == null || joList.zygcfx.Count == 0)
                return;

            var zygcfx_list = joList.zygcfx;

            foreach (var zygcfx in zygcfx_list)
            {
                var cp_list = zygcfx.cp;
                foreach (var item in cp_list)
                {
                    EMDataAccess.OperateMajorBusinessScopeData(securitycode, item, "cp");
                }

                var hy_list = zygcfx.hy;
                foreach (var item in hy_list)
                {
                    EMDataAccess.OperateMajorBusinessScopeData(securitycode, item, "hy");
                }
            }
        }

        public void CrawlCoreConceptData()
        {
            //抓取季报数据
            Logger.Info(string.Format("同步{0}的数据", xqsymbol));

            var json_str = HttpHelper.DownloadJson(string.Format(em_core_concept_url, xqsymbol));
            if (json_str.Contains("无F10资料"))
                return;

            var joList = JsonConvert.DeserializeObject<em_core_concept_jo>(json_str);
            if (joList == null || joList.ssbk == null || joList.ssbk.Count == 0)
                return;

            var ssbk_list = joList.ssbk;

            foreach (var ssbk in ssbk_list)
            {
                var isprecise = ssbk.IS_PRECISE;
                if (isprecise == "1")
                {
                    EMDataAccess.OperateCoreConceptData(securitycode, ssbk);
                }
            }
        }


        #endregion

    }
}
