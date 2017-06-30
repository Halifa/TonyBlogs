using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.DataAnnotations;

namespace TonyBlogs.Entity
{
    public class PurviewEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public long PurviewID { get; set; }

        /// <summary>
        /// 权限标题
        /// </summary>
        public string PurviewTitle { get; set; }

        /// <summary>
        /// 权限内容
        /// </summary>
        public string PurviewContent { get; set; }

        /// <summary>
        /// 权限功能组
        /// </summary>
        public string PurviewFuncIDs { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InsertTime { get; set; }
    }
}
