using Common.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBOpertion
    {
        private string connString = ConfigurationManager.AppSettings["ConnString"].ToString();
        private List<string> tableNames = new List<string>();
        private string dbName = null;
        private Dictionary<string, List<Fieid>> FieidDic = new Dictionary<string, List<Fieid>>();

        public DBOpertion()
        {
            GetTables();
            foreach (string tableName in tableNames)
            {
                GetFieId(tableName);
            }
        }

        /// <summary>
        /// 从数据库中获取表名
        /// </summary>
        private void GetTables()
        {
            DataTable table;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                table = conn.GetSchema(SqlClientMetaDataCollectionNames.Tables);
                conn.Close();
            }
            foreach (DataRow dr in table.Rows)
            {
                tableNames.Add(dr[2].ToString());
                dbName = dr[0].ToString();
            }
            tableNames = tableNames.OrderBy(p => p.ToString()).ToList();
        }

        /// <summary>
        /// 根据表名获取字段
        /// </summary>
        private void GetFieId(string tableName)
        {
            DataTable table = new DataTable();
            List<Fieid> fieids = new List<Fieid>();
            List<string> keyName = new List<string>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlDataAdapter adpter = new SqlDataAdapter("Select * from [" + tableName + "] where 1!=1", conn);
                //添加key
                adpter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adpter.Fill(table);
                conn.Close();
            }
            foreach (DataColumn dc in table.PrimaryKey)
            {
                keyName.Add(dc.ColumnName);
            }
            foreach (DataColumn dc in table.Columns)
            {
                Fieid fieid = new Fieid();
                fieid.Fieid_Length = dc.MaxLength;
                fieid.Fieid_Name = dc.ColumnName;
                fieid.Fieid_Type = dc.DataType.Name;
                fieid.AllowDBNull = dc.AllowDBNull;
                if (keyName.Where(p => p == dc.ColumnName).FirstOrDefault() != null)
                {
                    fieid.PrimaryKey = true;
                }
                else
                {
                    fieid.PrimaryKey = false;
                }
                fieids.Add(fieid);
            }
            FieidDic.Add(tableName, fieids);
        }

        /// <summary>
        /// 获取表中字段信息
        /// </summary>
        /// <returns></returns>
        public List<Fieid> GetFieIdInfo(string tableName)
        {
            List<Fieid> fieids = FieidDic.Where(p => p.Key == tableName).FirstOrDefault().Value;
            return fieids;
        }

        /// <summary>
        /// 获取表中字段信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableName()
        {
            return tableNames;
        }

        /// <summary>
        /// 获取库名
        /// </summary>
        /// <returns></returns>
        public string GetDbName()
        {
            return dbName;
        }
    }
}
