using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace NiceShot.Core.DataAccess
{
    public class THSDataAccess
    {
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
