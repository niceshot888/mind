using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Crawlers;
using NiceShot.Core.Criterias;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.DotNetWinFormsClient.Core;
using NiceShot.DotNetWinFormsClient.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NiceShot.DotNetWinFormsClient.ChildForms
{
    public partial class ExcelAnalysisForm : Form
    {

        private static string xq_v5_cookie = "s=cv11hmboue; device_id=faee55f3422c95cf1ebdedc40a1dfb83; bid=aae471e1c9766f681d9300f260c39d5c_kf8bd5rl; Hm_lvt_fe218c11eab60b6ab1b6f84fb38bcc4a=1618204374; cookiesu=121618751055835; remember=1; xq_a_token=cc5712b5d68f2db3ee3fe5a14a78ca5898a43cab; xq_id_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJ1aWQiOjY0MDYzMDExODEsImlzcyI6InVjIiwiZXhwIjoxNjIxMzQzMDUxLCJjdG0iOjE2MTkzNjQ0MjQwMzUsImNpZCI6ImQ5ZDBuNEFadXAifQ.WJturMNiaxbIZtaywFQFnkXMjGIsZjuSaYdZq5Zhmyb9ahJck0OdtqIQSTdrw8Xe2yV7b41P_Tyyp7sX3G7KyvcRnCLsf4CzxJwedly7GSpJ0It_xeQlt6BzM8KeAQ72UiCcDLcvid-1XPMLq_d1XWMh1dGdHK__G2K-RbY_FjcrUP6wE8OieHP18HfITXQrmY4AY7Hm7FkB7sAOQAzXQ7mcmrY7zJgbzojHjV5PdgcJCvSA9fkpjXeLwOMhP6mXvh3KbhEmxlEdprBwMM6-ahDndLcpz3aK2ooaVkYuLHt9JcIvVZCoIxf4U2VyQh2kLkpeGu8_0smOoyBA2QAhSw; xqat=cc5712b5d68f2db3ee3fe5a14a78ca5898a43cab; xq_r_token=92d8ab632e43a806aa013d8986cdf0e622bc844f; xq_is_login=1; u=6406301181; snbim_minify=true; Hm_lvt_1db88642e346389874251b5a1eded6e3=1619440787,1619594804,1619854399,1619854509; Hm_lpvt_1db88642e346389874251b5a1eded6e3=1619879777";

        private string _tscode;
        private string _ass_excel_fllepath;
        public ExcelAnalysisForm(string tscode)
        {
            InitializeComponent();

            this._tscode = tscode;

            WinFormHelper.InitChildWindowStyle(this);

            InitForm();
        }

        private void InitForm()
        {

            cbxReportType.DisplayMember = "Name";
            cbxReportType.ValueMember = "Value";
            cbxReportType.Items.Clear();
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "一季报", Value = "3" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "中报", Value = "6" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "三季报", Value = "9" });
            cbxReportType.Items.Add(new AutoCompleteNameAndValue { Name = "年报", Value = "12" });
            cbxReportType.SelectedIndex = 3;

            cbx_othercurrassetype.DisplayMember = "Name";
            cbx_othercurrassetype.ValueMember = "Value";
            cbx_othercurrassetype.Items.Clear();
            cbx_othercurrassetype.Items.Add(new AutoCompleteNameAndValue { Name = "金融资产", Value = "1" });
            cbx_othercurrassetype.Items.Add(new AutoCompleteNameAndValue { Name = "营运资产", Value = "2" });
            cbx_othercurrassetype.Items.Add(new AutoCompleteNameAndValue { Name = "营运负债", Value = "3" });
            cbx_othercurrassetype.SelectedIndex = 1;

            cbx_othercurreliabitype.DisplayMember = "Name";
            cbx_othercurreliabitype.ValueMember = "Value";
            cbx_othercurreliabitype.Items.Clear();
            cbx_othercurreliabitype.Items.Add(new AutoCompleteNameAndValue { Name = "营运资产", Value = "1" });
            cbx_othercurreliabitype.Items.Add(new AutoCompleteNameAndValue { Name = "营运负债", Value = "2" });
            cbx_othercurreliabitype.Items.Add(new AutoCompleteNameAndValue { Name = "短期债务", Value = "3" });
            cbx_othercurreliabitype.SelectedIndex = 1;

            cbx_expinoncurrassettype.DisplayMember = "Name";
            cbx_expinoncurrassettype.ValueMember = "Value";
            cbx_expinoncurrassettype.Items.Clear();
            cbx_expinoncurrassettype.Items.Add(new AutoCompleteNameAndValue { Name = "金融资产", Value = "1" });
            cbx_expinoncurrassettype.Items.Add(new AutoCompleteNameAndValue { Name = "营运资产", Value = "2" });
            cbx_expinoncurrassettype.SelectedIndex = 0;

            ShowExcelLink();
        }

        private void ShowExcelLink()
        {
            var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            var str_datetype = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;
            var excelPath = Path.Combine(dirPath, _tscode + "_" + str_datetype + ".xlsx");
            if (File.Exists(excelPath))
            {
                _ass_excel_fllepath = excelPath;
                link_excel.Visible = true;
            }
        }

        private void btnGengerate_Click(object sender, EventArgs e)
        {
            BuildAllWookSheet(this._tscode);
        }

        #region 生成资产资本表

        private void BuildAllWookSheet(string tscode)
        {
            link_excel.Visible = false;

            new EMCrawlerV1(new EMCrawlerV1Criteria { SecurityCode = tscode, InitDate = string.Format("{0}-12-31", DateTime.Now.Year), Industry = "普通行业", IncludeUpdate = true, QuarterNums = 5 }).CrawlFinReportData();
            new EMCrawler(new EMCrawlerCriteria { SecurityCode = tscode, IncludeUpdate = true }).CrawlAMainIndexData();
            //var em = new XQCrawler(new XQCrawlerCriteria { SecurityCode = tscode, Cookie = xq_v5_cookie });
            //em.CrawlAMainIndexData();

            //new XQCrawler(new XQCrawlerCriteria { SecurityCode = tscode,Cookie= "" }).CrawlXQFinBalanceData();
            //new THSCrawler(new THSCrawlerCriteria { SecurityCode = tscode }).CrawlCashflowAdditionalData();

            var str_datetype = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;

            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            //var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files"); Path.Combine(dirPath, tscode + "_" + str_datetype + ".xlsx")
            var excelFilePath = Path.Combine(dirPath, tscode + "_" + str_datetype + ".xlsx");
            _ass_excel_fllepath = excelFilePath;
            var excelFile = new FileInfo(excelFilePath);
            if (excelFile.Exists)
            {
                try
                {
                    excelFile.Delete();
                    excelFile = new FileInfo(excelFilePath);
                }
                catch
                {
                    MessageBox.Show("EXCEL文件已经被打开，请关闭后再操作");
                    return;
                }
            }

            using (var package = new ExcelPackage(excelFile))
            {

                BuildZhangAnalysisWookSheet(tscode, package);

                BuildBasicReportWookSheet(tscode, package, ReportType.BalanceSheet);
                BuildBasicReportWookSheet(tscode, package, ReportType.Income);
                BuildBasicReportWookSheet(tscode, package, ReportType.Cashflow);

                BuildAssAndCapWookSheet(tscode, package);

                package.Save();
                link_excel.Visible = true;
            }
        }

        private void BuildAssAndCapWookSheet(string tscode, ExcelPackage package)
        {
            var str_othercurrassetype = (cbx_othercurrassetype.SelectedItem as AutoCompleteNameAndValue).Value;
            var str_othercurreliabitype = (cbx_othercurreliabitype.SelectedItem as AutoCompleteNameAndValue).Value;
            var str_expinoncurrassettype = (cbx_expinoncurrassettype.SelectedItem as AutoCompleteNameAndValue).Value;

            var guoDataStructs = GetGuoDataStructs();

            var ws = package.Workbook.Worksheets.Add("资产资本表");

            ws.Cells.Style.WrapText = true;
            ws.Cells.Style.Font.Size = 10;
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells.Style.Numberformat.Format = "#,##0.00;[Red](#,##0.00)";//保留两位小数
            ws.Column(1).Width = 30;
            //ws.Row(45).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
            ws.Column(1).Style.Font.Name = "微软雅黑";
            ws.Row(1).Style.Font.Name = "微软雅黑";
            ws.OutLineSummaryBelow = false;

            //添加标题列
            var rowIndex = 1;
            ws.Cells[1, 1].Style.Font.Bold = true;
            foreach (var model in guoDataStructs)
            {
                ws.Cells[rowIndex++, 1].Value = model.fieldname_cn;
                if (!string.IsNullOrEmpty(model.font_style))
                    ws.Row(rowIndex - 1).Style.Font.Bold = true;
            }
            ws.View.FreezePanes(2, 2);

            BuildAssAndCapExcelFields(guoDataStructs);

            //添加数据列
            var dataList = GetAssetAndCapitalList(tscode, guoDataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.report_date.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}3:{0}20)", colName);//1.1金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.monetaryfunds;//货币资金
                ws.Cells[rowIndex++, columnIndex].Value = data.lend_fund;//以公允价值计量且其变动计入当期损益的金融资产+债务工具投资+拆出资金
                ws.Cells[rowIndex++, columnIndex].Value = data.trade_finasset_notfvtpl;//交易性金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.derive_finasset;//衍生金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_rece;//应收利息
                ws.Cells[rowIndex++, columnIndex].Value = data.dividend_rece;//应收股利
                ws.Cells[rowIndex++, columnIndex].Value = data.buy_resale_finasset;//买入返售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.holdsale_asset;//划分为持有待售的资产
                ws.Cells[rowIndex++, columnIndex].Value = data.loan_advance;//发放贷款和垫款
                ws.Cells[rowIndex++, columnIndex].Value = data.available_sale_finasset;//可供出售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.other_equity_invest;//其他权益工具投资
                ws.Cells[rowIndex++, columnIndex].Value = data.other_noncurrent_finasset;//其他非流动金融资产
                ws.Cells[rowIndex++, columnIndex].Value = "";//持有至到期投资
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_realestate;//投资性房地产
                ws.Cells[rowIndex++, columnIndex].Value = data.sell_repo_finasset;//卖出回购金融资产款
                ws.Cells[rowIndex++, columnIndex].Value = "";//其他应收款

                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.totalfinasset).ToString()) ? data.other_current_asset : null;//其他流动资产(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_expinoncurrassettype == ((int)expinoncurrassettype.totalfinasset).ToString()) ? data.noncurrent_asset_1year : null;//一年内到期的非流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.long_equity_invest;//1.2长期股权投资
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}23-{0}35", colName);//1.3营运资本需求=1.3.1-1.3.2
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}24:{0}34)", colName);//1.3.1营运资产
                ws.Cells[rowIndex++, columnIndex].Value = data.note_rece;//应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_rece;//应收账款+债权投资
                ws.Cells[rowIndex++, columnIndex].Value = data.contract_asset;//合同资产
                ws.Cells[rowIndex++, columnIndex].Value = data.prepayment;//预付款项
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//存货
                ws.Cells[rowIndex++, columnIndex].Value = data.long_rece;//长期应收款
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_tax_asset;//递延所得税资产(营运)
                ws.Cells[rowIndex++, columnIndex].Value = (str_expinoncurrassettype == ((int)expinoncurrassettype.subtotaloperasset).ToString()) ? data.noncurrent_asset_1year : null;//一年内到期的非流动资产(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.subtotaloperasset).ToString()) ? data.other_current_asset : null;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.subtotaloperasset).ToString()) ? data.other_current_liab : null;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.total_other_rece != null ? data.total_other_rece : data.other_rece;//其他应收款(常)
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}36:{0}50)", colName);//1.3.2营运负债
                ws.Cells[rowIndex++, columnIndex].Value = "";//衍生金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.note_payable;//应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_payable;//应付账款+持有待售负债+租赁负债
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//预收款项+合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.staff_salary_payable;//应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.tax_payable;//应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_income;//递延收益(-流动负债)
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_tax_liab;//递延所得税负债(营运)
                ws.Cells[rowIndex++, columnIndex].Value = data.long_staffsalary_payable;//长期应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.total_other_payable;//其他应付款(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.accept_deposit_interbank;//吸收存款及同业存放
                ws.Cells[rowIndex++, columnIndex].Value = data.predict_liab;//预计负债(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.totaloperliab).ToString()) ? data.other_current_asset : null;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.totaloperliab).ToString()) ? data.other_current_liab : null;//其他流动负债(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.special_payable;//专项应付款
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}52:{0}63)", colName);//1.4长期经营资产
                ws.Cells[rowIndex++, columnIndex].Value = data.fixed_asset;//固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.cip;//在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.project_material;//工程物资
                ws.Cells[rowIndex++, columnIndex].Value = data.fixed_asset_disposal;//固定资产清理
                ws.Cells[rowIndex++, columnIndex].Value = data.productive_biology_asset;//生产性生物资产
                ws.Cells[rowIndex++, columnIndex].Value = data.oil_gas_asset;//油气资产
                ws.Cells[rowIndex++, columnIndex].Value = data.intangible_asset;//无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.develop_expense;//开发支出
                ws.Cells[rowIndex++, columnIndex].Value = data.goodwill;//商誉
                ws.Cells[rowIndex++, columnIndex].Value = data.long_prepaid_expense;//长期待摊费用
                ws.Cells[rowIndex++, columnIndex].Value = "";//递延所得税资产-递延所得税负债
                ws.Cells[rowIndex++, columnIndex].Value = data.other_noncurrent_asset;//其他非流动资产
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}22+{0}51", colName);//经营资产合计=1.3+1.4
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}2+{0}21+{0}64", colName);//资产总额=1.1+1.2+1.3+1.4
                ws.Cells[rowIndex++, columnIndex].Value = "";//(二)资本结构
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}68:{0}76)", colName);//2.1短期债务
                ws.Cells[rowIndex++, columnIndex].Value = data.short_loan;//短期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.trade_finliab_notfvtpl;//交易性金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.noncurrent_liab_1year;//一年内到期的非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_payable;//应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.borrow_fund;//拆入资金
                ws.Cells[rowIndex++, columnIndex].Value = "";//以公允价值计量且其变动计入当期损益的金融负债
                ws.Cells[rowIndex++, columnIndex].Value = "";//其他应付款
                ws.Cells[rowIndex++, columnIndex].Value = "";//预计负债
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.totalshorttermliab).ToString()) ? data.other_current_liab : null;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}78:{0}84)", colName);//2.2长期债务
                ws.Cells[rowIndex++, columnIndex].Value = data.long_loan;//长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bond_payable;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.long_payable;//长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.perpetual_bond;//永续债
                ws.Cells[rowIndex++, columnIndex].Value = "";//专项应付款
                ws.Cells[rowIndex++, columnIndex].Value = "";//吸收存款及同业存放(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.other_noncurrent_liab;//其他非流动负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}67+{0}77", colName);//有息负债=2.1+2.2
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}91:{0}93)-{0}94+{0}95", colName);//2.3股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.share_capital;//实收资本（或股本）
                ws.Cells[rowIndex++, columnIndex].Value = data.capital_reserve;//资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.surplus_reserve;//盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.unassign_rpofit;//未分配利润
                ws.Cells[rowIndex++, columnIndex].Value = data.total_parent_equity;//归属于母公司股东权益合计
                ws.Cells[rowIndex++, columnIndex].Value = data.minority_equity;//少数股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.dividend_payable;//应付股利
                ws.Cells[rowIndex++, columnIndex].Value = data.treasury_shares;//减：库存股
                ws.Cells[rowIndex++, columnIndex].Value = data.other_compre_income;//其他综合收益
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}67+{0}77+{0}86", colName);//资本总额=2.1+2.2+2.3

                #endregion

                #region 计算同比

                columnIndex++;
                ws.Column(columnIndex).Width = 8;
                ws.Column(columnIndex).Style.Numberformat.Format = "#0\\.00%";
                var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);
                var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex + 1);

                rowIndex = 1;
                ws.Cells[rowIndex++, columnIndex].Value = "占比";//同比
                ws.Column(columnIndex).Style.Font.Color.SetColor(Color.DimGray);
                var fmlRowIdx = 2;
                foreach (var model in guoDataStructs)
                {
                    //同比
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                    //占比 ,{0}{1}/{0}65*100>=1000
                    ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{1}=0,{0}{1}=\"\"),\"\",{0}{1}/{0}65*100)", colName1, fmlRowIdx++);
                }

                #endregion

            }
        }

        private void BuildBasicReportWookSheet(string tscode, ExcelPackage package, ReportType reportType)
        {
            var dataStructs = GetCommonStockDataStructs(reportType.ToReportTypeCnName());

            var ws = package.Workbook.Worksheets.Add(reportType.ToReportTypeCnName());

            ws.Cells.Style.WrapText = true;
            ws.Cells.Style.Font.Size = 10;
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells.Style.Numberformat.Format = "#,##0.00;[Red](#,##0.00)";//保留两位小数
            ws.Column(1).Width = 30;
            //ws.Row(45).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
            ws.Column(1).Style.Font.Name = "微软雅黑";
            ws.Row(1).Style.Font.Name = "微软雅黑";
            ws.OutLineSummaryBelow = false;

            var rowIndex = 1;
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[rowIndex++, 1].Value = "项目";
            foreach (var model in dataStructs)
            {
                ws.Cells[rowIndex++, 1].Value = model.fieldname_cn;
                if (!string.IsNullOrEmpty(model.font_style))
                    ws.Row(rowIndex - 1).Style.Font.Bold = true;
            }
            ws.View.FreezePanes(2, 2);

            BuildCommonStockExcelFields(dataStructs, reportType);

            if (reportType == ReportType.BalanceSheet)
                BindBalanceSheetData(tscode, dataStructs, ws);
            else if (reportType == ReportType.Income)
                BindIncomeData(tscode, dataStructs, ws);
            else if (reportType == ReportType.Cashflow)
                BindCashflowData(tscode, dataStructs, ws);

        }

        private void BuildZhangAnalysisWookSheet(string tscode, ExcelPackage package)
        {
            var dataStructs = GetZhangAnalysisDataStructs();

            var ws = package.Workbook.Worksheets.Add("张氏分析");

            ws.Cells.Style.WrapText = true;
            ws.Cells.Style.Font.Size = 10;
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells.Style.Numberformat.Format = "#,##0.00;[Red](#,##0.00)";//保留两位小数
            ws.Column(1).Width = 35;
            //ws.Row(45).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
            ws.Column(1).Style.Font.Name = "微软雅黑";
            ws.Row(1).Style.Font.Name = "微软雅黑";
            ws.OutLineSummaryBelow = false;

            var rowIndex = 1;
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[rowIndex++, 1].Value = "项目";
            foreach (var model in dataStructs)
            {
                ws.Cells[rowIndex++, 1].Value = model.fieldname_cn;
                if (!string.IsNullOrEmpty(model.font_style))
                    ws.Row(rowIndex - 1).Style.Font.Bold = true;
            }
            ws.View.FreezePanes(2, 2);

            BuildZhangAnalysisExcelFields(dataStructs);

            BindZhangAnalysisData(tscode, dataStructs, ws);
        }

        private void BindZhangAnalysisData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetZhangAnalysisDataList(tscode, dataStructs);
            var columnIndex = 1;
            StringBuilder sbCashflowSummary = new StringBuilder();
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.report_date.Value.ToString("yyyy/MM/dd") + "-" + data.name;
                sbCashflowSummary.AppendLine("公司名称：" + data.name + "，报表日期：" + data.report_date.Value.Year + "年");
                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = "";//一、资产负债表分析
                ws.Cells[rowIndex++, columnIndex].Value = "";//（一）考察企业资产总额的规模变化
                ws.Cells[rowIndex++, columnIndex].Value = data.total_assets;//   总资产
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//（二）考察支撑企业资产的四大动力的变化情况
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营性负债
                ws.Cells[rowIndex++, columnIndex].Value = data.note_payable;//    应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_payable;//    应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//    预收款项
                ws.Cells[rowIndex++, columnIndex].Value = data.staff_salary_payable;//    应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.tax_payable;//    应交税费
                ws.Cells[rowIndex++, columnIndex].Value = "";//金融性负债
                ws.Cells[rowIndex++, columnIndex].Value = data.short_loan;//    短期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_payable;//    应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.trade_finliab_notfvtpl;//    交易性金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.noncurrent_liab_1year;//    一年内到期的非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.long_loan;//    长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bond_payable;//    应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.long_payable;//    长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.perpetual_bond;//    永续债
                ws.Cells[rowIndex++, columnIndex].Value = "";//股东入资
                ws.Cells[rowIndex++, columnIndex].Value = data.share_capital;//    实收资本
                ws.Cells[rowIndex++, columnIndex].Value = data.capital_reserve;//    资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.minority_equity;//    少数股东权益
                ws.Cells[rowIndex++, columnIndex].Value = "";//利润积累
                ws.Cells[rowIndex++, columnIndex].Value = data.surplus_reserve;//     盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.unassign_rpofit;//     未分配利润
                ws.Cells[rowIndex++, columnIndex].Value = "";//（三）考察资产结构的变化
                ws.Cells[rowIndex++, columnIndex].Value = "";//企业从事生产经营活动的基础
                ws.Cells[rowIndex++, columnIndex].Value = data.fixed_asset;//    固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.cip;//    在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.intangible_asset;//    无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.long_equity_invest;//    长期股权投资
                ws.Cells[rowIndex++, columnIndex].Value = "";//考察流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.finasset;//     金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//     存货
                ws.Cells[rowIndex++, columnIndex].Value = data.contract_asset;//     合同资产
                ws.Cells[rowIndex++, columnIndex].Value = data.note_rece;//     应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_rece;//     应收账款（与销售有关的商业债权）
                ws.Cells[rowIndex++, columnIndex].Value = data.prepayment;//     预付款项（与购买有关的商业债权）
                ws.Cells[rowIndex++, columnIndex].Value = data.other_rece;//     其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = "";//（四）考察企业营运资本管理状况
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营性流动资产的核心项目
                ws.Cells[rowIndex++, columnIndex].Value = data.note_rece;//    应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_rece;//    应收账款
                ws.Cells[rowIndex++, columnIndex].Value = data.prepayment;//    预付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//    存货
                ws.Cells[rowIndex++, columnIndex].Value = data.other_rece;//    其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营性流动负债的核心项目
                ws.Cells[rowIndex++, columnIndex].Value = data.note_payable;//     应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_payable;//     应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//     预收款项
                ws.Cells[rowIndex++, columnIndex].Value = data.staff_salary_payable;//     应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.tax_payable;//     应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.total_other_payable;//     其他应付款
                ws.Cells[rowIndex++, columnIndex].Value = "";//（五）考察企业的融资潜力
                ws.Cells[rowIndex++, columnIndex].Value = data.ass_fin_liab_ratio;//资产金融负债率
                ws.Cells[rowIndex++, columnIndex].Value = "";//二、利润表分析
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_income;//营业收入
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_cost;//营业成本
                ws.Cells[rowIndex++, columnIndex].Value = data.rotar;//总资产回报率
                ws.Cells[rowIndex++, columnIndex].Value = data.gross_profit;//毛利润
                ws.Cells[rowIndex++, columnIndex].Value = data.gross_margin;//毛利率
                ws.Cells[rowIndex++, columnIndex].Value = data.sale_expense;//销售费用
                ws.Cells[rowIndex++, columnIndex].Value = data.saleexp_ratio;//销售费用率
                ws.Cells[rowIndex++, columnIndex].Value = data.manage_expense;//管理费用
                ws.Cells[rowIndex++, columnIndex].Value = data.manageexp_ratio;//管理费用率
                ws.Cells[rowIndex++, columnIndex].Value = data.research_expense;//研发费用
                ws.Cells[rowIndex++, columnIndex].Value = data.rdexp_ratio;//研发费用率
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit;//核心利润
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit_margin;//核心利润率
                ws.Cells[rowIndex++, columnIndex].Value = data.netcash_operate;//经营活动现金流净额
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit_hxl;//核心利润获现率
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_income;//投资收益
                ws.Cells[rowIndex++, columnIndex].Value = data.nonbusiness_income;//其他收益：营业外收入
                ws.Cells[rowIndex++, columnIndex].Value = data.roe;//净资产收益率
                ws.Cells[rowIndex++, columnIndex].Value = data.deduct_parent_netprofit;//扣非净利润
                ws.Cells[rowIndex++, columnIndex].Value = data.kfjll;//扣非净利率
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_sumassert;//平均总资产
                ws.Cells[rowIndex++, columnIndex].Value = data.sumasset_tor;//总资产周转率
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_fixedasset;//平均固定资产原值
                ws.Cells[rowIndex++, columnIndex].Value = data.fixedasset_tor;//固定资产周转率
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_inventory;//平均存货
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory_tor;//存货周转率
                ws.Cells[rowIndex++, columnIndex].Value = "";//三、现金流动分析
                ws.Cells[rowIndex++, columnIndex].Value = data.accept_invest_cash;//1.1.找股东要钱：吸收投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_loan_cash;//1.2.找银行借钱：取得借款收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.issue_bond;//1.3.发行债券收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_other_finance;//1.4.收到其他与筹资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = "";//筹资合计：
                ws.Cells[rowIndex++, columnIndex].Value = data.construct_long_asset;//2.1.要花钱了：购建固定资产、无形资产和其他长期资产支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_staff_cash;//2.2.要雇人了：支付给职工以及为职工支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.buy_services;//2.3.1.倒买倒卖：购买商品、接受劳务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_other_operate;//2.3.2.三费+往来款：支付其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = "";//投资经营流出量合计：
                ws.Cells[rowIndex++, columnIndex].Value = "";//PS：筹资流入量支持投资流出量、经营流出量
                ws.Cells[rowIndex++, columnIndex].Value = data.sales_services;//3.1.1.卖出商品：销售商品、提供劳务收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_other_operate;//3.1.2.营业外收入+往来款：收到其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = "";//销售回款合计：
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_all_tax;//3.2.交税：支付的各项税费
                ws.Cells[rowIndex++, columnIndex].Value = data.cf_diviprofitorintpay;//3.3.支付利息+子公司支付给少数股东的股利、利润
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_debt_cash;//3.4.还有钱，还债：偿还债务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.cf_bonus;//3.5.还有钱，分红
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_pay_cash;//3.6.还有钱，投资其他公司或者购买银行理财产品：投资支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.withdraw_invest;//3.7.收回投资收到的现金

                //ws.Cells[rowIndex++, columnIndex].Value = "";
                //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}98+{0}97-{0}94-{0}93", colName);//真实毛利润
                ////ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}91+{0}92+{0}100+{0}101+{0}103", colName);//所有支出
                //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}92+{0}100+{0}101", colName);//所有支出
                //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}107-{0}108", colName);//纯利润
                ////ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}107-{0}108+{0}104", colName);//纯利润

                sbCashflowSummary.AppendLine("");
                #endregion

                #region 计算同比

                if (columnIndex <= dataList.Count * 2 - 1)
                {
                    columnIndex++;

                    ws.Column(columnIndex).Width = 8;
                    ws.Column(columnIndex).Style.Numberformat.Format = "#0\\.00%";

                    var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);
                    var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex + 1);

                    rowIndex = 1;
                    ws.Cells[rowIndex++, columnIndex].Value = "同比";
                    ws.Column(columnIndex).Style.Font.Color.SetColor(Color.DimGray);
                    var fmlRowIdx = 2;
                    foreach (var model in dataStructs)
                    {
                        if (model.fieldname_cn.Contains("率"))
                        {
                            ws.Cells[rowIndex++, columnIndex].Value = "";
                            fmlRowIdx++;
                        }
                        else
                        {
                            if (rowIndex <= 106)
                                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                            else
                                ws.Cells[rowIndex++, columnIndex].Value = "";
                        }
                    }
                }

                #endregion
            }

            IOHelper.CreateFileInFilesDir("cashflow-" + tscode + ".txt", sbCashflowSummary.ToString());
        }

        private void BindBalanceSheetData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetEMBalTableDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.report_date.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = "";//资产负债表
                ws.Cells[rowIndex++, columnIndex].Value = "";//流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.monetaryfunds;//货币资金
                ws.Cells[rowIndex++, columnIndex].Value = data.lend_fund;//拆出资金
                ws.Cells[rowIndex++, columnIndex].Value = data.note_accounts_rece;//应收票据及应收账款
                ws.Cells[rowIndex++, columnIndex].Value = data.note_rece;//其中:应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_rece;//        应收账款
                ws.Cells[rowIndex++, columnIndex].Value = data.prepayment;//预付款项
                ws.Cells[rowIndex++, columnIndex].Value = data.total_other_rece;//其他应收款合计
                ws.Cells[rowIndex++, columnIndex].Value = data.other_rece;//其中:其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//存货
                ws.Cells[rowIndex++, columnIndex].Value = data.other_current_asset;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.total_current_assets;//流动资产合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//非流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.loan_advance;//发放贷款及垫款
                ws.Cells[rowIndex++, columnIndex].Value = data.creditor_invest;//债权投资
                ws.Cells[rowIndex++, columnIndex].Value = data.other_creditor_invest;//其他债权投资
                ws.Cells[rowIndex++, columnIndex].Value = data.other_noncurrent_finasset;//其他非流动金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.fixed_asset;//固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.cip;//在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.intangible_asset;//无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.long_prepaid_expense;//长期待摊费用
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_tax_asset;//递延所得税资产
                ws.Cells[rowIndex++, columnIndex].Value = data.total_noncurrent_assets;//非流动资产合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.total_assets;//资产总计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.note_accounts_payable;//应付票据及应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.accounts_payable;//其中:应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.note_payable;//        应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.advance_receivables;//预收款项
                ws.Cells[rowIndex++, columnIndex].Value = data.contract_liab;//合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.staff_salary_payable;//应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.tax_payable;//应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.total_other_payable;//其他应付款合计
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_payable;//其中:应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.dividend_payable;//应付股利
                ws.Cells[rowIndex++, columnIndex].Value = data.other_current_liab;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.total_current_liab;//流动负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//非流动负债

                ws.Cells[rowIndex++, columnIndex].Value = data.long_loan;//长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bond_payable;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.long_payable;//长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.long_staffsalary_payable;//长期应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.predict_liab;//预计负债
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_tax_liab;//递延所得税负债
                ws.Cells[rowIndex++, columnIndex].Value = data.bond_payable;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_income;//递延收益
                ws.Cells[rowIndex++, columnIndex].Value = data.other_noncurrent_liab;//其他非流动负债
                
                ws.Cells[rowIndex++, columnIndex].Value = data.total_noncurrent_liab;//非流动负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.total_liabilities;//负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//所有者权益(或股东权益)
                ws.Cells[rowIndex++, columnIndex].Value = data.share_capital;//实收资本（或股本）
                ws.Cells[rowIndex++, columnIndex].Value = data.capital_reserve;//资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.other_compre_income;//其他综合收益
                ws.Cells[rowIndex++, columnIndex].Value = data.surplus_reserve;//盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.general_risk_reserve;//一般风险准备
                ws.Cells[rowIndex++, columnIndex].Value = data.unassign_rpofit;//未分配利润
                ws.Cells[rowIndex++, columnIndex].Value = data.total_parent_equity;//归属于母公司股东权益总计
                ws.Cells[rowIndex++, columnIndex].Value = data.minority_equity;//少数股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.total_equity;//股东权益合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.total_liab_equity;//负债和股东权益总计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//审计意见(境内)

                #endregion

                #region 计算同比

                columnIndex++;
                ws.Column(columnIndex).Width = 8;
                ws.Column(columnIndex).Style.Numberformat.Format = "#0\\.00%";
                var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);
                var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex + 1);

                rowIndex = 1;
                ws.Cells[rowIndex++, columnIndex].Value = "占比";//同比
                ws.Column(columnIndex).Style.Font.Color.SetColor(Color.DimGray);
                var fmlRowIdx = 2;
                foreach (var model in dataStructs)
                {
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                    ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{1}=0,{0}{1}=\"\"),\"\",{0}{1}/{0}26*100)", colName1, fmlRowIdx++);
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF({0}{2}=0,\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                }

                #endregion
            }
        }

        private void BindIncomeData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetEMIncTableDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.report_date.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = "";//利润表
                ws.Cells[rowIndex++, columnIndex].Value = data.total_operate_income;//营业总收入
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_income;//营业收入
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_income;//利息收入
                ws.Cells[rowIndex++, columnIndex].Value = data.fee_commission_income;//手续费及佣金收入
                ws.Cells[rowIndex++, columnIndex].Value = data.total_operate_cost;//营业总成本
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_cost;//营业成本
                ws.Cells[rowIndex++, columnIndex].Value = data.interest_expense;//利息支出
                ws.Cells[rowIndex++, columnIndex].Value = data.fee_commission_expense;//手续费及佣金支出
                ws.Cells[rowIndex++, columnIndex].Value = data.research_expense;//研发费用
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_tax_add;//营业税金及附加
                ws.Cells[rowIndex++, columnIndex].Value = data.sale_expense;//销售费用
                ws.Cells[rowIndex++, columnIndex].Value = data.manage_expense;//管理费用
                ws.Cells[rowIndex++, columnIndex].Value = data.finance_expense;//财务费用
                ws.Cells[rowIndex++, columnIndex].Value = data.fe_interest_income;//其中:利息收入
                ws.Cells[rowIndex++, columnIndex].Value = data.fe_interest_expense;//        利息费用
                ws.Cells[rowIndex++, columnIndex].Value = "";//其他经营收益
                ws.Cells[rowIndex++, columnIndex].Value = data.fairvalue_change_income;//加:公允价值变动收益
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_income;//投资收益
                ws.Cells[rowIndex++, columnIndex].Value = data.asset_disposal_income;//资产处置收益
                ws.Cells[rowIndex++, columnIndex].Value = data.asset_impairment_income;//资产减值损失(新)
                ws.Cells[rowIndex++, columnIndex].Value = data.credit_impairment_income;//信用减值损失(新)
                ws.Cells[rowIndex++, columnIndex].Value = data.other_income;//其他收益
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_profit;//营业利润
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.nonbusiness_income;//加:营业外收入
                ws.Cells[rowIndex++, columnIndex].Value = data.nonbusiness_expense;//减:营业外支出
                ws.Cells[rowIndex++, columnIndex].Value = data.total_profit;//利润总额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.income_tax;//减:所得税
                ws.Cells[rowIndex++, columnIndex].Value = data.netprofit;//净利润
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//(一)按经营持续性分类
                ws.Cells[rowIndex++, columnIndex].Value = data.continued_netprofit;//持续经营净利润
                ws.Cells[rowIndex++, columnIndex].Value = "";//(二)按所有权归属分类
                ws.Cells[rowIndex++, columnIndex].Value = data.parent_netprofit;//归属于母公司股东的净利润
                ws.Cells[rowIndex++, columnIndex].Value = data.minority_interest;//少数股东损益
                ws.Cells[rowIndex++, columnIndex].Value = data.deduct_parent_netprofit;//扣除非经常性损益后的净利润
                ws.Cells[rowIndex++, columnIndex].Value = "";//每股收益
                ws.Cells[rowIndex++, columnIndex].Value = data.basic_eps;//基本每股收益
                ws.Cells[rowIndex++, columnIndex].Value = data.diluted_eps;//稀释每股收益
                ws.Cells[rowIndex++, columnIndex].Value = data.other_compre_income;//其他综合收益
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.parent_oci;//归属于母公司股东的其他综合收益
                ws.Cells[rowIndex++, columnIndex].Value = data.total_compre_income;//综合收益总额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.parent_tci;//归属于母公司股东的综合收益总额
                ws.Cells[rowIndex++, columnIndex].Value = data.minority_tci;//归属于少数股东的综合收益总额
                ws.Cells[rowIndex++, columnIndex].Value = "";//审计意见(境内)

                #endregion

                #region 计算同比

                columnIndex++;
                ws.Column(columnIndex).Width = 8;
                ws.Column(columnIndex).Style.Numberformat.Format = "#0\\.00%";
                var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);
                var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex + 1);

                rowIndex = 1;
                ws.Cells[rowIndex++, columnIndex].Value = "同比";
                ws.Column(columnIndex).Style.Font.Color.SetColor(Color.DimGray);
                var fmlRowIdx = 2;
                foreach (var model in dataStructs)
                {
                    ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF({0}{2}=0,\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                }

                #endregion
            }
        }

        private void BindCashflowData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetEMCFTableDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.report_date.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = "";//现金流量表
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营活动产生的现金流量
                ws.Cells[rowIndex++, columnIndex].Value = data.sales_services;//销售商品、提供劳务收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.deposit_interbank_add;//客户存款和同业存放款项净增加额
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_interest_commission;//收取利息、手续费及佣金的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_other_operate;//收到其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.total_operate_inflow;//经营活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.buy_services;//购买商品、接受劳务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.loan_advance_add;//客户贷款及垫款净增加额
                ws.Cells[rowIndex++, columnIndex].Value = data.pbc_interbank_add;//存放中央银行和同业款项净增加额
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_interest_commission;//支付利息、手续费及佣金的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_staff_cash;//支付给职工以及为职工支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_all_tax;//支付的各项税费
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_other_operate;//支付其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_outflow_other;//经营活动现金流出的其他项目
                ws.Cells[rowIndex++, columnIndex].Value = data.total_operate_outflow;//经营活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netcash_operate;//经营活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//投资活动产生的现金流量
                ws.Cells[rowIndex++, columnIndex].Value = data.withdraw_invest;//收回投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_invest_income;//取得投资收益收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.disposal_long_asset;//处置固定资产、无形资产和其他长期资产收回的现金净额
                ws.Cells[rowIndex++, columnIndex].Value = data.disposal_subsidiary_other;//处置子公司及其他营业单位收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_other_invest;//收到的其他与投资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.total_invest_inflow;//投资活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.construct_long_asset;//购建固定资产、无形资产和其他长期资产支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_pay_cash;//投资支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_other_invest;//支付其他与投资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.total_invest_outflow;//投资活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netcash_invest;//投资活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = "";//筹资活动产生的现金流量
                ws.Cells[rowIndex++, columnIndex].Value = data.accept_invest_cash;//吸收投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.subsidiary_accept_invest;//其中:子公司吸收少数股东投资收到的现金

                ws.Cells[rowIndex++, columnIndex].Value = data.receive_loan_cash;//取得借款收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.issue_bond;//发行债券收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.receive_other_finance;//收到其他与筹资活动有关的现金

                ws.Cells[rowIndex++, columnIndex].Value = data.total_finance_inflow;//筹资活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_debt_cash;//偿还债务所支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.assign_dividend_porfit;//分配股利、利润或偿付利息支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.subsidiary_pay_dividend;//其中:子公司支付给少数股东的股利、利润
                ws.Cells[rowIndex++, columnIndex].Value = data.pay_other_finance;//支付的其他与筹资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.total_finance_outflow;//筹资活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netcash_finance;//筹资活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.rate_change_effect;//汇率变动对现金及现金等价物的影响
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.cce_add;//现金及现金等价物净增加额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.begin_cce;//加:期初现金及现金等价物余额
                ws.Cells[rowIndex++, columnIndex].Value = data.end_cce;//期末现金及现金等价物余额
                ws.Cells[rowIndex++, columnIndex].Value = "";//补充资料
                ws.Cells[rowIndex++, columnIndex].Value = data.netprofit;//净利润
                ws.Cells[rowIndex++, columnIndex].Value = data.asset_impairment;//资产减值准备
                ws.Cells[rowIndex++, columnIndex].Value = data.fa_ir_depr;//固定资产和投资性房地产折旧
                ws.Cells[rowIndex++, columnIndex].Value = data.oilgas_biology_depr;//其中:固定资产折旧、油气资产折耗、生产性生物资产折旧
                ws.Cells[rowIndex++, columnIndex].Value = data.ir_depr;//投资性房地产折旧
                ws.Cells[rowIndex++, columnIndex].Value = data.ia_amortize;//无形资产摊销
                ws.Cells[rowIndex++, columnIndex].Value = data.lpe_amortize;//长期待摊费用摊销
                ws.Cells[rowIndex++, columnIndex].Value = data.disposal_longasset_loss;//处置固定资产、无形资产和其他长期资产的损失
                ws.Cells[rowIndex++, columnIndex].Value = data.fa_scrap_loss;//固定资产报废损失
                ws.Cells[rowIndex++, columnIndex].Value = data.fairvalue_change_loss;//公允价值变动损失
                ws.Cells[rowIndex++, columnIndex].Value = data.invest_loss;//投资损失
                ws.Cells[rowIndex++, columnIndex].Value = data.defer_tax;//递延所得税
                ws.Cells[rowIndex++, columnIndex].Value = data.dt_asset_reduce;//其中:递延所得税资产减少
                ws.Cells[rowIndex++, columnIndex].Value = data.dt_liab_add;//递延所得税负债增加
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory_reduce;//存货的减少
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_rece_reduce;//经营性应收项目的减少
                ws.Cells[rowIndex++, columnIndex].Value = data.operate_payable_add;//经营性应付项目的增加
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netcash_operatenote;//经营活动产生的现金流量净额
                ws.Cells[rowIndex++, columnIndex].Value = data.end_cash;//现金的期末余额
                ws.Cells[rowIndex++, columnIndex].Value = data.begin_cash;//减:现金的期初余额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.cce_addnote;//现金及现金等价物的净增加额
                ws.Cells[rowIndex++, columnIndex].Value = "";//审计意见(境内)

                #endregion

                #region 计算同比

                columnIndex++;
                ws.Column(columnIndex).Width = 8;
                ws.Column(columnIndex).Style.Numberformat.Format = "#0\\.00%";
                var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);
                var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex + 1);

                rowIndex = 1;
                ws.Cells[rowIndex++, columnIndex].Value = "同比";
                ws.Column(columnIndex).Style.Font.Color.SetColor(Color.DimGray);
                var fmlRowIdx = 2;
                foreach (var model in dataStructs)
                {
                    ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF({0}{2}=0,\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                }

                #endregion
            }
        }

        private ExcelDataStructModel GetGuoDataStructModel(string fieldName, List<ExcelDataStructModel> guoDataStucts)
        {
            return guoDataStucts.FirstOrDefault(s => s.fieldname == fieldName);
        }

        private List<ass_and_cap_entity> GetAssetAndCapitalList(string tscode, List<ExcelDataStructModel> guoDataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetAssAndCapSql(tscode, guoDataStucts);
                return conn.Query<ass_and_cap_entity>(sql).ToList();
            }
        }

        private List<excel_bal_entity> GetEMBalTableDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.BalanceSheet);
                return conn.Query<excel_bal_entity>(sql).ToList();
            }
        }

        private List<excel_inc_entity> GetEMIncTableDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.Income);
                return conn.Query<excel_inc_entity>(sql).ToList();
            }
        }

        private List<excel_cf_entity> GetEMCFTableDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.Cashflow);
                return conn.Query<excel_cf_entity>(sql).ToList();
            }
        }

        private List<ZhangAnalysisV1Model> GetZhangAnalysisDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetZhangAnalysisSql(tscode, dataStucts);
                return conn.Query<ZhangAnalysisV1Model>(sql).ToList();
            }
        }

        private void BuildAssAndCapExcelFields(List<ExcelDataStructModel> dataStucts)
        {
            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "ass_and_cap_excel.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            var dicFieldNames = new List<string>();
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("GB2312")))
            {
                var count = 0;
                foreach (var model in dataStucts)
                {
                    count++;
                    if (count == 1)
                        continue;

                    if (!string.IsNullOrEmpty(model.fieldname))
                    {
                        if (dicFieldNames.Contains(model.fieldname))
                        {
                            sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = \"\";//" + model.fieldname_cn);
                            continue;
                        }

                        sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = data." + model.fieldname + ";//" + model.fieldname_cn);
                        dicFieldNames.Add(model.fieldname);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.formula))
                            sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Formula = string.Format(\"" + model.formula + "\", colName);//" + model.fieldname_cn);
                        else
                            sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = \"\";//" + model.fieldname_cn);
                    }
                }
            }
        }

        private void BuildCommonStockExcelFields(List<ExcelDataStructModel> dataStucts, ReportType reportType)
        {
            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "comm_rep_" + reportType.ToReportTypeName() + "_excel.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            var dicFieldNames = new List<string>();
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("GB2312")))
            {
                foreach (var model in dataStucts)
                {
                    if (!string.IsNullOrEmpty(model.fieldname))
                    {
                        sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = data." + model.fieldname + ";//" + model.fieldname_cn);
                        if (model.font_style == "h")
                            sw.WriteLine("ws.Row(rowIndex-1).Style.Font.Bold = true;");
                        dicFieldNames.Add(model.fieldname);
                    }
                    else
                    {
                        sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = \"\";//" + model.fieldname_cn);
                    }
                }
            }


            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "comm_rep_" + reportType.ToReportTypeName() + "_entity_excel.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("GB2312")))
            {
                foreach (var model in dataStucts)
                {
                    if (!string.IsNullOrEmpty(model.fieldname))
                    {
                        sw.WriteLine("public decimal? " + model.fieldname + " {get;set;}");
                    }
                }
            }

        }

        private void BuildZhangAnalysisExcelFields(List<ExcelDataStructModel> dataStucts)
        {
            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "zhang_anaysis_fields.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            var dicFieldNames = new List<string>();
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("GB2312")))
            {
                foreach (var model in dataStucts)
                {
                    if (!string.IsNullOrEmpty(model.fieldname))
                    {
                        sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = data." + model.fieldname + ";//" + model.fieldname_cn);
                        if (model.font_style == "h")
                            sw.WriteLine("ws.Row(rowIndex-1).Style.Font.Bold = true;");
                        dicFieldNames.Add(model.fieldname);
                    }
                    else
                    {
                        sw.WriteLine("ws.Cells[rowIndex++, columnIndex].Value = \"\";//" + model.fieldname_cn);
                    }
                }
            }


            fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "zhang_anaysis_entity.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("GB2312")))
            {
                foreach (var model in dataStucts)
                {
                    if (!string.IsNullOrEmpty(model.fieldname))
                    {
                        sw.WriteLine("public decimal? " + model.fieldname + " {get;set;}");
                    }
                }
            }

        }

        private void BuildAssAndCapEntity(List<ExcelDataStructModel> guoDataStucts)
        {
            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "ass_and_cap_entity.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            var dicFieldNames = new List<string>();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("public string name{get;set;}");
                sw.WriteLine("public DateTime reportdate{get;set;}");

                foreach (var model in guoDataStucts)
                {
                    if (string.IsNullOrEmpty(model.fieldname) || dicFieldNames.Contains(model.fieldname))
                        continue;

                    sw.WriteLine("public decimal? " + model.fieldname + "{get;set;}");
                    dicFieldNames.Add(model.fieldname);
                }
            }
        }

        private string GetAssAndCapSql(string tscode, List<ExcelDataStructModel> guoDataStucts)
        {
            var dateType = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;

            var dicFieldNames = new List<string>();
            string sql = "SELECT s.name,b.report_date,{1} from em_bal_common_v1 b left join xq_stock s on b.secucode=s.ts_code where s.ts_code='{0}' and month(b.report_date)=" + dateType + " order by b.report_date desc";

            if (dateType == "12")
            {
                sql = "SELECT s.name,b.report_date,{1} from em_bal_common_v1 b left join xq_stock s on b.secucode=s.ts_code where s.ts_code='{0}' and (month(b.report_date)=" + dateType + " or b.report_date='2021-6-30') order by b.report_date desc";// or b.REPORTDATE='2020-9-30'
            }

            var sbSql = new StringBuilder();
            foreach (var model in guoDataStucts)
            {
                if (string.IsNullOrEmpty(model.fieldname) || dicFieldNames.Contains(model.fieldname))
                    continue;

                if (!string.IsNullOrEmpty(model.sqlexpr))
                    sbSql.AppendFormat("convert_to_yiyuan({0}) {1},", model.sqlexpr, model.fieldname);
                else
                    sbSql.AppendFormat("convert_to_yiyuan({0}) {1},", model.fieldname, model.fieldname);

                dicFieldNames.Add(model.fieldname);
            }

            //Logger.Info(string.Format(sql, tscode, sbSql.ToString().TrimEnd(',')));

            return string.Format(sql, tscode, sbSql.ToString().TrimEnd(','));
        }

        private string GetCommonStockReportSql(string tscode, List<ExcelDataStructModel> dataStucts, ReportType reportType)
        {
            var dateType = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;

            var dicFieldNames = new List<string>();
            string sql = "SELECT s.name,b.report_date,{1} from " + GetCommonStockTableName(reportType) + " b left join xq_stock s on b.secucode=s.ts_code where s.ts_code='{0}' and $reporttype$ order by b.report_date desc";

            if (dateType == "12")
                sql = sql.Replace("$reporttype$", "(month(b.report_date)=12)");// or b.reportdate='2020-9-30'
            else
                sql = sql.Replace("$reporttype$", "month(b.report_date)=" + dateType);

            StringBuilder sbSql = new StringBuilder();
            foreach (var model in dataStucts)
            {
                if (string.IsNullOrEmpty(model.fieldname) || dicFieldNames.Contains(model.fieldname))
                    continue;

                sbSql.AppendFormat("convert_to_yiyuan({0}) {1},", model.fieldname, model.fieldname);

                dicFieldNames.Add(model.fieldname);
            }

            Logger.Info(string.Format(sql, tscode, sbSql.ToString().TrimEnd(',')));

            return string.Format(sql, tscode, sbSql.ToString().TrimEnd(','));
        }

        private string GetZhangAnalysisSql(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            var dateType = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;
            var isLatest5years = cbxIsLatest5years.Checked;
            var dicFieldNames = new List<string>();
            //-ifnull(ic.rdexp,0) cf.subsidiarypay ic.minorityincome
            string sql = @"select s.name,b.report_date,calc_proportion(ifnull(b.short_loan,0)+ifnull(b.noncurrent_liab_1year,0)+ifnull(b.trade_finliab_notfvtpl,0)+ifnull(b.long_loan,0)+ifnull(b.bond_payable,0)+ifnull(b.long_payable,0)+ifnull(b.perpetual_bond,0),b.total_assets) ass_fin_liab_ratio,
convert_to_yiyuan(ic.operate_income-ic.operate_cost) gross_profit,calc_proportion(ic.operate_income-ic.operate_cost,ic.operate_income) gross_margin,calc_proportion(ic.sale_expense,ic.operate_income) saleexp_ratio,calc_proportion(ic.manage_expense,ic.operate_income) manageexp_ratio,calc_proportion(ic.research_expense,ic.operate_income) rdexp_ratio,convert_to_yiyuan(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) core_profit,
calc_proportion(ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0),ic.operate_income) core_profit_margin,
(case when (ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0))<>0 then divide(cf.netcash_operate,ifnull(ic.operate_income,0)-ifnull(ic.operate_cost,0)-ifnull(ic.operate_tax_add,0)-ifnull(ic.sale_expense,0)-ifnull(ic.manage_expense,0)-ifnull(ic.finance_expense,0)) else null end) core_profit_hxl,
calc_proportion(ic.deduct_parent_netprofit,ic.operate_income) kfjll,convert_to_yiyuan((b.total_assets+b2.total_assets)/2) avg_sumassert,
divide(ic.operate_income,(b.total_assets+b2.total_assets)/2) sumasset_tor,convert_to_yiyuan((b.fixed_asset+b2.fixed_asset)/2) avg_fixedasset,
divide(ic.operate_income,(b.fixed_asset+b2.fixed_asset)/2) fixedasset_tor,convert_to_yiyuan((b.inventory+b2.inventory)/2) avg_inventory,
divide(ic.operate_cost,(b.inventory+b2.inventory)/2) inventory_tor,
convert_to_yiyuan(ifnull(b.monetaryfunds,0)+ifnull(b.trade_finasset_notfvtpl,0)+ifnull(b.other_equity_invest,0)+ifnull(b.other_noncurrent_finasset,0)+ifnull(b.available_sale_finasset,0)+ifnull(b.lend_fund,0)) finasset,
convert_to_yiyuan(cf.pay_all_tax) cf_taxpay,
convert_to_yiyuan(case when (ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0))<0 then ifnull(cf.assign_dividend_porfit,0) else (ifnull(cf.assign_dividend_porfit,0)-ifnull(cf.subsidiary_pay_dividend,0)-ifnull(m.bonus,0)) end ) cf_diviprofitorintpay,
convert_to_yiyuan(m.bonus) cf_bonus,
convert(ic.deduct_parent_netprofit/((b.total_parent_equity+b2.total_parent_equity)/2)*100,decimal(18,2)) roe,
convert_to_yiyuan(IFNULL(b.contract_liab,0)+IFNULL(b.advance_receivables,0)) advancereceive,
{1} from em_bal_common_v1 b 
LEFT JOIN em_a_mainindex m on m.ts_code = b.secucode and m.date = b.report_date
LEFT JOIN em_inc_common_v1 ic on ic.secucode = b.secucode and ic.report_date = b.report_date
LEFT JOIN em_cf_common_v1 cf on cf.secucode = b.secucode and cf.report_date = b.report_date
LEFT JOIN em_bal_common_v1 b2 on b2.secucode=b.secucode and b2.report_date=DATE_ADD(b.report_date,INTERVAL '-1' YEAR)
left join xq_stock s on b.secucode=s.ts_code where s.ts_code='{0}' and $reporttype$ and $isLatest5years$ order by b.report_date desc";

            if (dateType == "12")
                sql = sql.Replace("$reporttype$", "(month(b.report_date)=12)");
            //sql = sql.Replace("$reporttype$", "(month(b.REPORTDATE)=12 or b.reportdate='2020-9-30')");
            else
                sql = sql.Replace("$reporttype$", "month(b.report_date)=" + dateType);

            if (isLatest5years)
                sql = sql.Replace("$isLatest5years$", "b.report_date>='2015-12-31'");

            var sbSql = new StringBuilder();
            foreach (var model in dataStucts)
            {
                if (string.IsNullOrEmpty(model.fieldname) || dicFieldNames.Contains(model.fieldname) || string.IsNullOrEmpty(model.alias))
                    continue;

                sbSql.AppendFormat("convert_to_yiyuan({0}{1}) {2},", model.alias, model.fieldname, model.fieldname);

                dicFieldNames.Add(model.fieldname);
            }

            Logger.Info(string.Format(sql, tscode, sbSql.ToString().TrimEnd(',')));

            return string.Format(sql, tscode, sbSql.ToString().TrimEnd(','));
        }

        private List<ExcelDataStructModel> GetGuoDataStructs()
        {
            var datalist = new List<ExcelDataStructModel>();
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "ass_cap_struct.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                string sheetName = "资产资本表";
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 1; row <= rowCount; row++)
                {
                    datalist.Add(new ExcelDataStructModel
                    {
                        fieldname = worksheet.Cells[row, 1].Value.ToEmptyString(),
                        fieldname_cn = worksheet.Cells[row, 2].Value.ToEmptyString(),
                        formula = worksheet.Cells[row, 4].Value.ToEmptyString(),
                        data_type = worksheet.Cells[row, 5].Value.ToEmptyString(),
                        font_style = worksheet.Cells[row, 6].Value.ToEmptyString(),
                        sqlexpr = worksheet.Cells[row, 7].Value.ToEmptyString()
                    });
                }
            }

            return datalist;
        }

        private List<ExcelDataStructModel> GetCommonStockDataStructs(string sheetName)
        {
            var datalist = new List<ExcelDataStructModel>();
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "excel_analysis_struct.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 1; row <= rowCount; row++)
                {
                    datalist.Add(new ExcelDataStructModel
                    {
                        fieldname = worksheet.Cells[row, 1].Value.ToEmptyString(),
                        fieldname_cn = worksheet.Cells[row, 2].Value.ToEmptyString(),
                        font_style = worksheet.Cells[row, 3].Value.ToEmptyString()
                    });
                }
            }

            return datalist;
        }

        private List<ExcelDataStructModel> GetZhangAnalysisDataStructs()
        {
            var datalist = new List<ExcelDataStructModel>();
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "excel_analysis_struct.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["张氏分析"];

                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                for (int row = 1; row <= rowCount; row++)
                {
                    datalist.Add(new ExcelDataStructModel
                    {
                        fieldname = worksheet.Cells[row, 1].Value.ToEmptyString(),
                        fieldname_cn = worksheet.Cells[row, 2].Value.ToEmptyString(),
                        font_style = worksheet.Cells[row, 3].Value.ToEmptyString(),
                        alias = worksheet.Cells[row, 4].Value.ToEmptyString()
                    });
                }
            }

            return datalist;
        }

        private string GetCommonStockTableName(ReportType reportType)
        {
            string tableName = string.Empty;
            switch (reportType)
            {
                case ReportType.BalanceSheet:
                    tableName = "em_bal_common_v1";
                    break;
                case ReportType.Income:
                    tableName = "em_inc_common_v1";
                    break;
                case ReportType.Cashflow:
                    tableName = "em_cf_common_v1";
                    break;
            }

            return tableName;
        }

        #endregion

        private void link_excel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(_ass_excel_fllepath))
                System.Diagnostics.Process.Start(_ass_excel_fllepath);
        }

        private void cbxReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            link_excel.Visible = false;
            ShowExcelLink();
        }
    }
}
