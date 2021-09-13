namespace NiceShot.Core.Enums
{
    /// <summary>
    /// 公司类型
    /// </summary>
    public enum CompanyType
    {
        /// <summary>
        /// 证券
        /// </summary>
        Broker = 1,
        /// <summary>
        /// 保险
        /// </summary>
        Insurance = 2,
        /// <summary>
        /// 银行
        /// </summary>
        Bank = 3,
        /// <summary>
        /// 普通
        /// </summary>
        Common = 4
    }

    public static class CompanyTypeHelper
    {
        public static string ToCompanyTypeName(this CompanyType companyType)
        {
            string companyTypeName = "Common".ToLower();
            switch (companyType)
            {
                case CompanyType.Broker:
                    companyTypeName = "Broker".ToLower();
                    break;
                case CompanyType.Insurance:
                    companyTypeName = "Insurance".ToLower();
                    break;
                case CompanyType.Bank:
                    companyTypeName = "Bank".ToLower();
                    break;
            }

            return companyTypeName;
        }

        public static CompanyType ConvertToCompanyType(this string industry)
        {
            CompanyType comptype = CompanyType.Common;
            if (industry == "证券" || industry == "券商信托")
                comptype = CompanyType.Broker;
            else if (industry == "保险")
                comptype = CompanyType.Insurance;
            else if (industry == "银行")
                comptype = CompanyType.Bank;
            return comptype;
        }
    }
}
