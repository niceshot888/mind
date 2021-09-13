using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NiceShot.Core.DataAccess
{
    public class NiceShotDataAccess
    {
        public static em_balancesheet_common GetEMBalanceSheetInfo(string ts_code, DateTime? date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from em_balancesheet_common where securitycode='" + ts_code + "' and reportdate='" + date.ToShortDateString() + "'";
                return conn.Query<em_balancesheet_common>(sql).FirstOrDefault();
            }
        }

        public static xq_stock GetStockInfo(string ts_code, MarketType marketType)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where ts_code='" + ts_code + "'  and markettype=" + (int)marketType;
                return conn.Query<xq_stock>(sql).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获得雪球全部股票列表
        /// </summary>
        /// <returns></returns>
        public static List<xq_stock> GetXQStockList(MarketType marketType)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where markettype=" + (int)marketType;

                // and sshy='房地产'
                if (marketType == MarketType.A)
                    sql += " and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%'";

                sql += " order by market_capital desc";

                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        /// <summary>
        /// 根据报表日期获得东财未抓取数据的股票列表
        /// </summary>
        /// <returns></returns>
        public static List<xq_stock> GetEMNoDataXQStockListByDate(MarketType marketType, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select s2.* from xq_stock s2 where s2.ts_code not in(select s.ts_code from xq_stock s left join em_bal_common_v1 b on s.ts_code=b.secucode where b.report_date='" + date + "' and s.markettype=" + (int)marketType + ") and s2.markettype=" + (int)marketType;

                if (marketType == MarketType.A)
                    sql += " and s2.type in (11,82) and s2.name not like '%b' and s2.name not like '%b股' and s2.name not like '%退' and s2.name not like '%退市%'";

                sql += " order by s2.market_capital desc";

                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        /// <summary>
        /// 根据报表日期获得雪球未抓取数据的股票列表
        /// </summary>
        /// <returns></returns>
        public static List<xq_stock> GetXQNoDataXQStockListByDate(MarketType marketType, string date)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select s2.* from xq_stock s2 where s2.ts_code not in(select s.ts_code from xq_stock s left join em_a_mainindex b on s.ts_code=b.ts_code where b.date='" + date + "' and s.markettype=" + (int)marketType + ") and s2.markettype=" + (int)marketType;

                if (marketType == MarketType.A)
                    sql += " and s2.type in (11,82) and s2.name not like '%b' and s2.name not like '%b股' and s2.name not like '%退' and s2.name not like '%退市%'";

                sql += " order by s2.market_capital desc";

                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        /// <summary>
        /// 获得金融行业股票列表
        /// </summary>
        /// <returns></returns>
        public static List<xq_stock> GetFinanceIndustriesStockList()
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where markettype=" + (int)MarketType.A;
                sql += " and type in (11,82) and name not like '%b' and name not like '%b股' and name not like '%退' and name not like '%退市%' and sshy in ('多元金融','券商信托','保险','银行')";
                sql += " order by market_capital desc";

                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        /// <summary>
        /// 获得雪球普通股票列表
        /// </summary>
        /// <returns></returns>
        public static List<xq_stock> GetXQANormalStockList()
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where markettype=1";
                // and sshy='房地产'
                sql += " and type in (11,82) and name not like '%b' and name not like '%b股'  and name not like '%退' and name not like '%退市%' and sshy not in ('多元金融','券商信托','保险','银行')";
                sql += " order by market_capital desc";
                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        public static List<string> GetXQANoMainIndexTsCodes()
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                //m2.date='2020-6-30'需要定期修改日期
                string sql = "SELECT m.ts_code from em_a_mainindex m LEFT JOIN em_a_mainindex m2 on m.ts_code = m2.ts_code and m2.date='2020-6-30' where m.date='2019-12-31' and m2.ts_code is null";
                var tscodes = conn.Query<string>(sql).ToList();

                return tscodes;
            }
        }


    }
}
