using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.Entity;

namespace TonyBlogs.IRepository
{
    public interface IBlogArticleRepository : IBaseRepository<BlogArticleEntity>
    {
        List<BlogArticleEntity> GetList(BlogArticleSearchDTO searchDTO, out long totalCount);

        List<BlogArticleViewRankItemPageDTO> GetViewRankList();

        ExecuteResult AddBlogTraffic(long blogID, string ip);
    }
}
