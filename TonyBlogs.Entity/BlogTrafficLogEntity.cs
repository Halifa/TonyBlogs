using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.DataAnnotations;

namespace TonyBlogs.Entity
{
    public class BlogTrafficLogEntity
    {
        /// <summary>
        /// 博客ID
        /// </summary>
        [PrimaryKey]
        public long BlogID { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        [PrimaryKey]
        public string IP { get; set; }

        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime InsertTime { get; set; }
    }
}
