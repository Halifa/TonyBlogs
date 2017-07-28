using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.BlogComment;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;

namespace TonyBlogs.IService
{
    public interface IBlogCommentService : IBaseServices<BlogCommentEntity>
    {
        ExecuteResult AddComment(BlogCommentAddPageDTO dto, IUserBasicInfo userInfo);

        List<BlogCommentListItemPageDTO> GetCommentList(long blogID);
    }
}
