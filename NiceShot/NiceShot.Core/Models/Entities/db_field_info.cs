namespace NiceShot.Core.Models.Entities
{
    /// <summary>
    /// 数据库表的字段信息
    /// </summary>
    public class db_field_info
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string column_name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string data_type { get; set; }
    }
}
