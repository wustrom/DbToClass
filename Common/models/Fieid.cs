using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.models
{
    /// <summary>
    /// 字段
    /// </summary>
    public class Fieid
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Fieid_Name { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Fieid_Length { get; set; }

        /// <summary>
        /// 是否允许数据库为空
        /// </summary>
        public bool AllowDBNull { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string Fieid_Type { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool PrimaryKey { get; set; }
    }
}
