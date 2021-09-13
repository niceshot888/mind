using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.MultiThread.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NiceShot.MultiThread.DataAccess
{
    public class EmDataAccess
    {
        public static readonly string conn_string = "server = localhost;User Id = root;password = root;Database = niceshotdb";

        public static void get_a_stock_dic()
        {
            using (IDbConnection conn = new MySqlConnection(conn_string))
            {
                string sql = "select * from stock_basic";
                var dic = conn.Query<object>(sql).Select(x => (IDictionary<string, object>)x).ToList();

                var keys = dic[0].Keys;
                Console.WriteLine(dic);
            }
        }

        public static List<stock_basic> get_a_stock_list()
        {
            using (IDbConnection conn = new MySqlConnection(conn_string))
            {
                string sql = "select * from stock_basic";
                var stockList = conn.Query<stock_basic>(sql).ToList();
                foreach(var stock in stockList)
                {
                    Console.WriteLine(stock.name+stock.ts_code);
                }
                return stockList;
            }
        }

        public static bool IsExistsMainIndexData(string symbol)
        {
            using (IDbConnection conn = new MySqlConnection(conn_string))
            {
                string sql = "select count(1) from stock_basic where ts_code='"+symbol+"'";
                Console.WriteLine(sql);
                return conn.ExecuteScalar<int>(sql)>0;
            }
        }
    }
}
