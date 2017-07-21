using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.BlogArticle
{
    public class BlogArticleSearchDTO : JQueryDataTableSearchDTO
    {
        public long UserID { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }
    }
}
