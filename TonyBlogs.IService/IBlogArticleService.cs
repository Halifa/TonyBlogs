using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;

namespace TonyBlogs.IService
{
    public interface IBlogArticleService : IBaseServices<BlogArticleEntity>
    {
        BlogArticleEditDTO GetBlogArticleEditDTO(long blogID);

        BlogArticleEditResultDTO AddOrEditBlogArticle(BlogArticleEditDTO dto, IUserBasicInfo userInfo);
    }
}
