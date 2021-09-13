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

namespace NiceShot.DotNetWinFormsClient
{
    public partial class GenerateAssAndCapExcelForm : Form
    {

        private string _tscode;
        private string _ass_excel_fllepath;
        public GenerateAssAndCapExcelForm(string tscode)
        {
            InitializeComponent();

            WinFormHelper.InitChildWindowStyle(this);
            this._tscode = tscode;
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

        }

        private void btnGengerate_Click(object sender, EventArgs e)
        {
            BuildAllWookSheet(this._tscode);
        }

        #region 生成资产资本表

        private void BuildAllWookSheet(string tscode)
        {
            link_excel.Visible = false;

            new EMCrawler(new EMCrawlerCriteria { SecurityCode = tscode }).CrawlFinIndustryData();
            new EMCrawler(new EMCrawlerCriteria { SecurityCode = tscode }).CrawlAMainIndexData();
            //new XQCrawler(new XQCrawlerCriteria { SecurityCode = tscode,Cookie= "" }).CrawlXQFinBalanceData();
            //new THSCrawler(new THSCrawlerCriteria { SecurityCode = tscode }).CrawlCashflowAdditionalData();

            var str_datetype = (cbxReportType.SelectedItem as AutoCompleteNameAndValue).Value;

            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

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

                ws.Cells[rowIndex++, columnIndex].Value = data.reportdate.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}3:{0}20)", colName);//1.1金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.monetaryfund;//货币资金
                ws.Cells[rowIndex++, columnIndex].Value = data.fvaluefasset;//以公允价值计量且其变动计入当期损益的金融资产+债务工具投资
                ws.Cells[rowIndex++, columnIndex].Value = data.TRADEFASSET;//交易性金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.definefvaluefasset;//衍生金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.interestrec;//应收利息
                ws.Cells[rowIndex++, columnIndex].Value = data.dividendrec;//应收股利
                ws.Cells[rowIndex++, columnIndex].Value = data.buysellbackfasset;//买入返售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.clheldsaleass;//划分为持有待售的资产
                ws.Cells[rowIndex++, columnIndex].Value = data.loanadvances;//发放贷款和垫款
                ws.Cells[rowIndex++, columnIndex].Value = data.saleablefasset;//可供出售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.otherequity_invest;//其他权益工具投资
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlfinasset;//其他非流动金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.heldmaturityinv;//持有至到期投资
                ws.Cells[rowIndex++, columnIndex].Value = data.estateinvest;//投资性房地产
                ws.Cells[rowIndex++, columnIndex].Value = data.sellbuybackfasset;//卖出回购金融资产款
                ws.Cells[rowIndex++, columnIndex].Value = "";//其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.totalfinasset).ToString()) ? data.otherlasset : null;//其他流动资产(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_expinoncurrassettype == ((int)expinoncurrassettype.totalfinasset).ToString()) ? data.nonlassetoneyear : null;//一年内到期的非流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.ltequityinv;//1.2长期股权投资
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}23-{0}35", colName);//1.3营运资本需求=1.3.1-1.3.2
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}24:{0}34)", colName);//1.3.1营运资产
                ws.Cells[rowIndex++, columnIndex].Value = data.billrec;//应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountrec;//应收账款+债权投资
                ws.Cells[rowIndex++, columnIndex].Value = data.contractasset;//合同资产
                ws.Cells[rowIndex++, columnIndex].Value = data.advancepay;//预付款项
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//存货
                ws.Cells[rowIndex++, columnIndex].Value = data.ltrec;//长期应收款
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincometaxasset;//递延所得税资产(营运)
                ws.Cells[rowIndex++, columnIndex].Value = (str_expinoncurrassettype == ((int)expinoncurrassettype.subtotaloperasset).ToString()) ? data.nonlassetoneyear : null;//一年内到期的非流动资产(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.subtotaloperasset).ToString()) ? data.otherlasset : null;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.subtotaloperasset).ToString()) ? data.otherlliab : null;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.otherrec;//其他应收款(常)
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}36:{0}50)", colName);//1.3.2营运负债
                ws.Cells[rowIndex++, columnIndex].Value = data.definefvaluefliab;//衍生金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.billpay;//应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountpay;//应付账款+持有待售负债
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//预收款项+合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.salarypay;//应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.taxpay;//应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincome;//递延收益(-流动负债)
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincometaxliab;//递延所得税负债(营运)
                ws.Cells[rowIndex++, columnIndex].Value = data.ltsalarypay;//长期应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.otherpay;//其他应付款(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.deposit;//吸收存款及同业存放
                ws.Cells[rowIndex++, columnIndex].Value = data.anticipateliab;//预计负债(常)
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurrassetype == ((int)othercurrassetype.totaloperliab).ToString()) ? data.otherlasset : null;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.totaloperliab).ToString()) ? data.otherlliab : null;//其他流动负债(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.specialpay;//专项应付款
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}52:{0}63)", colName);//1.4长期经营资产
                ws.Cells[rowIndex++, columnIndex].Value = data.fixedasset;//固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.constructionprogress;//在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.constructionmaterial;//工程物资
                ws.Cells[rowIndex++, columnIndex].Value = data.liquidatefixedasset;//固定资产清理
                ws.Cells[rowIndex++, columnIndex].Value = data.productbiologyasset;//生产性生物资产
                ws.Cells[rowIndex++, columnIndex].Value = data.oilgasasset;//油气资产
                ws.Cells[rowIndex++, columnIndex].Value = data.intangibleasset;//无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.developexp;//开发支出
                ws.Cells[rowIndex++, columnIndex].Value = data.goodwill;//商誉
                ws.Cells[rowIndex++, columnIndex].Value = data.ltdeferasset;//长期待摊费用
                ws.Cells[rowIndex++, columnIndex].Value = "";//递延所得税资产-递延所得税负债
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlasset;//其他非流动资产
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}22+{0}51", colName);//经营资产合计=1.3+1.4
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}2+{0}21+{0}64", colName);//资产总额=1.1+1.2+1.3+1.4
                ws.Cells[rowIndex++, columnIndex].Value = "";//(二)资本结构
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}68:{0}76)", colName);//2.1短期债务
                ws.Cells[rowIndex++, columnIndex].Value = data.stborrow;//短期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.tradefliab;//交易性金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlliaboneyear;//一年内到期的非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.interestpay;//应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.borrowfund;//拆入资金
                ws.Cells[rowIndex++, columnIndex].Value = data.fvaluefliab;//以公允价值计量且其变动计入当期损益的金融负债
                ws.Cells[rowIndex++, columnIndex].Value = "";//其他应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.anticipatelliab;//预计负债
                ws.Cells[rowIndex++, columnIndex].Value = (str_othercurreliabitype == ((int)othercurreliabitype.totalshorttermliab).ToString()) ? data.otherlliab : null;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}78:{0}84)", colName);//2.2长期债务
                ws.Cells[rowIndex++, columnIndex].Value = data.ltborrow;//长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bondpay;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.ltaccountpay;//长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.sustainabledebt;//永续债
                ws.Cells[rowIndex++, columnIndex].Value = "";//专项应付款
                ws.Cells[rowIndex++, columnIndex].Value = "";//吸收存款及同业存放(常)
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlliab;//其他非流动负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}67+{0}77", colName);//有息负债=2.1+2.2
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUBTOTAL(9,{0}91:{0}93)", colName);//2.3股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.sharecapital;//实收资本（或股本）
                ws.Cells[rowIndex++, columnIndex].Value = data.capitalreserve;//资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.surplusreserve;//盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.retainedearning;//未分配利润
                ws.Cells[rowIndex++, columnIndex].Value = data.sumparentequity;//归属于母公司股东权益合计
                ws.Cells[rowIndex++, columnIndex].Value = data.minorityequity;//少数股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.dividendpay;//应付股利
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}67+{0}77+{0}86", colName);//资本总额=2.1+2.2+2.3

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
                foreach (var model in guoDataStructs)
                {
                    ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF(OR({0}{2}=0,AND({0}{2}<0,{1}{2}>0),,AND({0}{2}>0,{1}{2}<0)),\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
                    //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("IF({0}{2}=0,\"\",IFERROR(({0}{2}-{1}{2})/{1}{2}*100,\"\"))", colName1, colName2, fmlRowIdx++);
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

                ws.Cells[rowIndex++, columnIndex].Value = data.reportdate.Value.ToString("yyyy/MM/dd") + "-" + data.name;
                sbCashflowSummary.AppendLine("公司名称：" + data.name + "，报表日期：" + data.reportdate.Value.Year + "年");
                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = "";//一、资产负债表分析
                ws.Cells[rowIndex++, columnIndex].Value = "";//（一）考察企业资产总额的规模变化
                ws.Cells[rowIndex++, columnIndex].Value = data.sumasset;//   总资产
                ws.Cells[rowIndex++, columnIndex].Value = "";//（二）考察支撑企业资产的四大动力的变化情况

                //ws.Cells[rowIndex++, columnIndex].Value = "";//经营性负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}7:{0}11)", colName);//经营性负债

                ws.Cells[rowIndex++, columnIndex].Value = data.billpay;//应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountpay;//应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//预收款项
                //ws.Cells[rowIndex++, columnIndex].Value = data.contractliab;//合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.salarypay;//应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.taxpay;//应交税费

                //ws.Cells[rowIndex++, columnIndex].Value = "";//金融性负债
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}13:{0}20)", colName);//金融性负债

                ws.Cells[rowIndex++, columnIndex].Value = data.stborrow;//短期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.interestpay;//应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.tradefliab;//交易性金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlliaboneyear;//一年内到期的非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.ltborrow;//长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bondpay;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.ltaccountpay;//长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.sustainabledebt;//永续债

                //ws.Cells[rowIndex++, columnIndex].Value = "";//股东入资
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}22:{0}24)", colName);//股东入资

                ws.Cells[rowIndex++, columnIndex].Value = data.sharecapital;//实收资本
                ws.Cells[rowIndex++, columnIndex].Value = data.capitalreserve;//资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.minorityequity;//少数股东权益

                //ws.Cells[rowIndex++, columnIndex].Value = "";//利润积累
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}26:{0}27)", colName);//利润积累

                ws.Cells[rowIndex++, columnIndex].Value = data.surplusreserve;// 盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.retainedearning;// 未分配利润
                //ws.Cells[rowIndex++, columnIndex].Value = "";//
                ws.Cells[rowIndex++, columnIndex].Value = "";//（三）考察资产结构的变化
                ws.Cells[rowIndex++, columnIndex].Value = "";//企业从事生产经营活动的基础
                ws.Cells[rowIndex++, columnIndex].Value = data.fixedasset;//固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.constructionprogress;//在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.intangibleasset;//无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.ltequityinv;//长期股权投资
                ws.Cells[rowIndex++, columnIndex].Value = "";//考察流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.finasset;// 金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;// 存货
                ws.Cells[rowIndex++, columnIndex].Value = data.contractasset;// 合同资产
                ws.Cells[rowIndex++, columnIndex].Value = data.billrec;// 应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountrec;// 应收账款（与销售有关的商业债权）
                ws.Cells[rowIndex++, columnIndex].Value = data.advancepay;// 预付款项（与购买有关的商业债权）
                ws.Cells[rowIndex++, columnIndex].Value = data.otherrec;//其他应收款
                //ws.Cells[rowIndex++, columnIndex].Value = data.otherpay;//其他应付款
                //ws.Cells[rowIndex++, columnIndex].Value = "";//
                ws.Cells[rowIndex++, columnIndex].Value = "";//（四）考察企业营运资本管理状况
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营性流动资产的核心项目
                ws.Cells[rowIndex++, columnIndex].Value = data.billrec;//应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountrec;//应收账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancepay;//预付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//存货
                ws.Cells[rowIndex++, columnIndex].Value = data.otherrec;//其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = "";//经营性流动负债的核心项目
                ws.Cells[rowIndex++, columnIndex].Value = data.billpay;// 应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountpay;// 应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;// 预收款项
                //ws.Cells[rowIndex++, columnIndex].Value = data.contractliab;// 合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.salarypay;// 应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.taxpay;// 应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.otherpay;//其他应付款
                //ws.Cells[rowIndex++, columnIndex].Value = "";//
                ws.Cells[rowIndex++, columnIndex].Value = "";//（五）考察企业的融资潜力
                ws.Cells[rowIndex++, columnIndex].Value = data.ass_fin_liab_ratio;//资产金融负债率
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                //ws.Cells[rowIndex++, columnIndex].Value = "";//
                ws.Cells[rowIndex++, columnIndex].Value = "";//二、利润表分析
                ws.Cells[rowIndex++, columnIndex].Value = data.operatereve;//营业收入
                ws.Cells[rowIndex++, columnIndex].Value = data.operateexp;//营业成本
                ws.Cells[rowIndex++, columnIndex].Value = data.rotar;//总资产报酬率
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.gross_profit;//毛利润
                ws.Cells[rowIndex++, columnIndex].Value = data.gross_margin;//毛利率
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.saleexp;//销售费用
                ws.Cells[rowIndex++, columnIndex].Value = data.saleexp_ratio;//销售费用率
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.manageexp;//管理费用
                ws.Cells[rowIndex++, columnIndex].Value = data.manageexp_ratio;//管理费用率
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.rdexp;//研发费用
                ws.Cells[rowIndex++, columnIndex].Value = data.rdexp_ratio;//研发费用率
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit;//核心利润
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit_margin;//核心利润率
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.netoperatecashflow;//经营活动现金流净额
                ws.Cells[rowIndex++, columnIndex].Value = data.core_profit_hxl;//核心利润获现率
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                //ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.investincome;//投资收益
                ws.Cells[rowIndex++, columnIndex].Value = data.nonoperatereve;//其他收益：营业外收入
                ws.Cells[rowIndex++, columnIndex].Value = data.roe;//ROE
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.kcfjcxsyjlr;//扣非净利润
                ws.Cells[rowIndex++, columnIndex].Value = data.kfjll;//扣非净利率
                ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_sumassert;//平均总资产
                ws.Cells[rowIndex++, columnIndex].Value = data.sumasset_tor;//总资产周转率
                //ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_fixedasset;//平均固定资产原值
                ws.Cells[rowIndex++, columnIndex].Value = data.fixedasset_tor;//固定资产周转率
                //ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";
                ws.Cells[rowIndex++, columnIndex].Value = data.avg_inventory;//平均存货
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory_tor;//存货周转率
                //ws.Cells[rowIndex - 1, columnIndex].Style.Numberformat.Format = "#0\\.00%";

                ws.Cells[rowIndex++, columnIndex].Value = "";//三、现金流动分析
                ws.Cells[rowIndex++, columnIndex].Value = data.acceptinvrec;//1.1.找股东要钱：吸收投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.loanrec;//1.2.找银行借钱：取得借款收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.issuebondrec;//1.3.发行债券收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.otherfinarec;//1.4.收到其他与筹资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}86:{0}89)", colName);//筹资合计
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                sbCashflowSummary.AppendLine("筹资："+(data.acceptinvrec.GetValueOrDefault() + data.loanrec.GetValueOrDefault() + data.issuebondrec.GetValueOrDefault() + data.otherfinarec.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.buyfilassetpay;//2.1.要花钱了：购建固定资产、无形资产和其他长期资产支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.employeepay;//2.2.要雇人了：支付给职工以及为职工支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.buygoodsservicepay;//2.3.1倒买倒卖：购买商品、接受劳务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.otheroperatepay;//2.3.2支付其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}91:{0}94)", colName);//投资经营流出量合计
                sbCashflowSummary.AppendLine("投资、经营流出量：" + (data.buyfilassetpay.GetValueOrDefault() + data.employeepay.GetValueOrDefault() + data.buygoodsservicepay.GetValueOrDefault() + data.otheroperatepay.GetValueOrDefault()) + " 亿元");
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex++, columnIndex].Value = "";//PS：筹资流入量支持投资流出量、经营流出量
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                ws.Cells[rowIndex++, columnIndex].Value = data.salegoodsservicerec;//3.1.1卖出商品：销售商品、提供劳务收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.otheroperaterec;//3.1.2收到其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("SUM({0}97:{0}98)", colName);//销售回款合计
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                sbCashflowSummary.AppendLine("销售回款：" + (data.salegoodsservicerec.GetValueOrDefault() + data.otheroperaterec.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.cf_taxpay;//3.2.交税：支付的各项税费
                sbCashflowSummary.AppendLine("交税：" + (data.cf_taxpay.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.cf_diviprofitorintpay;//3.3.支付利息：分配股利、利润或偿付利息支付的现金
                sbCashflowSummary.AppendLine("支付利息和分红：" + (data.cf_diviprofitorintpay.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.repaydebtpay;//3.4.还有钱，还债：偿还债务支付的现金
                ws.Row(rowIndex - 1).Style.Font.Color.SetColor(Color.FromArgb(199, 21, 133));
                sbCashflowSummary.AppendLine("偿还债务：" + (data.repaydebtpay.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.cf_bonus;//3.5.还有钱，分红
                ws.Cells[rowIndex++, columnIndex].Value = data.invpay;//3.6.还有钱，设立子公司：投资支付的现金
                sbCashflowSummary.AppendLine("投资其他公司：" + (data.invpay.GetValueOrDefault()) + " 亿元");
                ws.Cells[rowIndex++, columnIndex].Value = data.disposalinvrec;//3.7.子公司还款：收回投资收到的现金
                sbCashflowSummary.AppendLine("其他公司还款：" + (data.disposalinvrec.GetValueOrDefault()) +" 亿元");

                ws.Cells[rowIndex++, columnIndex].Value = ""; 
 
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}98+{0}97-{0}94-{0}93", colName);//真实毛利润
                //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}91+{0}92+{0}100+{0}101+{0}103", colName);//所有支出
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}92+{0}100+{0}101", colName);//所有支出
                ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}107-{0}108", colName);//纯利润
                //ws.Cells[rowIndex++, columnIndex].Formula = string.Format("{0}107-{0}108+{0}104", colName);//纯利润

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
                else
                {
                    columnIndex++;
                    ws.Column(columnIndex).Width = 8;

                    rowIndex = 1;
                    ws.Cells[rowIndex++, columnIndex].Value = "变化";

                    var valColName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex * 2);
                    var colName1 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - dataList.Count * 2 + 1);
                    var colName2 = StringUtils.ConvertExcelColumnIndexToChar(columnIndex - 1);

                    ws.Cells[4, columnIndex].Formula = string.Format("{0}4-{1}4", colName1, colName2);
                    ws.Cells[6, columnIndex].Formula = string.Format("{0}6-{1}6", colName1, colName2);
                    ws.Cells[12, columnIndex].Formula = string.Format("{0}12-{1}12", colName1, colName2);
                    ws.Cells[21, columnIndex].Formula = string.Format("{0}21-{1}21", colName1, colName2);
                    ws.Cells[25, columnIndex].Formula = string.Format("{0}25-{1}25", colName1, colName2);

                    ws.Cells[30, columnIndex].Formula = string.Format("{0}30-{1}30", colName1, colName2);
                    ws.Cells[31, columnIndex].Formula = string.Format("{0}31-{1}31", colName1, colName2);
                    ws.Cells[32, columnIndex].Formula = string.Format("{0}32-{1}32", colName1, colName2);
                    ws.Cells[33, columnIndex].Formula = string.Format("{0}33-{1}33", colName1, colName2);

                    ws.Cells[35, columnIndex].Formula = string.Format("{0}35-{1}35", colName1, colName2);
                    ws.Cells[36, columnIndex].Formula = string.Format("{0}36-{1}36", colName1, colName2);

                    ws.Cells[39, columnIndex].Formula = string.Format("{0}39-{1}39", colName1, colName2);

                    ws.Cells[41, columnIndex].Formula = string.Format("{0}41-{1}41", colName1, colName2);
                    ws.Cells[44, columnIndex].Formula = string.Format("{0}44-{1}44", colName1, colName2);
                    ws.Cells[45, columnIndex].Formula = string.Format("{0}45-{1}45", colName1, colName2);
                    ws.Cells[47, columnIndex].Formula = string.Format("{0}47-{1}47", colName1, colName2);
                    ws.Cells[48, columnIndex].Formula = string.Format("{0}48-{1}48", colName1, colName2);

                    ws.Cells[50, columnIndex].Formula = string.Format("{0}50-{1}50", colName1, colName2);
                    ws.Cells[51, columnIndex].Formula = string.Format("{0}51-{1}51", colName1, colName2);
                    ws.Cells[52, columnIndex].Formula = string.Format("{0}52-{1}52", colName1, colName2);
                    ws.Cells[55, columnIndex].Formula = string.Format("{0}55-{1}55", colName1, colName2);

                }

                #endregion
            }

            IOHelper.CreateFileInFilesDir("cashflow-" + tscode + ".txt",sbCashflowSummary.ToString());
        }

        private void BindBalanceSheetData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetCSBalanceSheetDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.reportdate.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = data.monetaryfund;//货币资金
                ws.Cells[rowIndex++, columnIndex].Value = data.fvaluefasset;//以公允价值计量且其变动计入当期损益的金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.tradefasset;//交易性金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.definefvaluefasset;//衍生金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.billrec;//应收票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountrec;//应收账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancepay;//预付款项
                ws.Cells[rowIndex++, columnIndex].Value = data.interestrec;//应收利息
                ws.Cells[rowIndex++, columnIndex].Value = data.dividendrec;//应收股利
                ws.Cells[rowIndex++, columnIndex].Value = data.otherrec;//其他应收款
                ws.Cells[rowIndex++, columnIndex].Value = data.buysellbackfasset;//买入返售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.inventory;//存货
                ws.Cells[rowIndex++, columnIndex].Value = data.contractasset;//合同资产
                ws.Cells[rowIndex++, columnIndex].Value = data.clheldsaleass;//划分为持有待售的资产
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlassetoneyear;//一年内到期的非流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.otherlasset;//其他流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.sumlasset;//流动资产合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.loanadvances;//发放贷款和垫款
                ws.Cells[rowIndex++, columnIndex].Value = data.saleablefasset;//可供出售金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.otherequity_invest;//其他权益工具投资
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlfinasset;//其他非流动金融资产
                ws.Cells[rowIndex++, columnIndex].Value = data.heldmaturityinv;//持有至到期投资
                ws.Cells[rowIndex++, columnIndex].Value = data.ltrec;//长期应收款
                ws.Cells[rowIndex++, columnIndex].Value = data.ltequityinv;//长期股权投资
                ws.Cells[rowIndex++, columnIndex].Value = data.estateinvest;//投资性房地产
                ws.Cells[rowIndex++, columnIndex].Value = data.fixedasset;//固定资产
                ws.Cells[rowIndex++, columnIndex].Value = data.constructionprogress;//在建工程
                ws.Cells[rowIndex++, columnIndex].Value = data.constructionmaterial;//工程物资
                ws.Cells[rowIndex++, columnIndex].Value = data.liquidatefixedasset;//固定资产清理
                ws.Cells[rowIndex++, columnIndex].Value = data.productbiologyasset;//生产性生物资产
                ws.Cells[rowIndex++, columnIndex].Value = data.oilgasasset;//油气资产
                ws.Cells[rowIndex++, columnIndex].Value = data.intangibleasset;//无形资产
                ws.Cells[rowIndex++, columnIndex].Value = data.developexp;//开发支出
                ws.Cells[rowIndex++, columnIndex].Value = data.goodwill;//商誉
                ws.Cells[rowIndex++, columnIndex].Value = data.ltdeferasset;//长期待摊费用
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincometaxasset;//递延所得税资产
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlasset;//其他非流动资产
                ws.Cells[rowIndex++, columnIndex].Value = data.sumnonlasset;//非流动资产合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.sumasset;//资产总计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.stborrow;//短期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.deposit;//吸收存款及同业存放
                ws.Cells[rowIndex++, columnIndex].Value = data.borrowfund;//拆入资金
                ws.Cells[rowIndex++, columnIndex].Value = data.fvaluefliab;//以公允价值计量且其变动计入当期损益的金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.tradefliab;//交易性金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.definefvaluefliab;//衍生金融负债
                ws.Cells[rowIndex++, columnIndex].Value = data.billpay;//应付票据
                ws.Cells[rowIndex++, columnIndex].Value = data.accountpay;//应付账款
                ws.Cells[rowIndex++, columnIndex].Value = data.advancereceive;//预收款项
                ws.Cells[rowIndex++, columnIndex].Value = data.contractliab;//合同负债
                ws.Cells[rowIndex++, columnIndex].Value = data.sellbuybackfasset;//卖出回购金融资产款
                ws.Cells[rowIndex++, columnIndex].Value = data.salarypay;//应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.taxpay;//应交税费
                ws.Cells[rowIndex++, columnIndex].Value = data.interestpay;//应付利息
                ws.Cells[rowIndex++, columnIndex].Value = data.dividendpay;//应付股利
                ws.Cells[rowIndex++, columnIndex].Value = data.otherpay;//其他应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.anticipatelliab;//预计负债
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlliaboneyear;//一年内到期的非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.otherlliab;//其他流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.sumlliab;//流动负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.ltborrow;//长期借款
                ws.Cells[rowIndex++, columnIndex].Value = data.bondpay;//应付债券
                ws.Cells[rowIndex++, columnIndex].Value = data.ltaccountpay;//长期应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.ltsalarypay;//长期应付职工薪酬
                ws.Cells[rowIndex++, columnIndex].Value = data.specialpay;//专项应付款
                ws.Cells[rowIndex++, columnIndex].Value = data.anticipateliab;//预计负债
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincome;//递延收益
                ws.Cells[rowIndex++, columnIndex].Value = data.deferincometaxliab;//递延所得税负债
                ws.Cells[rowIndex++, columnIndex].Value = data.othernonlliab;//其他非流动负债
                ws.Cells[rowIndex++, columnIndex].Value = data.sumnonlliab;//非流动负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.sumliab;//负债合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.sharecapital;//实收资本（或股本）
                ws.Cells[rowIndex++, columnIndex].Value = data.otherequity;//其他权益工具
                ws.Cells[rowIndex++, columnIndex].Value = data.preferredstock;//优先股
                ws.Cells[rowIndex++, columnIndex].Value = data.sustainabledebt;//永续债
                ws.Cells[rowIndex++, columnIndex].Value = data.capitalreserve;//资本公积
                ws.Cells[rowIndex++, columnIndex].Value = data.surplusreserve;//盈余公积
                ws.Cells[rowIndex++, columnIndex].Value = data.retainedearning;//未分配利润
                ws.Cells[rowIndex++, columnIndex].Value = data.sumparentequity;//归属于母公司股东权益合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.minorityequity;//少数股东权益
                ws.Cells[rowIndex++, columnIndex].Value = data.sumshequity;//股东权益合计
                ws.Cells[rowIndex++, columnIndex].Value = data.sumliabshequity;//负债和股东权益合计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;

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

        private void BindIncomeData(string tscode, List<ExcelDataStructModel> dataStructs, ExcelWorksheet ws)
        {
            var dataList = GetCSIncomeDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.reportdate.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = data.totaloperatereve;//营业总收入
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.operatereve;//营业收入
                ws.Cells[rowIndex++, columnIndex].Value = data.totaloperateexp;//营业总成本
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.operateexp;//营业成本
                ws.Cells[rowIndex++, columnIndex].Value = data.rdexp;//研发费用
                ws.Cells[rowIndex++, columnIndex].Value = data.operatetax;//营业税金及附加
                ws.Cells[rowIndex++, columnIndex].Value = data.saleexp;//销售费用
                ws.Cells[rowIndex++, columnIndex].Value = data.manageexp;//管理费用
                ws.Cells[rowIndex++, columnIndex].Value = data.financeexp;//财务费用
                ws.Cells[rowIndex++, columnIndex].Value = data.assetdevalueloss;//资产减值损失
                ws.Cells[rowIndex++, columnIndex].Value = data.fvalueincome;//加：公允价值变动收益
                ws.Cells[rowIndex++, columnIndex].Value = data.investincome;//投资收益
                ws.Cells[rowIndex++, columnIndex].Value = data.investjointincome;//其中：对联营企业和合营企业的投资收益
                ws.Cells[rowIndex++, columnIndex].Value = data.operateprofit;//营业利润
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.nonoperatereve;//加：营业外收入
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlassetreve;//其中：非流动资产处置利得
                ws.Cells[rowIndex++, columnIndex].Value = data.nonoperateexp;//减：营业外支出
                ws.Cells[rowIndex++, columnIndex].Value = data.nonlassetnetloss;//其中：非流动资产处置净损失
                ws.Cells[rowIndex++, columnIndex].Value = data.sumprofit;//利润总额
                ws.Cells[rowIndex++, columnIndex].Value = data.incometax;//减：所得税费用
                ws.Cells[rowIndex++, columnIndex].Value = data.netprofit;//净利润
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.parentnetprofit;//其中：归属于母公司股东的净利润
                ws.Cells[rowIndex++, columnIndex].Value = data.minorityincome;//少数股东损益
                ws.Cells[rowIndex++, columnIndex].Value = data.kcfjcxsyjlr;//扣除非经常性损益后的净利润

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
            var dataList = GetCSCashflowDataList(tscode, dataStructs);
            var columnIndex = 1;
            for (var i = 0; i < dataList.Count; i++)
            {
                var rowIndex = 1;

                var data = dataList[i];

                //添加每年报表数据
                columnIndex++;
                ws.Column(columnIndex).Width = 12;
                var colName = StringUtils.ConvertExcelColumnIndexToChar(columnIndex);

                ws.Cells[rowIndex++, columnIndex].Value = data.reportdate.Value.ToString("yyyy/MM/dd") + "-" + data.name;

                //数据列
                #region 数据列

                ws.Cells[rowIndex++, columnIndex].Value = data.salegoodsservicerec;//销售商品、提供劳务收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.intandcommrec;//收取利息、手续费及佣金的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.taxreturnrec;//收到的税费返还
                ws.Cells[rowIndex++, columnIndex].Value = data.otheroperaterec;//收到其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.sumoperateflowin;//经营活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.buygoodsservicepay;//购买商品、接受劳务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.intandcommpay;//支付利息、手续费及佣金的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.employeepay;//支付给职工以及为职工支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.taxpay;//支付的各项税费
                ws.Cells[rowIndex++, columnIndex].Value = data.otheroperatepay;//支付其他与经营活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.sumoperateflowout;//经营活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netoperatecashflow;//经营活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.disposalinvrec;//收回投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.invincomerec;//取得投资收益收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.dispfilassetrec;//处置固定资产、无形资产和其他长期资产收回的现金净额
                ws.Cells[rowIndex++, columnIndex].Value = data.dispsubsidiaryrec;//处置子公司及其他营业单位收到的现金净额
                ws.Cells[rowIndex++, columnIndex].Value = data.otherinvrec;//收到其他与投资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.suminvflowin;//投资活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.buyfilassetpay;//购建固定资产、无形资产和其他长期资产支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.invpay;//投资支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.getsubsidiarypay;//取得子公司及其他营业单位支付的现金净额
                ws.Cells[rowIndex++, columnIndex].Value = data.otherinvpay;//支付其他与投资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.suminvflowout;//投资活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netinvcashflow;//投资活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.acceptinvrec;//吸收投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.subsidiaryaccept;//子公司吸收少数股东投资收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.loanrec;//取得借款收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.issuebondrec;//发行债券收到的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.otherfinarec;//收到其他与筹资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.sumfinaflowin;//筹资活动现金流入小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.repaydebtpay;//偿还债务支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.diviprofitorintpay;//分配股利、利润或偿付利息支付的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.subsidiarypay;//子公司支付给少数股东的股利、利润
                ws.Cells[rowIndex++, columnIndex].Value = data.otherfinapay;//支付其他与筹资活动有关的现金
                ws.Cells[rowIndex++, columnIndex].Value = data.sumfinaflowout;//筹资活动现金流出小计
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.netfinacashflow;//筹资活动产生的现金流量净额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;
                ws.Cells[rowIndex++, columnIndex].Value = data.effectexchangerate;//汇率变动对现金及现金等价物的影响
                ws.Cells[rowIndex++, columnIndex].Value = data.nicashequi;//现金及现金等价物净增加额
                ws.Cells[rowIndex++, columnIndex].Value = data.cashequibeginning;//加:期初现金及现金等价物余额
                ws.Cells[rowIndex++, columnIndex].Value = data.cashequiending;//期末现金及现金等价物余额
                ws.Row(rowIndex - 1).Style.Font.Bold = true;

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

        private List<AssetAndCapital> GetAssetAndCapitalList(string tscode, List<ExcelDataStructModel> guoDataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetAssAndCapSql(tscode, guoDataStucts);
                return conn.Query<AssetAndCapital>(sql).ToList();
            }
        }

        private List<BalanceSheet_Common> GetCSBalanceSheetDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.BalanceSheet);
                return conn.Query<BalanceSheet_Common>(sql).ToList();
            }
        }

        private List<Income_Common> GetCSIncomeDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.Income);
                return conn.Query<Income_Common>(sql).ToList();
            }
        }

        private List<Cashflow_Common> GetCSCashflowDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetCommonStockReportSql(tscode, dataStucts, ReportType.Cashflow);
                return conn.Query<Cashflow_Common>(sql).ToList();
            }
        }

        private List<ZhangAnalysisModel> GetZhangAnalysisDataList(string tscode, List<ExcelDataStructModel> dataStucts)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = GetZhangAnalysisSql(tscode, dataStucts);
                return conn.Query<ZhangAnalysisModel>(sql).ToList();
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
            string sql = "SELECT s.name,b.reportdate,{1} from em_balancesheet_common b left join xq_stock s on b.SECURITYCODE=s.ts_code where s.ts_code='{0}' and month(b.REPORTDATE)=" + dateType + " order by b.REPORTDATE desc";

            if (dateType=="12")
            {
                 sql = "SELECT s.name,b.reportdate,{1} from em_balancesheet_common b left join xq_stock s on b.SECURITYCODE=s.ts_code where s.ts_code='{0}' and (month(b.REPORTDATE)=" + dateType + ") order by b.REPORTDATE desc";// or b.REPORTDATE='2020-9-30'
            }

            StringBuilder sbSql = new StringBuilder();
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
            string sql = "SELECT s.name,b.reportdate,{1} from " + GetCommonStockTableName(reportType) + " b left join xq_stock s on b.SECURITYCODE=s.ts_code where s.ts_code='{0}' and $reporttype$ order by b.REPORTDATE desc";

            if (dateType == "12")
                sql = sql.Replace("$reporttype$", "(month(b.REPORTDATE)=12)");// or b.reportdate='2020-9-30'
            else
                sql = sql.Replace("$reporttype$", "month(b.REPORTDATE)=" + dateType);

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
            string sql = @"SELECT s.name,b.reportdate,calc_proportion(ifnull(b.stborrow,0)+ifnull(b.nonlliaboneyear,0)+ifnull(b.tradefliab,0)+ifnull(b.ltborrow,0)+ifnull(b.bondpay,0)+ifnull(b.ltaccountpay,0)+ifnull(b.sustainabledebt,0),b.SUMASSET) ass_fin_liab_ratio,convert_to_yiyuan(ic.operatereve-ic.operateexp) gross_profit,calc_proportion(ic.operatereve-ic.operateexp,ic.operatereve) gross_margin,calc_proportion(ic.saleexp,ic.operatereve) saleexp_ratio,calc_proportion(ic.manageexp,ic.operatereve) manageexp_ratio,calc_proportion(ic.rdexp,ic.operatereve) rdexp_ratio,convert_to_yiyuan(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.FINANCEEXP,0)) core_profit,
calc_proportion(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.FINANCEEXP,0),ic.operatereve) core_profit_margin,(case when (ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.FINANCEEXP,0))<>0 then divide(cf.netoperatecashflow,(ifnull(ic.operatereve,0)-ifnull(ic.operateexp,0)-ifnull(ic.operatetax,0)-ifnull(ic.saleexp,0)-ifnull(ic.manageexp,0)-ifnull(ic.financeexp,0))) else null end) core_profit_hxl,calc_proportion(ic.kcfjcxsyjlr,ic.operatereve) kfjll,convert_to_yiyuan((b.SUMASSET+b2.SUMASSET)/2) avg_sumassert,divide(ic.operatereve,(b.SUMASSET+b2.SUMASSET)/2) sumasset_tor,convert_to_yiyuan((b.fixedasset+b2.fixedasset)/2) avg_fixedasset,divide(ic.operatereve,(b.fixedasset+b2.fixedasset)/2) fixedasset_tor,convert_to_yiyuan((b.inventory+b2.inventory)/2) avg_inventory,divide(ic.operateexp,(b.inventory+b2.inventory)/2) inventory_tor,convert_to_yiyuan(ifnull(b.MONETARYFUND,0)+ifnull(b.TRADEFASSET,0)+ifnull(b.otherequity_invest,0)+ifnull(b.othernonlfinasset,0)+ifnull(b.SALEABLEFASSET,0)) finasset,convert_to_yiyuan(cf.taxpay) cf_taxpay,convert_to_yiyuan(case when (ifnull(cf.diviprofitorintpay,0)-ifnull(cf.subsidiarypay,0)-ifnull(m.bonus,0))<0 then ifnull(cf.diviprofitorintpay,0) else (ifnull(cf.diviprofitorintpay,0)-ifnull(cf.subsidiarypay,0)-ifnull(m.bonus,0)) end ) cf_diviprofitorintpay,convert_to_yiyuan(m.bonus) cf_bonus,convert(ic.KCFJCXSYJLR/((b.sumparentequity+b2.sumparentequity)/2)*100,decimal(18,2)) roe,
convert_to_yiyuan(IFNULL(b.ADVANCERECEIVE,0)+IFNULL(b.CONTRACTLIAB,0)) advancereceive,calc_proportion((ifnull(ic.sumprofit,0)+ifnull(ic.incometax,0)+ifnull(ic.interestexp,0)),((b.SUMASSET+b2.SUMASSET)/2)) rotar,
{1} from em_balancesheet_common b 
LEFT JOIN em_a_mainindex m on m.ts_code = b.securitycode and m.date = b.reportdate
LEFT JOIN em_income_common ic on ic.SECURITYCODE = b.securitycode and ic.REPORTDATE = b.reportdate
LEFT JOIN em_cashflow_common cf on cf.SECURITYCODE = b.securitycode and cf.REPORTDATE = b.reportdate
LEFT JOIN em_balancesheet_common b2 on b2.SECURITYCODE=b.securitycode and b2.REPORTDATE=DATE_ADD(b.REPORTDATE,INTERVAL '-1' YEAR)
left join xq_stock s on b.SECURITYCODE=s.ts_code where s.ts_code='{0}' and $reporttype$ and $isLatest5years$ order by b.REPORTDATE desc";

            if (dateType == "12")
                sql = sql.Replace("$reporttype$", "(month(b.REPORTDATE)=12)");
            //sql = sql.Replace("$reporttype$", "(month(b.REPORTDATE)=12 or b.reportdate='2020-9-30')");
            else
                sql = sql.Replace("$reporttype$", "month(b.REPORTDATE)=" + dateType);

            if (isLatest5years)
                sql = sql.Replace("$isLatest5years$", "b.reportdate>='2015-12-31'");

            StringBuilder sbSql = new StringBuilder();
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
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "资产资本表_new.xlsx");

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
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "普通股数据结构.xlsx");

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
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "普通股数据结构.xlsx");

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
                    tableName = "em_balancesheet_common";
                    break;
                case ReportType.Income:
                    tableName = "em_income_common";
                    break;
                case ReportType.Cashflow:
                    tableName = "em_cashflow_common";
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
    }
}
