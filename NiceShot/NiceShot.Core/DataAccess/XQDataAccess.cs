using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Helper;
using NiceShot.Core.Models.Entities;
using NiceShot.Core.Models.JsonObjects;
using System;
using System.Data;

namespace NiceShot.Core.DataAccess
{
    public class XQDataAccess
    {
        public static void OperateXQHKTradeType(string symbol, string tradeType)
        {
            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(symbol, MarketType.HK);
                if (stockInfo != null)
                {
                    //修改数据
                    string sql = "update xq_stock set tradetype=@tradetype where id=@id";

                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            id = stockInfo.id,
                            tradetype = tradeType
                        });
                    }

                    Logger.Info("update xueqiu hk stock tradetype：" + tradeType);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("同步雪球港股交易类型失败: tscode={0};详细：{1}", symbol, ex));
            }
        }

        public static void OperateXQHKStock(xq_stock_jo jo)
        {
            var ts_code = jo.symbol;
            var totalPinyin = PinYinConverterHelper.GetTotalPingYin(jo.name);
            string pinyin = string.Join(",", totalPinyin.FirstPingYin);
            string fullpinyin = string.Join(",", totalPinyin.TotalPingYin);

            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(ts_code, MarketType.HK);
                if (stockInfo != null)
                {
                    //修改数据
                    string sql = "update xq_stock set  ts_code=@ts_code,net_profit_cagr=@net_profit_cagr,ps=@ps,type=@type,percent=@percent,has_follow=@has_follow,float_shares=@float_shares,current=@current,amplitude=@amplitude,pcf=@pcf,current_year_percent=@current_year_percent,float_market_capital=@float_market_capital,market_capital=@market_capital,dividend_yield=@dividend_yield,roe_ttm=@roe_ttm,total_percent=@total_percent,percent5m=@percent5m,income_cagr=@income_cagr,amount=@amount,chg=@chg,issue_date=@issue_date,main_net_inflows=@main_net_inflows,volume=@volume,volume_ratio=@volume_ratio,pb=@pb,followers=@followers,turnover_rate=@turnover_rate,first_percent=@first_percent,name=@name,pinyin=@pinyin,fullpinyin=@fullpinyin,pe_ttm=@pe_ttm,total_shares=@total_shares,markettype=@markettype where id=@id";

                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            id = stockInfo.id,
                            ts_code = ts_code,
                            net_profit_cagr = jo.net_profit_cagr,
                            ps = jo.ps,
                            type = jo.type,
                            percent = jo.percent,
                            has_follow = jo.has_follow,
                            float_shares = jo.float_shares,
                            current = jo.current,
                            amplitude = jo.amplitude,
                            pcf = jo.pcf,
                            current_year_percent = jo.current_year_percent,
                            float_market_capital = jo.float_market_capital,
                            market_capital = jo.market_capital,
                            dividend_yield = jo.dividend_yield,
                            roe_ttm = jo.roe_ttm,
                            total_percent = jo.total_percent,
                            percent5m = jo.percent5m,
                            income_cagr = jo.income_cagr,
                            amount = jo.amount,
                            chg = jo.chg,
                            issue_date = jo.issue_date_ts.ToLocalTimeDateByMilliseconds(),
                            main_net_inflows = jo.main_net_inflows,
                            volume = jo.volume,
                            volume_ratio = jo.volume_ratio,
                            pb = jo.pb,
                            followers = jo.followers,
                            turnover_rate = jo.turnover_rate,
                            first_percent = jo.first_percent,
                            name = jo.name,
                            pinyin = pinyin,
                            fullpinyin = fullpinyin,
                            pe_ttm = jo.pe_ttm,
                            total_shares = jo.total_shares,
                            markettype = 2
                        });
                    }

                    Logger.Info("update xueqiu basic data：" + jo.name + ",pe:" + jo.pe_ttm);
                }
                else
                {
                    //添加数据
                    string sql = "insert into xq_stock (symbol,ts_code,net_profit_cagr,ps,type,percent,has_follow,float_shares,current,amplitude,pcf,current_year_percent,float_market_capital,market_capital,dividend_yield,roe_ttm,total_percent,percent5m,income_cagr,amount,chg,issue_date,main_net_inflows,volume,volume_ratio,pb,followers,turnover_rate,first_percent,name,pinyin,fullpinyin,pe_ttm,total_shares,markettype) values (@symbol,@ts_code,@net_profit_cagr,@ps,@type,@percent,@has_follow,@float_shares,@current,@amplitude,@pcf,@current_year_percent,@float_market_capital,@market_capital,@dividend_yield,@roe_ttm,@total_percent,@percent5m,@income_cagr,@amount,@chg,@issue_date,@main_net_inflows,@volume,@volume_ratio,@pb,@followers,@turnover_rate,@first_percent,@name,@pinyin,@fullpinyin,@pe_ttm,@total_shares,@markettype)";

                    xq_stock entity = new xq_stock()
                    {
                        symbol = jo.symbol,
                        ts_code = ts_code,
                        net_profit_cagr = jo.net_profit_cagr,
                        ps = jo.ps,
                        type = jo.type,
                        percent = jo.percent,
                        has_follow = jo.has_follow,
                        float_shares = jo.float_shares,
                        current = jo.current,
                        amplitude = jo.amplitude,
                        pcf = jo.pcf,
                        current_year_percent = jo.current_year_percent,
                        float_market_capital = jo.float_market_capital,
                        market_capital = jo.market_capital,
                        dividend_yield = jo.dividend_yield,
                        roe_ttm = jo.roe_ttm,
                        total_percent = jo.total_percent,
                        percent5m = jo.percent5m,
                        income_cagr = jo.income_cagr,
                        amount = jo.amount,
                        chg = jo.chg,
                        issue_date = jo.issue_date_ts.ToLocalTimeDateByMilliseconds(),
                        main_net_inflows = jo.main_net_inflows,
                        volume = jo.volume,
                        volume_ratio = jo.volume_ratio,
                        pb = jo.pb,
                        followers = jo.followers,
                        turnover_rate = jo.turnover_rate,
                        first_percent = jo.first_percent,
                        name = jo.name,
                        pinyin = pinyin,
                        fullpinyin = fullpinyin,
                        pe_ttm = jo.pe_ttm,
                        total_shares = jo.total_shares,
                        markettype = 2
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);

                        Logger.Info("insert xueqiu basic data：" + jo.name + ",pe:" + jo.pe_ttm);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("同步雪球港股列表数据失败: tscode={0};详细：{1}", ts_code, ex));
            }

        }

        public static void OperateXQAStock(xq_stock_jo jo)
        {
            var ts_code = StringUtils.ConvertXQToEMSymbol(jo.symbol);
            var totalPinyin = PinYinConverterHelper.GetTotalPingYin(jo.name);
            string pinyin = string.Join(",", totalPinyin.FirstPingYin);
            string fullpinyin = string.Join(",", totalPinyin.TotalPingYin);

            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(ts_code, MarketType.A);
                if (stockInfo != null)
                {
                    //修改数据
                    string sql = "update xq_stock set  ts_code=@ts_code,net_profit_cagr=@net_profit_cagr,ps=@ps,type=@type,percent=@percent,has_follow=@has_follow,float_shares=@float_shares,current=@current,amplitude=@amplitude,pcf=@pcf,current_year_percent=@current_year_percent,float_market_capital=@float_market_capital,market_capital=@market_capital,dividend_yield=@dividend_yield,roe_ttm=@roe_ttm,total_percent=@total_percent,percent5m=@percent5m,income_cagr=@income_cagr,amount=@amount,chg=@chg,issue_date=@issue_date,main_net_inflows=@main_net_inflows,volume=@volume,volume_ratio=@volume_ratio,followers=@followers,turnover_rate=@turnover_rate,first_percent=@first_percent,name=@name,pinyin=@pinyin,fullpinyin=@fullpinyin,pe_ttm=@pe_ttm,total_shares=@total_shares,markettype=@markettype where id=@id";

                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            id = stockInfo.id,
                            ts_code = ts_code,
                            net_profit_cagr = jo.net_profit_cagr,
                            ps = jo.ps,
                            type = jo.type,
                            percent = jo.percent,
                            has_follow = jo.has_follow,
                            float_shares = jo.float_shares,
                            current = jo.current,
                            amplitude = jo.amplitude,
                            pcf = jo.pcf,
                            current_year_percent = jo.current_year_percent,
                            float_market_capital = jo.float_market_capital,
                            market_capital = jo.market_capital,
                            dividend_yield = jo.dividend_yield,
                            roe_ttm = jo.roe_ttm,
                            total_percent = jo.total_percent,
                            percent5m = jo.percent5m,
                            income_cagr = jo.income_cagr,
                            amount = jo.amount,
                            chg = jo.chg,
                            issue_date = jo.issue_date_ts.ToLocalTimeDateByMilliseconds(),
                            main_net_inflows = jo.main_net_inflows,
                            volume = jo.volume,
                            volume_ratio = jo.volume_ratio,
                            //pb = jo.pb,
                            followers = jo.followers,
                            turnover_rate = jo.turnover_rate,
                            first_percent = jo.first_percent,
                            name = jo.name,
                            pinyin = pinyin,
                            fullpinyin = fullpinyin,
                            pe_ttm = jo.pe_ttm,
                            total_shares = jo.total_shares,
                            markettype = 1
                        });
                    }

                    Logger.Info("update xueqiu basic data：" + jo.name + ",pe:" + jo.pe_ttm);
                }
                else
                {
                    //添加数据
                    string sql = "insert into xq_stock (symbol,ts_code,net_profit_cagr,ps,type,percent,has_follow,float_shares,current,amplitude,pcf,current_year_percent,float_market_capital,market_capital,dividend_yield,roe_ttm,total_percent,percent5m,income_cagr,amount,chg,issue_date,main_net_inflows,volume,volume_ratio,pb,followers,turnover_rate,first_percent,name,pinyin,fullpinyin,pe_ttm,total_shares,markettype) values (@symbol,@ts_code,@net_profit_cagr,@ps,@type,@percent,@has_follow,@float_shares,@current,@amplitude,@pcf,@current_year_percent,@float_market_capital,@market_capital,@dividend_yield,@roe_ttm,@total_percent,@percent5m,@income_cagr,@amount,@chg,@issue_date,@main_net_inflows,@volume,@volume_ratio,@pb,@followers,@turnover_rate,@first_percent,@name,@pinyin,@fullpinyin,@pe_ttm,@total_shares,@markettype)";

                    xq_stock entity = new xq_stock()
                    {
                        symbol = jo.symbol,
                        ts_code = ts_code,
                        net_profit_cagr = jo.net_profit_cagr,
                        ps = jo.ps,
                        type = jo.type,
                        percent = jo.percent,
                        has_follow = jo.has_follow,
                        float_shares = jo.float_shares,
                        current = jo.current,
                        amplitude = jo.amplitude,
                        pcf = jo.pcf,
                        current_year_percent = jo.current_year_percent,
                        float_market_capital = jo.float_market_capital,
                        market_capital = jo.market_capital,
                        dividend_yield = jo.dividend_yield,
                        roe_ttm = jo.roe_ttm,
                        total_percent = jo.total_percent,
                        percent5m = jo.percent5m,
                        income_cagr = jo.income_cagr,
                        amount = jo.amount,
                        chg = jo.chg,
                        issue_date = jo.issue_date_ts.ToLocalTimeDateByMilliseconds(),
                        main_net_inflows = jo.main_net_inflows,
                        volume = jo.volume,
                        volume_ratio = jo.volume_ratio,
                        pb = jo.pb,
                        followers = jo.followers,
                        turnover_rate = jo.turnover_rate,
                        first_percent = jo.first_percent,
                        name = jo.name,
                        pinyin = pinyin,
                        fullpinyin = fullpinyin,
                        pe_ttm = jo.pe_ttm,
                        total_shares = jo.total_shares,
                        markettype = 1
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);

                        Logger.Info("insert xueqiu basic data：" + jo.name + ",pe:" + jo.pe_ttm);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("同步雪球A股列表数据失败: tscode={0};详细：{1}", ts_code, ex));
            }

        }

        public static void OperateBalanceSheetData(string securityCode, xq_balancesheet_common_item item)
        {
            em_balancesheet_common balance = NiceShotDataAccess.GetEMBalanceSheetInfo(securityCode, item.report_date.ToLocalTimeDateByMilliseconds());
            if (balance != null)
            {
                //修改数据
                string sql = "update em_balancesheet_common set tradefasset=@tradefasset,otherequity_invest=@otherequity_invest,othernonlfinasset=@othernonlfinasset,contractliab=@contractliab,contractasset=@contractasset where id=@id";

                using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                {
                    conn.Execute(sql, new
                    {
                        tradefasset = item.tradable_fnncl_assets[0],
                        otherequity_invest = item.other_eq_ins_invest[0],
                        othernonlfinasset = item.other_illiquid_fnncl_assets[0],
                        contractliab = item.contract_liabilities[0],
                        contractasset = item.contractual_assets[0],
                        id = balance.id,
                    });
                }

                Logger.Info("update xq balance sheet data：" + securityCode + ",contractasset=" + item.contractual_assets[0]);
            }
        }

        public static void OperateAMainIndexData(string ts_code, xq_fin_indicator_data_item jo)
        {
            var date = jo.report_date.ToLocalTimeDateByMilliseconds();
            try
            {
                em_mainindex mainindex = EMDataAccess.GetAMainIndexData(ts_code, date.Value.ToString("yyyy-MM-dd"));
                if (mainindex != null)
                {
                    //修改数据
                    Logger.Info(string.Format("update data: tscode={0};date={1}", ts_code, date));
                    string sql = "update em_a_mainindex set yyzsr=@yyzsr,gsjlr=@gsjlr,kfjlr=@kfjlr,yyzsrtbzz=@yyzsrtbzz,gsjlrtbzz=@gsjlrtbzz,kfjlrtbzz=@kfjlrtbzz,jqjzcsyl=@jqjzcsyl,tbjzcsyl=@tbjzcsyl,tbzzcsyl=@tbzzcsyl,mll=@mll,jll=@jll where id=@id";
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, new
                        {
                            yyzsr = jo.total_revenue.ConvertToDecimalArrayToDecimal(),
                            gsjlr = jo.net_profit_atsopc.ConvertToDecimalArrayToDecimal(),
                            kfjlr = jo.net_profit_after_nrgal_atsolc.ConvertToDecimalArrayToDecimal(),
                            yyzsrtbzz = jo.operating_income_yoy.ConvertToDecimalArrayToDecimal(),
                            gsjlrtbzz = jo.net_profit_atsopc_yoy.ConvertToDecimalArrayToDecimal(),
                            kfjlrtbzz = jo.np_atsopc_nrgal_yoy.ConvertToDecimalArrayToDecimal(),
                            jqjzcsyl = jo.avg_roe.ConvertToDecimalArrayToDecimal(),
                            tbjzcsyl = jo.ore_dlt.ConvertToDecimalArrayToDecimal(),
                            tbzzcsyl = jo.net_interest_of_total_assets.ConvertToDecimalArrayToDecimal(),
                            mll = jo.gross_selling_rate.ConvertToDecimalArrayToDecimal(),
                            jll = jo.net_selling_rate.ConvertToDecimalArrayToDecimal(),
                            id = mainindex.id
                        });
                    }
                }
                else
                {
                    //添加数据
                    Logger.Info(string.Format("insert data: tscode={0};date={1}", ts_code, date));

                    string sql = "insert into em_a_mainindex(yyzsr,gsjlr,kfjlr,yyzsrtbzz,gsjlrtbzz,kfjlrtbzz,jqjzcsyl,tbjzcsyl,tbzzcsyl,mll,jll,ts_code,date) values (@yyzsr,@gsjlr,@kfjlr,@yyzsrtbzz,@gsjlrtbzz,@kfjlrtbzz,@jqjzcsyl,@tbjzcsyl,@tbzzcsyl,@mll,@jll,@ts_code,@date)";

                    em_mainindex entity = new em_mainindex()
                    {
                        yyzsr = jo.total_revenue.ConvertToDecimalArrayToDecimal(),
                        gsjlr = jo.net_profit_atsopc.ConvertToDecimalArrayToDecimal(),
                        kfjlr = jo.net_profit_after_nrgal_atsolc.ConvertToDecimalArrayToDecimal(),
                        yyzsrtbzz = jo.operating_income_yoy.ConvertToDecimalArrayToDecimal(),
                        gsjlrtbzz = jo.net_profit_atsopc_yoy.ConvertToDecimalArrayToDecimal(),
                        kfjlrtbzz = jo.np_atsopc_nrgal_yoy.ConvertToDecimalArrayToDecimal(),
                        jqjzcsyl = jo.avg_roe.ConvertToDecimalArrayToDecimal(),
                        tbjzcsyl = jo.ore_dlt.ConvertToDecimalArrayToDecimal(),
                        tbzzcsyl = jo.net_interest_of_total_assets.ConvertToDecimalArrayToDecimal(),
                        mll = jo.gross_selling_rate.ConvertToDecimalArrayToDecimal(),
                        jll = jo.net_selling_rate.ConvertToDecimalArrayToDecimal(),
                        ts_code = ts_code,
                        date = jo.report_date.ToLocalTimeDateByMilliseconds(),
                    };
                    using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                    {
                        conn.Execute(sql, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("sync a stock finance main index error: tscode={0};date={1},详细：{2}", ts_code, date, ex));
            }

        }

        public static void OperateTopHoldersData(TopHolderType top_holders_type, string securitycode, string json_str)
        {
            try
            {
                xq_stock stockInfo = NiceShotDataAccess.GetStockInfo(securitycode, MarketType.A);
                if (stockInfo != null)
                {
                    if (top_holders_type == TopHolderType.Sdgd)
                    {
                        string sql = "update xq_stock set sdgd=@sdgd where id=@id";

                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                id = stockInfo.id,
                                sdgd = json_str
                            });
                        }

                        Logger.Info("update xueqiu stock sdgd：" + json_str);
                    }
                    else if (top_holders_type == TopHolderType.Sdltgd)
                    {
                        string sql = "update xq_stock set sdltgd=@sdltgd where id=@id";

                        using (IDbConnection conn = new MySqlConnection(MySqlDbHelper.NICESHOTDB_CONN_STRING))
                        {
                            conn.Execute(sql, new
                            {
                                id = stockInfo.id,
                                sdltgd = json_str
                            });
                        }

                        Logger.Info("update xueqiu stock sdltgd：" + json_str);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("同步雪球十大股东数据失败: tscode={0};详细：{1}", securitycode, ex));
            }
        }
    }
}
