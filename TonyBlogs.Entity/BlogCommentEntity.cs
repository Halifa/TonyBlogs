using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.Entity
{
    public class BlogCommentEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public long ID { get; set; }

        /// <summary>
        /// 博客ID
        /// </summary>
        public long BlogID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime InsertTime { get; set; }
    }
}
