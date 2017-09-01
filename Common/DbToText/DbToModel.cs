using Common.models;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Common.DbToText
{
    public class DbToModel : SingleTon<DbToModel>
    {
        StringBuilder strBuilderHead = new StringBuilder();
        string Wrap = "\n";
        string tab = "    ";
        string leftbra = "{";
        string rightbra = "}";
        string Delimiter = ";";
        private string space = ConfigurationManager.AppSettings["namespace"].ToString();
        public DbToModel()
        {
            Head();
        }
        private void Head()
        {
            strBuilderHead.Append("using System;")
                          .Append(Wrap).Append(Wrap).Append("namespace ").Append(space).Append(".Models")
                          
                          .Append(Wrap).Append(leftbra).Append(Wrap);
        }


        /// <summary>
        /// 内容文本
        /// </summary>
        /// <param name="Fieids">对应字段</param>
        /// <param name="TableName">对应表名</param>
        private StringBuilder Content(List<Fieid> Fieids, string TableName)
        {
            StringBuilder strBuilderContent = new StringBuilder();
            StringBuilder strBuilderPrimaryKey = new StringBuilder();
            strBuilderContent.Append(tab).Append("[Serializable]").Append(Wrap)
                             .Append(tab).Append("public class ").Append(TableName).Append(Wrap).Append(tab).Append(leftbra)
                             .Append(Wrap);
            foreach (Fieid fieid in Fieids)
            {
                strBuilderContent.Append(tab).Append(tab).Append("/// <summary>")
                                 .Append(Wrap).Append(tab).Append(tab).Append("///")
                                 .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>").Append(Wrap);
                strBuilderContent.Append(tab).Append(tab).Append("public ").Append(fieid.Fieid_Type);
                if (fieid.AllowDBNull.Value && fieid.Fieid_Type != "String")
                {
                    strBuilderContent.Append("?");
                }
                strBuilderContent.Append(" ").Append(fieid.Fieid_Name).Append(" { get; set; }").Append(Wrap);
                if (fieid.PrimaryKey == true)
                {
                    strBuilderPrimaryKey.Append(tab).Append(tab).Append("/// <summary>")
                                        .Append(Wrap).Append(tab).Append(tab).Append("/// 获取对应主键")
                                        .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>").Append(Wrap);
                    strBuilderPrimaryKey.Append(tab).Append(tab).Append("public string GetBuilderPrimaryKey()")
                                        .Append(Wrap).Append(tab).Append(tab).Append(leftbra)
                                        .Append(Wrap).Append(tab).Append(tab).Append(tab).Append("return ").Append("\"").Append(fieid.Fieid_Name).Append("\";")
                                        .Append(Wrap).Append(tab).Append(tab).Append(rightbra).Append(Wrap);
                }
            }
            strBuilderPrimaryKey.Append(tab).Append(tab).Append("/// <summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// 排序语句格式为 字段名,字段名,字段名...")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("public string OrderBy { get; set; }")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// 排序语句 字段名,字段名,字段名...")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("public string GroupBy { get; set; }")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// <summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// 筛选字段")
                                .Append(Wrap).Append(tab).Append(tab).Append("/// </summary>")
                                .Append(Wrap).Append(tab).Append(tab).Append("public string Field { get; set; }").Append(Wrap);
            strBuilderContent.Append(strBuilderPrimaryKey).Append(Wrap).Append(rightbra).Append(Wrap).Append(rightbra);
            return strBuilderContent;
        }

        /// <summary>
        /// 获取对应模型文本
        /// </summary>
        /// <param name="Fieids"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string GetText(List<Fieid> Fieids, string TableName)
        {
            var strBuilder = Content(Fieids, TableName);
            return strBuilderHead.ToString() + strBuilder.ToString();
        }
    }
}
