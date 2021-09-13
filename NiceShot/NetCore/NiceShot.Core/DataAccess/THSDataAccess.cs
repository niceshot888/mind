using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NiceShot.Core.DataAccess
{
    public class THSDataAccess
    {

        public static ths_hd GetYftrInfo(string ts_code, string reportdate)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from ths_hd where ts_code='" + ts_code + "'  and reportdate='" + reportdate + "'";
                return conn.Query<ths_hd>(sql).FirstOrDefault();
            }
        }

        public static void SyncBeforeYearsPB(int years)
        {
            var dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "涨跌幅.xlsx");
            using (var package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                var worksheet = package.Workbook.Worksheets["pb" + years + "yearsago"];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    var ts_code = worksheet.Cells[row, 1].Value.ToEmptyString();
                    var pb = worksheet.Cells[row, 2].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();

                    try
                    {
                        var sql = string.Empty;
                        if (years == 1)
                            sql = "update xq_stock set pb1yearsago=@pb where ts_code=@ts_code";
                        else if (years == 4)
                            sql = "update xq_stock set pb4yearsago=@pb where ts_code=@ts_code";

                        using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                ts_code = ts_code,
                                pb = pb
                            });

                            Logger.Info("update data:ts_code=" + ts_code + ",pb=" + pb);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(string.Format("sync data error:tscode={0};详细：{1}", ts_code, ex));
                    }
                }
            }
        }

        public static void SyncStockPriceChangePercent(int days)
        {
            var dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "涨跌幅.xlsx");
            using (var package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                var worksheet = package.Workbook.Worksheets["chg" + days];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    var ts_code = worksheet.Cells[row, 1].Value.ToEmptyString();
                    var chg = worksheet.Cells[row, 2].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();

                    try
                    {
                        var sql = string.Empty;
                        if (days == 720)
                            sql = "update xq_stock set chg720=@chg where ts_code=@ts_code";
                        else if (days == 360)
                            sql = "update xq_stock set chg360=@chg where ts_code=@ts_code";

                        using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                ts_code = ts_code,
                                chg = chg
                            });

                            Logger.Info("update data:ts_code=" + ts_code + ",chg=" + chg);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(string.Format("sync data error:tscode={0};详细：{1}", ts_code, ex));
                    }
                }
            }
        }

        /// <summary>
        /// 同步研发投入
        /// </summary>
        public static void SyncYftr()
        {
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "研发投入.xlsx");
            string reportdate = "2020-12-31";
            int year = 2020;
            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["yftr" + year];

                //获取表格的列数和行数
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    var ts_code = worksheet.Cells[row, 1].Value.ToEmptyString();
                    var rdspendsum = worksheet.Cells[row, 2].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();

                    /*var yysr = worksheet.Cells[row, 6].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();
                    decimal? rdspendsum_ratio = null;
                    if (rdspendsum.HasValue && yysr.HasValue)
                        rdspendsum_ratio = Math.Round(rdspendsum.Value / yysr.Value * 100, 2);*/

                    try
                    {
                        ths_hd hd = GetYftrInfo(ts_code, reportdate);
                        if (hd != null)
                        {
                            //修改数据 ,rdspendsum_ratio=@rdspendsum_ratio
                            string sql = "update ths_hd set rdspendsum=@rdspendsum where id=@id";

                            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                            {
                                conn.Execute(sql, new
                                {
                                    id = hd.id,
                                    rdspendsum = rdspendsum,
                                    //rdspendsum_ratio = rdspendsum_ratio,
                                });
                            }

                            Logger.Info("update ths hd data:ts_code=" + ts_code + ",rd=" + rdspendsum);
                        }
                        else
                        {
                            //添加数据,rdspendsum_ratio,@rdspendsum_ratio
                            string sql = "insert into ths_hd (ts_code,reportdate,rdspendsum) values (@ts_code,@reportdate,@rdspendsum)";

                            ths_hd entity = new ths_hd()
                            {
                                ts_code = ts_code,
                                reportdate = DateTime.Parse(reportdate),
                                rdspendsum = rdspendsum,
                                //rdspendsum_ratio = rdspendsum_ratio,
                            };
                            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                            {
                                conn.Execute(sql, entity);
                                Logger.Info("insert ths hd data:ts_code=" + ts_code + ",rd=" + rdspendsum);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(string.Format("sync ths hd error: tscode={0};详细：{1}", ts_code, ex));
                    }
                }
            }
        }

        public static void Sync10YearsBonus()
        {
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "bonus.xlsx");

            using (ExcelPackage package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                for (int year = 2020; year <= 2020; year++)
                {
                    var reportdate = string.Format("{0}-6-30", year);

                    ExcelWorksheet worksheet = package.Workbook.Worksheets[year.ToString()];

                    //获取表格的列数和行数
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var ts_code = worksheet.Cells[row, 1].Value.ToEmptyString();
                        var bonus = worksheet.Cells[row, 5].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();

                        try
                        {
                            //修改数据
                            string sql = "update em_a_mainindex set bonus=@bonus where ts_code=@ts_code and date=@date";

                            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                            {
                                conn.Execute(sql, new
                                {
                                    bonus = bonus,
                                    ts_code = ts_code,
                                    date = reportdate
                                });
                            }

                            Logger.Info("update ths bonus data:date=" + reportdate + ";ts_code=" + ts_code + ",bonus=" + bonus);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(string.Format("sync ths bonus error: tscode={0};详细：{1}", ts_code, ex));
                        }
                    }
                }
            }
        }


        public static void SyncBonusByYear(int year)
        {
            string dataStructsFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "docs", "bonus" + year + ".xlsx");

            using (var package = new ExcelPackage(new FileInfo(dataStructsFileName)))
            {
                var reportdate = string.Format("{0}-12-31", year);

                var worksheet = package.Workbook.Worksheets["bonus"];

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    var ts_code = worksheet.Cells[row, 1].Value.ToEmptyString();
                    var bonus = worksheet.Cells[row, 2].Value.ToEmptyString().Replace("--", "").ConvertToDecimal();

                    try
                    {
                        //修改数据
                        string sql = "update em_a_mainindex set bonus=@bonus where ts_code=@ts_code and date=@date";

                        using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                bonus = bonus,
                                ts_code = ts_code,
                                date = reportdate
                            });
                        }

                        Logger.Info("update ths bonus data:date=" + reportdate + ";ts_code=" + ts_code + ",bonus=" + bonus);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(string.Format("sync ths bonus error: tscode={0};详细：{1}", ts_code, ex));
                    }
                }
            }
        }

        public static void OperateThsCashflowAdditionalData(string updateSql)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    Logger.Info(updateSql);

                    conn.Execute(updateSql);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync ths cashflow addtional data error:{0},详细：{1}", updateSql, ex));
            }
        }
    }
}
