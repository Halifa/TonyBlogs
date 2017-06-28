using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;

namespace TonyBlogs.Repository
{
    public class BlogArticleRepository : BaseRepository<BlogArticle>, IBlogArticleRepository
    {

    }
}
