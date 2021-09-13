using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace NiceShot.Core.DataAccess
{
    public class TDXDataAccess
    {
        public static void UpdateForeRoeAndNP(string ts_code, List<string> dataArr)
        {
            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(ts_code, MarketType.A);

                if (stockInfo == null)
                    return;

                string sql = "update xq_stock set fore_roe_1yearlater=@fore_roe_1yearlater,fore_roe_2yearlater=@fore_roe_2yearlater,fore_roe_3yearlater=@fore_roe_3yearlater,fore_np_1yearlater=@fore_np_1yearlater,fore_np_2yearlater=@fore_np_2yearlater,fore_np_3yearlater=@fore_np_3yearlater where id=@id";

                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    conn.Execute(sql, new
                    {
                        id = stockInfo.id,
                        fore_roe_1yearlater = dataArr[0],
                        fore_roe_2yearlater = dataArr[0],
                        fore_roe_3yearlater = dataArr[0],
                        fore_np_1yearlater = dataArr[0],
                        fore_np_2yearlater = dataArr[0],
                        fore_np_3yearlater = dataArr[0],
                    });

                    Logger.Info("update stock " + ts_code + " fore roe and np");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("update stock fore roe and np occurs error : tscode={0};details:{1}", ts_code, ex));
            }
        }

        public static void UpdateStockIndustryName(string ts_code, string tdxindustry, string zjhindustry)
        {
            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(ts_code,MarketType.A);
                Logger.Info(ts_code + "," + tdxindustry);

                if (stockInfo == null)
                    return;

                string sql = "update xq_stock set tdxindustry=@tdxindustry,zjhindustry=@zjhindustry where id=@id";

                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    conn.Execute(sql, new
                    {
                        id = stockInfo.id,
                        tdxindustry = tdxindustry,
                        zjhindustry = zjhindustry
                    });

                    Logger.Info("修改股票(" + stockInfo.name + ")所在行业：" + tdxindustry);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("修改股票所属行业失败: tscode={0};详细：{1}", ts_code, ex));
            }
        }
    }
}
