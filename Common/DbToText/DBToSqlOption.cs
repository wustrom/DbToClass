using Common.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbToText
{
    public class DBToSqlOption : SingleTon<DBToSqlOption>
    {
        StringBuilder strBuilderHead = new StringBuilder();
        string Wrap = "\n";
        string tab = "    ";
        string leftbra = "{";
        string rightbra = "}";
        private string space = ConfigurationManager.AppSettings["namespace"].ToString();
        public DBToSqlOption()
        {
            Head();
        }

        /// <summary>
        /// 头部
        /// </summary>
        private void Head()
        {
            strBuilderHead.Append("using Dapper;")
                          .Append(Wrap).Append("using System.Data.SqlClient;")
                          .Append(Wrap).Append("using System.Configuration;")
                          .Append(Wrap).Append("using System.Text;")
                          .Append(Wrap).Append("using Common.Extend;")
                          .Append(Wrap).Append("using Common;")
                          .Append(Wrap).Append("using System.Collections.Generic;")
                          .Append(Wrap).Append("using ").Append(space).Append(".Models;")
                          .Append(Wrap).Append(Wrap).Append("namespace ").Append(space).Append(".DBoperation")
                          .Append(Wrap).Append(leftbra).Append(Wrap);
        }

        /// <summary>
        /// 内容文本
        /// </summary>
        /// <param name="Fieids">对应字段</param>
        /// <param name="TableName">对应表名</param>
        private StringBuilder Content(List<Fieid> Fieids, string TableName)
        {
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderPrimaryKey = new StringBuilder();
            strBuliderContent.Append(tab).Append("public partial class ").Append(TableName).Append("Oper : SingleTon<").Append(TableName).Append("Oper>")
                             .Append(Wrap).Append(tab).Append(leftbra).Append(Wrap).Append(tab).Append(tab).Append("public string ConnString = ConfigurationManager.AppSettings[\"ConnString\"].ToString();");
            var insert = InsertSql(Fieids, TableName);
            var delete = DeleteSql(Fieids, TableName);
            var update = UpdataSql(Fieids, TableName);
            var select = SelectSql(Fieids, TableName);
            var selectByPage = SelectSqlByPage(Fieids, TableName);
            var selectByIds = SelectSqlByIds(Fieids, TableName);
            strBuliderContent.Append(insert).Append(delete).Append(update).Append(select).Append(selectByPage).Append(selectByIds).Append(tab).Append(rightbra).Append(Wrap).Append(rightbra);
            return strBuliderContent;
        }

        /// <summary>
        /// 更新sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder UpdataSql(List<Fieid> fieids, string TableName)
        {
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            var tableName = TableName.ToLower();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 更新")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool Update(").Append(TableName).Append(" ").Append(tableName).Append(")")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder sql = new StringBuilder(\"update ").Append(TableName).Append(" set \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part1 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part2 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var parm = new DynamicParameters();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("bool flag = true;").Append(Wrap);
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("part2.Append(\"").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("parm.Add(\"").Append(fieid.Fieid_Name).Append("\", ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
                }
                else
                {
                    strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (flag)")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\"").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("else")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\", ").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("parm.Add(\"").Append(fieid.Fieid_Name).Append("\", ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
                }
            }
            strBuliderContent.Append(strBuliderSql).Append(tab).Append(tab).Append(tab).Append("sql.Append(part1).Append(\" where \").Append(part2);");
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = conn.Execute(sql.ToString(), parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r > 0;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
            return strBuliderContent;
        }


        /// <summary>
        /// 删除sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder DeleteSql(List<Fieid> fieids, string TableName)
        {
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            StringBuilder strBuliderSqlSet = new StringBuilder();
            StringBuilder strBuliderSqlWhere = new StringBuilder();
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 删除")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"Id\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool Delete(int id)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra);
            strBuliderSql.Append("Delete From ").Append(TableName);
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderSqlWhere.Append(fieid.Fieid_Name).Append("=@").Append(fieid.Fieid_Name);
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("object parm = new { ").Append(fieid.Fieid_Name).Append(" = id };");
                }
            }
            strBuliderSql.Append(" where ").Append(strBuliderSqlWhere);
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = conn.Execute(@").Append("\"")
                             .Append(strBuliderSql).Append("\", parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r > 0;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap);

            return strBuliderContent;
        }

        /// <summary>
        /// 插入sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder InsertSql(List<Fieid> fieids, string TableName)
        {
            var tableName = TableName.ToLower();
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 插入")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool Insert(").Append(TableName).Append(" ").Append(tableName).Append(")")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder sql = new StringBuilder(\"insert into ").Append(TableName).Append(" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part1 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part2 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var parm = new DynamicParameters();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("bool flag = true;").Append(Wrap);
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey != true)
                {
                    strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (flag)")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\"").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part2.Append(\"@").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("else")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\",").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part2.Append(\",@").Append(fieid.Fieid_Name).Append("\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("parm.Add(\"").Append(fieid.Fieid_Name).Append("\", ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
                }
            }
            strBuliderContent.Append(strBuliderSql)
                             .Append(tab).Append(tab).Append(tab).Append("sql.Append(\"(\").Append(part1).Append(\") values(\").Append(part2).Append(\")\");");
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = conn.Execute(sql.ToString(), parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r > 0;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra);
            return strBuliderContent;
        }

        /// <summary>
        /// 查询sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectSql(List<Fieid> fieids, string TableName)
        {
            var tableName = TableName.ToLower();
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();

            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 查询")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>对象列表</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public List<").Append(TableName).Append("> Select(").Append(TableName).Append(" ").Append(tableName).Append(")")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder sql = new StringBuilder(\"Select \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".Field.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(").Append(tableName).Append(".Field);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("else")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(\"*\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("sql.Append(\" from ").Append(TableName).Append(" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part1 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var parm = new DynamicParameters();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("bool flag = true;").Append(Wrap);
            foreach (Fieid fieid in fieids)
            {
                strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (flag)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\"").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("else")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\" and ").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("parm.Add(\"").Append(fieid.Fieid_Name).Append("\", ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
            }
            strBuliderContent.Append(strBuliderSql)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".GroupBy.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\" Group By \").Append(").Append(tableName).Append(".GroupBy).Append(\" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".OrderBy.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\" Order By \").Append(").Append(tableName).Append(".OrderBy).Append(\" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!flag)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(\" where \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("sql.Append(part1);");

            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = (List<").Append(TableName).Append(">)conn.Query<").Append(TableName).Append(">(sql.ToString(), parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (r == null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("r = new List<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
            return strBuliderContent;
        }

        /// <summary>
        /// 分页查询sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectSqlByPage(List<Fieid> fieids, string TableName)
        {
            var tableName = TableName.ToLower();
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 分页查询")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"pageSize\">页面大小</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"pageNo\">页面编号</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>对象列表</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public List<").Append(TableName).Append("> SelectByPage(").Append(TableName).Append(" ").Append(tableName).Append(", int pageSize, int pageNo)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder sql = new StringBuilder(\"Select Top \").Append(pageSize).Append(\" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".Field.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(").Append(tableName).Append(".Field);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("else")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(\"*\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("sql.Append(\" from ").Append(TableName).Append(" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part1 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder part2 = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("StringBuilder strBuliderPage = new StringBuilder();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var parm = new DynamicParameters();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("bool flag = true;").Append(Wrap);
            foreach (Fieid fieid in fieids)
            {
                strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (flag)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\"").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("else")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("part1.Append(\" and ").Append(fieid.Fieid_Name).Append(" = @").Append(fieid.Fieid_Name).Append("\");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("parm.Add(\"").Append(fieid.Fieid_Name).Append("\", ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
            }
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderSql.Append(tab).Append(tab).Append(tab).Append("if (!flag)")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("strBuliderPage.Append(\" and\");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra);
                    strBuliderSql.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("strBuliderPage.Append(\" ").Append(fieid.Fieid_Name)
                                 .Append(" not in (\").Append(\"Select Top \").Append(pageSize * (pageNo - 1)).Append(\" ").Append(fieid.Fieid_Name).Append(" from ").Append(TableName).Append(" \");");
                    strBuliderSql.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".GroupBy.IsNullOrEmpty())")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("strBuliderPage.Append(\" Group By \").Append(").Append(tableName).Append(".GroupBy).Append(\" \");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".OrderBy.IsNullOrEmpty())")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("strBuliderPage.Append(\" Order By \").Append(").Append(tableName).Append(".OrderBy).Append(\" \");")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("flag = false;")
                                 .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra);
                    strBuliderSql.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("strBuliderPage.Append(\" )\");");
                }
            }
            strBuliderContent.Append(strBuliderSql)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!flag)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("sql.Append(\" where \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("sql.Append(part1).Append(strBuliderPage).Append(part1);");
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".GroupBy.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("part2.Append(\" Group By \").Append(").Append(tableName).Append(".GroupBy).Append(\" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".OrderBy.IsNullOrEmpty())")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("part2.Append(\" Order By \").Append(").Append(tableName).Append(".OrderBy).Append(\" \");")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("sql.Append(part2);");
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = (List<").Append(TableName).Append(">)conn.Query<").Append(TableName).Append(">(sql.ToString(), parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (r == null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("r = new List<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra);
            return strBuliderContent;
        }

        /// <summary>
        /// 查询sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectSqlByIds(List<Fieid> fieids, string TableName)
        {
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            StringBuilder strBuliderSqlSet = new StringBuilder();
            StringBuilder strBuliderSqlWhere = new StringBuilder();
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据Id查询")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"Id\"></param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public List<").Append(TableName).Append("> SelectByIds(List<string> List_Id)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra);
            strBuliderSql.Append("Select * From ").Append(TableName);
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderSqlWhere.Append(fieid.Fieid_Name).Append(" in @").Append(fieid.Fieid_Name);
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("object parm = new { ").Append(fieid.Fieid_Name).Append(" = List_Id.ToArray() };");
                }
            }
            strBuliderSql.Append(" where ").Append(strBuliderSqlWhere);
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("using (var conn = new SqlConnection(ConnString))")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Open();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("var r = (List<").Append(TableName).Append(">)conn.Query<").Append(TableName).Append(">(\"").Append(strBuliderSql).Append("\", parm);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("conn.Close();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("if (r == null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(tab).Append("r = new List<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("return r;")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap);

            return strBuliderContent;
        }

        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string GetText(List<Fieid> fieids, string TableName)
        {
            var strBuilder = Content(fieids, TableName);
            return strBuilderHead.ToString() + strBuilder.ToString();
        }
    }
}
