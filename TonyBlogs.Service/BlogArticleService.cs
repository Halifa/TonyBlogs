using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using TonyBlogs.IService;
using TonyBlogs.DTO.BlogArticle;
using AutoMapper;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.DTO;
using TonyBlogs.Common.Html;

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

        public BlogArticleEditDTO GetBlogArticleEditDTO(long blogID)
        {
            BlogArticleEditDTO dto = new BlogArticleEditDTO();
            if (blogID <= 0)
            {
                return dto;
            }

            var entity = baseDal.Single(m => m.ID == blogID);
            if (entity == null)
            {
                return dto;
            }

            dto = Mapper.DynamicMap<BlogArticleEditDTO>(entity);
            return dto;

        }

        public BlogArticleEditResultDTO AddOrEditBlogArticle(BlogArticleEditDTO dto, IUserBasicInfo userInfo)
        {
            BlogArticleEditResultDTO resultDTO = new BlogArticleEditResultDTO() { IsSuccess = true};
            long blogID = dto.ID;

            var entity = Mapper.DynamicMap<BlogArticleEntity>(dto);
            bool isAdd = blogID == 0;

            if (isAdd)
            {
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.UserID = userInfo.UserID;
                entity.RealName = userInfo.RealName;
                SetBlogSummary(entity);
                blogID = baseDal.Add(entity, true);
            }
            else
            {
                entity.UpdateTime = DateTime.Now;
                SetBlogSummary(entity);
                baseDal.UpdateOnly(entity,
                    m => new {
                        m.Title,
                        m.Category,
                        m.Content,
                        m.Summary,
                        m.UpdateTime,
                        m.Remark,
                    },
                    m => m.ID == blogID);
            }

            resultDTO.BlogID = blogID;

            return resultDTO;
        }

        private void SetBlogSummary(BlogArticleEntity entity)
        {
            string summary = HtmlTools.ReplaceHtmlTag(entity.Content);

            if (summary.Length > 200)
            {
                summary = summary.Substring(0, 200);
                summary += "...";
            }

            entity.Summary = summary;
        }

        public BlogArticleListDTO GetList(JQueryDataTableSearchDTO searchDTO, IUserBasicInfo userInfo)
        {
            BlogArticleSearchDTO blogSearchDTO = new BlogArticleSearchDTO() 
            {
                PageIndex = searchDTO.PageIndex,
                PageSize = searchDTO.iDisplayLength,
                UserID = userInfo.UserID
            };

            BlogArticleListDTO result = new BlogArticleListDTO();

            long totalCount = 0;
            var entityList = this.dal.GetList(blogSearchDTO, out totalCount);

            result.TotalRecords = totalCount;
            result.List = entityList.Select(m => Mapper.DynamicMap<BlogArticleListItemDTO>(m)).ToList();

            return result;
        }

        public BlogArticleListPageDTO GetListPage(BlogArticleSearchDTO searchDTO)
        {
            long totalCount = 0;
            var entityList = this.dal.GetList(searchDTO, out totalCount);

            BlogArticleListPageDTO result = new BlogArticleListPageDTO();

            result.TotalRecords = totalCount;
            result.PageSize = searchDTO.PageSize;
            result.List = entityList.Select(m => Mapper.DynamicMap<BlogArticleListItemPageDTO>(m)).ToList();

            return result;
        }

        public ExecuteResult Delete(long blogID)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true};

            dal.Delete(m => m.ID == blogID);

            return result;
        }
    }
}
