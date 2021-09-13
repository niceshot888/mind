//using System;

//namespace NiceShot.DotNetWinFormsClient.Core
//{
//    public static class StringUtils
//    {
//        /// <summary>
//        /// 转换Excel列数字索引为字母列名
//        /// </summary>
//        public static string ConvertExcelColumnIndexToChar(int colIndex)
//        {
//            string result = string.Empty;
//            int iHead = (colIndex - 1) / 26;
//            int iLeftOver = (colIndex - 1) % 26;
//            if (iHead >= 26)
//                result = ConvertExcelColumnIndexToChar(iHead);
//            else if (iHead > 0)
//                result += (char)(64 + iHead);
//            result += (char)(65 + iLeftOver);
//            return result;
//        }

//        public static string ToEmptyString(this object str)
//        {
//            return str == null ? string.Empty : str.ToString();
//        }

//        /// <summary>
//        /// 统一转换为单位:元
//        /// </summary>
//        public static decimal? ConvertToUnitYuan(this string str_money)
//        {
//            if (string.IsNullOrEmpty(str_money) || str_money == "--")
//                return null;

//            if (str_money.Contains("万亿"))
//                return Math.Round(decimal.Parse(str_money.Replace("万亿", "")) * 1000000000000, 2);
//            else if (str_money.Contains("亿"))
//                return Math.Round(decimal.Parse(str_money.Replace("亿", "")) * 100000000, 2);
//            else if (str_money.Contains("万"))
//                return Math.Round(decimal.Parse(str_money.Replace("万", "")) * 10000, 2);
//            else
//            {
//                /*decimal val;
//                var rs = decimal.TryParse(str_money, out val);
//                return rs ? Math.Round(val, 2) : 0;*/

//                return Math.Round(decimal.Parse(str_money), 2);
//            }

//        }

//        public static decimal? ConvertToDecimal(this string str_money)
//        {
//            if (string.IsNullOrEmpty(str_money) || str_money == "--")
//                return null;

//            return decimal.Parse(str_money);
//        }

//        /// <summary>
//        /// 保留小数点后2位数
//        /// </summary>
//        public static decimal? Round2Decimals(this string str_money)
//        {
//            if (string.IsNullOrEmpty(str_money) || str_money == "--")
//                return null;

//            return Math.Round(decimal.Parse(str_money), 2);
//        }

//        public static DateTime? ConvertToDate(this string date)
//        {
//            if (string.IsNullOrEmpty(date))
//                return null;

//            date = date.Replace("\\", "-").Replace(" 0:00:00", "");

//            return DateTime.Parse(date);
//        }

//        /// <summary>
//        /// 将SZ000961转变为000961.SZ格式
//        /// </summary>
//        public static string ConvertXQToEMSymbol(string symbol)
//        {
//            if (string.IsNullOrEmpty(symbol))
//                return string.Empty;
//            return symbol.Substring(2) + "." + symbol.Substring(0, 2);
//        }

//        /// <summary>
//        /// 将000961.SZ转变为SZ000961格式
//        /// </summary>
//        public static string ConvertEMToXQSymbol(string ts_code)
//        {
//            if (string.IsNullOrEmpty(ts_code))
//                return string.Empty;

//            return ts_code.Substring(7) + ts_code.Substring(0, 6);
//        }

//        #region unixtime时间转换
//        /// <summary>
//        ///  时间戳转本地时间-时间戳精确到秒
//        /// </summary> 
//        public static DateTime ToLocalTimeDateBySeconds(this long unix)
//        {
//            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
//            return dto.ToLocalTime().DateTime;
//        }

//        /// <summary>
//        ///  时间转时间戳Unix-时间戳精确到秒
//        /// </summary> 
//        public static long ToUnixTimestampBySeconds(this DateTime dt)
//        {
//            DateTimeOffset dto = new DateTimeOffset(dt);
//            return dto.ToUnixTimeSeconds();
//        }


//        /// <summary>
//        ///  时间戳转本地时间-时间戳精确到毫秒
//        /// </summary> 
//        public static DateTime ToLocalTimeDateByMilliseconds(this long unix)
//        {
//            var dto = DateTimeOffset.FromUnixTimeMilliseconds(unix);
//            return dto.ToLocalTime().DateTime;
//        }

//        /// <summary>
//        ///  时间转时间戳Unix-时间戳精确到毫秒
//        /// </summary> 
//        public static long ToUnixTimestampByMilliseconds(this DateTime dt)
//        {
//            DateTimeOffset dto = new DateTimeOffset(dt);
//            return dto.ToUnixTimeMilliseconds();
//        }
//        #endregion

//    }
//}
