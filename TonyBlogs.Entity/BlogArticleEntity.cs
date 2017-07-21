using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.Entity
{
    public class BlogArticleEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public long ID { get; set; }

        /// <summary>
        /// 创建人用户编号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Traffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
