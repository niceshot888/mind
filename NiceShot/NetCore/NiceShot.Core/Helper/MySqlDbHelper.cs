using Dapper;
using MySql.Data.MySqlClient;
using NiceShot.Core.Enums;
using NiceShot.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NiceShot.Core.Helper
{
    public class MySqlDbHelper
    {
        public static readonly string NICESHOTDB_CONN_STRING = "server = localhost;User Id = root;password = root;Database = niceshotdb;Charset=utf8";

        #region 公用方法

        /// <summary>
        /// 构建代码
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dbname"></param>
        public static void BuildEntityBatchedCode(string tableName, SeparatedType separatedType, string[] ignoreInsertFields, string[] ignoreUpdateFields, string dbname = "niceshotdb")
        {
            BuildEntityCode(tableName, dbname);
            BuildAssignEntityCode(tableName, OperType.Add, separatedType, ignoreInsertFields, dbname);
            BuildAssignEntityCode(tableName, OperType.Edit, separatedType, ignoreUpdateFields, dbname);
            BuildInsertSql(tableName, ignoreInsertFields);
            BuildUpdateSql(tableName, ignoreUpdateFields);
        }

        /// <summary>
        /// 生成数据库实体类
        /// </summary>
        public static void BuildEntityCode(string tableName, string dbname = "niceshotdb")
        {
            var fields = GetDbFields(tableName, dbname);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + ".cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var field in fields)
                {
                    string dataType = ConvertDbTypeToDataType(field.data_type);
                    sw.WriteLine($"public {dataType} {field.column_name} {{get;set;}}");
                }
            }
        }

        /// <summary>
        /// 生成赋值实体代码
        /// </summary>
        /// <param name="tableName">表名</param>
        public static void BuildAssignEntityCode(string tableName, OperType operType, SeparatedType separatedType, string[] ignoreFields, string dbname = "niceshotdb")
        {
            var fields = GetDbFields(tableName, dbname);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + "_jo_to_entity_" + (operType == OperType.Add ? "add" : "edit") + "_" + (separatedType == SeparatedType.Comma ? "comma" : "semicolon") + ".cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var field in fields)
                {
                    if (field.column_name != "id" && ignoreFields.Contains(field.column_name))
                        continue;

                    if (separatedType == SeparatedType.Comma)
                    {
                        //sw.WriteLine($"{field.column_name} = StringUtils.ConvertToUnitYuan(jo.{field.column_name}),");
                        sw.WriteLine($"{field.column_name} = jo.{field.column_name},");
                    }
                    else
                    {
                        sw.WriteLine($"entity.{field.column_name} = jo.{field.column_name};");
                    }
                }
            }
        }

        public static void BuildInsertSql(string tableName, string[] ignoreFields)
        {
            var fields = GetDbFields(tableName);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + "_insert_" + ".cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write("insert into " + tableName);

                StringBuilder sbFields = new StringBuilder();
                StringBuilder sbValues = new StringBuilder();
                foreach (var field in fields)
                {
                    if (ignoreFields.Contains(field.column_name))
                        continue;

                    sbFields.AppendFormat(field.column_name + ",");
                    sbValues.AppendFormat("@" + field.column_name + ",");
                }

                sw.Write(" (" + sbFields.ToString().TrimEnd(',') + ")");
                sw.Write(" values (" + sbValues.ToString().TrimEnd(',') + ")");
            }
        }

        public static void BuildUpdateSql(string tableName, string[] ignoreFields)
        {
            var fields = GetDbFields(tableName);

            var dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files");
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files", tableName + "_update_" + ".cs");
            if (File.Exists(fileName))
                File.WriteAllText(fileName, string.Empty);

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write("update " + tableName + " set ");

                StringBuilder sbFields = new StringBuilder();
                foreach (var field in fields)
                {
                    if (ignoreFields.Contains(field.column_name))
                        continue;

                    sbFields.AppendFormat("{0}=@{0},", field.column_name);
                }

                sw.Write(" " + sbFields.ToString().TrimEnd(','));
                sw.Write(" where id=@id");
            }
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 获取数据库表字段信息
        /// </summary>
        private static List<db_field_info> GetDbFields(string tableName, string dbname = "niceshotdb")
        {
            // and COLUMN_NAME!='id'
            string sql = "select column_name,data_type from information_schema.columns where table_name='" + tableName + "' and table_schema='" + dbname + "'";
            using (IDbConnection conn = new MySqlConnection(NICESHOTDB_CONN_STRING))
            {
                var dataList = conn.Query<db_field_info>(sql).ToList();
                return dataList;
            }
        }

        /// <summary>
        /// 转换数据库字段类型为程序数据类型
        /// </summary>
        private static string ConvertDbTypeToDataType(string dbtype)
        {
            string datatype;
            switch (dbtype)
            {
                case "date":
                    datatype = "DateTime?";
                    break;
                case "int":
                    datatype = "int?";
                    break;
                case "bigint":
                    datatype = "int";
                    break;
                case "decimal":
                    datatype = "decimal?";
                    break;
                case "tinyint":
                    datatype = "bool?";
                    break;
                default:
                    datatype = "string";
                    break;
            }

            return datatype;
        }

        #endregion


    }
}
