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
        public static List<xq_stock> GetXQStockList(MarketType marketType)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where markettype=" + (int)marketType;

                if (marketType == MarketType.A)
                    sql += " and type in (11,82) and name not like '%b' and name not like '%b股'  and name not like '%退' and name not like '%退市%' ";

                //sql += " and fore_roe_1yearlater is null";

                sql += " order by market_capital desc";

                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

        /// <summary>
        /// 获得雪球普通股票列表
        /// </summary>
        public static List<xq_stock> GetXQNormalStockList(MarketType marketType)
        {
            using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from xq_stock where markettype=" + (int)marketType;
                if (marketType == MarketType.A)
                    sql += " and type in (11,82) and name not like '%b' and name not like '%b股'  and name not like '%退' and name not like '%退市%' and sshy not in ('证券','保险','银行')";
                sql += " order by market_capital desc";
                var stockList = conn.Query<xq_stock>(sql).ToList();

                return stockList;
            }
        }

    }
}
