using Common.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbToText
{
    public class DbToSql_SQLite
    {
        StringBuilder strBuilderHead = new StringBuilder();
        /// <summary>
        /// 删除Sql语句
        /// </summary>
        string DropSqlHead = "DROP TABLE IF EXISTS  [main].[";
        /// <summary>
        /// 创建Sql语句
        /// </summary>
        string CreatSqlHead = "CREATE TABLE [main].[";
        /// <summary>
        /// 换行
        /// </summary>
        string Wrap = "\n";
        /// <summary>
        /// tab
        /// </summary>
        string tab = "    ";

        public DbToSql_SQLite()
        {
            Head();
        }
        private void Head()
        {

        }

        /// <summary>
        /// 内容文本
        /// </summary>
        /// <param name="Fieids">对应字段</param>
        /// <param name="TableName">对应表名</param>
        public StringBuilder Content(List<Fieid> Fieids, string TableName)
        {
            StringBuilder bulider = new StringBuilder();
            StringBuilder PrimaryString = new StringBuilder();
            bulider.Append(DropSqlHead).Append(TableName).Append("];").Append(Wrap)
                   .Append(CreatSqlHead).Append(TableName).Append("] (").Append(Wrap);
            foreach (var item in Fieids)
            {
                //是否是主键
                if (item.PrimaryKey.Value)
                {
                    PrimaryString.Append("PRIMARY KEY (\"").Append(item.Fieid_Name).Append("\"),").Append(Wrap);
                }
                bulider.Append(item.Fieid_Name);
                //当类型为string时的Sql
                if (item.Fieid_Type.ToLower() == "string")
                {
                    bulider.Append("nvarchar");
                    if (item.Fieid_Length != -1)
                    {
                        bulider.Append("(").Append(item.Fieid_Length).Append(")");
                    }
                }
                //数据库是否允许为空
                if (item.AllowDBNull == false)
                {
                    bulider.Append(" NOT NULL");
                }
                //结束是的逗号与换行
                bulider.Append(",").Append(Wrap);
            }
            bulider.Append(PrimaryString).Append(");").Append(Wrap).Append(Wrap);
            return bulider;
        }
    }
}
