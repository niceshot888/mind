using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.Core.Models.JsonObjects;
using System;
using System.Data;
using System.Linq;

namespace NiceShot.Core.DataAccess
{
    public class SinaDataAccess
    {
        public static void OperateSinaBond(sina_bond_jo jo)
        {
            try
            {
                var sina_bond = GetSinaBond(jo.symbol);
                if (sina_bond != null)
                {
                    string sql = "update sina_bond set name=@name,trade=@trade,changepercent=@changepercent,settlement=@settlement where symbol=@symbol";

                    using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            symbol = jo.symbol,
                            name = jo.name,
                            trade = jo.trade,
                            changepercent = jo.changepercent,
                            settlement = jo.settlement
                        });
                    }

                    Logger.Info("update sina bond data：" + jo.name);
                }
                else
                {
                    string sql = "insert into sina_bond (symbol,name,trade,changepercent,settlement) values (@symbol,@name,@trade,@changepercent,@settlement)";

                    var entity = new sina_bond
                    {
                        symbol = jo.symbol,
                        name = jo.name,
                        trade = jo.trade,
                        changepercent = jo.changepercent,
                        settlement = jo.settlement
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);

                        Logger.Info("insert sina bond data：" + jo.name);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("同步SINA BOND数据失败，详细：{0}", ex));
            }

        }

        public static sina_bond GetSinaBond(string symbol)
        {
            using (var conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
            {
                string sql = "select * from sina_bond where symbol='" + symbol + "'";
                return conn.Query<sina_bond>(sql).FirstOrDefault();
            }
        }

    }
}
