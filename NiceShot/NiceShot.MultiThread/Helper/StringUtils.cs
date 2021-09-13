using System;

namespace NiceShot.MultiThread.Helper
{
    public static class StringUtils
    {
        public static decimal? GetMoney_Yuan(string str_money)
        {
            if (str_money.Contains("亿"))
                return Math.Round(decimal.Parse(str_money.Replace("亿", "")) * 100000000, 2);
            else if (str_money.Contains("万"))
                return Math.Round(decimal.Parse(str_money.Replace("万", "")) * 10000, 2);
            else
                return decimal.Parse(str_money);
        }
    }
}
