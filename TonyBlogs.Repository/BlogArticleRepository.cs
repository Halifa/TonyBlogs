using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using ServiceStack.OrmLite;

namespace TonyBlogs.Repository
{
    public class BlogArticleRepository : BaseRepository<BlogArticleEntity>, IBlogArticleRepository
    {
        public List<BlogArticleEntity> GetList(BlogArticleSearchDTO searchDTO, out long totalCount)
        {
            var sqlExp = GetSqlExp();

            if (searchDTO.UserID > 0)
            {
                sqlExp.Where(m => m.UserID == searchDTO.UserID);
            }

            if (!string.IsNullOrEmpty(searchDTO.Title))
            {
                sqlExp.Where(m => m.Title == searchDTO.Title);
            }

            if (!string.IsNullOrEmpty(searchDTO.Category))
            {
                sqlExp.Where(m => m.Category == searchDTO.Category);
            }

            totalCount = base.Count(sqlExp);

            sqlExp.OrderByDescending(m => m.CreateTime);
            sqlExp.Select(m => new
            {
                m.ID,
                m.UserID,
                m.RealName,
                m.Title,
                m.Category,
                m.Summary,
                m.Traffic,
                m.CommentNum,
                m.UpdateTime,
                m.CreateTime,
                m.Remark,
            });
            sqlExp.Limit(searchDTO.PageIndex - 1, searchDTO.PageSize);

            var list = base.QueryWhere(sqlExp);

            return list;
        }

        public List<BlogArticleViewRankItemPageDTO> GetViewRankList()
        {
            var sqlExp = GetSqlExp();
            sqlExp.Select(m => new
            {
                m.ID,
                m.Title,
            });
            sqlExp.OrderByDescending(m => m.Traffic);
            sqlExp.Limit(0, 10);

            var list = base.Query<BlogArticleViewRankItemPageDTO>(sqlExp);

            return list;
        }
    }
}
