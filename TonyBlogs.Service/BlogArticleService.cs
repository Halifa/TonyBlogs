using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using TonyBlogs.IService;

namespace TonyBlogs.Service
{
    public class BlogArticleService : BaseService<BlogArticleEntity>, IBlogArticleService
    {
        private IBlogArticleRepository dal;

        public BlogArticleService(IBlogArticleRepository dal)
        {
            this.dal = dal;
            base.baseDal = dal;
        }
    }
}
