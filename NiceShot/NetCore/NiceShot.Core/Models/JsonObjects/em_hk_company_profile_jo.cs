namespace NiceShot.Core.Models.JsonObjects
{
    public class em_hk_company_profile_jo
    {
        public em_hk_company_profile_gszl gszl { get; set; }
    }

    public class em_hk_company_profile_gszl
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string gsmc { get; set; }
        /// <summary>
        /// 公司成立日期
        /// </summary>
        public string gsclrq { get; set; }
        /// <summary>
        /// 董事长
        /// </summary>
        public string dsz { get; set; }
        /// <summary>
        /// 公司秘书
        /// </summary>
        public string gsms { get; set; }
        /// <summary>
        /// 员工人数
        /// </summary>
        public string ygrs { get; set; }
        /// <summary>
        /// 核数师
        /// </summary>
        public string hss { get; set; }
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string gsjs { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string sshy { get; set; }
    }
}
