using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.BlogComment
{
    public class BlogCommentAddPageDTO
    {
        /// <summary>
        /// 博客ID
        /// </summary>
        public long BlogID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论父ID
        /// </summary>
        public long ParentID { get; set; }
    }
}
