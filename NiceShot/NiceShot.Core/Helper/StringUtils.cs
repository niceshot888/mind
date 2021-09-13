using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NiceShot.Core.Helper
{
    public static class StringUtils
    {
        /// <summary>
        /// 转换Excel列数字索引为字母列名
        /// </summary>
        public static string ConvertExcelColumnIndexToChar(int colIndex)
        {
            string result = string.Empty;
            int iHead = (colIndex - 1) / 26;
            int iLeftOver = (colIndex - 1) % 26;
            if (iHead >= 26)
                result = ConvertExcelColumnIndexToChar(iHead);
            else if (iHead > 0)
                result += (char)(64 + iHead);
            result += (char)(65 + iLeftOver);
            return result;
        }

        public static string ToEmptyString(this object str)
        {
            return str == null ? string.Empty : str.ToString();
        }

        public static string ConvertDecimalToString(this decimal? val,bool haveComma)
        {
            return val == null ? string.Empty : (haveComma? String.Format("{0:N2}", val) : val.ToString());
        }

        /// <summary>
        /// 统一转换为单位:元
        /// </summary>
        public static decimal? ConvertToUnitYuan(this string str_money)
        {
            if (string.IsNullOrEmpty(str_money) || str_money == "--")
                return null;

            if (str_money.Contains("万亿"))
                return Math.Round(decimal.Parse(str_money.Replace("万亿", "")) * 1000000000000, 2);
            else if (str_money.Contains("亿"))
                return Math.Round(decimal.Parse(str_money.Replace("亿", "")) * 100000000, 2);
            else if (str_money.Contains("万"))
                return Math.Round(decimal.Parse(str_money.Replace("万", "")) * 10000, 2);
            else
            {
                /*decimal val;
                var rs = decimal.TryParse(str_money, out val);
                return rs ? Math.Round(val, 2) : 0;*/

                return Math.Round(decimal.Parse(str_money), 2);
            }

        }

        public static decimal? ConvertToDecimal(this string str_money)
        {
            if (string.IsNullOrEmpty(str_money) || str_money == "--")
                return null;

            return decimal.Parse(str_money);
        }

        public static decimal ConvertToNotNullDecimal(string str)
        {
            decimal val;
            return decimal.TryParse(str, out val) ? val : 0;
        }

        public static int ConvertToNotNullInt(string str)
        {
            int val;
            return int.TryParse(str, out val) ? val : 0;
        }


        public static decimal? ConvertToDecimalArrayToDecimal(this decimal?[] decimalArray)
        {
            if (decimalArray == null || decimalArray.Length < 2)
                return null;

            return decimalArray[0];
        }

        /// <summary>
        /// 保留小数点后2位数
        /// </summary>
        public static decimal? Round2Decimals(this string str_money)
        {
            if (string.IsNullOrEmpty(str_money) || str_money == "--")
                return null;

            return Math.Round(decimal.Parse(str_money), 2);
        }

        /// <summary>
        /// 保留小数点后0位数
        /// </summary>
        public static decimal? Round0Decimals(this string str_money)
        {
            if (string.IsNullOrEmpty(str_money) || str_money == "--")
                return null;

            return Math.Round(decimal.Parse(str_money), 0);
        }

        /*public static DateTime? ConvertToDateTime(this string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;

            //date = date.Replace("\\", "-").Replace(" 0:00:00", "");

            return DateTime.Parse(date);
        }*/

        public static DateTime? ConvertToDate(this string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;

            date = date.Replace("\\", "-").Replace(" 0:00:00", "");

            return DateTime.Parse(date);
        }

        /// <summary>
        /// 将SZ000961转变为000961.SZ格式
        /// </summary>
        public static string ConvertXQToEMSymbol(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
                return string.Empty;
            return symbol.Substring(2) + "." + symbol.Substring(0, 2);
        }

        /// <summary>
        /// 将000961.SZ转变为SZ000961格式
        /// </summary>
        public static string ConvertEMToXQSymbol(string ts_code)
        {
            if (string.IsNullOrEmpty(ts_code))
                return string.Empty;

            return ts_code.Substring(7) + ts_code.Substring(0, 6);
        }

        #region unixtime时间转换
        /// <summary>
        ///  时间戳转本地时间-时间戳精确到秒
        /// </summary> 
        public static DateTime ToLocalTimeDateBySeconds(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间转时间戳Unix-时间戳精确到秒
        /// </summary> 
        public static long ToUnixTimestampBySeconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeSeconds();
        }


        /// <summary>
        ///  时间戳转本地时间-时间戳精确到毫秒
        /// </summary> 
        public static DateTime? ToLocalTimeDateByMilliseconds(this long? unix)
        {
            if (!unix.HasValue)
                return null;

            var dto = DateTimeOffset.FromUnixTimeMilliseconds(unix.Value);
            return dto.ToLocalTime().DateTime;
        }

        /// <summary>
        ///  时间转时间戳Unix-时间戳精确到毫秒
        /// </summary> 
        public static long ToUnixTimestampByMilliseconds(this DateTime dt)
        {
            DateTimeOffset dto = new DateTimeOffset(dt);
            return dto.ToUnixTimeMilliseconds();
        }

        public static string ToShortDateString(this DateTime? date)
        {
            if (!date.HasValue)
                return string.Empty;
            return date.Value.ToShortDateString();
        }

        public static string FromUnicodeString(this string str)
        {
            //最直接的方法Regex.Unescape(str);
            var strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }

        #endregion

    }
}
