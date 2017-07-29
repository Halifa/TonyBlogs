using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.BlogArticle;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using ServiceStack.OrmLite;
using TonyBlogs.DTO;
using System.Data;
using TonyBlogs.Framework.Data;

namespace TonyBlogs.Repository
{
    public class BlogArticleRepository : BaseRepository<BlogArticleEntity>, IBlogArticleRepository
    {
        private IBlogTrafficLogRepository _trafficDal;

        public BlogArticleRepository(IBlogTrafficLogRepository trafficDal)
        {
            this._trafficDal = trafficDal;
        }

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

        public List<BlogArticleViewRankItemPageDTO> GetViewRankList(long userID)
        {
            var sqlExp = GetSqlExp();
            if (userID > 0)
            {
                sqlExp.Where(m=>m.UserID == userID);
            }

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

        public ExecuteResult AddBlogTraffic(long blogID, string ip)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true };

            if (_trafficDal.ExistView(blogID, ip))
            {
                return result;
            }

            var blogEntity = base.Single(m => m.ID == blogID);
            if (blogEntity == null)
            {
                return result;
            }

            BlogTrafficLogEntity trafficEntity = new BlogTrafficLogEntity()
                {
                    BlogID = blogID,
                    IP = ip,
                    InsertTime = DateTime.Now
                };
            blogEntity.Traffic += 1;

            MyTransaction transaction = OpenTransaction();
            try
            {
                long id = _trafficDal.Add(trafficEntity, true);

                base.UpdateOnly(blogEntity, m => new { m.Traffic }, m => m.ID == blogID);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }

        public ExecuteResult UpdateBlogComment(long blogID, int commentCount)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true };

            var blogEntity = base.Single(m => m.ID == blogID);
            if (blogEntity == null)
            {
                return result;
            }

            blogEntity.CommentNum += commentCount;

            base.UpdateOnly(blogEntity, m => new { m.CommentNum }, m => m.ID == blogID);

            return result;
        }
    }
}
