using Common.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DbToText
{
    public class DBToSqlOptionLambda : SingleTon<DBToSqlOptionLambda>
    {
        StringBuilder strBuilderHead = new StringBuilder();
        string Wrap = "\n";
        string tab = "    ";
        string leftbra = "{";
        string rightbra = "}";
        private string space = ConfigurationManager.AppSettings["namespace"].ToString();
        public DBToSqlOptionLambda()
        {
            Head();
        }

        /// <summary>
        /// 头部
        /// </summary>
        private void Head()
        {
            strBuilderHead.Append("using Common;")
                          .Append(Wrap).Append("using System;")
                          .Append(Wrap).Append("using System.Configuration;")
                          .Append(Wrap).Append("using System.Collections.Generic;")
                          .Append(Wrap).Append("using System.Linq;")
                          .Append(Wrap).Append("using System.Text;")
                          .Append(Wrap).Append("using Common.LambdaOpertion;")
                          .Append(Wrap).Append("using Common.Extend;")
                          .Append(Wrap).Append("using System.Data;")
                          .Append(Wrap).Append("using ").Append(space).Append(".Models;")
                          .Append(Wrap).Append(Wrap).Append("namespace ").Append(space).Append(".Operation")
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
                             .Append(Wrap).Append(tab).Append(leftbra).Append(Wrap);
            var insert = InsertSql(Fieids, TableName);
            var delete = DeleteSql(Fieids, TableName);
            var update = UpdataSql(Fieids, TableName);
            var selectById = SelectByIdSql(Fieids, TableName);
            var select = SelectSql(Fieids, TableName);
            var selectPage = SelectSqlPage(Fieids, TableName);
            var selectCount = SelectCount(Fieids, TableName);
            strBuliderContent.Append(select).Append(selectById).Append(selectPage).Append(selectCount).Append(delete).Append(update).Append(insert).Append(Wrap).Append(tab).Append(rightbra).Append(Wrap).Append(rightbra);
            return strBuliderContent;
        }

        /// <summary>
        /// 根据Id查询sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectByIdSql(List<Fieid> fieids, string TableName)
        {
            StringBuilder strBuliderContent = new StringBuilder();
            StringBuilder strBuliderSql = new StringBuilder();
            StringBuilder strBuliderSqlSet = new StringBuilder();
            StringBuilder strBuliderSqlWhere = new StringBuilder();
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据主键筛选数据")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"KeyId\">主键Id</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public ").Append(TableName).Append(" SelectById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var query = new LambdaQuery<").Append(TableName).Append(">();");
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("query.Where(p => p.").Append(fieid.Fieid_Name).Append(" == KeyId);");
                }
            }
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return query.GetQueryList(connection, transaction).FirstOrDefault();")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
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
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 筛选全部数据")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"Key\">主键</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"desc\">排序</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>对象列表</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public List<").Append(TableName).Append("> SelectAll(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var query = new LambdaQuery<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (Key != null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("query.OrderByKey(Key, desc);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return query.GetQueryList(connection, transaction);")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
            return strBuliderContent;
        }

        /// <summary>
        /// 分页查询sql
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectSqlPage(List<Fieid> fieids, string TableName)
        {
            var tableName = TableName.ToLower();
            StringBuilder strBuliderContent = new StringBuilder();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据分页筛选数据")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"Key\">主键</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"start\">开始数据</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"PageSize\">页面长度</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"desc\">排序</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>对象列表</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public List<").Append(TableName).Append("> SelectByPage(string Key, int start, int PageSize, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var query = new LambdaQuery<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (Key != null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("query.OrderByKey(Key, desc);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return query.GetQueryPageList(start, PageSize, connection, transaction);")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
            return strBuliderContent;
        }


        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private StringBuilder SelectCount(List<Fieid> fieids, string TableName)
        {
            var tableName = TableName.ToLower();
            StringBuilder strBuliderContent = new StringBuilder();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 数据条数")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"Key\">主键</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"start\">开始数据</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"PageSize\">页面长度</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("///  <param name=\"desc\">排序</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>对象列表</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public int SelectCount(string Key, bool desc = true, IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var query = new LambdaQuery<").Append(TableName).Append(">();")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (Key != null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("query.OrderByKey(Key, desc);")
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return query.GetQueryCount(connection, transaction);")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
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
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据主键删除数据")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"KeyId\">主键Id</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool DeleteById(int KeyId, IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var delete = new LambdaDelete<").Append(TableName).Append(">();");
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("delete.Where(p => p.").Append(fieid.Fieid_Name).Append(" == KeyId);");
                }
            }
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return delete.GetDeleteResult(connection, transaction);")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
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
            var tableName = TableName.ToLower();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据模型更新")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\">模型</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool Update(").Append(TableName).Append(" ").Append(tableName).Append(", IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var update = new LambdaUpdate<").Append(TableName).Append(">();");
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey == true)
                {
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("update.Where(p => p.")
                                     .Append(fieid.Fieid_Name).Append(" == ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra);
                }
                else
                {
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("update.Set(p => p.")
                                     .Append(fieid.Fieid_Name).Append(" == ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra);
                }
            }
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return update.GetUpdateResult(connection, transaction);")
                             .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap).Append(Wrap);
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
            StringBuilder strBuliderContent = new StringBuilder();
            var tableName = TableName.ToLower();
            strBuliderContent.Append(tab).Append(tab).Append("/// <summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// 根据模型更新")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <param name=\"").Append(tableName).Append("\">模型</param>")
                             .Append(Wrap).Append(tab).Append(tab).Append("/// <returns>是否成功</returns>")
                             .Append(Wrap).Append(tab).Append(tab).Append("public bool Insert(").Append(TableName).Append(" ").Append(tableName).Append(", IDbConnection connection = null, IDbTransaction transaction = null)")
                             .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                             .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("var insert = new LambdaInsert<").Append(TableName).Append(">();");
            foreach (Fieid fieid in fieids)
            {
                if (fieid.PrimaryKey != true)
                {
                    strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("if (!").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(".IsNullOrEmpty())")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(leftbra)
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(tab).Append("insert.Insert(p => p.")
                                     .Append(fieid.Fieid_Name).Append(" == ").Append(tableName).Append(".").Append(fieid.Fieid_Name).Append(");")
                                     .Append(Wrap).Append(tab).Append(tab).Append(tab).Append(rightbra);
                }
            }
            strBuliderContent.Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return insert.GetInsertResult(connection, transaction);")
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
