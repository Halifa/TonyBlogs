using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using TonyBlogs.IService;

namespace TonyBlogs.Service
{
    public class BlogCommentService : BaseService<BlogCommentEntity>, IBlogCommentService
    {
        private IBlogCommentRepository _commentDal;

        public BlogCommentService(IBlogCommentRepository commentDal)
        {
            this._commentDal = commentDal;
            this.baseDal = commentDal;
        }
    }
}
