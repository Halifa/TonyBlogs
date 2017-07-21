using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.BlogArticle
{
    public class BlogArticleListItemDTO
    {
        public long ID { get; set; }

        /// <summary>
        /// 创建人用户编号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
