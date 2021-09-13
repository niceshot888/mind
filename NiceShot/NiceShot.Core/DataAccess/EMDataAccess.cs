using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.Core.Models.JsonObjects;
using NiceShot.Core.Models.Structs;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NiceShot.Core.DataAccess
{
    public class EMDataAccess
    {
        #region 三张财务报表基本方法

        /// <summary>
        /// 获取财务报表数据库表名称
        /// </summary>
        public static string GetReportTableName(ReportType reportType, CompanyType compType)
        {
            string modelname = string.Empty;
            string tablename = string.Empty;
            if (reportType == ReportType.BalanceSheet)
                modelname = "BalanceSheet";
            else if (reportType == ReportType.Income)
                modelname = "Income";
            else if (reportType == ReportType.Cashflow)
                modelname = "Cashflow";

            if (compType == CompanyType.Broker)
                tablename = "em_" + modelname.ToLower() + "_broker";
            else if (compType == CompanyType.Insurance)
                tablename = "em_" + modelname.ToLower() + "_insurance";
            else if (compType == CompanyType.Bank)
                tablename = "em_" + modelname.ToLower() + "_bank";
            else if (compType == CompanyType.Common)
                tablename = "em_" + modelname.ToLower() + "_common";

            return tablename;
        }

        /// <summary>
        /// 根据财务报表类型和公司类型查询表数据字段
        /// </summary>
        /// <param name="reportType">报表类型</param>
        /// <param name="compType">公司类型</param>
        public static List<DataField> GetReportDataFields(ReportType reportType, CompanyType compType)
        {
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "财务报表结构.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                string sheetName = GetWorkSheetName(reportType);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                var dataList = new List<DataField>();
                for (int row = 2; row <= rowCount; row++)
                {
                    DataField dataField = new DataField();

                    int beginIndex = ((int)compType - 1) * 3;

                    dataField.FieldName = worksheet.Cells[row, beginIndex + 1].Value.ToEmptyString();
                    dataField.DataType = worksheet.Cells[row, beginIndex + 2].Value.ToEmptyString();
                    dataField.CnFieldName = worksheet.Cells[row, beginIndex + 3].Value.ToEmptyString();

                    if (string.IsNullOrEmpty(dataField.FieldName))
                        break;
                    dataList.Add(dataField);
                }

                return dataList;
            }
        }

        /// <summary>
        /// 获取EXCEL表格名称
        /// </summary>
        /// <param name="reportType">报表类型</param>
        private static string GetWorkSheetName(ReportType reportType)
        {
            string sheetName = "";
            switch (reportType)
            {
                case ReportType.BalanceSheet:
                    sheetName = "资产负债表";
                    break;
                case ReportType.Income:
                    sheetName = "利润表";
                    break;
                case ReportType.Cashflow:
                    sheetName = "现金流量表";
                    break;
            }
            return sheetName;
        }

        #endregion

        #region 生成三张财务报表代码

        /// <summary>
        /// 不建议使用
        /// </summary>
        private static void DeleteAllTruncatedData()
        {
            for (int reportType = 1; reportType <= 3; reportType++)
            {
                for (int compType = 1; compType <= 4; compType++)
                {
                    DeleteTruncatedData((ReportType)reportType, (CompanyType)compType);
                }
            }
        }

        private static void DeleteTruncatedData(ReportType reportType, CompanyType companyType)
        {
            var tableName = GetReportTableName(reportType, companyType);
            var sql = @"truncate table {0}";
            sql = string.Format(sql, tableName);
            Logger.Info(sql);
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                conn.Execute(sql);
            }
        }

        private static long GetMinTruncatedId(ReportType reportType, CompanyType companyType)
        {
            var tableName = GetReportTableName(reportType, companyType);
            var sql = @"select min(b1.id) minid from {0} b1 where (b1.SECURITYCODE,b1.REPORTDATE) in 
(select SECURITYCODE, REPORTDATE from {0} GROUP BY SECURITYCODE, REPORTDATE HAVING count(*) > 1)
and b1.id not in (select min(id) from {0} GROUP BY SECURITYCODE,REPORTDATE HAVING count(*) > 1)";
            sql = string.Format(sql, tableName);
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                return conn.ExecuteScalar<long>(sql);
            }
        }

        public static void BuildAllReportCode()
        {
            for (int reportType = 1; reportType <= 3; reportType++)
            {
                for (int compType = 1; compType <= 4; compType++)
                {
                    BuildReportJsonObjectCode((ReportType)reportType, (CompanyType)compType);
                    BuildReportEntityCode((ReportType)reportType, (CompanyType)compType);
                }
            }
        }

        /// <summary>
        /// 生成三表JsonObject类
        /// </summary>
        private static void BuildReportJsonObjectCode(ReportType reportType, CompanyType companyType)
        {
            var tableName = GetReportTableName(reportType, companyType);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + "_jo.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("using Newtonsoft.Json;");
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("namespace NiceShot.Core.Models.JsonObjects{");
                sw.WriteLine("[JsonObject]");
                sw.WriteLine($"public class {tableName}_jo{{");
                var dataFieldList = GetReportDataFields(reportType, companyType);
                foreach (var field in dataFieldList)
                {
                    string dataType = "string";
                    if (field.DataType == "varchar")
                        dataType = "string";
                    else if (field.DataType == "tinyint")
                        dataType = "int";
                    else if (field.DataType == "date")
                        dataType = "string";
                    sw.WriteLine($"public {dataType} {field.FieldName} {{get;set;}}");
                }
                sw.WriteLine("}}");
            }
        }

        /// <summary>
        /// 生成操作财务报表SQL代码
        /// </summary>
        public static void BuildOperReportSQLCode()
        {
            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", "em_all_rep_oper.cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int reportType = 1; reportType <= 3; reportType++)
                {
                    for (int compType = 1; compType <= 4; compType++)
                    {
                        var tmpl_oper = GetOperReportSQLCode((ReportType)reportType, (CompanyType)compType);
                        sw.WriteLine(tmpl_oper);
                    }
                }

            }
        }

        private static string GetOperReportSQLCode(ReportType reportType, CompanyType companyType)
        {
            var tableName = GetReportTableName(reportType, companyType);

            var tmpl_oper = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "oper_em_findata.txt"));
            tmpl_oper = tmpl_oper.Replace("$tablename$", tableName);
            tmpl_oper = tmpl_oper.Replace("$insert_sql$", GetReportInsertSql(reportType, companyType, new string[] { }));
            tmpl_oper = tmpl_oper.Replace("$update_sql$", GetReportUpdateSql(reportType, companyType, new string[] { }));
            tmpl_oper = tmpl_oper.Replace("$assign_data$", GetAssignEntityCode(reportType, companyType, new string[] { }));

            return tmpl_oper;
        }

        /// <summary>
        /// 生成三表数据库实体类
        /// </summary>
        private static void BuildReportEntityCode(ReportType reportType, CompanyType companyType)
        {
            var tableName = GetReportTableName(reportType, companyType);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + ".cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("using Newtonsoft.Json;");
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("namespace NiceShot.Core.Models.Entities{");

                sw.WriteLine($"public class {tableName}{{");
                sw.WriteLine("public long id {get;set;}");
                var dataFieldList = GetReportDataFields(reportType, companyType);
                foreach (var field in dataFieldList)
                {
                    string dataType = "decimal?";
                    if (field.DataType == "varchar")
                        dataType = "string";
                    else if (field.DataType == "tinyint")
                        dataType = "int";
                    else if (field.DataType == "date")
                        dataType = "DateTime?";
                    sw.WriteLine($"public {dataType} {field.FieldName.ToLower()} {{get;set;}}");
                }
                sw.WriteLine("}}");
            }
        }

        private static string GetAssignEntityCode(ReportType reportType, CompanyType companyType, string[] ignoreFields)
        {
            StringBuilder sw = new StringBuilder();
            var dataFieldList = GetReportDataFields(reportType, companyType);
            foreach (var field in dataFieldList)
            {
                if (field.FieldName != "id" && ignoreFields.Contains(field.FieldName.ToLower()))
                    continue;

                if (field.DataType == "date")
                    sw.AppendLine($"{field.FieldName.ToLower()} = jo.{field.FieldName}.ConvertToDate(),");
                else if (field.DataType == "varchar" || field.DataType == "tinyint")
                    sw.AppendLine($"{field.FieldName.ToLower()} = jo.{field.FieldName},");
                else
                    sw.AppendLine($"{field.FieldName.ToLower()} = jo.{field.FieldName}.ConvertToDecimal(),");
            }
            return sw.ToString();
        }

        private static string GetReportInsertSql(ReportType reportType, CompanyType companyType, string[] ignoreFields)
        {
            var tableName = GetReportTableName(reportType, companyType);

            StringBuilder sw = new StringBuilder();

            var dataFieldList = GetReportDataFields(reportType, companyType);

            sw.Append("insert into " + tableName);

            StringBuilder sbFields = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();
            foreach (var field in dataFieldList)
            {
                if (ignoreFields.Contains(field.FieldName.ToLower()))
                    continue;

                sbFields.AppendFormat(field.FieldName.ToLower() + ",");
                sbValues.AppendFormat("@" + field.FieldName.ToLower() + ",");
            }

            sw.Append(" (" + sbFields.ToString().TrimEnd(',') + ")");
            sw.Append(" values (" + sbValues.ToString().TrimEnd(',') + ")");

            return sw.ToString();
        }

        private static string GetReportUpdateSql(ReportType reportType, CompanyType companyType, string[] ignoreFields)
        {
            var tableName = GetReportTableName(reportType, companyType);
            StringBuilder sw = new StringBuilder();

            var dataFieldList = GetReportDataFields(reportType, companyType);

            sw.Append("update " + tableName + " set ");

            StringBuilder sbFields = new StringBuilder();
            foreach (var field in dataFieldList)
            {
                if (ignoreFields.Contains(field.FieldName.ToLower()))
                    continue;

                sbFields.AppendFormat("{0}=@{0},", field.FieldName.ToLower());
            }

            sw.Append(" " + sbFields.ToString().TrimEnd(','));
            sw.Append(" where id=@id");
            return sw.ToString();
        }

        #endregion

        #region 主要财务指标

        public static bool IsExistsMainIndexData(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select count(1) from em_a_mainindex where ts_code='" + ts_code + "' and date='" + date + "'";
                return conn.ExecuteScalar<int>(sql) > 0;
            }
        }

        public static em_mainindex GetAMainIndexData(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_a_mainindex where ts_code='" + ts_code + "' and date='" + date + "'";
                return conn.Query<em_mainindex>(sql).FirstOrDefault();
            }
        }

        public static em_mainindex GetHKMainIndexData(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_hk_mainindex where ts_code='" + ts_code + "' and date='" + date + "'";
                return conn.Query<em_mainindex>(sql).FirstOrDefault();
            }
        }

        public static void OperateHKMainIndexData(string ts_code, em_hk_mainindex_item_jo jo)
        {
            try
            {
                em_mainindex mainindex = GetHKMainIndexData(ts_code, jo.date);
                if (mainindex != null)
                {
                    //修改数据
                    Logger.Info(string.Format("update data: tscode={0};date={1}", ts_code, jo.date));
                    string sql = "update em_hk_mainindex set yyzsr=@yyzsr,gsjlr=@gsjlr,kfjlr=@kfjlr,yyzsrtbzz=@yyzsrtbzz,gsjlrtbzz=@gsjlrtbzz,kfjlrtbzz=@kfjlrtbzz,jqjzcsyl=@jqjzcsyl,tbjzcsyl=@tbjzcsyl,tbzzcsyl=@tbzzcsyl,mll=@mll,jll=@jll where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            yyzsr = jo.yyzsr.ConvertToUnitYuan(),
                            gsjlr = jo.gsjlr.ConvertToUnitYuan(),
                            kfjlr = jo.kfjlr.ConvertToUnitYuan(),
                            yyzsrtbzz = jo.yyzsrtbzz.Round2Decimals(),
                            gsjlrtbzz = jo.gsjlrtbzz.Round2Decimals(),
                            kfjlrtbzz = jo.kfjlrtbzz.Round2Decimals(),
                            jqjzcsyl = jo.jqjzcsyl.Round2Decimals(),
                            tbjzcsyl = jo.tbjzcsyl.Round2Decimals(),
                            tbzzcsyl = jo.tbzzcsyl.Round2Decimals(),
                            mll = jo.mll.Round2Decimals(),
                            jll = jo.jll.Round2Decimals(),
                            id = mainindex.id
                        });
                    }
                }
                else
                {
                    //添加数据
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", ts_code, jo.date));

                    string sql = "insert into em_hk_mainindex(yyzsr,gsjlr,kfjlr,yyzsrtbzz,gsjlrtbzz,kfjlrtbzz,jqjzcsyl,tbjzcsyl,tbzzcsyl,mll,jll,ts_code,date) values (@yyzsr,@gsjlr,@kfjlr,@yyzsrtbzz,@gsjlrtbzz,@kfjlrtbzz,@jqjzcsyl,@tbjzcsyl,@tbzzcsyl,@mll,@jll,@ts_code,@date)";

                    em_mainindex entity = new em_mainindex()
                    {
                        yyzsr = jo.yyzsr.ConvertToUnitYuan(),
                        gsjlr = jo.gsjlr.ConvertToUnitYuan(),
                        kfjlr = jo.kfjlr.ConvertToUnitYuan(),
                        yyzsrtbzz = jo.yyzsrtbzz.Round2Decimals(),
                        gsjlrtbzz = jo.gsjlrtbzz.Round2Decimals(),
                        kfjlrtbzz = jo.kfjlrtbzz.Round2Decimals(),
                        jqjzcsyl = jo.jqjzcsyl.Round2Decimals(),
                        tbjzcsyl = jo.tbjzcsyl.Round2Decimals(),
                        tbzzcsyl = jo.tbzzcsyl.Round2Decimals(),
                        mll = jo.mll.Round2Decimals(),
                        jll = jo.jll.Round2Decimals(),
                        ts_code = ts_code,
                        date = jo.date.ConvertToDate(),
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync hk finance main index error: tscode={0};date={1},详细：{2}", ts_code, jo.date, ex));
            }

        }

        public static void OperateAMainIndexData(string ts_code, em_mainindex_jo jo, bool includeUpdate)
        {
            try
            {
                em_mainindex mainindex = GetAMainIndexData(ts_code, jo.date);
                if (mainindex != null)
                {
                    if (!includeUpdate)
                        return;

                    //修改数据
                    Logger.Info(string.Format("update data: tscode={0};date={1}", ts_code, jo.date));
                    string sql = "update em_a_mainindex set yyzsr=@yyzsr,gsjlr=@gsjlr,kfjlr=@kfjlr,yyzsrtbzz=@yyzsrtbzz,gsjlrtbzz=@gsjlrtbzz,kfjlrtbzz=@kfjlrtbzz,jqjzcsyl=@jqjzcsyl,tbjzcsyl=@tbjzcsyl,tbzzcsyl=@tbzzcsyl,mll=@mll,jll=@jll where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            yyzsr = jo.yyzsr.ConvertToUnitYuan(),
                            gsjlr = jo.gsjlr.ConvertToUnitYuan(),
                            kfjlr = jo.kfjlr.ConvertToUnitYuan(),
                            yyzsrtbzz = jo.yyzsrtbzz.Round2Decimals(),
                            gsjlrtbzz = jo.gsjlrtbzz.Round2Decimals(),
                            kfjlrtbzz = jo.kfjlrtbzz.Round2Decimals(),
                            jqjzcsyl = jo.jqjzcsyl.Round2Decimals(),
                            tbjzcsyl = jo.tbjzcsyl.Round2Decimals(),
                            tbzzcsyl = jo.tbzzcsyl.Round2Decimals(),
                            mll = jo.mll.Round2Decimals(),
                            jll = jo.jll.Round2Decimals(),
                            id = mainindex.id
                        });
                    }
                }
                else
                {
                    //添加数据
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", ts_code, jo.date));

                    string sql = "insert into em_a_mainindex(yyzsr,gsjlr,kfjlr,yyzsrtbzz,gsjlrtbzz,kfjlrtbzz,jqjzcsyl,tbjzcsyl,tbzzcsyl,mll,jll,ts_code,date) values (@yyzsr,@gsjlr,@kfjlr,@yyzsrtbzz,@gsjlrtbzz,@kfjlrtbzz,@jqjzcsyl,@tbjzcsyl,@tbzzcsyl,@mll,@jll,@ts_code,@date)";

                    em_mainindex entity = new em_mainindex()
                    {
                        yyzsr = jo.yyzsr.ConvertToUnitYuan(),
                        gsjlr = jo.gsjlr.ConvertToUnitYuan(),
                        kfjlr = jo.kfjlr.ConvertToUnitYuan(),
                        yyzsrtbzz = jo.yyzsrtbzz.Round2Decimals(),
                        gsjlrtbzz = jo.gsjlrtbzz.Round2Decimals(),
                        kfjlrtbzz = jo.kfjlrtbzz.Round2Decimals(),
                        jqjzcsyl = jo.jqjzcsyl.Round2Decimals(),
                        tbjzcsyl = jo.tbjzcsyl.Round2Decimals(),
                        tbzzcsyl = jo.tbzzcsyl.Round2Decimals(),
                        mll = jo.mll.Round2Decimals(),
                        jll = jo.jll.Round2Decimals(),
                        ts_code = ts_code,
                        date = jo.date.ConvertToDate(),
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync a stock finance main index error: tscode={0};date={1},详细：{2}", ts_code, jo.date, ex));
            }

        }

        #endregion

        #region 公司概况信息

        public static void OperateHKCompanySurveyData(string symbol, em_hk_company_profile_jo jo)
        {
            try
            {
                xq_stock stock = NiceShotDataAccess.GetStockInfo(symbol, MarketType.HK);
                if (stock != null)
                {
                    //修改数据
                    Logger.Info(string.Format("update hk company profile: symbol={0};", symbol));

                    var totalPinyin = PinYinConverterHelper.GetTotalPingYin(jo.gszl.sshy);
                    string pinyin = string.Join(",", totalPinyin.FirstPingYin);
                    string fullpinyin = string.Join(",", totalPinyin.TotalPingYin);

                    string sql = "update xq_stock set sshy=@sshy,sshy_py=@sshy_py,sshy_fullpy=@sshy_fullpy,sszjhhy=@sszjhhy,lssws=@lssws,kjssws=@kjssws,dm=@dm,dsz=@dsz,gsjj=@gsjj,jyfw=@jyfw,gyrs=@gyrs,glryrs=@glryrs where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            sshy = jo.gszl.sshy,
                            sshy_py = pinyin,
                            sshy_fullpy = fullpinyin,
                            kjssws = jo.gszl.hss,
                            dm = jo.gszl.gsms,
                            dsz = jo.gszl.dsz,
                            gsjj = jo.gszl.gsjs,
                            gyrs = jo.gszl.ygrs,
                            id = stock.id
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync hk company profile error: tscode={0},详细：{1}", symbol, ex));
            }
        }

        public static void OperateACompanySurveyData(em_company_survey_jo jo)
        {
            try
            {
                xq_stock stock = NiceShotDataAccess.GetStockInfo(jo.SecuCode, MarketType.A);
                if (stock != null)
                {
                    //修改数据
                    Logger.Info(string.Format("update compant survey data: tscode={0};", jo.SecuCode));

                    var totalPinyin = PinYinConverterHelper.GetTotalPingYin(jo.jbzl.sshy);
                    string pinyin = string.Join(",", totalPinyin.FirstPingYin);
                    string fullpinyin = string.Join(",", totalPinyin.TotalPingYin);

                    string sql = "update xq_stock set sshy=@sshy,sshy_py=@sshy_py,sshy_fullpy=@sshy_fullpy,sszjhhy=@sszjhhy,lssws=@lssws,kjssws=@kjssws,dm=@dm,dsz=@dsz,gsjj=@gsjj,jyfw=@jyfw,gyrs=@gyrs,glryrs=@glryrs where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            sshy = jo.jbzl.sshy,
                            sshy_py = pinyin,
                            sshy_fullpy = fullpinyin,
                            sszjhhy = jo.jbzl.sszjhhy,
                            lssws = jo.jbzl.lssws,
                            kjssws = jo.jbzl.kjssws,
                            dm = jo.jbzl.dm,
                            dsz = jo.jbzl.dsz,
                            gsjj = jo.jbzl.gsjj,
                            jyfw = jo.jbzl.jyfw,
                            gyrs = jo.jbzl.gyrs,
                            glryrs = jo.jbzl.glryrs,
                            id = stock.id
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync company survey data error: tscode={0},详细：{1}", jo.SecuCode, ex));
            }
        }

        public static void OperateLuGuTongZBData(string securitycode, em_lugutong_jo jo)
        {
            try
            {
                xq_stock stock = NiceShotDataAccess.GetStockInfo(securitycode, MarketType.A);
                if (stock != null)
                {
                    //修改数据
                    Logger.Info(string.Format("update lugutongzb data: tscode={0};", securitycode));

                    string sql = "update xq_stock set lugutong_zb=@lugutong_zb,lugutong_data=@lugutong_data where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            //lugutong_zb = jo.data[jo.data.Count-1],
                            lugutong_zb = jo.SHARESRATE,
                            lugutong_data = jo.SHAREHOLDPRICE,
                            id = stock.id
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync company survey data error: tscode={0},详细：{1}", securitycode, ex));
            }
        }

        #endregion

        #region 三张财务报表数据
        #region	em_balancesheet_broker
        public static em_balancesheet_broker get_em_balancesheet_broker_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_balancesheet_broker where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_balancesheet_broker>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_balancesheet_broker_data(em_balancesheet_broker_jo jo, bool includeUpdate)
        {
            try
            {
                em_balancesheet_broker edit_entity = get_em_balancesheet_broker_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_balancesheet_broker set  securitycode=@securitycode,reporttype=@reporttype,reportdatetype=@reportdatetype,type=@type,reportdate=@reportdate,currency=@currency,monetaryfund=@monetaryfund,clientfund=@clientfund,clientcreditfund=@clientcreditfund,settlementprovision=@settlementprovision,clientprovision=@clientprovision,creditprovision=@creditprovision,lendfund=@lendfund,marginoutfund=@marginoutfund,marginoutsecurity=@marginoutsecurity,fvaluefasset=@fvaluefasset,tradefasset=@tradefasset,definefvaluefasset=@definefvaluefasset,derivefasset=@derivefasset,buysellbackfasset=@buysellbackfasset,interestrec=@interestrec,dividendrec=@dividendrec,receivables=@receivables,gdepositpay=@gdepositpay,saleablefasset=@saleablefasset,heldmaturityinv=@heldmaturityinv,agencyassets=@agencyassets,ltequityinv=@ltequityinv,estateinvest=@estateinvest,fixedasset=@fixedasset,constructionprogress=@constructionprogress,intangibleasset=@intangibleasset,seatfee=@seatfee,goodwill=@goodwill,deferincometaxasset=@deferincometaxasset,otherasset=@otherasset,sumasset=@sumasset,stborrow=@stborrow,pledgeborrow=@pledgeborrow,borrowfund=@borrowfund,fvaluefliab=@fvaluefliab,tradefliab=@tradefliab,definefvaluefliab=@definefvaluefliab,derivefliab=@derivefliab,sellbuybackfasset=@sellbuybackfasset,agenttradesecurity=@agenttradesecurity,cagenttradesecurity=@cagenttradesecurity,agentuwsecurity=@agentuwsecurity,accountpay=@accountpay,salarypay=@salarypay,taxpay=@taxpay,interestpay=@interestpay,dividendpay=@dividendpay,shortfinancing=@shortfinancing,agencyliab=@agencyliab,anticipateliab=@anticipateliab,ltborrow=@ltborrow,bondpay=@bondpay,deferincometaxliab=@deferincometaxliab,otherliab=@otherliab,sumliab=@sumliab,sharecapital=@sharecapital,capitalreserve=@capitalreserve,inventoryshare=@inventoryshare,otherequity=@otherequity,preferredstock=@preferredstock,sustainabledebt=@sustainabledebt,otherequityother=@otherequityother,surplusreserve=@surplusreserve,generalriskprepare=@generalriskprepare,traderiskprepare=@traderiskprepare,retainedearning=@retainedearning,diffconversionfc=@diffconversionfc,sumparentequity=@sumparentequity,minorityequity=@minorityequity,sumshequity=@sumshequity,sumliabshequity=@sumliabshequity where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                reportdatetype = jo.REPORTDATETYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                currency = jo.CURRENCY,
                                monetaryfund = jo.MONETARYFUND.ConvertToDecimal(),
                                clientfund = jo.CLIENTFUND.ConvertToDecimal(),
                                clientcreditfund = jo.CLIENTCREDITFUND.ConvertToDecimal(),
                                settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal(),
                                clientprovision = jo.CLIENTPROVISION.ConvertToDecimal(),
                                creditprovision = jo.CREDITPROVISION.ConvertToDecimal(),
                                lendfund = jo.LENDFUND.ConvertToDecimal(),
                                marginoutfund = jo.MARGINOUTFUND.ConvertToDecimal(),
                                marginoutsecurity = jo.MARGINOUTSECURITY.ConvertToDecimal(),
                                fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                                tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                                definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                                derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                                buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                                interestrec = jo.INTERESTREC.ConvertToDecimal(),
                                dividendrec = jo.DIVIDENDREC.ConvertToDecimal(),
                                receivables = jo.RECEIVABLES.ConvertToDecimal(),
                                gdepositpay = jo.GDEPOSITPAY.ConvertToDecimal(),
                                saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                                heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                                agencyassets = jo.AGENCYASSETS.ConvertToDecimal(),
                                ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                                estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                                fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                                constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                                intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                                seatfee = jo.SEATFEE.ConvertToDecimal(),
                                goodwill = jo.GOODWILL.ConvertToDecimal(),
                                deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                                otherasset = jo.OTHERASSET.ConvertToDecimal(),
                                sumasset = jo.SUMASSET.ConvertToDecimal(),
                                stborrow = jo.STBORROW.ConvertToDecimal(),
                                pledgeborrow = jo.PLEDGEBORROW.ConvertToDecimal(),
                                borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                                fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                                tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                                definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                                derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                                sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                                agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                                cagenttradesecurity = jo.CAGENTTRADESECURITY.ConvertToDecimal(),
                                agentuwsecurity = jo.AGENTUWSECURITY.ConvertToDecimal(),
                                accountpay = jo.ACCOUNTPAY.ConvertToDecimal(),
                                salarypay = jo.SALARYPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                                dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                                shortfinancing = jo.SHORTFINANCING.ConvertToDecimal(),
                                agencyliab = jo.AGENCYLIAB.ConvertToDecimal(),
                                anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                                ltborrow = jo.LTBORROW.ConvertToDecimal(),
                                bondpay = jo.BONDPAY.ConvertToDecimal(),
                                deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                                otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                                sumliab = jo.SUMLIAB.ConvertToDecimal(),
                                sharecapital = jo.SHARECAPITAL.ConvertToDecimal(),
                                capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                                inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal(),
                                otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                                preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                                sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                                otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                                surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                                generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                                traderiskprepare = jo.TRADERISKPREPARE.ConvertToDecimal(),
                                retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                                diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                                sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                                minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                                sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                                sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_balancesheet_broker (securitycode,reporttype,reportdatetype,type,reportdate,currency,monetaryfund,clientfund,clientcreditfund,settlementprovision,clientprovision,creditprovision,lendfund,marginoutfund,marginoutsecurity,fvaluefasset,tradefasset,definefvaluefasset,derivefasset,buysellbackfasset,interestrec,dividendrec,receivables,gdepositpay,saleablefasset,heldmaturityinv,agencyassets,ltequityinv,estateinvest,fixedasset,constructionprogress,intangibleasset,seatfee,goodwill,deferincometaxasset,otherasset,sumasset,stborrow,pledgeborrow,borrowfund,fvaluefliab,tradefliab,definefvaluefliab,derivefliab,sellbuybackfasset,agenttradesecurity,cagenttradesecurity,agentuwsecurity,accountpay,salarypay,taxpay,interestpay,dividendpay,shortfinancing,agencyliab,anticipateliab,ltborrow,bondpay,deferincometaxliab,otherliab,sumliab,sharecapital,capitalreserve,inventoryshare,otherequity,preferredstock,sustainabledebt,otherequityother,surplusreserve,generalriskprepare,traderiskprepare,retainedearning,diffconversionfc,sumparentequity,minorityequity,sumshequity,sumliabshequity) values (@securitycode,@reporttype,@reportdatetype,@type,@reportdate,@currency,@monetaryfund,@clientfund,@clientcreditfund,@settlementprovision,@clientprovision,@creditprovision,@lendfund,@marginoutfund,@marginoutsecurity,@fvaluefasset,@tradefasset,@definefvaluefasset,@derivefasset,@buysellbackfasset,@interestrec,@dividendrec,@receivables,@gdepositpay,@saleablefasset,@heldmaturityinv,@agencyassets,@ltequityinv,@estateinvest,@fixedasset,@constructionprogress,@intangibleasset,@seatfee,@goodwill,@deferincometaxasset,@otherasset,@sumasset,@stborrow,@pledgeborrow,@borrowfund,@fvaluefliab,@tradefliab,@definefvaluefliab,@derivefliab,@sellbuybackfasset,@agenttradesecurity,@cagenttradesecurity,@agentuwsecurity,@accountpay,@salarypay,@taxpay,@interestpay,@dividendpay,@shortfinancing,@agencyliab,@anticipateliab,@ltborrow,@bondpay,@deferincometaxliab,@otherliab,@sumliab,@sharecapital,@capitalreserve,@inventoryshare,@otherequity,@preferredstock,@sustainabledebt,@otherequityother,@surplusreserve,@generalriskprepare,@traderiskprepare,@retainedearning,@diffconversionfc,@sumparentequity,@minorityequity,@sumshequity,@sumliabshequity)";
                    em_balancesheet_broker entity = new em_balancesheet_broker()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        reportdatetype = jo.REPORTDATETYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        currency = jo.CURRENCY,
                        monetaryfund = jo.MONETARYFUND.ConvertToDecimal(),
                        clientfund = jo.CLIENTFUND.ConvertToDecimal(),
                        clientcreditfund = jo.CLIENTCREDITFUND.ConvertToDecimal(),
                        settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal(),
                        clientprovision = jo.CLIENTPROVISION.ConvertToDecimal(),
                        creditprovision = jo.CREDITPROVISION.ConvertToDecimal(),
                        lendfund = jo.LENDFUND.ConvertToDecimal(),
                        marginoutfund = jo.MARGINOUTFUND.ConvertToDecimal(),
                        marginoutsecurity = jo.MARGINOUTSECURITY.ConvertToDecimal(),
                        fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                        tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                        definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                        derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                        buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                        interestrec = jo.INTERESTREC.ConvertToDecimal(),
                        dividendrec = jo.DIVIDENDREC.ConvertToDecimal(),
                        receivables = jo.RECEIVABLES.ConvertToDecimal(),
                        gdepositpay = jo.GDEPOSITPAY.ConvertToDecimal(),
                        saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                        heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                        agencyassets = jo.AGENCYASSETS.ConvertToDecimal(),
                        ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                        estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                        fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                        constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                        intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                        seatfee = jo.SEATFEE.ConvertToDecimal(),
                        goodwill = jo.GOODWILL.ConvertToDecimal(),
                        deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                        otherasset = jo.OTHERASSET.ConvertToDecimal(),
                        sumasset = jo.SUMASSET.ConvertToDecimal(),
                        stborrow = jo.STBORROW.ConvertToDecimal(),
                        pledgeborrow = jo.PLEDGEBORROW.ConvertToDecimal(),
                        borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                        fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                        tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                        definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                        derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                        sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                        agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                        cagenttradesecurity = jo.CAGENTTRADESECURITY.ConvertToDecimal(),
                        agentuwsecurity = jo.AGENTUWSECURITY.ConvertToDecimal(),
                        accountpay = jo.ACCOUNTPAY.ConvertToDecimal(),
                        salarypay = jo.SALARYPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                        dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                        shortfinancing = jo.SHORTFINANCING.ConvertToDecimal(),
                        agencyliab = jo.AGENCYLIAB.ConvertToDecimal(),
                        anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                        ltborrow = jo.LTBORROW.ConvertToDecimal(),
                        bondpay = jo.BONDPAY.ConvertToDecimal(),
                        deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                        otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                        sumliab = jo.SUMLIAB.ConvertToDecimal(),
                        sharecapital = jo.SHARECAPITAL.ConvertToDecimal(),
                        capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                        inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal(),
                        otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                        preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                        sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                        otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                        surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                        generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                        traderiskprepare = jo.TRADERISKPREPARE.ConvertToDecimal(),
                        retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                        diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                        sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                        minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                        sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                        sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }

                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }

            return false;
        }
        #endregion
        #region	em_balancesheet_insurance
        public static em_balancesheet_insurance get_em_balancesheet_insurance_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_balancesheet_insurance where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_balancesheet_insurance>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_balancesheet_insurance_data(em_balancesheet_insurance_jo jo, bool includeUpdate)
        {
            try
            {
                em_balancesheet_insurance edit_entity = get_em_balancesheet_insurance_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_balancesheet_insurance set  securitycode=@securitycode,reporttype=@reporttype,reportdatetype=@reportdatetype,type=@type,reportdate=@reportdate,currency=@currency,monetaryfund=@monetaryfund,settlementprovision=@settlementprovision,lendfund=@lendfund,fvaluefasset=@fvaluefasset,tradefasset=@tradefasset,definefvaluefasset=@definefvaluefasset,derivefasset=@derivefasset,buysellbackfasset=@buysellbackfasset,interestrec=@interestrec,premiumrec=@premiumrec,rirec=@rirec,ricontactreserverec=@ricontactreserverec,unduerireserverec=@unduerireserverec,claimrireserverec=@claimrireserverec,liferireserverec=@liferireserverec,lthealthrireserverec=@lthealthrireserverec,insuredpledgeloan=@insuredpledgeloan,loanadvances=@loanadvances,gdepositpay=@gdepositpay,creditorplaninv=@creditorplaninv,otherrec=@otherrec,tdeposit=@tdeposit,saleablefasset=@saleablefasset,heldmaturityinv=@heldmaturityinv,investrec=@investrec,accountrec=@accountrec,ltequityinv=@ltequityinv,capitalgdepositpay=@capitalgdepositpay,estateinvest=@estateinvest,fixedasset=@fixedasset,constructionprogress=@constructionprogress,intangibleasset=@intangibleasset,goodwill=@goodwill,deferincometaxasset=@deferincometaxasset,otherasset=@otherasset,independentasset=@independentasset,sumasset=@sumasset,stborrow=@stborrow,fideposit=@fideposit,gdepositrec=@gdepositrec,borrowfund=@borrowfund,fvaluefliab=@fvaluefliab,tradefliab=@tradefliab,definefvaluefliab=@definefvaluefliab,derivefliab=@derivefliab,sellbuybackfasset=@sellbuybackfasset,acceptdeposit=@acceptdeposit,agenttradesecurity=@agenttradesecurity,accountpay=@accountpay,billpay=@billpay,advancerec=@advancerec,premiumadvance=@premiumadvance,commpay=@commpay,ripay=@ripay,salarypay=@salarypay,taxpay=@taxpay,interestpay=@interestpay,dividendpay=@dividendpay,anticipateliab=@anticipateliab,claimpay=@claimpay,policydivipay=@policydivipay,otherpay=@otherpay,insureddepositinv=@insureddepositinv,contactreserve=@contactreserve,unduereserve=@unduereserve,claimreserve=@claimreserve,lifereserve=@lifereserve,lthealthreserve=@lthealthreserve,ltborrow=@ltborrow,bondpay=@bondpay,preferstocbond=@preferstocbond,sustainbond=@sustainbond,juniorbondpay=@juniorbondpay,deferincometaxliab=@deferincometaxliab,otherliab=@otherliab,independentliab=@independentliab,sumliab=@sumliab,sharecapital=@sharecapital,capitalreserve=@capitalreserve,otherequity=@otherequity,preferredstock=@preferredstock,sustainabledebt=@sustainabledebt,otherequityother=@otherequityother,surplusreserve=@surplusreserve,generalriskprepare=@generalriskprepare,retainedearning=@retainedearning,diffconversionfc=@diffconversionfc,sumparentequity=@sumparentequity,minorityequity=@minorityequity,sumshequity=@sumshequity,sumliabshequity=@sumliabshequity where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                reportdatetype = jo.REPORTDATETYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                currency = jo.CURRENCY,
                                monetaryfund = jo.MONETARYFUND.ConvertToDecimal(),
                                settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal(),
                                lendfund = jo.LENDFUND.ConvertToDecimal(),
                                fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                                tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                                definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                                derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                                buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                                interestrec = jo.INTERESTREC.ConvertToDecimal(),
                                premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                                rirec = jo.RIREC.ConvertToDecimal(),
                                ricontactreserverec = jo.RICONTACTRESERVEREC.ConvertToDecimal(),
                                unduerireserverec = jo.UNDUERIRESERVEREC.ConvertToDecimal(),
                                claimrireserverec = jo.CLAIMRIRESERVEREC.ConvertToDecimal(),
                                liferireserverec = jo.LIFERIRESERVEREC.ConvertToDecimal(),
                                lthealthrireserverec = jo.LTHEALTHRIRESERVEREC.ConvertToDecimal(),
                                insuredpledgeloan = jo.INSUREDPLEDGELOAN.ConvertToDecimal(),
                                loanadvances = jo.LOANADVANCES.ConvertToDecimal(),
                                gdepositpay = jo.GDEPOSITPAY.ConvertToDecimal(),
                                creditorplaninv = jo.CREDITORPLANINV.ConvertToDecimal(),
                                otherrec = jo.OTHERREC.ConvertToDecimal(),
                                tdeposit = jo.TDEPOSIT.ConvertToDecimal(),
                                saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                                heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                                investrec = jo.INVESTREC.ConvertToDecimal(),
                                accountrec = jo.ACCOUNTREC.ConvertToDecimal(),
                                ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                                capitalgdepositpay = jo.CAPITALGDEPOSITPAY.ConvertToDecimal(),
                                estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                                fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                                constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                                intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                                goodwill = jo.GOODWILL.ConvertToDecimal(),
                                deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                                otherasset = jo.OTHERASSET.ConvertToDecimal(),
                                independentasset = jo.INDEPENDENTASSET.ConvertToDecimal(),
                                sumasset = jo.SUMASSET.ConvertToDecimal(),
                                stborrow = jo.STBORROW.ConvertToDecimal(),
                                fideposit = jo.FIDEPOSIT.ConvertToDecimal(),
                                gdepositrec = jo.GDEPOSITREC.ConvertToDecimal(),
                                borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                                fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                                tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                                definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                                derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                                sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                                acceptdeposit = jo.ACCEPTDEPOSIT.ConvertToDecimal(),
                                agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                                accountpay = jo.ACCOUNTPAY.ConvertToDecimal(),
                                billpay = jo.BILLPAY.ConvertToDecimal(),
                                advancerec = jo.ADVANCEREC.ConvertToDecimal(),
                                premiumadvance = jo.PREMIUMADVANCE.ConvertToDecimal(),
                                commpay = jo.COMMPAY.ConvertToDecimal(),
                                ripay = jo.RIPAY.ConvertToDecimal(),
                                salarypay = jo.SALARYPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                                dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                                anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                                claimpay = jo.CLAIMPAY.ConvertToDecimal(),
                                policydivipay = jo.POLICYDIVIPAY.ConvertToDecimal(),
                                otherpay = jo.OTHERPAY.ConvertToDecimal(),
                                insureddepositinv = jo.INSUREDDEPOSITINV.ConvertToDecimal(),
                                contactreserve = jo.CONTACTRESERVE.ConvertToDecimal(),
                                unduereserve = jo.UNDUERESERVE.ConvertToDecimal(),
                                claimreserve = jo.CLAIMRESERVE.ConvertToDecimal(),
                                lifereserve = jo.LIFERESERVE.ConvertToDecimal(),
                                lthealthreserve = jo.LTHEALTHRESERVE.ConvertToDecimal(),
                                ltborrow = jo.LTBORROW.ConvertToDecimal(),
                                bondpay = jo.BONDPAY.ConvertToDecimal(),
                                preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal(),
                                sustainbond = jo.SUSTAINBOND.ConvertToDecimal(),
                                juniorbondpay = jo.JUNIORBONDPAY.ConvertToDecimal(),
                                deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                                otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                                independentliab = jo.INDEPENDENTLIAB.ConvertToDecimal(),
                                sumliab = jo.SUMLIAB.ConvertToDecimal(),
                                sharecapital = jo.SHARECAPITAL.ConvertToDecimal(),
                                capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                                otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                                preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                                sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                                otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                                surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                                generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                                retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                                diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                                sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                                minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                                sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                                sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_balancesheet_insurance (securitycode,reporttype,reportdatetype,type,reportdate,currency,monetaryfund,settlementprovision,lendfund,fvaluefasset,tradefasset,definefvaluefasset,derivefasset,buysellbackfasset,interestrec,premiumrec,rirec,ricontactreserverec,unduerireserverec,claimrireserverec,liferireserverec,lthealthrireserverec,insuredpledgeloan,loanadvances,gdepositpay,creditorplaninv,otherrec,tdeposit,saleablefasset,heldmaturityinv,investrec,accountrec,ltequityinv,capitalgdepositpay,estateinvest,fixedasset,constructionprogress,intangibleasset,goodwill,deferincometaxasset,otherasset,independentasset,sumasset,stborrow,fideposit,gdepositrec,borrowfund,fvaluefliab,tradefliab,definefvaluefliab,derivefliab,sellbuybackfasset,acceptdeposit,agenttradesecurity,accountpay,billpay,advancerec,premiumadvance,commpay,ripay,salarypay,taxpay,interestpay,dividendpay,anticipateliab,claimpay,policydivipay,otherpay,insureddepositinv,contactreserve,unduereserve,claimreserve,lifereserve,lthealthreserve,ltborrow,bondpay,preferstocbond,sustainbond,juniorbondpay,deferincometaxliab,otherliab,independentliab,sumliab,sharecapital,capitalreserve,otherequity,preferredstock,sustainabledebt,otherequityother,surplusreserve,generalriskprepare,retainedearning,diffconversionfc,sumparentequity,minorityequity,sumshequity,sumliabshequity) values (@securitycode,@reporttype,@reportdatetype,@type,@reportdate,@currency,@monetaryfund,@settlementprovision,@lendfund,@fvaluefasset,@tradefasset,@definefvaluefasset,@derivefasset,@buysellbackfasset,@interestrec,@premiumrec,@rirec,@ricontactreserverec,@unduerireserverec,@claimrireserverec,@liferireserverec,@lthealthrireserverec,@insuredpledgeloan,@loanadvances,@gdepositpay,@creditorplaninv,@otherrec,@tdeposit,@saleablefasset,@heldmaturityinv,@investrec,@accountrec,@ltequityinv,@capitalgdepositpay,@estateinvest,@fixedasset,@constructionprogress,@intangibleasset,@goodwill,@deferincometaxasset,@otherasset,@independentasset,@sumasset,@stborrow,@fideposit,@gdepositrec,@borrowfund,@fvaluefliab,@tradefliab,@definefvaluefliab,@derivefliab,@sellbuybackfasset,@acceptdeposit,@agenttradesecurity,@accountpay,@billpay,@advancerec,@premiumadvance,@commpay,@ripay,@salarypay,@taxpay,@interestpay,@dividendpay,@anticipateliab,@claimpay,@policydivipay,@otherpay,@insureddepositinv,@contactreserve,@unduereserve,@claimreserve,@lifereserve,@lthealthreserve,@ltborrow,@bondpay,@preferstocbond,@sustainbond,@juniorbondpay,@deferincometaxliab,@otherliab,@independentliab,@sumliab,@sharecapital,@capitalreserve,@otherequity,@preferredstock,@sustainabledebt,@otherequityother,@surplusreserve,@generalriskprepare,@retainedearning,@diffconversionfc,@sumparentequity,@minorityequity,@sumshequity,@sumliabshequity)";
                    em_balancesheet_insurance entity = new em_balancesheet_insurance()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        reportdatetype = jo.REPORTDATETYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        currency = jo.CURRENCY,
                        monetaryfund = jo.MONETARYFUND.ConvertToDecimal(),
                        settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal(),
                        lendfund = jo.LENDFUND.ConvertToDecimal(),
                        fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                        tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                        definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                        derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                        buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                        interestrec = jo.INTERESTREC.ConvertToDecimal(),
                        premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                        rirec = jo.RIREC.ConvertToDecimal(),
                        ricontactreserverec = jo.RICONTACTRESERVEREC.ConvertToDecimal(),
                        unduerireserverec = jo.UNDUERIRESERVEREC.ConvertToDecimal(),
                        claimrireserverec = jo.CLAIMRIRESERVEREC.ConvertToDecimal(),
                        liferireserverec = jo.LIFERIRESERVEREC.ConvertToDecimal(),
                        lthealthrireserverec = jo.LTHEALTHRIRESERVEREC.ConvertToDecimal(),
                        insuredpledgeloan = jo.INSUREDPLEDGELOAN.ConvertToDecimal(),
                        loanadvances = jo.LOANADVANCES.ConvertToDecimal(),
                        gdepositpay = jo.GDEPOSITPAY.ConvertToDecimal(),
                        creditorplaninv = jo.CREDITORPLANINV.ConvertToDecimal(),
                        otherrec = jo.OTHERREC.ConvertToDecimal(),
                        tdeposit = jo.TDEPOSIT.ConvertToDecimal(),
                        saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                        heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                        investrec = jo.INVESTREC.ConvertToDecimal(),
                        accountrec = jo.ACCOUNTREC.ConvertToDecimal(),
                        ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                        capitalgdepositpay = jo.CAPITALGDEPOSITPAY.ConvertToDecimal(),
                        estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                        fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                        constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                        intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                        goodwill = jo.GOODWILL.ConvertToDecimal(),
                        deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                        otherasset = jo.OTHERASSET.ConvertToDecimal(),
                        independentasset = jo.INDEPENDENTASSET.ConvertToDecimal(),
                        sumasset = jo.SUMASSET.ConvertToDecimal(),
                        stborrow = jo.STBORROW.ConvertToDecimal(),
                        fideposit = jo.FIDEPOSIT.ConvertToDecimal(),
                        gdepositrec = jo.GDEPOSITREC.ConvertToDecimal(),
                        borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                        fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                        tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                        definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                        derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                        sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                        acceptdeposit = jo.ACCEPTDEPOSIT.ConvertToDecimal(),
                        agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                        accountpay = jo.ACCOUNTPAY.ConvertToDecimal(),
                        billpay = jo.BILLPAY.ConvertToDecimal(),
                        advancerec = jo.ADVANCEREC.ConvertToDecimal(),
                        premiumadvance = jo.PREMIUMADVANCE.ConvertToDecimal(),
                        commpay = jo.COMMPAY.ConvertToDecimal(),
                        ripay = jo.RIPAY.ConvertToDecimal(),
                        salarypay = jo.SALARYPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                        dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                        anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                        claimpay = jo.CLAIMPAY.ConvertToDecimal(),
                        policydivipay = jo.POLICYDIVIPAY.ConvertToDecimal(),
                        otherpay = jo.OTHERPAY.ConvertToDecimal(),
                        insureddepositinv = jo.INSUREDDEPOSITINV.ConvertToDecimal(),
                        contactreserve = jo.CONTACTRESERVE.ConvertToDecimal(),
                        unduereserve = jo.UNDUERESERVE.ConvertToDecimal(),
                        claimreserve = jo.CLAIMRESERVE.ConvertToDecimal(),
                        lifereserve = jo.LIFERESERVE.ConvertToDecimal(),
                        lthealthreserve = jo.LTHEALTHRESERVE.ConvertToDecimal(),
                        ltborrow = jo.LTBORROW.ConvertToDecimal(),
                        bondpay = jo.BONDPAY.ConvertToDecimal(),
                        preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal(),
                        sustainbond = jo.SUSTAINBOND.ConvertToDecimal(),
                        juniorbondpay = jo.JUNIORBONDPAY.ConvertToDecimal(),
                        deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                        otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                        independentliab = jo.INDEPENDENTLIAB.ConvertToDecimal(),
                        sumliab = jo.SUMLIAB.ConvertToDecimal(),
                        sharecapital = jo.SHARECAPITAL.ConvertToDecimal(),
                        capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                        otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                        preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                        sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                        otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                        surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                        generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                        retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                        diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                        sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                        minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                        sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                        sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
            return false;
        }
        #endregion
        #region	em_balancesheet_bank
        public static em_balancesheet_bank get_em_balancesheet_bank_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_balancesheet_bank where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_balancesheet_bank>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_balancesheet_bank_data(em_balancesheet_bank_jo jo, bool includeUpdate)
        {
            try
            {
                em_balancesheet_bank edit_entity = get_em_balancesheet_bank_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_balancesheet_bank set  securitycode=@securitycode,reporttype=@reporttype,reportdatetype=@reportdatetype,type=@type,reportdate=@reportdate,currency=@currency,cashanddepositcbank=@cashanddepositcbank,depositinfi=@depositinfi,preciousmetal=@preciousmetal,lendfund=@lendfund,fvaluefasset=@fvaluefasset,tradefasset=@tradefasset,definefvaluefasset=@definefvaluefasset,derivefasset=@derivefasset,buysellbackfasset=@buysellbackfasset,accountrec=@accountrec,interestrec=@interestrec,loanadvances=@loanadvances,saleablefasset=@saleablefasset,heldmaturityinv=@heldmaturityinv,investrec=@investrec,ltequityinv=@ltequityinv,invsubsidiary=@invsubsidiary,invjoint=@invjoint,estateinvest=@estateinvest,fixedasset=@fixedasset,constructionprogress=@constructionprogress,intangibleasset=@intangibleasset,goodwill=@goodwill,masset=@masset,massetdevalue=@massetdevalue,netmasset=@netmasset,deferincometaxasset=@deferincometaxasset,otherasset=@otherasset,sumasset=@sumasset,borrowfromcbank=@borrowfromcbank,fideposit=@fideposit,borrowfund=@borrowfund,fvaluefliab=@fvaluefliab,tradefliab=@tradefliab,definefvaluefliab=@definefvaluefliab,derivefliab=@derivefliab,sellbuybackfasset=@sellbuybackfasset,acceptdeposit=@acceptdeposit,outwardremittance=@outwardremittance,cdandbillrec=@cdandbillrec,cd=@cd,salarypay=@salarypay,taxpay=@taxpay,interestpay=@interestpay,dividendpay=@dividendpay,anticipateliab=@anticipateliab,deferincometaxliab=@deferincometaxliab,bondpay=@bondpay,preferstocbond=@preferstocbond,sustainbond=@sustainbond,juniorbondpay=@juniorbondpay,otherliab=@otherliab,sumliab=@sumliab,shequity=@shequity,otherequity=@otherequity,preferredstock=@preferredstock,sustainabledebt=@sustainabledebt,otherequityother=@otherequityother,capitalreserve=@capitalreserve,invrevaluereserve=@invrevaluereserve,inventoryshare=@inventoryshare,hedgereserve=@hedgereserve,surplusreserve=@surplusreserve,generalriskprepare=@generalriskprepare,retainedearning=@retainedearning,suggestassigndivi=@suggestassigndivi,diffconversionfc=@diffconversionfc,sumparentequity=@sumparentequity,minorityequity=@minorityequity,sumshequity=@sumshequity,sumliabshequity=@sumliabshequity where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                reportdatetype = jo.REPORTDATETYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                currency = jo.CURRENCY,
                                cashanddepositcbank = jo.CASHANDDEPOSITCBANK.ConvertToDecimal(),
                                depositinfi = jo.DEPOSITINFI.ConvertToDecimal(),
                                preciousmetal = jo.PRECIOUSMETAL.ConvertToDecimal(),
                                lendfund = jo.LENDFUND.ConvertToDecimal(),
                                fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                                tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                                definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                                derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                                buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                                accountrec = jo.ACCOUNTREC.ConvertToDecimal(),
                                interestrec = jo.INTERESTREC.ConvertToDecimal(),
                                loanadvances = jo.LOANADVANCES.ConvertToDecimal(),
                                saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                                heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                                investrec = jo.INVESTREC.ConvertToDecimal(),
                                ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                                invsubsidiary = jo.INVSUBSIDIARY.ConvertToDecimal(),
                                invjoint = jo.INVJOINT.ConvertToDecimal(),
                                estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                                fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                                constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                                intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                                goodwill = jo.GOODWILL.ConvertToDecimal(),
                                masset = jo.MASSET.ConvertToDecimal(),
                                massetdevalue = jo.MASSETDEVALUE.ConvertToDecimal(),
                                netmasset = jo.NETMASSET.ConvertToDecimal(),
                                deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                                otherasset = jo.OTHERASSET.ConvertToDecimal(),
                                sumasset = jo.SUMASSET.ConvertToDecimal(),
                                borrowfromcbank = jo.BORROWFROMCBANK.ConvertToDecimal(),
                                fideposit = jo.FIDEPOSIT.ConvertToDecimal(),
                                borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                                fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                                tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                                definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                                derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                                sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                                acceptdeposit = jo.ACCEPTDEPOSIT.ConvertToDecimal(),
                                outwardremittance = jo.OUTWARDREMITTANCE.ConvertToDecimal(),
                                cdandbillrec = jo.CDANDBILLREC.ConvertToDecimal(),
                                cd = jo.CD.ConvertToDecimal(),
                                salarypay = jo.SALARYPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                                dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                                anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                                deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                                bondpay = jo.BONDPAY.ConvertToDecimal(),
                                preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal(),
                                sustainbond = jo.SUSTAINBOND.ConvertToDecimal(),
                                juniorbondpay = jo.JUNIORBONDPAY.ConvertToDecimal(),
                                otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                                sumliab = jo.SUMLIAB.ConvertToDecimal(),
                                shequity = jo.SHEQUITY.ConvertToDecimal(),
                                otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                                preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                                sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                                otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                                capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                                invrevaluereserve = jo.INVREVALUERESERVE.ConvertToDecimal(),
                                inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal(),
                                hedgereserve = jo.HEDGERESERVE.ConvertToDecimal(),
                                surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                                generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                                retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                                suggestassigndivi = jo.SUGGESTASSIGNDIVI.ConvertToDecimal(),
                                diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                                sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                                minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                                sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                                sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_balancesheet_bank (securitycode,reporttype,reportdatetype,type,reportdate,currency,cashanddepositcbank,depositinfi,preciousmetal,lendfund,fvaluefasset,tradefasset,definefvaluefasset,derivefasset,buysellbackfasset,accountrec,interestrec,loanadvances,saleablefasset,heldmaturityinv,investrec,ltequityinv,invsubsidiary,invjoint,estateinvest,fixedasset,constructionprogress,intangibleasset,goodwill,masset,massetdevalue,netmasset,deferincometaxasset,otherasset,sumasset,borrowfromcbank,fideposit,borrowfund,fvaluefliab,tradefliab,definefvaluefliab,derivefliab,sellbuybackfasset,acceptdeposit,outwardremittance,cdandbillrec,cd,salarypay,taxpay,interestpay,dividendpay,anticipateliab,deferincometaxliab,bondpay,preferstocbond,sustainbond,juniorbondpay,otherliab,sumliab,shequity,otherequity,preferredstock,sustainabledebt,otherequityother,capitalreserve,invrevaluereserve,inventoryshare,hedgereserve,surplusreserve,generalriskprepare,retainedearning,suggestassigndivi,diffconversionfc,sumparentequity,minorityequity,sumshequity,sumliabshequity) values (@securitycode,@reporttype,@reportdatetype,@type,@reportdate,@currency,@cashanddepositcbank,@depositinfi,@preciousmetal,@lendfund,@fvaluefasset,@tradefasset,@definefvaluefasset,@derivefasset,@buysellbackfasset,@accountrec,@interestrec,@loanadvances,@saleablefasset,@heldmaturityinv,@investrec,@ltequityinv,@invsubsidiary,@invjoint,@estateinvest,@fixedasset,@constructionprogress,@intangibleasset,@goodwill,@masset,@massetdevalue,@netmasset,@deferincometaxasset,@otherasset,@sumasset,@borrowfromcbank,@fideposit,@borrowfund,@fvaluefliab,@tradefliab,@definefvaluefliab,@derivefliab,@sellbuybackfasset,@acceptdeposit,@outwardremittance,@cdandbillrec,@cd,@salarypay,@taxpay,@interestpay,@dividendpay,@anticipateliab,@deferincometaxliab,@bondpay,@preferstocbond,@sustainbond,@juniorbondpay,@otherliab,@sumliab,@shequity,@otherequity,@preferredstock,@sustainabledebt,@otherequityother,@capitalreserve,@invrevaluereserve,@inventoryshare,@hedgereserve,@surplusreserve,@generalriskprepare,@retainedearning,@suggestassigndivi,@diffconversionfc,@sumparentequity,@minorityequity,@sumshequity,@sumliabshequity)";
                    em_balancesheet_bank entity = new em_balancesheet_bank()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        reportdatetype = jo.REPORTDATETYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        currency = jo.CURRENCY,
                        cashanddepositcbank = jo.CASHANDDEPOSITCBANK.ConvertToDecimal(),
                        depositinfi = jo.DEPOSITINFI.ConvertToDecimal(),
                        preciousmetal = jo.PRECIOUSMETAL.ConvertToDecimal(),
                        lendfund = jo.LENDFUND.ConvertToDecimal(),
                        fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                        tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                        definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                        derivefasset = jo.DERIVEFASSET.ConvertToDecimal(),
                        buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                        accountrec = jo.ACCOUNTREC.ConvertToDecimal(),
                        interestrec = jo.INTERESTREC.ConvertToDecimal(),
                        loanadvances = jo.LOANADVANCES.ConvertToDecimal(),
                        saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                        heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                        investrec = jo.INVESTREC.ConvertToDecimal(),
                        ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                        invsubsidiary = jo.INVSUBSIDIARY.ConvertToDecimal(),
                        invjoint = jo.INVJOINT.ConvertToDecimal(),
                        estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                        fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                        constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                        intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                        goodwill = jo.GOODWILL.ConvertToDecimal(),
                        masset = jo.MASSET.ConvertToDecimal(),
                        massetdevalue = jo.MASSETDEVALUE.ConvertToDecimal(),
                        netmasset = jo.NETMASSET.ConvertToDecimal(),
                        deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                        otherasset = jo.OTHERASSET.ConvertToDecimal(),
                        sumasset = jo.SUMASSET.ConvertToDecimal(),
                        borrowfromcbank = jo.BORROWFROMCBANK.ConvertToDecimal(),
                        fideposit = jo.FIDEPOSIT.ConvertToDecimal(),
                        borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                        fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                        tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                        definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                        derivefliab = jo.DERIVEFLIAB.ConvertToDecimal(),
                        sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                        acceptdeposit = jo.ACCEPTDEPOSIT.ConvertToDecimal(),
                        outwardremittance = jo.OUTWARDREMITTANCE.ConvertToDecimal(),
                        cdandbillrec = jo.CDANDBILLREC.ConvertToDecimal(),
                        cd = jo.CD.ConvertToDecimal(),
                        salarypay = jo.SALARYPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                        dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                        anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                        deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                        bondpay = jo.BONDPAY.ConvertToDecimal(),
                        preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal(),
                        sustainbond = jo.SUSTAINBOND.ConvertToDecimal(),
                        juniorbondpay = jo.JUNIORBONDPAY.ConvertToDecimal(),
                        otherliab = jo.OTHERLIAB.ConvertToDecimal(),
                        sumliab = jo.SUMLIAB.ConvertToDecimal(),
                        shequity = jo.SHEQUITY.ConvertToDecimal(),
                        otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                        preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                        sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                        otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                        capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                        invrevaluereserve = jo.INVREVALUERESERVE.ConvertToDecimal(),
                        inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal(),
                        hedgereserve = jo.HEDGERESERVE.ConvertToDecimal(),
                        surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                        generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                        retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                        suggestassigndivi = jo.SUGGESTASSIGNDIVI.ConvertToDecimal(),
                        diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                        sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                        minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                        sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                        sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }

            return false;
        }
        #endregion
        #region	em_balancesheet_common
        public static em_balancesheet_common get_em_balancesheet_common_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_balancesheet_common where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_balancesheet_common>(sql).FirstOrDefault();
            }
        }

        public static void oper_em_balancesheet_common_data(em_balancesheet_common_jo jo)
        {
            try
            {
                em_balancesheet_common edit_entity = get_em_balancesheet_common_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "update em_balancesheet_common set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,monetaryfund=@monetaryfund,settlementprovision=@settlementprovision,lendfund=@lendfund,fvaluefasset=@fvaluefasset,tradefasset=@tradefasset,definefvaluefasset=@definefvaluefasset,billrec=@billrec,accountrec=@accountrec,advancepay=@advancepay,premiumrec=@premiumrec,rirec=@rirec,ricontactreserverec=@ricontactreserverec,interestrec=@interestrec,dividendrec=@dividendrec,otherrec=@otherrec,exportrebaterec=@exportrebaterec,subsidyrec=@subsidyrec,internalrec=@internalrec,buysellbackfasset=@buysellbackfasset,inventory=@inventory,clheldsaleass=@clheldsaleass,nonlassetoneyear=@nonlassetoneyear,otherlasset=@otherlasset,sumlasset=@sumlasset,loanadvances=@loanadvances,saleablefasset=@saleablefasset,heldmaturityinv=@heldmaturityinv,ltrec=@ltrec,ltequityinv=@ltequityinv,estateinvest=@estateinvest,fixedasset=@fixedasset,constructionprogress=@constructionprogress,constructionmaterial=@constructionmaterial,liquidatefixedasset=@liquidatefixedasset,productbiologyasset=@productbiologyasset,oilgasasset=@oilgasasset,intangibleasset=@intangibleasset,developexp=@developexp,goodwill=@goodwill,ltdeferasset=@ltdeferasset,deferincometaxasset=@deferincometaxasset,othernonlasset=@othernonlasset,sumnonlasset=@sumnonlasset,sumasset=@sumasset,stborrow=@stborrow,borrowfromcbank=@borrowfromcbank,deposit=@deposit,borrowfund=@borrowfund,fvaluefliab=@fvaluefliab,tradefliab=@tradefliab,definefvaluefliab=@definefvaluefliab,billpay=@billpay,accountpay=@accountpay,advancereceive=@advancereceive,sellbuybackfasset=@sellbuybackfasset,commpay=@commpay,salarypay=@salarypay,taxpay=@taxpay,interestpay=@interestpay,dividendpay=@dividendpay,ripay=@ripay,internalpay=@internalpay,otherpay=@otherpay,anticipatelliab=@anticipatelliab,contactreserve=@contactreserve,agenttradesecurity=@agenttradesecurity,agentuwsecurity=@agentuwsecurity,deferincomeoneyear=@deferincomeoneyear,stbondrec=@stbondrec,clheldsaleliab=@clheldsaleliab,nonlliaboneyear=@nonlliaboneyear,otherlliab=@otherlliab,sumlliab=@sumlliab,ltborrow=@ltborrow,bondpay=@bondpay,preferstocbond=@preferstocbond,sustainbond=@sustainbond,ltaccountpay=@ltaccountpay,ltsalarypay=@ltsalarypay,specialpay=@specialpay,anticipateliab=@anticipateliab,deferincome=@deferincome,deferincometaxliab=@deferincometaxliab,othernonlliab=@othernonlliab,sumnonlliab=@sumnonlliab,sumliab=@sumliab,sharecapital=@sharecapital,otherequity=@otherequity,preferredstock=@preferredstock,sustainabledebt=@sustainabledebt,otherequityother=@otherequityother,capitalreserve=@capitalreserve,inventoryshare=@inventoryshare,specialreserve=@specialreserve,surplusreserve=@surplusreserve,generalriskprepare=@generalriskprepare,unconfirminvloss=@unconfirminvloss,retainedearning=@retainedearning,plancashdivi=@plancashdivi,diffconversionfc=@diffconversionfc,sumparentequity=@sumparentequity,minorityequity=@minorityequity,sumshequity=@sumshequity,sumliabshequity=@sumliabshequity where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        //修改
                        edit_entity.securitycode = jo.SECURITYCODE;
                        edit_entity.reporttype = jo.REPORTTYPE;
                        edit_entity.type = jo.TYPE;
                        edit_entity.reportdate = jo.REPORTDATE.ConvertToDate();
                        edit_entity.monetaryfund = jo.MONETARYFUND.ConvertToDecimal();
                        edit_entity.settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal();
                        edit_entity.lendfund = jo.LENDFUND.ConvertToDecimal();
                        edit_entity.fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal();
                        //edit_entity.tradefasset = jo.TRADEFASSET.ConvertToDecimal();
                        edit_entity.definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal();
                        edit_entity.billrec = jo.BILLREC.ConvertToDecimal();
                        edit_entity.accountrec = jo.ACCOUNTREC.ConvertToDecimal();
                        edit_entity.advancepay = jo.ADVANCEPAY.ConvertToDecimal();
                        edit_entity.premiumrec = jo.PREMIUMREC.ConvertToDecimal();
                        edit_entity.rirec = jo.RIREC.ConvertToDecimal();
                        edit_entity.ricontactreserverec = jo.RICONTACTRESERVEREC.ConvertToDecimal();
                        edit_entity.interestrec = jo.INTERESTREC.ConvertToDecimal();
                        edit_entity.dividendrec = jo.DIVIDENDREC.ConvertToDecimal();
                        edit_entity.otherrec = jo.OTHERREC.ConvertToDecimal();
                        edit_entity.exportrebaterec = jo.EXPORTREBATEREC.ConvertToDecimal();
                        edit_entity.subsidyrec = jo.SUBSIDYREC.ConvertToDecimal();
                        edit_entity.internalrec = jo.INTERNALREC.ConvertToDecimal();
                        edit_entity.buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal();
                        edit_entity.inventory = jo.INVENTORY.ConvertToDecimal();
                        edit_entity.clheldsaleass = jo.CLHELDSALEASS.ConvertToDecimal();
                        edit_entity.nonlassetoneyear = jo.NONLASSETONEYEAR.ConvertToDecimal();
                        edit_entity.otherlasset = jo.OTHERLASSET.ConvertToDecimal();
                        edit_entity.sumlasset = jo.SUMLASSET.ConvertToDecimal();
                        edit_entity.loanadvances = jo.LOANADVANCES.ConvertToDecimal();
                        edit_entity.saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal();
                        edit_entity.heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal();
                        edit_entity.ltrec = jo.LTREC.ConvertToDecimal();
                        edit_entity.ltequityinv = jo.LTEQUITYINV.ConvertToDecimal();
                        edit_entity.estateinvest = jo.ESTATEINVEST.ConvertToDecimal();
                        edit_entity.fixedasset = jo.FIXEDASSET.ConvertToDecimal();
                        edit_entity.constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal();
                        edit_entity.constructionmaterial = jo.CONSTRUCTIONMATERIAL.ConvertToDecimal();
                        edit_entity.liquidatefixedasset = jo.LIQUIDATEFIXEDASSET.ConvertToDecimal();
                        edit_entity.productbiologyasset = jo.PRODUCTBIOLOGYASSET.ConvertToDecimal();
                        edit_entity.oilgasasset = jo.OILGASASSET.ConvertToDecimal();
                        edit_entity.intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal();
                        edit_entity.developexp = jo.DEVELOPEXP.ConvertToDecimal();
                        edit_entity.goodwill = jo.GOODWILL.ConvertToDecimal();
                        edit_entity.ltdeferasset = jo.LTDEFERASSET.ConvertToDecimal();
                        edit_entity.deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal();
                        edit_entity.othernonlasset = jo.OTHERNONLASSET.ConvertToDecimal();
                        edit_entity.sumnonlasset = jo.SUMNONLASSET.ConvertToDecimal();
                        edit_entity.sumasset = jo.SUMASSET.ConvertToDecimal();
                        edit_entity.stborrow = jo.STBORROW.ConvertToDecimal();
                        edit_entity.borrowfromcbank = jo.BORROWFROMCBANK.ConvertToDecimal();
                        edit_entity.deposit = jo.DEPOSIT.ConvertToDecimal();
                        edit_entity.borrowfund = jo.BORROWFUND.ConvertToDecimal();
                        edit_entity.fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal();
                        //edit_entity.tradefliab = jo.TRADEFLIAB.ConvertToDecimal();
                        edit_entity.definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal();
                        edit_entity.billpay = jo.BILLPAY.ConvertToDecimal();
                        edit_entity.accountpay = jo.ACCOUNTPAY.ConvertToDecimal();
                        edit_entity.advancereceive = jo.ADVANCERECEIVE.ConvertToDecimal();
                        edit_entity.sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal();
                        edit_entity.commpay = jo.COMMPAY.ConvertToDecimal();
                        edit_entity.salarypay = jo.SALARYPAY.ConvertToDecimal();
                        edit_entity.taxpay = jo.TAXPAY.ConvertToDecimal();
                        edit_entity.interestpay = jo.INTERESTPAY.ConvertToDecimal();
                        edit_entity.dividendpay = jo.DIVIDENDPAY.ConvertToDecimal();
                        edit_entity.ripay = jo.RIPAY.ConvertToDecimal();
                        edit_entity.internalpay = jo.INTERNALPAY.ConvertToDecimal();
                        edit_entity.otherpay = jo.OTHERPAY.ConvertToDecimal();
                        edit_entity.anticipatelliab = jo.ANTICIPATELLIAB.ConvertToDecimal();
                        edit_entity.contactreserve = jo.CONTACTRESERVE.ConvertToDecimal();
                        edit_entity.agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal();
                        edit_entity.agentuwsecurity = jo.AGENTUWSECURITY.ConvertToDecimal();
                        edit_entity.deferincomeoneyear = jo.DEFERINCOMEONEYEAR.ConvertToDecimal();
                        edit_entity.stbondrec = jo.STBONDREC.ConvertToDecimal();
                        edit_entity.clheldsaleliab = jo.CLHELDSALELIAB.ConvertToDecimal();
                        edit_entity.nonlliaboneyear = jo.NONLLIABONEYEAR.ConvertToDecimal();
                        edit_entity.otherlliab = jo.OTHERLLIAB.ConvertToDecimal();
                        edit_entity.sumlliab = jo.SUMLLIAB.ConvertToDecimal();
                        edit_entity.ltborrow = jo.LTBORROW.ConvertToDecimal();
                        edit_entity.bondpay = jo.BONDPAY.ConvertToDecimal();
                        edit_entity.preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal();
                        edit_entity.sustainbond = jo.SUSTAINBOND.ConvertToDecimal();
                        edit_entity.ltaccountpay = jo.LTACCOUNTPAY.ConvertToDecimal();
                        edit_entity.ltsalarypay = jo.LTSALARYPAY.ConvertToDecimal();
                        edit_entity.specialpay = jo.SPECIALPAY.ConvertToDecimal();
                        edit_entity.anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal();
                        edit_entity.deferincome = jo.DEFERINCOME.ConvertToDecimal();
                        edit_entity.deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal();
                        edit_entity.othernonlliab = jo.OTHERNONLLIAB.ConvertToDecimal();
                        edit_entity.sumnonlliab = jo.SUMNONLLIAB.ConvertToDecimal();
                        edit_entity.sumliab = jo.SUMLIAB.ConvertToDecimal();
                        edit_entity.sharecapital = jo.SHARECAPITAL.ConvertToDecimal();
                        edit_entity.otherequity = jo.OTHEREQUITY.ConvertToDecimal();
                        edit_entity.preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal();
                        edit_entity.sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal();
                        edit_entity.otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal();
                        edit_entity.capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal();
                        edit_entity.inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal();
                        edit_entity.specialreserve = jo.SPECIALRESERVE.ConvertToDecimal();
                        edit_entity.surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal();
                        edit_entity.generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal();
                        edit_entity.unconfirminvloss = jo.UNCONFIRMINVLOSS.ConvertToDecimal();
                        edit_entity.retainedearning = jo.RETAINEDEARNING.ConvertToDecimal();
                        edit_entity.plancashdivi = jo.PLANCASHDIVI.ConvertToDecimal();
                        edit_entity.diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal();
                        edit_entity.sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal();
                        edit_entity.minorityequity = jo.MINORITYEQUITY.ConvertToDecimal();
                        edit_entity.sumshequity = jo.SUMSHEQUITY.ConvertToDecimal();
                        edit_entity.sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal();
                        edit_entity.id = edit_entity.id;

                        if (!edit_entity.tradefasset.HasValue)
                            edit_entity.tradefasset = jo.TRADEFASSET.ConvertToDecimal();
                        if (!edit_entity.tradefliab.HasValue)
                            edit_entity.tradefliab = jo.TRADEFLIAB.ConvertToDecimal();
                        conn.Execute(sql, edit_entity);
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_balancesheet_common (securitycode,reporttype,type,reportdate,monetaryfund,settlementprovision,lendfund,fvaluefasset,tradefasset,definefvaluefasset,billrec,accountrec,advancepay,premiumrec,rirec,ricontactreserverec,interestrec,dividendrec,otherrec,exportrebaterec,subsidyrec,internalrec,buysellbackfasset,inventory,clheldsaleass,nonlassetoneyear,otherlasset,sumlasset,loanadvances,saleablefasset,heldmaturityinv,ltrec,ltequityinv,estateinvest,fixedasset,constructionprogress,constructionmaterial,liquidatefixedasset,productbiologyasset,oilgasasset,intangibleasset,developexp,goodwill,ltdeferasset,deferincometaxasset,othernonlasset,sumnonlasset,sumasset,stborrow,borrowfromcbank,deposit,borrowfund,fvaluefliab,tradefliab,definefvaluefliab,billpay,accountpay,advancereceive,sellbuybackfasset,commpay,salarypay,taxpay,interestpay,dividendpay,ripay,internalpay,otherpay,anticipatelliab,contactreserve,agenttradesecurity,agentuwsecurity,deferincomeoneyear,stbondrec,clheldsaleliab,nonlliaboneyear,otherlliab,sumlliab,ltborrow,bondpay,preferstocbond,sustainbond,ltaccountpay,ltsalarypay,specialpay,anticipateliab,deferincome,deferincometaxliab,othernonlliab,sumnonlliab,sumliab,sharecapital,otherequity,preferredstock,sustainabledebt,otherequityother,capitalreserve,inventoryshare,specialreserve,surplusreserve,generalriskprepare,unconfirminvloss,retainedearning,plancashdivi,diffconversionfc,sumparentequity,minorityequity,sumshequity,sumliabshequity) values (@securitycode,@reporttype,@type,@reportdate,@monetaryfund,@settlementprovision,@lendfund,@fvaluefasset,@tradefasset,@definefvaluefasset,@billrec,@accountrec,@advancepay,@premiumrec,@rirec,@ricontactreserverec,@interestrec,@dividendrec,@otherrec,@exportrebaterec,@subsidyrec,@internalrec,@buysellbackfasset,@inventory,@clheldsaleass,@nonlassetoneyear,@otherlasset,@sumlasset,@loanadvances,@saleablefasset,@heldmaturityinv,@ltrec,@ltequityinv,@estateinvest,@fixedasset,@constructionprogress,@constructionmaterial,@liquidatefixedasset,@productbiologyasset,@oilgasasset,@intangibleasset,@developexp,@goodwill,@ltdeferasset,@deferincometaxasset,@othernonlasset,@sumnonlasset,@sumasset,@stborrow,@borrowfromcbank,@deposit,@borrowfund,@fvaluefliab,@tradefliab,@definefvaluefliab,@billpay,@accountpay,@advancereceive,@sellbuybackfasset,@commpay,@salarypay,@taxpay,@interestpay,@dividendpay,@ripay,@internalpay,@otherpay,@anticipatelliab,@contactreserve,@agenttradesecurity,@agentuwsecurity,@deferincomeoneyear,@stbondrec,@clheldsaleliab,@nonlliaboneyear,@otherlliab,@sumlliab,@ltborrow,@bondpay,@preferstocbond,@sustainbond,@ltaccountpay,@ltsalarypay,@specialpay,@anticipateliab,@deferincome,@deferincometaxliab,@othernonlliab,@sumnonlliab,@sumliab,@sharecapital,@otherequity,@preferredstock,@sustainabledebt,@otherequityother,@capitalreserve,@inventoryshare,@specialreserve,@surplusreserve,@generalriskprepare,@unconfirminvloss,@retainedearning,@plancashdivi,@diffconversionfc,@sumparentequity,@minorityequity,@sumshequity,@sumliabshequity)";
                    em_balancesheet_common entity = new em_balancesheet_common()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        monetaryfund = jo.MONETARYFUND.ConvertToDecimal(),
                        settlementprovision = jo.SETTLEMENTPROVISION.ConvertToDecimal(),
                        lendfund = jo.LENDFUND.ConvertToDecimal(),
                        fvaluefasset = jo.FVALUEFASSET.ConvertToDecimal(),
                        tradefasset = jo.TRADEFASSET.ConvertToDecimal(),
                        definefvaluefasset = jo.DEFINEFVALUEFASSET.ConvertToDecimal(),
                        billrec = jo.BILLREC.ConvertToDecimal(),
                        accountrec = jo.ACCOUNTREC.ConvertToDecimal(),
                        advancepay = jo.ADVANCEPAY.ConvertToDecimal(),
                        premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                        rirec = jo.RIREC.ConvertToDecimal(),
                        ricontactreserverec = jo.RICONTACTRESERVEREC.ConvertToDecimal(),
                        interestrec = jo.INTERESTREC.ConvertToDecimal(),
                        dividendrec = jo.DIVIDENDREC.ConvertToDecimal(),
                        otherrec = jo.OTHERREC.ConvertToDecimal(),
                        exportrebaterec = jo.EXPORTREBATEREC.ConvertToDecimal(),
                        subsidyrec = jo.SUBSIDYREC.ConvertToDecimal(),
                        internalrec = jo.INTERNALREC.ConvertToDecimal(),
                        buysellbackfasset = jo.BUYSELLBACKFASSET.ConvertToDecimal(),
                        inventory = jo.INVENTORY.ConvertToDecimal(),
                        clheldsaleass = jo.CLHELDSALEASS.ConvertToDecimal(),
                        nonlassetoneyear = jo.NONLASSETONEYEAR.ConvertToDecimal(),
                        otherlasset = jo.OTHERLASSET.ConvertToDecimal(),
                        sumlasset = jo.SUMLASSET.ConvertToDecimal(),
                        loanadvances = jo.LOANADVANCES.ConvertToDecimal(),
                        saleablefasset = jo.SALEABLEFASSET.ConvertToDecimal(),
                        heldmaturityinv = jo.HELDMATURITYINV.ConvertToDecimal(),
                        ltrec = jo.LTREC.ConvertToDecimal(),
                        ltequityinv = jo.LTEQUITYINV.ConvertToDecimal(),
                        estateinvest = jo.ESTATEINVEST.ConvertToDecimal(),
                        fixedasset = jo.FIXEDASSET.ConvertToDecimal(),
                        constructionprogress = jo.CONSTRUCTIONPROGRESS.ConvertToDecimal(),
                        constructionmaterial = jo.CONSTRUCTIONMATERIAL.ConvertToDecimal(),
                        liquidatefixedasset = jo.LIQUIDATEFIXEDASSET.ConvertToDecimal(),
                        productbiologyasset = jo.PRODUCTBIOLOGYASSET.ConvertToDecimal(),
                        oilgasasset = jo.OILGASASSET.ConvertToDecimal(),
                        intangibleasset = jo.INTANGIBLEASSET.ConvertToDecimal(),
                        developexp = jo.DEVELOPEXP.ConvertToDecimal(),
                        goodwill = jo.GOODWILL.ConvertToDecimal(),
                        ltdeferasset = jo.LTDEFERASSET.ConvertToDecimal(),
                        deferincometaxasset = jo.DEFERINCOMETAXASSET.ConvertToDecimal(),
                        othernonlasset = jo.OTHERNONLASSET.ConvertToDecimal(),
                        sumnonlasset = jo.SUMNONLASSET.ConvertToDecimal(),
                        sumasset = jo.SUMASSET.ConvertToDecimal(),
                        stborrow = jo.STBORROW.ConvertToDecimal(),
                        borrowfromcbank = jo.BORROWFROMCBANK.ConvertToDecimal(),
                        deposit = jo.DEPOSIT.ConvertToDecimal(),
                        borrowfund = jo.BORROWFUND.ConvertToDecimal(),
                        fvaluefliab = jo.FVALUEFLIAB.ConvertToDecimal(),
                        tradefliab = jo.TRADEFLIAB.ConvertToDecimal(),
                        definefvaluefliab = jo.DEFINEFVALUEFLIAB.ConvertToDecimal(),
                        billpay = jo.BILLPAY.ConvertToDecimal(),
                        accountpay = jo.ACCOUNTPAY.ConvertToDecimal(),
                        advancereceive = jo.ADVANCERECEIVE.ConvertToDecimal(),
                        sellbuybackfasset = jo.SELLBUYBACKFASSET.ConvertToDecimal(),
                        commpay = jo.COMMPAY.ConvertToDecimal(),
                        salarypay = jo.SALARYPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        interestpay = jo.INTERESTPAY.ConvertToDecimal(),
                        dividendpay = jo.DIVIDENDPAY.ConvertToDecimal(),
                        ripay = jo.RIPAY.ConvertToDecimal(),
                        internalpay = jo.INTERNALPAY.ConvertToDecimal(),
                        otherpay = jo.OTHERPAY.ConvertToDecimal(),
                        anticipatelliab = jo.ANTICIPATELLIAB.ConvertToDecimal(),
                        contactreserve = jo.CONTACTRESERVE.ConvertToDecimal(),
                        agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                        agentuwsecurity = jo.AGENTUWSECURITY.ConvertToDecimal(),
                        deferincomeoneyear = jo.DEFERINCOMEONEYEAR.ConvertToDecimal(),
                        stbondrec = jo.STBONDREC.ConvertToDecimal(),
                        clheldsaleliab = jo.CLHELDSALELIAB.ConvertToDecimal(),
                        nonlliaboneyear = jo.NONLLIABONEYEAR.ConvertToDecimal(),
                        otherlliab = jo.OTHERLLIAB.ConvertToDecimal(),
                        sumlliab = jo.SUMLLIAB.ConvertToDecimal(),
                        ltborrow = jo.LTBORROW.ConvertToDecimal(),
                        bondpay = jo.BONDPAY.ConvertToDecimal(),
                        preferstocbond = jo.PREFERSTOCBOND.ConvertToDecimal(),
                        sustainbond = jo.SUSTAINBOND.ConvertToDecimal(),
                        ltaccountpay = jo.LTACCOUNTPAY.ConvertToDecimal(),
                        ltsalarypay = jo.LTSALARYPAY.ConvertToDecimal(),
                        specialpay = jo.SPECIALPAY.ConvertToDecimal(),
                        anticipateliab = jo.ANTICIPATELIAB.ConvertToDecimal(),
                        deferincome = jo.DEFERINCOME.ConvertToDecimal(),
                        deferincometaxliab = jo.DEFERINCOMETAXLIAB.ConvertToDecimal(),
                        othernonlliab = jo.OTHERNONLLIAB.ConvertToDecimal(),
                        sumnonlliab = jo.SUMNONLLIAB.ConvertToDecimal(),
                        sumliab = jo.SUMLIAB.ConvertToDecimal(),
                        sharecapital = jo.SHARECAPITAL.ConvertToDecimal(),
                        otherequity = jo.OTHEREQUITY.ConvertToDecimal(),
                        preferredstock = jo.PREFERREDSTOCK.ConvertToDecimal(),
                        sustainabledebt = jo.SUSTAINABLEDEBT.ConvertToDecimal(),
                        otherequityother = jo.OTHEREQUITYOTHER.ConvertToDecimal(),
                        capitalreserve = jo.CAPITALRESERVE.ConvertToDecimal(),
                        inventoryshare = jo.INVENTORYSHARE.ConvertToDecimal(),
                        specialreserve = jo.SPECIALRESERVE.ConvertToDecimal(),
                        surplusreserve = jo.SURPLUSRESERVE.ConvertToDecimal(),
                        generalriskprepare = jo.GENERALRISKPREPARE.ConvertToDecimal(),
                        unconfirminvloss = jo.UNCONFIRMINVLOSS.ConvertToDecimal(),
                        retainedearning = jo.RETAINEDEARNING.ConvertToDecimal(),
                        plancashdivi = jo.PLANCASHDIVI.ConvertToDecimal(),
                        diffconversionfc = jo.DIFFCONVERSIONFC.ConvertToDecimal(),
                        sumparentequity = jo.SUMPARENTEQUITY.ConvertToDecimal(),
                        minorityequity = jo.MINORITYEQUITY.ConvertToDecimal(),
                        sumshequity = jo.SUMSHEQUITY.ConvertToDecimal(),
                        sumliabshequity = jo.SUMLIABSHEQUITY.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
        }
        #endregion
        #region	em_income_broker
        public static em_income_broker get_em_income_broker_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_income_broker where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_income_broker>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_income_broker_data(em_income_broker_jo jo, bool includeUpdate)
        {
            try
            {
                em_income_broker edit_entity = get_em_income_broker_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_income_broker set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,operatereve=@operatereve,commnreve=@commnreve,agenttradesecurity=@agenttradesecurity,securityuw=@securityuw,clientassetmanage=@clientassetmanage,finaconsult=@finaconsult,sponsor=@sponsor,fundmanage=@fundmanage,fundsale=@fundsale,securitybroker=@securitybroker,commnreveother=@commnreveother,intnreve=@intnreve,investincome=@investincome,investjointincome=@investjointincome,fvalueincome=@fvalueincome,fvalueosalable=@fvalueosalable,exchangeincome=@exchangeincome,otherreve=@otherreve,operateexp=@operateexp,operatetax=@operatetax,operatemanageexp=@operatemanageexp,assetdevalueloss=@assetdevalueloss,otherexp=@otherexp,operateprofit=@operateprofit,nonoperatereve=@nonoperatereve,nonlassetreve=@nonlassetreve,nonoperateexp=@nonoperateexp,nonlassetnetloss=@nonlassetnetloss,sumprofit=@sumprofit,incometax=@incometax,netprofit=@netprofit,parentnetprofit=@parentnetprofit,minorityincome=@minorityincome,kcfjcxsyjlr=@kcfjcxsyjlr,basiceps=@basiceps,dilutedeps=@dilutedeps,othercincome=@othercincome,parentothercincome=@parentothercincome,minorityothercincome=@minorityothercincome,sumcincome=@sumcincome,parentcincome=@parentcincome,minoritycincome=@minoritycincome where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                                commnreve = jo.COMMNREVE.ConvertToDecimal(),
                                agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                                securityuw = jo.SECURITYUW.ConvertToDecimal(),
                                clientassetmanage = jo.CLIENTASSETMANAGE.ConvertToDecimal(),
                                finaconsult = jo.FINACONSULT.ConvertToDecimal(),
                                sponsor = jo.SPONSOR.ConvertToDecimal(),
                                fundmanage = jo.FUNDMANAGE.ConvertToDecimal(),
                                fundsale = jo.FUNDSALE.ConvertToDecimal(),
                                securitybroker = jo.SECURITYBROKER.ConvertToDecimal(),
                                commnreveother = jo.COMMNREVEOTHER.ConvertToDecimal(),
                                intnreve = jo.INTNREVE.ConvertToDecimal(),
                                investincome = jo.INVESTINCOME.ConvertToDecimal(),
                                investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                                fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                                fvalueosalable = jo.FVALUEOSALABLE.ConvertToDecimal(),
                                exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                                otherreve = jo.OTHERREVE.ConvertToDecimal(),
                                operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                                operatetax = jo.OPERATETAX.ConvertToDecimal(),
                                operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                                assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                                otherexp = jo.OTHEREXP.ConvertToDecimal(),
                                operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                                nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                                nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                                nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                                nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                                sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                                incometax = jo.INCOMETAX.ConvertToDecimal(),
                                netprofit = jo.NETPROFIT.ConvertToDecimal(),
                                parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                                minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                                kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                                basiceps = jo.BASICEPS.ConvertToDecimal(),
                                dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                                othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                                parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                                minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                                sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                                parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                                minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_income_broker (securitycode,reporttype,type,reportdate,operatereve,commnreve,agenttradesecurity,securityuw,clientassetmanage,finaconsult,sponsor,fundmanage,fundsale,securitybroker,commnreveother,intnreve,investincome,investjointincome,fvalueincome,fvalueosalable,exchangeincome,otherreve,operateexp,operatetax,operatemanageexp,assetdevalueloss,otherexp,operateprofit,nonoperatereve,nonlassetreve,nonoperateexp,nonlassetnetloss,sumprofit,incometax,netprofit,parentnetprofit,minorityincome,kcfjcxsyjlr,basiceps,dilutedeps,othercincome,parentothercincome,minorityothercincome,sumcincome,parentcincome,minoritycincome) values (@securitycode,@reporttype,@type,@reportdate,@operatereve,@commnreve,@agenttradesecurity,@securityuw,@clientassetmanage,@finaconsult,@sponsor,@fundmanage,@fundsale,@securitybroker,@commnreveother,@intnreve,@investincome,@investjointincome,@fvalueincome,@fvalueosalable,@exchangeincome,@otherreve,@operateexp,@operatetax,@operatemanageexp,@assetdevalueloss,@otherexp,@operateprofit,@nonoperatereve,@nonlassetreve,@nonoperateexp,@nonlassetnetloss,@sumprofit,@incometax,@netprofit,@parentnetprofit,@minorityincome,@kcfjcxsyjlr,@basiceps,@dilutedeps,@othercincome,@parentothercincome,@minorityothercincome,@sumcincome,@parentcincome,@minoritycincome)";
                    em_income_broker entity = new em_income_broker()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                        commnreve = jo.COMMNREVE.ConvertToDecimal(),
                        agenttradesecurity = jo.AGENTTRADESECURITY.ConvertToDecimal(),
                        securityuw = jo.SECURITYUW.ConvertToDecimal(),
                        clientassetmanage = jo.CLIENTASSETMANAGE.ConvertToDecimal(),
                        finaconsult = jo.FINACONSULT.ConvertToDecimal(),
                        sponsor = jo.SPONSOR.ConvertToDecimal(),
                        fundmanage = jo.FUNDMANAGE.ConvertToDecimal(),
                        fundsale = jo.FUNDSALE.ConvertToDecimal(),
                        securitybroker = jo.SECURITYBROKER.ConvertToDecimal(),
                        commnreveother = jo.COMMNREVEOTHER.ConvertToDecimal(),
                        intnreve = jo.INTNREVE.ConvertToDecimal(),
                        investincome = jo.INVESTINCOME.ConvertToDecimal(),
                        investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                        fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                        fvalueosalable = jo.FVALUEOSALABLE.ConvertToDecimal(),
                        exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                        otherreve = jo.OTHERREVE.ConvertToDecimal(),
                        operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                        operatetax = jo.OPERATETAX.ConvertToDecimal(),
                        operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                        assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                        otherexp = jo.OTHEREXP.ConvertToDecimal(),
                        operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                        nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                        nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                        nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                        nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                        sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                        incometax = jo.INCOMETAX.ConvertToDecimal(),
                        netprofit = jo.NETPROFIT.ConvertToDecimal(),
                        parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                        minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                        kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                        basiceps = jo.BASICEPS.ConvertToDecimal(),
                        dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                        othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                        parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                        minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                        sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                        parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                        minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }

            return false;
        }
        #endregion
        #region	em_income_insurance
        public static em_income_insurance get_em_income_insurance_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_income_insurance where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_income_insurance>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_income_insurance_data(em_income_insurance_jo jo, bool includeUpdate)
        {
            try
            {
                em_income_insurance edit_entity = get_em_income_insurance_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_income_insurance set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,operatereve=@operatereve,premiumearned=@premiumearned,insurreve=@insurreve,rireve=@rireve,ripremium=@ripremium,unduereserve=@unduereserve,bankintnreve=@bankintnreve,bankintreve=@bankintreve,bankintexp=@bankintexp,ninsurcommnreve=@ninsurcommnreve,ninsurcommreve=@ninsurcommreve,ninsurcommexp=@ninsurcommexp,investincome=@investincome,investjointincome=@investjointincome,fvalueincome=@fvalueincome,fvalueosalable=@fvalueosalable,exchangeincome=@exchangeincome,otherreve=@otherreve,operateexp=@operateexp,surrenderpremium=@surrenderpremium,indemnityexp=@indemnityexp,amortiseindemnityexp=@amortiseindemnityexp,dutyreserve=@dutyreserve,amortisedutyreserve=@amortisedutyreserve,policydiviexp=@policydiviexp,riexp=@riexp,operatetax=@operatetax,commexp=@commexp,operatemanageexp=@operatemanageexp,amortiseriexp=@amortiseriexp,intexp=@intexp,financeexp=@financeexp,otherexp=@otherexp,assetdevalueloss=@assetdevalueloss,operateprofit=@operateprofit,nonoperatereve=@nonoperatereve,nonlassetreve=@nonlassetreve,nonoperateexp=@nonoperateexp,nonlassetnetloss=@nonlassetnetloss,sumprofit=@sumprofit,incometax=@incometax,netprofit=@netprofit,parentnetprofit=@parentnetprofit,minorityincome=@minorityincome,kcfjcxsyjlr=@kcfjcxsyjlr,basiceps=@basiceps,dilutedeps=@dilutedeps,othercincome=@othercincome,parentothercincome=@parentothercincome,minorityothercincome=@minorityothercincome,sumcincome=@sumcincome,parentcincome=@parentcincome,minoritycincome=@minoritycincome where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                                premiumearned = jo.PREMIUMEARNED.ConvertToDecimal(),
                                insurreve = jo.INSURREVE.ConvertToDecimal(),
                                rireve = jo.RIREVE.ConvertToDecimal(),
                                ripremium = jo.RIPREMIUM.ConvertToDecimal(),
                                unduereserve = jo.UNDUERESERVE.ConvertToDecimal(),
                                bankintnreve = jo.BANKINTNREVE.ConvertToDecimal(),
                                bankintreve = jo.BANKINTREVE.ConvertToDecimal(),
                                bankintexp = jo.BANKINTEXP.ConvertToDecimal(),
                                ninsurcommnreve = jo.NINSURCOMMNREVE.ConvertToDecimal(),
                                ninsurcommreve = jo.NINSURCOMMREVE.ConvertToDecimal(),
                                ninsurcommexp = jo.NINSURCOMMEXP.ConvertToDecimal(),
                                investincome = jo.INVESTINCOME.ConvertToDecimal(),
                                investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                                fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                                fvalueosalable = jo.FVALUEOSALABLE.ConvertToDecimal(),
                                exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                                otherreve = jo.OTHERREVE.ConvertToDecimal(),
                                operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                                surrenderpremium = jo.SURRENDERPREMIUM.ConvertToDecimal(),
                                indemnityexp = jo.INDEMNITYEXP.ConvertToDecimal(),
                                amortiseindemnityexp = jo.AMORTISEINDEMNITYEXP.ConvertToDecimal(),
                                dutyreserve = jo.DUTYRESERVE.ConvertToDecimal(),
                                amortisedutyreserve = jo.AMORTISEDUTYRESERVE.ConvertToDecimal(),
                                policydiviexp = jo.POLICYDIVIEXP.ConvertToDecimal(),
                                riexp = jo.RIEXP.ConvertToDecimal(),
                                operatetax = jo.OPERATETAX.ConvertToDecimal(),
                                commexp = jo.COMMEXP.ConvertToDecimal(),
                                operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                                amortiseriexp = jo.AMORTISERIEXP.ConvertToDecimal(),
                                intexp = jo.INTEXP.ConvertToDecimal(),
                                financeexp = jo.FINANCEEXP.ConvertToDecimal(),
                                otherexp = jo.OTHEREXP.ConvertToDecimal(),
                                assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                                operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                                nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                                nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                                nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                                nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                                sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                                incometax = jo.INCOMETAX.ConvertToDecimal(),
                                netprofit = jo.NETPROFIT.ConvertToDecimal(),
                                parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                                minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                                kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                                basiceps = jo.BASICEPS.ConvertToDecimal(),
                                dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                                othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                                parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                                minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                                sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                                parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                                minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_income_insurance (securitycode,reporttype,type,reportdate,operatereve,premiumearned,insurreve,rireve,ripremium,unduereserve,bankintnreve,bankintreve,bankintexp,ninsurcommnreve,ninsurcommreve,ninsurcommexp,investincome,investjointincome,fvalueincome,fvalueosalable,exchangeincome,otherreve,operateexp,surrenderpremium,indemnityexp,amortiseindemnityexp,dutyreserve,amortisedutyreserve,policydiviexp,riexp,operatetax,commexp,operatemanageexp,amortiseriexp,intexp,financeexp,otherexp,assetdevalueloss,operateprofit,nonoperatereve,nonlassetreve,nonoperateexp,nonlassetnetloss,sumprofit,incometax,netprofit,parentnetprofit,minorityincome,kcfjcxsyjlr,basiceps,dilutedeps,othercincome,parentothercincome,minorityothercincome,sumcincome,parentcincome,minoritycincome) values (@securitycode,@reporttype,@type,@reportdate,@operatereve,@premiumearned,@insurreve,@rireve,@ripremium,@unduereserve,@bankintnreve,@bankintreve,@bankintexp,@ninsurcommnreve,@ninsurcommreve,@ninsurcommexp,@investincome,@investjointincome,@fvalueincome,@fvalueosalable,@exchangeincome,@otherreve,@operateexp,@surrenderpremium,@indemnityexp,@amortiseindemnityexp,@dutyreserve,@amortisedutyreserve,@policydiviexp,@riexp,@operatetax,@commexp,@operatemanageexp,@amortiseriexp,@intexp,@financeexp,@otherexp,@assetdevalueloss,@operateprofit,@nonoperatereve,@nonlassetreve,@nonoperateexp,@nonlassetnetloss,@sumprofit,@incometax,@netprofit,@parentnetprofit,@minorityincome,@kcfjcxsyjlr,@basiceps,@dilutedeps,@othercincome,@parentothercincome,@minorityothercincome,@sumcincome,@parentcincome,@minoritycincome)";
                    em_income_insurance entity = new em_income_insurance()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                        premiumearned = jo.PREMIUMEARNED.ConvertToDecimal(),
                        insurreve = jo.INSURREVE.ConvertToDecimal(),
                        rireve = jo.RIREVE.ConvertToDecimal(),
                        ripremium = jo.RIPREMIUM.ConvertToDecimal(),
                        unduereserve = jo.UNDUERESERVE.ConvertToDecimal(),
                        bankintnreve = jo.BANKINTNREVE.ConvertToDecimal(),
                        bankintreve = jo.BANKINTREVE.ConvertToDecimal(),
                        bankintexp = jo.BANKINTEXP.ConvertToDecimal(),
                        ninsurcommnreve = jo.NINSURCOMMNREVE.ConvertToDecimal(),
                        ninsurcommreve = jo.NINSURCOMMREVE.ConvertToDecimal(),
                        ninsurcommexp = jo.NINSURCOMMEXP.ConvertToDecimal(),
                        investincome = jo.INVESTINCOME.ConvertToDecimal(),
                        investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                        fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                        fvalueosalable = jo.FVALUEOSALABLE.ConvertToDecimal(),
                        exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                        otherreve = jo.OTHERREVE.ConvertToDecimal(),
                        operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                        surrenderpremium = jo.SURRENDERPREMIUM.ConvertToDecimal(),
                        indemnityexp = jo.INDEMNITYEXP.ConvertToDecimal(),
                        amortiseindemnityexp = jo.AMORTISEINDEMNITYEXP.ConvertToDecimal(),
                        dutyreserve = jo.DUTYRESERVE.ConvertToDecimal(),
                        amortisedutyreserve = jo.AMORTISEDUTYRESERVE.ConvertToDecimal(),
                        policydiviexp = jo.POLICYDIVIEXP.ConvertToDecimal(),
                        riexp = jo.RIEXP.ConvertToDecimal(),
                        operatetax = jo.OPERATETAX.ConvertToDecimal(),
                        commexp = jo.COMMEXP.ConvertToDecimal(),
                        operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                        amortiseriexp = jo.AMORTISERIEXP.ConvertToDecimal(),
                        intexp = jo.INTEXP.ConvertToDecimal(),
                        financeexp = jo.FINANCEEXP.ConvertToDecimal(),
                        otherexp = jo.OTHEREXP.ConvertToDecimal(),
                        assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                        operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                        nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                        nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                        nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                        nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                        sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                        incometax = jo.INCOMETAX.ConvertToDecimal(),
                        netprofit = jo.NETPROFIT.ConvertToDecimal(),
                        parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                        minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                        kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                        basiceps = jo.BASICEPS.ConvertToDecimal(),
                        dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                        othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                        parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                        minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                        sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                        parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                        minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }

            return false;
        }
        #endregion
        #region	em_income_bank
        public static em_income_bank get_em_income_bank_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_income_bank where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_income_bank>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_income_bank_data(em_income_bank_jo jo, bool includeUpdate)
        {
            try
            {
                em_income_bank edit_entity = get_em_income_bank_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_income_bank set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,operatereve=@operatereve,intnreve=@intnreve,intreve=@intreve,intexp=@intexp,commnreve=@commnreve,commreve=@commreve,commexp=@commexp,investincome=@investincome,investjointincome=@investjointincome,fvalueincome=@fvalueincome,exchangeincome=@exchangeincome,otherreve=@otherreve,operateexp=@operateexp,operatetax=@operatetax,operatemanageexp=@operatemanageexp,assetdevalueloss=@assetdevalueloss,otherexp=@otherexp,operateprofit=@operateprofit,nonoperatereve=@nonoperatereve,nonoperateexp=@nonoperateexp,sumprofit=@sumprofit,incometax=@incometax,netprofit=@netprofit,parentnetprofit=@parentnetprofit,minorityincome=@minorityincome,kcfjcxsyjlr=@kcfjcxsyjlr,dilutedeps=@dilutedeps,basiceps=@basiceps,othercincome=@othercincome,parentothercincome=@parentothercincome,minorityothercincome=@minorityothercincome,sumcincome=@sumcincome,parentcincome=@parentcincome,minoritycincome=@minoritycincome where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                                intnreve = jo.INTNREVE.ConvertToDecimal(),
                                intreve = jo.INTREVE.ConvertToDecimal(),
                                intexp = jo.INTEXP.ConvertToDecimal(),
                                commnreve = jo.COMMNREVE.ConvertToDecimal(),
                                commreve = jo.COMMREVE.ConvertToDecimal(),
                                commexp = jo.COMMEXP.ConvertToDecimal(),
                                investincome = jo.INVESTINCOME.ConvertToDecimal(),
                                investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                                fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                                exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                                otherreve = jo.OTHERREVE.ConvertToDecimal(),
                                operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                                operatetax = jo.OPERATETAX.ConvertToDecimal(),
                                operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                                assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                                otherexp = jo.OTHEREXP.ConvertToDecimal(),
                                operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                                nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                                nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                                sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                                incometax = jo.INCOMETAX.ConvertToDecimal(),
                                netprofit = jo.NETPROFIT.ConvertToDecimal(),
                                parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                                minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                                kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                                dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                                basiceps = jo.BASICEPS.ConvertToDecimal(),
                                othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                                parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                                minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                                sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                                parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                                minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_income_bank (securitycode,reporttype,type,reportdate,operatereve,intnreve,intreve,intexp,commnreve,commreve,commexp,investincome,investjointincome,fvalueincome,exchangeincome,otherreve,operateexp,operatetax,operatemanageexp,assetdevalueloss,otherexp,operateprofit,nonoperatereve,nonoperateexp,sumprofit,incometax,netprofit,parentnetprofit,minorityincome,kcfjcxsyjlr,dilutedeps,basiceps,othercincome,parentothercincome,minorityothercincome,sumcincome,parentcincome,minoritycincome) values (@securitycode,@reporttype,@type,@reportdate,@operatereve,@intnreve,@intreve,@intexp,@commnreve,@commreve,@commexp,@investincome,@investjointincome,@fvalueincome,@exchangeincome,@otherreve,@operateexp,@operatetax,@operatemanageexp,@assetdevalueloss,@otherexp,@operateprofit,@nonoperatereve,@nonoperateexp,@sumprofit,@incometax,@netprofit,@parentnetprofit,@minorityincome,@kcfjcxsyjlr,@dilutedeps,@basiceps,@othercincome,@parentothercincome,@minorityothercincome,@sumcincome,@parentcincome,@minoritycincome)";
                    em_income_bank entity = new em_income_bank()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                        intnreve = jo.INTNREVE.ConvertToDecimal(),
                        intreve = jo.INTREVE.ConvertToDecimal(),
                        intexp = jo.INTEXP.ConvertToDecimal(),
                        commnreve = jo.COMMNREVE.ConvertToDecimal(),
                        commreve = jo.COMMREVE.ConvertToDecimal(),
                        commexp = jo.COMMEXP.ConvertToDecimal(),
                        investincome = jo.INVESTINCOME.ConvertToDecimal(),
                        investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                        fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                        exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                        otherreve = jo.OTHERREVE.ConvertToDecimal(),
                        operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                        operatetax = jo.OPERATETAX.ConvertToDecimal(),
                        operatemanageexp = jo.OPERATEMANAGEEXP.ConvertToDecimal(),
                        assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                        otherexp = jo.OTHEREXP.ConvertToDecimal(),
                        operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                        nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                        nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                        sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                        incometax = jo.INCOMETAX.ConvertToDecimal(),
                        netprofit = jo.NETPROFIT.ConvertToDecimal(),
                        parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                        minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                        kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                        dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                        basiceps = jo.BASICEPS.ConvertToDecimal(),
                        othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                        parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                        minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                        sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                        parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                        minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }

                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }

            return false;
        }
        #endregion
        #region	em_income_common
        public static em_income_common get_em_income_common_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_income_common where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_income_common>(sql).FirstOrDefault();
            }
        }

        public static void oper_em_income_common_data(em_income_common_jo jo)
        {
            try
            {
                em_income_common edit_entity = get_em_income_common_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "update em_income_common set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,totaloperatereve=@totaloperatereve,operatereve=@operatereve,intreve=@intreve,premiumearned=@premiumearned,commreve=@commreve,otherreve=@otherreve,totaloperateexp=@totaloperateexp,operateexp=@operateexp,intexp=@intexp,commexp=@commexp,rdexp=@rdexp,surrenderpremium=@surrenderpremium,netindemnityexp=@netindemnityexp,netcontactreserve=@netcontactreserve,policydiviexp=@policydiviexp,riexp=@riexp,otherexp=@otherexp,operatetax=@operatetax,saleexp=@saleexp,manageexp=@manageexp,financeexp=@financeexp,assetdevalueloss=@assetdevalueloss,fvalueincome=@fvalueincome,investincome=@investincome,investjointincome=@investjointincome,exchangeincome=@exchangeincome,operateprofit=@operateprofit,nonoperatereve=@nonoperatereve,nonlassetreve=@nonlassetreve,nonoperateexp=@nonoperateexp,nonlassetnetloss=@nonlassetnetloss,sumprofit=@sumprofit,incometax=@incometax,netprofit=@netprofit,combinednetprofitb=@combinednetprofitb,parentnetprofit=@parentnetprofit,minorityincome=@minorityincome,kcfjcxsyjlr=@kcfjcxsyjlr,basiceps=@basiceps,dilutedeps=@dilutedeps,othercincome=@othercincome,parentothercincome=@parentothercincome,minorityothercincome=@minorityothercincome,sumcincome=@sumcincome,parentcincome=@parentcincome,minoritycincome=@minoritycincome where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            securitycode = jo.SECURITYCODE,
                            reporttype = jo.REPORTTYPE,
                            type = jo.TYPE,
                            reportdate = jo.REPORTDATE.ConvertToDate(),
                            totaloperatereve = jo.TOTALOPERATEREVE.ConvertToDecimal(),
                            operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                            intreve = jo.INTREVE.ConvertToDecimal(),
                            premiumearned = jo.PREMIUMEARNED.ConvertToDecimal(),
                            commreve = jo.COMMREVE.ConvertToDecimal(),
                            otherreve = jo.OTHERREVE.ConvertToDecimal(),
                            totaloperateexp = jo.TOTALOPERATEEXP.ConvertToDecimal(),
                            operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                            intexp = jo.INTEXP.ConvertToDecimal(),
                            commexp = jo.COMMEXP.ConvertToDecimal(),
                            rdexp = jo.RDEXP.ConvertToDecimal(),
                            surrenderpremium = jo.SURRENDERPREMIUM.ConvertToDecimal(),
                            netindemnityexp = jo.NETINDEMNITYEXP.ConvertToDecimal(),
                            netcontactreserve = jo.NETCONTACTRESERVE.ConvertToDecimal(),
                            policydiviexp = jo.POLICYDIVIEXP.ConvertToDecimal(),
                            riexp = jo.RIEXP.ConvertToDecimal(),
                            otherexp = jo.OTHEREXP.ConvertToDecimal(),
                            operatetax = jo.OPERATETAX.ConvertToDecimal(),
                            saleexp = jo.SALEEXP.ConvertToDecimal(),
                            manageexp = jo.MANAGEEXP.ConvertToDecimal(),
                            financeexp = jo.FINANCEEXP.ConvertToDecimal(),
                            assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                            fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                            investincome = jo.INVESTINCOME.ConvertToDecimal(),
                            investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                            exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                            operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                            nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                            nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                            nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                            nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                            sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                            incometax = jo.INCOMETAX.ConvertToDecimal(),
                            netprofit = jo.NETPROFIT.ConvertToDecimal(),
                            combinednetprofitb = jo.COMBINEDNETPROFITB.ConvertToDecimal(),
                            parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                            minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                            kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                            basiceps = jo.BASICEPS.ConvertToDecimal(),
                            dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                            othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                            parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                            minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                            sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                            parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                            minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                            id = edit_entity.id
                        });
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_income_common (securitycode,reporttype,type,reportdate,totaloperatereve,operatereve,intreve,premiumearned,commreve,otherreve,totaloperateexp,operateexp,intexp,commexp,rdexp,surrenderpremium,netindemnityexp,netcontactreserve,policydiviexp,riexp,otherexp,operatetax,saleexp,manageexp,financeexp,assetdevalueloss,fvalueincome,investincome,investjointincome,exchangeincome,operateprofit,nonoperatereve,nonlassetreve,nonoperateexp,nonlassetnetloss,sumprofit,incometax,netprofit,combinednetprofitb,parentnetprofit,minorityincome,kcfjcxsyjlr,basiceps,dilutedeps,othercincome,parentothercincome,minorityothercincome,sumcincome,parentcincome,minoritycincome) values (@securitycode,@reporttype,@type,@reportdate,@totaloperatereve,@operatereve,@intreve,@premiumearned,@commreve,@otherreve,@totaloperateexp,@operateexp,@intexp,@commexp,@rdexp,@surrenderpremium,@netindemnityexp,@netcontactreserve,@policydiviexp,@riexp,@otherexp,@operatetax,@saleexp,@manageexp,@financeexp,@assetdevalueloss,@fvalueincome,@investincome,@investjointincome,@exchangeincome,@operateprofit,@nonoperatereve,@nonlassetreve,@nonoperateexp,@nonlassetnetloss,@sumprofit,@incometax,@netprofit,@combinednetprofitb,@parentnetprofit,@minorityincome,@kcfjcxsyjlr,@basiceps,@dilutedeps,@othercincome,@parentothercincome,@minorityothercincome,@sumcincome,@parentcincome,@minoritycincome)";
                    em_income_common entity = new em_income_common()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        totaloperatereve = jo.TOTALOPERATEREVE.ConvertToDecimal(),
                        operatereve = jo.OPERATEREVE.ConvertToDecimal(),
                        intreve = jo.INTREVE.ConvertToDecimal(),
                        premiumearned = jo.PREMIUMEARNED.ConvertToDecimal(),
                        commreve = jo.COMMREVE.ConvertToDecimal(),
                        otherreve = jo.OTHERREVE.ConvertToDecimal(),
                        totaloperateexp = jo.TOTALOPERATEEXP.ConvertToDecimal(),
                        operateexp = jo.OPERATEEXP.ConvertToDecimal(),
                        intexp = jo.INTEXP.ConvertToDecimal(),
                        commexp = jo.COMMEXP.ConvertToDecimal(),
                        rdexp = jo.RDEXP.ConvertToDecimal(),
                        surrenderpremium = jo.SURRENDERPREMIUM.ConvertToDecimal(),
                        netindemnityexp = jo.NETINDEMNITYEXP.ConvertToDecimal(),
                        netcontactreserve = jo.NETCONTACTRESERVE.ConvertToDecimal(),
                        policydiviexp = jo.POLICYDIVIEXP.ConvertToDecimal(),
                        riexp = jo.RIEXP.ConvertToDecimal(),
                        otherexp = jo.OTHEREXP.ConvertToDecimal(),
                        operatetax = jo.OPERATETAX.ConvertToDecimal(),
                        saleexp = jo.SALEEXP.ConvertToDecimal(),
                        manageexp = jo.MANAGEEXP.ConvertToDecimal(),
                        financeexp = jo.FINANCEEXP.ConvertToDecimal(),
                        assetdevalueloss = jo.ASSETDEVALUELOSS.ConvertToDecimal(),
                        fvalueincome = jo.FVALUEINCOME.ConvertToDecimal(),
                        investincome = jo.INVESTINCOME.ConvertToDecimal(),
                        investjointincome = jo.INVESTJOINTINCOME.ConvertToDecimal(),
                        exchangeincome = jo.EXCHANGEINCOME.ConvertToDecimal(),
                        operateprofit = jo.OPERATEPROFIT.ConvertToDecimal(),
                        nonoperatereve = jo.NONOPERATEREVE.ConvertToDecimal(),
                        nonlassetreve = jo.NONLASSETREVE.ConvertToDecimal(),
                        nonoperateexp = jo.NONOPERATEEXP.ConvertToDecimal(),
                        nonlassetnetloss = jo.NONLASSETNETLOSS.ConvertToDecimal(),
                        sumprofit = jo.SUMPROFIT.ConvertToDecimal(),
                        incometax = jo.INCOMETAX.ConvertToDecimal(),
                        netprofit = jo.NETPROFIT.ConvertToDecimal(),
                        combinednetprofitb = jo.COMBINEDNETPROFITB.ConvertToDecimal(),
                        parentnetprofit = jo.PARENTNETPROFIT.ConvertToDecimal(),
                        minorityincome = jo.MINORITYINCOME.ConvertToDecimal(),
                        kcfjcxsyjlr = jo.KCFJCXSYJLR.ConvertToDecimal(),
                        basiceps = jo.BASICEPS.ConvertToDecimal(),
                        dilutedeps = jo.DILUTEDEPS.ConvertToDecimal(),
                        othercincome = jo.OTHERCINCOME.ConvertToDecimal(),
                        parentothercincome = jo.PARENTOTHERCINCOME.ConvertToDecimal(),
                        minorityothercincome = jo.MINORITYOTHERCINCOME.ConvertToDecimal(),
                        sumcincome = jo.SUMCINCOME.ConvertToDecimal(),
                        parentcincome = jo.PARENTCINCOME.ConvertToDecimal(),
                        minoritycincome = jo.MINORITYCINCOME.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
        }
        #endregion
        #region	em_cashflow_broker
        public static em_cashflow_broker get_em_cashflow_broker_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_cashflow_broker where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_cashflow_broker>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_cashflow_broker_data(em_cashflow_broker_jo jo, bool includeUpdate)
        {
            try
            {
                em_cashflow_broker edit_entity = get_em_cashflow_broker_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_cashflow_broker set  securitycode=@securitycode,reporttype=@reporttype,reportdatetype=@reportdatetype,type=@type,reportdate=@reportdate,currency=@currency,nidisptradefasset=@nidisptradefasset,niotherfinainstru=@niotherfinainstru,intandcommrec=@intandcommrec,uwsecurityrec=@uwsecurityrec,niborrowfund=@niborrowfund,agenttradesecurityrec=@agenttradesecurityrec,buysellbackfassetrec=@buysellbackfassetrec,agentuwsecurityrec=@agentuwsecurityrec,nibuybackfund=@nibuybackfund,nitradesettlement=@nitradesettlement,nidirectinv=@nidirectinv,taxreturnrec=@taxreturnrec,otheroperaterec=@otheroperaterec,sumoperateflowin=@sumoperateflowin,buysellbackfassetpay=@buysellbackfassetpay,nddisptradefasset=@nddisptradefasset,ndotherfinainstr=@ndotherfinainstr,intandcommpay=@intandcommpay,ndborrowfund=@ndborrowfund,employeepay=@employeepay,taxpay=@taxpay,ndtradesettlement=@ndtradesettlement,nddirectinv=@nddirectinv,nilendfund=@nilendfund,ndbuybackfund=@ndbuybackfund,agenttradesecuritypay=@agenttradesecuritypay,otheroperatepay=@otheroperatepay,sumoperateflowout=@sumoperateflowout,netoperatecashflow=@netoperatecashflow,disposalinvrec=@disposalinvrec,nidispsaleablefasset=@nidispsaleablefasset,invincomerec=@invincomerec,dispfilassetrec=@dispfilassetrec,dispsubsidiaryrec=@dispsubsidiaryrec,otherinvrec=@otherinvrec,suminvflowin=@suminvflowin,invpay=@invpay,nddispsaleablefasset=@nddispsaleablefasset,buyfilassetpay=@buyfilassetpay,getsubsidiarypay=@getsubsidiarypay,otherinvpay=@otherinvpay,suminvflowout=@suminvflowout,netinvcashflow=@netinvcashflow,acceptinvrec=@acceptinvrec,subsidiaryaccept=@subsidiaryaccept,loanrec=@loanrec,issuebondrec=@issuebondrec,otherfinarec=@otherfinarec,sumfinaflowin=@sumfinaflowin,repaydebtpay=@repaydebtpay,diviprofitorintpay=@diviprofitorintpay,subsidiarypay=@subsidiarypay,otherfinapay=@otherfinapay,sumfinaflowout=@sumfinaflowout,netfinacashflow=@netfinacashflow,effectexchangerate=@effectexchangerate,nicashequi=@nicashequi,cashequibeginning=@cashequibeginning,cashequiending=@cashequiending where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                reportdatetype = jo.REPORTDATETYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                currency = jo.CURRENCY,
                                nidisptradefasset = jo.NIDISPTRADEFASSET.ConvertToDecimal(),
                                niotherfinainstru = jo.NIOTHERFINAINSTRU.ConvertToDecimal(),
                                intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                                uwsecurityrec = jo.UWSECURITYREC.ConvertToDecimal(),
                                niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                                agenttradesecurityrec = jo.AGENTTRADESECURITYREC.ConvertToDecimal(),
                                buysellbackfassetrec = jo.BUYSELLBACKFASSETREC.ConvertToDecimal(),
                                agentuwsecurityrec = jo.AGENTUWSECURITYREC.ConvertToDecimal(),
                                nibuybackfund = jo.NIBUYBACKFUND.ConvertToDecimal(),
                                nitradesettlement = jo.NITRADESETTLEMENT.ConvertToDecimal(),
                                nidirectinv = jo.NIDIRECTINV.ConvertToDecimal(),
                                taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                                otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                                sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                                buysellbackfassetpay = jo.BUYSELLBACKFASSETPAY.ConvertToDecimal(),
                                nddisptradefasset = jo.NDDISPTRADEFASSET.ConvertToDecimal(),
                                ndotherfinainstr = jo.NDOTHERFINAINSTR.ConvertToDecimal(),
                                intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                                ndborrowfund = jo.NDBORROWFUND.ConvertToDecimal(),
                                employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                ndtradesettlement = jo.NDTRADESETTLEMENT.ConvertToDecimal(),
                                nddirectinv = jo.NDDIRECTINV.ConvertToDecimal(),
                                nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                                ndbuybackfund = jo.NDBUYBACKFUND.ConvertToDecimal(),
                                agenttradesecuritypay = jo.AGENTTRADESECURITYPAY.ConvertToDecimal(),
                                otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                                sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                                netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                                disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                                nidispsaleablefasset = jo.NIDISPSALEABLEFASSET.ConvertToDecimal(),
                                invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                                dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                                dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                                otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                                suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                                invpay = jo.INVPAY.ConvertToDecimal(),
                                nddispsaleablefasset = jo.NDDISPSALEABLEFASSET.ConvertToDecimal(),
                                buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                                getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                                otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                                suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                                netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                                acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                                subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                                loanrec = jo.LOANREC.ConvertToDecimal(),
                                issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                                otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                                sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                                repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                                diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                                subsidiarypay = jo.SUBSIDIARYPAY.ConvertToDecimal(),
                                otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                                sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                                netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                                effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                                nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                                cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                                cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_cashflow_broker (securitycode,reporttype,reportdatetype,type,reportdate,currency,nidisptradefasset,niotherfinainstru,intandcommrec,uwsecurityrec,niborrowfund,agenttradesecurityrec,buysellbackfassetrec,agentuwsecurityrec,nibuybackfund,nitradesettlement,nidirectinv,taxreturnrec,otheroperaterec,sumoperateflowin,buysellbackfassetpay,nddisptradefasset,ndotherfinainstr,intandcommpay,ndborrowfund,employeepay,taxpay,ndtradesettlement,nddirectinv,nilendfund,ndbuybackfund,agenttradesecuritypay,otheroperatepay,sumoperateflowout,netoperatecashflow,disposalinvrec,nidispsaleablefasset,invincomerec,dispfilassetrec,dispsubsidiaryrec,otherinvrec,suminvflowin,invpay,nddispsaleablefasset,buyfilassetpay,getsubsidiarypay,otherinvpay,suminvflowout,netinvcashflow,acceptinvrec,subsidiaryaccept,loanrec,issuebondrec,otherfinarec,sumfinaflowin,repaydebtpay,diviprofitorintpay,subsidiarypay,otherfinapay,sumfinaflowout,netfinacashflow,effectexchangerate,nicashequi,cashequibeginning,cashequiending) values (@securitycode,@reporttype,@reportdatetype,@type,@reportdate,@currency,@nidisptradefasset,@niotherfinainstru,@intandcommrec,@uwsecurityrec,@niborrowfund,@agenttradesecurityrec,@buysellbackfassetrec,@agentuwsecurityrec,@nibuybackfund,@nitradesettlement,@nidirectinv,@taxreturnrec,@otheroperaterec,@sumoperateflowin,@buysellbackfassetpay,@nddisptradefasset,@ndotherfinainstr,@intandcommpay,@ndborrowfund,@employeepay,@taxpay,@ndtradesettlement,@nddirectinv,@nilendfund,@ndbuybackfund,@agenttradesecuritypay,@otheroperatepay,@sumoperateflowout,@netoperatecashflow,@disposalinvrec,@nidispsaleablefasset,@invincomerec,@dispfilassetrec,@dispsubsidiaryrec,@otherinvrec,@suminvflowin,@invpay,@nddispsaleablefasset,@buyfilassetpay,@getsubsidiarypay,@otherinvpay,@suminvflowout,@netinvcashflow,@acceptinvrec,@subsidiaryaccept,@loanrec,@issuebondrec,@otherfinarec,@sumfinaflowin,@repaydebtpay,@diviprofitorintpay,@subsidiarypay,@otherfinapay,@sumfinaflowout,@netfinacashflow,@effectexchangerate,@nicashequi,@cashequibeginning,@cashequiending)";
                    em_cashflow_broker entity = new em_cashflow_broker()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        reportdatetype = jo.REPORTDATETYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        currency = jo.CURRENCY,
                        nidisptradefasset = jo.NIDISPTRADEFASSET.ConvertToDecimal(),
                        niotherfinainstru = jo.NIOTHERFINAINSTRU.ConvertToDecimal(),
                        intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                        uwsecurityrec = jo.UWSECURITYREC.ConvertToDecimal(),
                        niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                        agenttradesecurityrec = jo.AGENTTRADESECURITYREC.ConvertToDecimal(),
                        buysellbackfassetrec = jo.BUYSELLBACKFASSETREC.ConvertToDecimal(),
                        agentuwsecurityrec = jo.AGENTUWSECURITYREC.ConvertToDecimal(),
                        nibuybackfund = jo.NIBUYBACKFUND.ConvertToDecimal(),
                        nitradesettlement = jo.NITRADESETTLEMENT.ConvertToDecimal(),
                        nidirectinv = jo.NIDIRECTINV.ConvertToDecimal(),
                        taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                        otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                        sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                        buysellbackfassetpay = jo.BUYSELLBACKFASSETPAY.ConvertToDecimal(),
                        nddisptradefasset = jo.NDDISPTRADEFASSET.ConvertToDecimal(),
                        ndotherfinainstr = jo.NDOTHERFINAINSTR.ConvertToDecimal(),
                        intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                        ndborrowfund = jo.NDBORROWFUND.ConvertToDecimal(),
                        employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        ndtradesettlement = jo.NDTRADESETTLEMENT.ConvertToDecimal(),
                        nddirectinv = jo.NDDIRECTINV.ConvertToDecimal(),
                        nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                        ndbuybackfund = jo.NDBUYBACKFUND.ConvertToDecimal(),
                        agenttradesecuritypay = jo.AGENTTRADESECURITYPAY.ConvertToDecimal(),
                        otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                        sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                        netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                        disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                        nidispsaleablefasset = jo.NIDISPSALEABLEFASSET.ConvertToDecimal(),
                        invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                        dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                        dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                        otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                        suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                        invpay = jo.INVPAY.ConvertToDecimal(),
                        nddispsaleablefasset = jo.NDDISPSALEABLEFASSET.ConvertToDecimal(),
                        buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                        getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                        otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                        suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                        netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                        acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                        subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                        loanrec = jo.LOANREC.ConvertToDecimal(),
                        issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                        otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                        sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                        repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                        diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                        subsidiarypay = jo.SUBSIDIARYPAY.ConvertToDecimal(),
                        otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                        sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                        netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                        effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                        nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                        cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                        cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
            return false;
        }
        #endregion
        #region	em_cashflow_insurance
        public static em_cashflow_insurance get_em_cashflow_insurance_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_cashflow_insurance where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_cashflow_insurance>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_cashflow_insurance_data(em_cashflow_insurance_jo jo, bool includeUpdate)
        {
            try
            {
                em_cashflow_insurance edit_entity = get_em_cashflow_insurance_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_cashflow_insurance set  securitycode=@securitycode,reporttype=@reporttype,reportdatetype=@reportdatetype,type=@type,reportdate=@reportdate,currency=@currency,nideposit=@nideposit,premiumrec=@premiumrec,netrirec=@netrirec,niinsureddepositinv=@niinsureddepositinv,taxreturnrec=@taxreturnrec,nettradefassetrec=@nettradefassetrec,intandcommrec=@intandcommrec,nilendfund=@nilendfund,nddepositincbankfi=@nddepositincbankfi,nisellbuyback=@nisellbuyback,ndbuysellback=@ndbuysellback,otheroperaterec=@otheroperaterec,sumoperateflowin=@sumoperateflowin,indemnitypay=@indemnitypay,netripay=@netripay,ndlendfund=@ndlendfund,nibuysellback=@nibuysellback,ndsellbuyback=@ndsellbuyback,niloanadvances=@niloanadvances,intandcommpay=@intandcommpay,divipay=@divipay,ndinsureddepositinv=@ndinsureddepositinv,nidepositincbankfi=@nidepositincbankfi,employeepay=@employeepay,taxpay=@taxpay,nettradefassetpay=@nettradefassetpay,otheroperatepay=@otheroperatepay,sumoperateflowout=@sumoperateflowout,netoperatecashflow=@netoperatecashflow,disposalinvrec=@disposalinvrec,invincomerec=@invincomerec,dispfilassetrec=@dispfilassetrec,dispsubsidiaryrec=@dispsubsidiaryrec,buysellbackfassetrec=@buysellbackfassetrec,otherinvrec=@otherinvrec,suminvflowin=@suminvflowin,invpay=@invpay,niinsuredpledgeloan=@niinsuredpledgeloan,buyfilassetpay=@buyfilassetpay,buysubsidiarypay=@buysubsidiarypay,dispsubsidiarypay=@dispsubsidiarypay,buysellbackfassetpay=@buysellbackfassetpay,otherinvpay=@otherinvpay,suminvflowout=@suminvflowout,netinvcashflow=@netinvcashflow,acceptinvrec=@acceptinvrec,loanrec=@loanrec,issuebondrec=@issuebondrec,netsellbuybackfassetrec=@netsellbuybackfassetrec,otherfinarec=@otherfinarec,sumfinaflowin=@sumfinaflowin,repaydebtpay=@repaydebtpay,diviprofitorintpay=@diviprofitorintpay,netsellbuybackfassetpay=@netsellbuybackfassetpay,otherfinapay=@otherfinapay,sumfinaflowout=@sumfinaflowout,netfinacashflow=@netfinacashflow,effectexchangerate=@effectexchangerate,nicashequi=@nicashequi,cashequibeginning=@cashequibeginning,cashequiending=@cashequiending where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                reportdatetype = jo.REPORTDATETYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                currency = jo.CURRENCY,
                                nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                                premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                                netrirec = jo.NETRIREC.ConvertToDecimal(),
                                niinsureddepositinv = jo.NIINSUREDDEPOSITINV.ConvertToDecimal(),
                                taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                                nettradefassetrec = jo.NETTRADEFASSETREC.ConvertToDecimal(),
                                intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                                nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                                nddepositincbankfi = jo.NDDEPOSITINCBANKFI.ConvertToDecimal(),
                                nisellbuyback = jo.NISELLBUYBACK.ConvertToDecimal(),
                                ndbuysellback = jo.NDBUYSELLBACK.ConvertToDecimal(),
                                otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                                sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                                indemnitypay = jo.INDEMNITYPAY.ConvertToDecimal(),
                                netripay = jo.NETRIPAY.ConvertToDecimal(),
                                ndlendfund = jo.NDLENDFUND.ConvertToDecimal(),
                                nibuysellback = jo.NIBUYSELLBACK.ConvertToDecimal(),
                                ndsellbuyback = jo.NDSELLBUYBACK.ConvertToDecimal(),
                                niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                                intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                                divipay = jo.DIVIPAY.ConvertToDecimal(),
                                ndinsureddepositinv = jo.NDINSUREDDEPOSITINV.ConvertToDecimal(),
                                nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                                employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                nettradefassetpay = jo.NETTRADEFASSETPAY.ConvertToDecimal(),
                                otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                                sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                                netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                                disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                                invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                                dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                                dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                                buysellbackfassetrec = jo.BUYSELLBACKFASSETREC.ConvertToDecimal(),
                                otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                                suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                                invpay = jo.INVPAY.ConvertToDecimal(),
                                niinsuredpledgeloan = jo.NIINSUREDPLEDGELOAN.ConvertToDecimal(),
                                buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                                buysubsidiarypay = jo.BUYSUBSIDIARYPAY.ConvertToDecimal(),
                                dispsubsidiarypay = jo.DISPSUBSIDIARYPAY.ConvertToDecimal(),
                                buysellbackfassetpay = jo.BUYSELLBACKFASSETPAY.ConvertToDecimal(),
                                otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                                suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                                netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                                acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                                loanrec = jo.LOANREC.ConvertToDecimal(),
                                issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                                netsellbuybackfassetrec = jo.NETSELLBUYBACKFASSETREC.ConvertToDecimal(),
                                otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                                sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                                repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                                diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                                netsellbuybackfassetpay = jo.NETSELLBUYBACKFASSETPAY.ConvertToDecimal(),
                                otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                                sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                                netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                                effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                                nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                                cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                                cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_cashflow_insurance (securitycode,reporttype,reportdatetype,type,reportdate,currency,nideposit,premiumrec,netrirec,niinsureddepositinv,taxreturnrec,nettradefassetrec,intandcommrec,nilendfund,nddepositincbankfi,nisellbuyback,ndbuysellback,otheroperaterec,sumoperateflowin,indemnitypay,netripay,ndlendfund,nibuysellback,ndsellbuyback,niloanadvances,intandcommpay,divipay,ndinsureddepositinv,nidepositincbankfi,employeepay,taxpay,nettradefassetpay,otheroperatepay,sumoperateflowout,netoperatecashflow,disposalinvrec,invincomerec,dispfilassetrec,dispsubsidiaryrec,buysellbackfassetrec,otherinvrec,suminvflowin,invpay,niinsuredpledgeloan,buyfilassetpay,buysubsidiarypay,dispsubsidiarypay,buysellbackfassetpay,otherinvpay,suminvflowout,netinvcashflow,acceptinvrec,loanrec,issuebondrec,netsellbuybackfassetrec,otherfinarec,sumfinaflowin,repaydebtpay,diviprofitorintpay,netsellbuybackfassetpay,otherfinapay,sumfinaflowout,netfinacashflow,effectexchangerate,nicashequi,cashequibeginning,cashequiending) values (@securitycode,@reporttype,@reportdatetype,@type,@reportdate,@currency,@nideposit,@premiumrec,@netrirec,@niinsureddepositinv,@taxreturnrec,@nettradefassetrec,@intandcommrec,@nilendfund,@nddepositincbankfi,@nisellbuyback,@ndbuysellback,@otheroperaterec,@sumoperateflowin,@indemnitypay,@netripay,@ndlendfund,@nibuysellback,@ndsellbuyback,@niloanadvances,@intandcommpay,@divipay,@ndinsureddepositinv,@nidepositincbankfi,@employeepay,@taxpay,@nettradefassetpay,@otheroperatepay,@sumoperateflowout,@netoperatecashflow,@disposalinvrec,@invincomerec,@dispfilassetrec,@dispsubsidiaryrec,@buysellbackfassetrec,@otherinvrec,@suminvflowin,@invpay,@niinsuredpledgeloan,@buyfilassetpay,@buysubsidiarypay,@dispsubsidiarypay,@buysellbackfassetpay,@otherinvpay,@suminvflowout,@netinvcashflow,@acceptinvrec,@loanrec,@issuebondrec,@netsellbuybackfassetrec,@otherfinarec,@sumfinaflowin,@repaydebtpay,@diviprofitorintpay,@netsellbuybackfassetpay,@otherfinapay,@sumfinaflowout,@netfinacashflow,@effectexchangerate,@nicashequi,@cashequibeginning,@cashequiending)";
                    em_cashflow_insurance entity = new em_cashflow_insurance()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        reportdatetype = jo.REPORTDATETYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        currency = jo.CURRENCY,
                        nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                        premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                        netrirec = jo.NETRIREC.ConvertToDecimal(),
                        niinsureddepositinv = jo.NIINSUREDDEPOSITINV.ConvertToDecimal(),
                        taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                        nettradefassetrec = jo.NETTRADEFASSETREC.ConvertToDecimal(),
                        intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                        nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                        nddepositincbankfi = jo.NDDEPOSITINCBANKFI.ConvertToDecimal(),
                        nisellbuyback = jo.NISELLBUYBACK.ConvertToDecimal(),
                        ndbuysellback = jo.NDBUYSELLBACK.ConvertToDecimal(),
                        otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                        sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                        indemnitypay = jo.INDEMNITYPAY.ConvertToDecimal(),
                        netripay = jo.NETRIPAY.ConvertToDecimal(),
                        ndlendfund = jo.NDLENDFUND.ConvertToDecimal(),
                        nibuysellback = jo.NIBUYSELLBACK.ConvertToDecimal(),
                        ndsellbuyback = jo.NDSELLBUYBACK.ConvertToDecimal(),
                        niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                        intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                        divipay = jo.DIVIPAY.ConvertToDecimal(),
                        ndinsureddepositinv = jo.NDINSUREDDEPOSITINV.ConvertToDecimal(),
                        nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                        employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        nettradefassetpay = jo.NETTRADEFASSETPAY.ConvertToDecimal(),
                        otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                        sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                        netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                        disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                        invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                        dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                        dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                        buysellbackfassetrec = jo.BUYSELLBACKFASSETREC.ConvertToDecimal(),
                        otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                        suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                        invpay = jo.INVPAY.ConvertToDecimal(),
                        niinsuredpledgeloan = jo.NIINSUREDPLEDGELOAN.ConvertToDecimal(),
                        buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                        buysubsidiarypay = jo.BUYSUBSIDIARYPAY.ConvertToDecimal(),
                        dispsubsidiarypay = jo.DISPSUBSIDIARYPAY.ConvertToDecimal(),
                        buysellbackfassetpay = jo.BUYSELLBACKFASSETPAY.ConvertToDecimal(),
                        otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                        suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                        netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                        acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                        loanrec = jo.LOANREC.ConvertToDecimal(),
                        issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                        netsellbuybackfassetrec = jo.NETSELLBUYBACKFASSETREC.ConvertToDecimal(),
                        otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                        sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                        repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                        diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                        netsellbuybackfassetpay = jo.NETSELLBUYBACKFASSETPAY.ConvertToDecimal(),
                        otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                        sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                        netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                        effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                        nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                        cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                        cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
            return false;
        }
        #endregion
        #region	em_cashflow_bank
        public static em_cashflow_bank get_em_cashflow_bank_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_cashflow_bank where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_cashflow_bank>(sql).FirstOrDefault();
            }
        }

        public static bool oper_em_cashflow_bank_data(em_cashflow_bank_jo jo, bool includeUpdate)
        {
            try
            {
                em_cashflow_bank edit_entity = get_em_cashflow_bank_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    if (includeUpdate)
                    {
                        Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                        string sql = "update em_cashflow_bank set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,nideposit=@nideposit,niclientdeposit=@niclientdeposit,nifideposit=@nifideposit,niborrowfromcbank=@niborrowfromcbank,nddepositincbankfi=@nddepositincbankfi,nddepositincbank=@nddepositincbank,nddepositinfi=@nddepositinfi,niborrowsellbuyback=@niborrowsellbuyback,niborrowfund=@niborrowfund,nisellbuybackfasset=@nisellbuybackfasset,ndlendbuysellback=@ndlendbuysellback,ndlendfund=@ndlendfund,ndbuysellbackfasset=@ndbuysellbackfasset,netcd=@netcd,nitradefliab=@nitradefliab,ndtradefasset=@ndtradefasset,intandcommrec=@intandcommrec,intrec=@intrec,commrec=@commrec,dispmassetrec=@dispmassetrec,cancelloanrec=@cancelloanrec,otheroperaterec=@otheroperaterec,sumoperateflowin=@sumoperateflowin,niloanadvances=@niloanadvances,ndborrowfromcbank=@ndborrowfromcbank,nidepositincbankfi=@nidepositincbankfi,nidepositincbank=@nidepositincbank,nidepositinfi=@nidepositinfi,ndfideposit=@ndfideposit,ndissuecd=@ndissuecd,nilendsellbuyback=@nilendsellbuyback,nilendfund=@nilendfund,nibuysellbackfasset=@nibuysellbackfasset,ndborrowsellbuyback=@ndborrowsellbuyback,ndborrowfund=@ndborrowfund,ndsellbuybackfasset=@ndsellbuybackfasset,nitradefasset=@nitradefasset,ndtradefliab=@ndtradefliab,intandcommpay=@intandcommpay,intpay=@intpay,commpay=@commpay,employeepay=@employeepay,taxpay=@taxpay,buyfinaleaseassetpay=@buyfinaleaseassetpay,niaccountrec=@niaccountrec,otheroperatepay=@otheroperatepay,sumoperateflowout=@sumoperateflowout,netoperatecashflow=@netoperatecashflow,disposalinvrec=@disposalinvrec,invincomerec=@invincomerec,diviorprofitrec=@diviorprofitrec,dispfilassetrec=@dispfilassetrec,dispsubsidiaryrec=@dispsubsidiaryrec,otherinvrec=@otherinvrec,suminvflowin=@suminvflowin,invpay=@invpay,buyfilassetpay=@buyfilassetpay,getsubsidiarypay=@getsubsidiarypay,otherinvpay=@otherinvpay,suminvflowout=@suminvflowout,netinvcashflow=@netinvcashflow,issuebondrec=@issuebondrec,issuejuniorbondrec=@issuejuniorbondrec,issueotherbondrec=@issueotherbondrec,otherfinarec=@otherfinarec,acceptinvrec=@acceptinvrec,subsidiaryaccept=@subsidiaryaccept,issuecd=@issuecd,addsharecapitalrec=@addsharecapitalrec,sumfinaflowin=@sumfinaflowin,repaydebtpay=@repaydebtpay,bondintpay=@bondintpay,issuesharerec=@issuesharerec,otherfinapay=@otherfinapay,diviprofitorintpay=@diviprofitorintpay,sumfinaflowout=@sumfinaflowout,netfinacashflow=@netfinacashflow,effectexchangerate=@effectexchangerate,nicashequi=@nicashequi,cashequibeginning=@cashequibeginning,cashequiending=@cashequiending where id=@id";
                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                securitycode = jo.SECURITYCODE,
                                reporttype = jo.REPORTTYPE,
                                type = jo.TYPE,
                                reportdate = jo.REPORTDATE.ConvertToDate(),
                                nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                                niclientdeposit = jo.NICLIENTDEPOSIT.ConvertToDecimal(),
                                nifideposit = jo.NIFIDEPOSIT.ConvertToDecimal(),
                                niborrowfromcbank = jo.NIBORROWFROMCBANK.ConvertToDecimal(),
                                nddepositincbankfi = jo.NDDEPOSITINCBANKFI.ConvertToDecimal(),
                                nddepositincbank = jo.NDDEPOSITINCBANK.ConvertToDecimal(),
                                nddepositinfi = jo.NDDEPOSITINFI.ConvertToDecimal(),
                                niborrowsellbuyback = jo.NIBORROWSELLBUYBACK.ConvertToDecimal(),
                                niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                                nisellbuybackfasset = jo.NISELLBUYBACKFASSET.ConvertToDecimal(),
                                ndlendbuysellback = jo.NDLENDBUYSELLBACK.ConvertToDecimal(),
                                ndlendfund = jo.NDLENDFUND.ConvertToDecimal(),
                                ndbuysellbackfasset = jo.NDBUYSELLBACKFASSET.ConvertToDecimal(),
                                netcd = jo.NETCD.ConvertToDecimal(),
                                nitradefliab = jo.NITRADEFLIAB.ConvertToDecimal(),
                                ndtradefasset = jo.NDTRADEFASSET.ConvertToDecimal(),
                                intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                                intrec = jo.INTREC.ConvertToDecimal(),
                                commrec = jo.COMMREC.ConvertToDecimal(),
                                dispmassetrec = jo.DISPMASSETREC.ConvertToDecimal(),
                                cancelloanrec = jo.CANCELLOANREC.ConvertToDecimal(),
                                otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                                sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                                niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                                ndborrowfromcbank = jo.NDBORROWFROMCBANK.ConvertToDecimal(),
                                nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                                nidepositincbank = jo.NIDEPOSITINCBANK.ConvertToDecimal(),
                                nidepositinfi = jo.NIDEPOSITINFI.ConvertToDecimal(),
                                ndfideposit = jo.NDFIDEPOSIT.ConvertToDecimal(),
                                ndissuecd = jo.NDISSUECD.ConvertToDecimal(),
                                nilendsellbuyback = jo.NILENDSELLBUYBACK.ConvertToDecimal(),
                                nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                                nibuysellbackfasset = jo.NIBUYSELLBACKFASSET.ConvertToDecimal(),
                                ndborrowsellbuyback = jo.NDBORROWSELLBUYBACK.ConvertToDecimal(),
                                ndborrowfund = jo.NDBORROWFUND.ConvertToDecimal(),
                                ndsellbuybackfasset = jo.NDSELLBUYBACKFASSET.ConvertToDecimal(),
                                nitradefasset = jo.NITRADEFASSET.ConvertToDecimal(),
                                ndtradefliab = jo.NDTRADEFLIAB.ConvertToDecimal(),
                                intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                                intpay = jo.INTPAY.ConvertToDecimal(),
                                commpay = jo.COMMPAY.ConvertToDecimal(),
                                employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                                taxpay = jo.TAXPAY.ConvertToDecimal(),
                                buyfinaleaseassetpay = jo.BUYFINALEASEASSETPAY.ConvertToDecimal(),
                                niaccountrec = jo.NIACCOUNTREC.ConvertToDecimal(),
                                otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                                sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                                netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                                disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                                invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                                diviorprofitrec = jo.DIVIORPROFITREC.ConvertToDecimal(),
                                dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                                dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                                otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                                suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                                invpay = jo.INVPAY.ConvertToDecimal(),
                                buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                                getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                                otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                                suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                                netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                                issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                                issuejuniorbondrec = jo.ISSUEJUNIORBONDREC.ConvertToDecimal(),
                                issueotherbondrec = jo.ISSUEOTHERBONDREC.ConvertToDecimal(),
                                otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                                acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                                subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                                issuecd = jo.ISSUECD.ConvertToDecimal(),
                                addsharecapitalrec = jo.ADDSHARECAPITALREC.ConvertToDecimal(),
                                sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                                repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                                bondintpay = jo.BONDINTPAY.ConvertToDecimal(),
                                issuesharerec = jo.ISSUESHAREREC.ConvertToDecimal(),
                                otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                                diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                                sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                                netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                                effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                                nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                                cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                                cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                                id = edit_entity.id
                            });
                        }
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_cashflow_bank (securitycode,reporttype,type,reportdate,nideposit,niclientdeposit,nifideposit,niborrowfromcbank,nddepositincbankfi,nddepositincbank,nddepositinfi,niborrowsellbuyback,niborrowfund,nisellbuybackfasset,ndlendbuysellback,ndlendfund,ndbuysellbackfasset,netcd,nitradefliab,ndtradefasset,intandcommrec,intrec,commrec,dispmassetrec,cancelloanrec,otheroperaterec,sumoperateflowin,niloanadvances,ndborrowfromcbank,nidepositincbankfi,nidepositincbank,nidepositinfi,ndfideposit,ndissuecd,nilendsellbuyback,nilendfund,nibuysellbackfasset,ndborrowsellbuyback,ndborrowfund,ndsellbuybackfasset,nitradefasset,ndtradefliab,intandcommpay,intpay,commpay,employeepay,taxpay,buyfinaleaseassetpay,niaccountrec,otheroperatepay,sumoperateflowout,netoperatecashflow,disposalinvrec,invincomerec,diviorprofitrec,dispfilassetrec,dispsubsidiaryrec,otherinvrec,suminvflowin,invpay,buyfilassetpay,getsubsidiarypay,otherinvpay,suminvflowout,netinvcashflow,issuebondrec,issuejuniorbondrec,issueotherbondrec,otherfinarec,acceptinvrec,subsidiaryaccept,issuecd,addsharecapitalrec,sumfinaflowin,repaydebtpay,bondintpay,issuesharerec,otherfinapay,diviprofitorintpay,sumfinaflowout,netfinacashflow,effectexchangerate,nicashequi,cashequibeginning,cashequiending) values (@securitycode,@reporttype,@type,@reportdate,@nideposit,@niclientdeposit,@nifideposit,@niborrowfromcbank,@nddepositincbankfi,@nddepositincbank,@nddepositinfi,@niborrowsellbuyback,@niborrowfund,@nisellbuybackfasset,@ndlendbuysellback,@ndlendfund,@ndbuysellbackfasset,@netcd,@nitradefliab,@ndtradefasset,@intandcommrec,@intrec,@commrec,@dispmassetrec,@cancelloanrec,@otheroperaterec,@sumoperateflowin,@niloanadvances,@ndborrowfromcbank,@nidepositincbankfi,@nidepositincbank,@nidepositinfi,@ndfideposit,@ndissuecd,@nilendsellbuyback,@nilendfund,@nibuysellbackfasset,@ndborrowsellbuyback,@ndborrowfund,@ndsellbuybackfasset,@nitradefasset,@ndtradefliab,@intandcommpay,@intpay,@commpay,@employeepay,@taxpay,@buyfinaleaseassetpay,@niaccountrec,@otheroperatepay,@sumoperateflowout,@netoperatecashflow,@disposalinvrec,@invincomerec,@diviorprofitrec,@dispfilassetrec,@dispsubsidiaryrec,@otherinvrec,@suminvflowin,@invpay,@buyfilassetpay,@getsubsidiarypay,@otherinvpay,@suminvflowout,@netinvcashflow,@issuebondrec,@issuejuniorbondrec,@issueotherbondrec,@otherfinarec,@acceptinvrec,@subsidiaryaccept,@issuecd,@addsharecapitalrec,@sumfinaflowin,@repaydebtpay,@bondintpay,@issuesharerec,@otherfinapay,@diviprofitorintpay,@sumfinaflowout,@netfinacashflow,@effectexchangerate,@nicashequi,@cashequibeginning,@cashequiending)";
                    em_cashflow_bank entity = new em_cashflow_bank()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                        niclientdeposit = jo.NICLIENTDEPOSIT.ConvertToDecimal(),
                        nifideposit = jo.NIFIDEPOSIT.ConvertToDecimal(),
                        niborrowfromcbank = jo.NIBORROWFROMCBANK.ConvertToDecimal(),
                        nddepositincbankfi = jo.NDDEPOSITINCBANKFI.ConvertToDecimal(),
                        nddepositincbank = jo.NDDEPOSITINCBANK.ConvertToDecimal(),
                        nddepositinfi = jo.NDDEPOSITINFI.ConvertToDecimal(),
                        niborrowsellbuyback = jo.NIBORROWSELLBUYBACK.ConvertToDecimal(),
                        niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                        nisellbuybackfasset = jo.NISELLBUYBACKFASSET.ConvertToDecimal(),
                        ndlendbuysellback = jo.NDLENDBUYSELLBACK.ConvertToDecimal(),
                        ndlendfund = jo.NDLENDFUND.ConvertToDecimal(),
                        ndbuysellbackfasset = jo.NDBUYSELLBACKFASSET.ConvertToDecimal(),
                        netcd = jo.NETCD.ConvertToDecimal(),
                        nitradefliab = jo.NITRADEFLIAB.ConvertToDecimal(),
                        ndtradefasset = jo.NDTRADEFASSET.ConvertToDecimal(),
                        intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                        intrec = jo.INTREC.ConvertToDecimal(),
                        commrec = jo.COMMREC.ConvertToDecimal(),
                        dispmassetrec = jo.DISPMASSETREC.ConvertToDecimal(),
                        cancelloanrec = jo.CANCELLOANREC.ConvertToDecimal(),
                        otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                        sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                        niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                        ndborrowfromcbank = jo.NDBORROWFROMCBANK.ConvertToDecimal(),
                        nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                        nidepositincbank = jo.NIDEPOSITINCBANK.ConvertToDecimal(),
                        nidepositinfi = jo.NIDEPOSITINFI.ConvertToDecimal(),
                        ndfideposit = jo.NDFIDEPOSIT.ConvertToDecimal(),
                        ndissuecd = jo.NDISSUECD.ConvertToDecimal(),
                        nilendsellbuyback = jo.NILENDSELLBUYBACK.ConvertToDecimal(),
                        nilendfund = jo.NILENDFUND.ConvertToDecimal(),
                        nibuysellbackfasset = jo.NIBUYSELLBACKFASSET.ConvertToDecimal(),
                        ndborrowsellbuyback = jo.NDBORROWSELLBUYBACK.ConvertToDecimal(),
                        ndborrowfund = jo.NDBORROWFUND.ConvertToDecimal(),
                        ndsellbuybackfasset = jo.NDSELLBUYBACKFASSET.ConvertToDecimal(),
                        nitradefasset = jo.NITRADEFASSET.ConvertToDecimal(),
                        ndtradefliab = jo.NDTRADEFLIAB.ConvertToDecimal(),
                        intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                        intpay = jo.INTPAY.ConvertToDecimal(),
                        commpay = jo.COMMPAY.ConvertToDecimal(),
                        employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        buyfinaleaseassetpay = jo.BUYFINALEASEASSETPAY.ConvertToDecimal(),
                        niaccountrec = jo.NIACCOUNTREC.ConvertToDecimal(),
                        otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                        sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                        netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                        disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                        invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                        diviorprofitrec = jo.DIVIORPROFITREC.ConvertToDecimal(),
                        dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                        dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                        otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                        suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                        invpay = jo.INVPAY.ConvertToDecimal(),
                        buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                        getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                        otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                        suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                        netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                        issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                        issuejuniorbondrec = jo.ISSUEJUNIORBONDREC.ConvertToDecimal(),
                        issueotherbondrec = jo.ISSUEOTHERBONDREC.ConvertToDecimal(),
                        otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                        acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                        subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                        issuecd = jo.ISSUECD.ConvertToDecimal(),
                        addsharecapitalrec = jo.ADDSHARECAPITALREC.ConvertToDecimal(),
                        sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                        repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                        bondintpay = jo.BONDINTPAY.ConvertToDecimal(),
                        issuesharerec = jo.ISSUESHAREREC.ConvertToDecimal(),
                        otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                        diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                        sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                        netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                        effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                        nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                        cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                        cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
                return edit_entity != null;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
            return false;
        }
        #endregion
        #region	em_cashflow_common
        public static em_cashflow_common get_em_cashflow_common_data(string ts_code, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_cashflow_common where securitycode='" + ts_code + "' and reportdate='" + date + "'";
                return conn.Query<em_cashflow_common>(sql).FirstOrDefault();
            }
        }

        public static void oper_em_cashflow_common_data(em_cashflow_common_jo jo)
        {
            try
            {
                em_cashflow_common edit_entity = get_em_cashflow_common_data(jo.SECURITYCODE, jo.REPORTDATE);
                if (edit_entity != null)
                {
                    Logger.Info(string.Format("update data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "update em_cashflow_common set  securitycode=@securitycode,reporttype=@reporttype,type=@type,reportdate=@reportdate,salegoodsservicerec=@salegoodsservicerec,nideposit=@nideposit,niborrowfromcbank=@niborrowfromcbank,niborrowfromfi=@niborrowfromfi,premiumrec=@premiumrec,netrirec=@netrirec,niinsureddepositinv=@niinsureddepositinv,nidisptradefasset=@nidisptradefasset,intandcommrec=@intandcommrec,niborrowfund=@niborrowfund,ndloanadvances=@ndloanadvances,nibuybackfund=@nibuybackfund,taxreturnrec=@taxreturnrec,otheroperaterec=@otheroperaterec,sumoperateflowin=@sumoperateflowin,buygoodsservicepay=@buygoodsservicepay,niloanadvances=@niloanadvances,nidepositincbankfi=@nidepositincbankfi,indemnitypay=@indemnitypay,intandcommpay=@intandcommpay,divipay=@divipay,employeepay=@employeepay,taxpay=@taxpay,otheroperatepay=@otheroperatepay,sumoperateflowout=@sumoperateflowout,netoperatecashflow=@netoperatecashflow,disposalinvrec=@disposalinvrec,invincomerec=@invincomerec,dispfilassetrec=@dispfilassetrec,dispsubsidiaryrec=@dispsubsidiaryrec,reducepledgetdeposit=@reducepledgetdeposit,otherinvrec=@otherinvrec,suminvflowin=@suminvflowin,buyfilassetpay=@buyfilassetpay,invpay=@invpay,nipledgeloan=@nipledgeloan,getsubsidiarypay=@getsubsidiarypay,addpledgetdeposit=@addpledgetdeposit,otherinvpay=@otherinvpay,suminvflowout=@suminvflowout,netinvcashflow=@netinvcashflow,acceptinvrec=@acceptinvrec,subsidiaryaccept=@subsidiaryaccept,loanrec=@loanrec,issuebondrec=@issuebondrec,otherfinarec=@otherfinarec,sumfinaflowin=@sumfinaflowin,repaydebtpay=@repaydebtpay,diviprofitorintpay=@diviprofitorintpay,subsidiarypay=@subsidiarypay,buysubsidiarypay=@buysubsidiarypay,otherfinapay=@otherfinapay,subsidiaryreductcapital=@subsidiaryreductcapital,sumfinaflowout=@sumfinaflowout,netfinacashflow=@netfinacashflow,effectexchangerate=@effectexchangerate,nicashequi=@nicashequi,cashequibeginning=@cashequibeginning,cashequiending=@cashequiending where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            securitycode = jo.SECURITYCODE,
                            reporttype = jo.REPORTTYPE,
                            type = jo.TYPE,
                            reportdate = jo.REPORTDATE.ConvertToDate(),
                            salegoodsservicerec = jo.SALEGOODSSERVICEREC.ConvertToDecimal(),
                            nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                            niborrowfromcbank = jo.NIBORROWFROMCBANK.ConvertToDecimal(),
                            niborrowfromfi = jo.NIBORROWFROMFI.ConvertToDecimal(),
                            premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                            netrirec = jo.NETRIREC.ConvertToDecimal(),
                            niinsureddepositinv = jo.NIINSUREDDEPOSITINV.ConvertToDecimal(),
                            nidisptradefasset = jo.NIDISPTRADEFASSET.ConvertToDecimal(),
                            intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                            niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                            ndloanadvances = jo.NDLOANADVANCES.ConvertToDecimal(),
                            nibuybackfund = jo.NIBUYBACKFUND.ConvertToDecimal(),
                            taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                            otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                            sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                            buygoodsservicepay = jo.BUYGOODSSERVICEPAY.ConvertToDecimal(),
                            niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                            nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                            indemnitypay = jo.INDEMNITYPAY.ConvertToDecimal(),
                            intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                            divipay = jo.DIVIPAY.ConvertToDecimal(),
                            employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                            taxpay = jo.TAXPAY.ConvertToDecimal(),
                            otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                            sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                            netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                            disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                            invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                            dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                            dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                            reducepledgetdeposit = jo.REDUCEPLEDGETDEPOSIT.ConvertToDecimal(),
                            otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                            suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                            buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                            invpay = jo.INVPAY.ConvertToDecimal(),
                            nipledgeloan = jo.NIPLEDGELOAN.ConvertToDecimal(),
                            getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                            addpledgetdeposit = jo.ADDPLEDGETDEPOSIT.ConvertToDecimal(),
                            otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                            suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                            netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                            acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                            subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                            loanrec = jo.LOANREC.ConvertToDecimal(),
                            issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                            otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                            sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                            repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                            diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                            subsidiarypay = jo.SUBSIDIARYPAY.ConvertToDecimal(),
                            buysubsidiarypay = jo.BUYSUBSIDIARYPAY.ConvertToDecimal(),
                            otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                            subsidiaryreductcapital = jo.SUBSIDIARYREDUCTCAPITAL.ConvertToDecimal(),
                            sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                            netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                            effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                            nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                            cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                            cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                            id = edit_entity.id
                        });
                    }
                }
                else
                {
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", jo.SECURITYCODE, jo.REPORTDATE));
                    string sql = "insert into em_cashflow_common (securitycode,reporttype,type,reportdate,salegoodsservicerec,nideposit,niborrowfromcbank,niborrowfromfi,premiumrec,netrirec,niinsureddepositinv,nidisptradefasset,intandcommrec,niborrowfund,ndloanadvances,nibuybackfund,taxreturnrec,otheroperaterec,sumoperateflowin,buygoodsservicepay,niloanadvances,nidepositincbankfi,indemnitypay,intandcommpay,divipay,employeepay,taxpay,otheroperatepay,sumoperateflowout,netoperatecashflow,disposalinvrec,invincomerec,dispfilassetrec,dispsubsidiaryrec,reducepledgetdeposit,otherinvrec,suminvflowin,buyfilassetpay,invpay,nipledgeloan,getsubsidiarypay,addpledgetdeposit,otherinvpay,suminvflowout,netinvcashflow,acceptinvrec,subsidiaryaccept,loanrec,issuebondrec,otherfinarec,sumfinaflowin,repaydebtpay,diviprofitorintpay,subsidiarypay,buysubsidiarypay,otherfinapay,subsidiaryreductcapital,sumfinaflowout,netfinacashflow,effectexchangerate,nicashequi,cashequibeginning,cashequiending) values (@securitycode,@reporttype,@type,@reportdate,@salegoodsservicerec,@nideposit,@niborrowfromcbank,@niborrowfromfi,@premiumrec,@netrirec,@niinsureddepositinv,@nidisptradefasset,@intandcommrec,@niborrowfund,@ndloanadvances,@nibuybackfund,@taxreturnrec,@otheroperaterec,@sumoperateflowin,@buygoodsservicepay,@niloanadvances,@nidepositincbankfi,@indemnitypay,@intandcommpay,@divipay,@employeepay,@taxpay,@otheroperatepay,@sumoperateflowout,@netoperatecashflow,@disposalinvrec,@invincomerec,@dispfilassetrec,@dispsubsidiaryrec,@reducepledgetdeposit,@otherinvrec,@suminvflowin,@buyfilassetpay,@invpay,@nipledgeloan,@getsubsidiarypay,@addpledgetdeposit,@otherinvpay,@suminvflowout,@netinvcashflow,@acceptinvrec,@subsidiaryaccept,@loanrec,@issuebondrec,@otherfinarec,@sumfinaflowin,@repaydebtpay,@diviprofitorintpay,@subsidiarypay,@buysubsidiarypay,@otherfinapay,@subsidiaryreductcapital,@sumfinaflowout,@netfinacashflow,@effectexchangerate,@nicashequi,@cashequibeginning,@cashequiending)";
                    em_cashflow_common entity = new em_cashflow_common()
                    {
                        securitycode = jo.SECURITYCODE,
                        reporttype = jo.REPORTTYPE,
                        type = jo.TYPE,
                        reportdate = jo.REPORTDATE.ConvertToDate(),
                        salegoodsservicerec = jo.SALEGOODSSERVICEREC.ConvertToDecimal(),
                        nideposit = jo.NIDEPOSIT.ConvertToDecimal(),
                        niborrowfromcbank = jo.NIBORROWFROMCBANK.ConvertToDecimal(),
                        niborrowfromfi = jo.NIBORROWFROMFI.ConvertToDecimal(),
                        premiumrec = jo.PREMIUMREC.ConvertToDecimal(),
                        netrirec = jo.NETRIREC.ConvertToDecimal(),
                        niinsureddepositinv = jo.NIINSUREDDEPOSITINV.ConvertToDecimal(),
                        nidisptradefasset = jo.NIDISPTRADEFASSET.ConvertToDecimal(),
                        intandcommrec = jo.INTANDCOMMREC.ConvertToDecimal(),
                        niborrowfund = jo.NIBORROWFUND.ConvertToDecimal(),
                        ndloanadvances = jo.NDLOANADVANCES.ConvertToDecimal(),
                        nibuybackfund = jo.NIBUYBACKFUND.ConvertToDecimal(),
                        taxreturnrec = jo.TAXRETURNREC.ConvertToDecimal(),
                        otheroperaterec = jo.OTHEROPERATEREC.ConvertToDecimal(),
                        sumoperateflowin = jo.SUMOPERATEFLOWIN.ConvertToDecimal(),
                        buygoodsservicepay = jo.BUYGOODSSERVICEPAY.ConvertToDecimal(),
                        niloanadvances = jo.NILOANADVANCES.ConvertToDecimal(),
                        nidepositincbankfi = jo.NIDEPOSITINCBANKFI.ConvertToDecimal(),
                        indemnitypay = jo.INDEMNITYPAY.ConvertToDecimal(),
                        intandcommpay = jo.INTANDCOMMPAY.ConvertToDecimal(),
                        divipay = jo.DIVIPAY.ConvertToDecimal(),
                        employeepay = jo.EMPLOYEEPAY.ConvertToDecimal(),
                        taxpay = jo.TAXPAY.ConvertToDecimal(),
                        otheroperatepay = jo.OTHEROPERATEPAY.ConvertToDecimal(),
                        sumoperateflowout = jo.SUMOPERATEFLOWOUT.ConvertToDecimal(),
                        netoperatecashflow = jo.NETOPERATECASHFLOW.ConvertToDecimal(),
                        disposalinvrec = jo.DISPOSALINVREC.ConvertToDecimal(),
                        invincomerec = jo.INVINCOMEREC.ConvertToDecimal(),
                        dispfilassetrec = jo.DISPFILASSETREC.ConvertToDecimal(),
                        dispsubsidiaryrec = jo.DISPSUBSIDIARYREC.ConvertToDecimal(),
                        reducepledgetdeposit = jo.REDUCEPLEDGETDEPOSIT.ConvertToDecimal(),
                        otherinvrec = jo.OTHERINVREC.ConvertToDecimal(),
                        suminvflowin = jo.SUMINVFLOWIN.ConvertToDecimal(),
                        buyfilassetpay = jo.BUYFILASSETPAY.ConvertToDecimal(),
                        invpay = jo.INVPAY.ConvertToDecimal(),
                        nipledgeloan = jo.NIPLEDGELOAN.ConvertToDecimal(),
                        getsubsidiarypay = jo.GETSUBSIDIARYPAY.ConvertToDecimal(),
                        addpledgetdeposit = jo.ADDPLEDGETDEPOSIT.ConvertToDecimal(),
                        otherinvpay = jo.OTHERINVPAY.ConvertToDecimal(),
                        suminvflowout = jo.SUMINVFLOWOUT.ConvertToDecimal(),
                        netinvcashflow = jo.NETINVCASHFLOW.ConvertToDecimal(),
                        acceptinvrec = jo.ACCEPTINVREC.ConvertToDecimal(),
                        subsidiaryaccept = jo.SUBSIDIARYACCEPT.ConvertToDecimal(),
                        loanrec = jo.LOANREC.ConvertToDecimal(),
                        issuebondrec = jo.ISSUEBONDREC.ConvertToDecimal(),
                        otherfinarec = jo.OTHERFINAREC.ConvertToDecimal(),
                        sumfinaflowin = jo.SUMFINAFLOWIN.ConvertToDecimal(),
                        repaydebtpay = jo.REPAYDEBTPAY.ConvertToDecimal(),
                        diviprofitorintpay = jo.DIVIPROFITORINTPAY.ConvertToDecimal(),
                        subsidiarypay = jo.SUBSIDIARYPAY.ConvertToDecimal(),
                        buysubsidiarypay = jo.BUYSUBSIDIARYPAY.ConvertToDecimal(),
                        otherfinapay = jo.OTHERFINAPAY.ConvertToDecimal(),
                        subsidiaryreductcapital = jo.SUBSIDIARYREDUCTCAPITAL.ConvertToDecimal(),
                        sumfinaflowout = jo.SUMFINAFLOWOUT.ConvertToDecimal(),
                        netfinacashflow = jo.NETFINACASHFLOW.ConvertToDecimal(),
                        effectexchangerate = jo.EFFECTEXCHANGERATE.ConvertToDecimal(),
                        nicashequi = jo.NICASHEQUI.ConvertToDecimal(),
                        cashequibeginning = jo.CASHEQUIBEGINNING.ConvertToDecimal(),
                        cashequiending = jo.CASHEQUIENDING.ConvertToDecimal(),

                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync em report data occurs error: tscode={0};date={1},details:{2}", jo.SECURITYCODE, jo.REPORTDATE, ex));
            }
        }
        #endregion

        #endregion

    }
}
