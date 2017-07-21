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
                blogID = baseDal.Add(entity, true);
            }
            else
            {
                entity.UpdateTime = DateTime.Now;
                baseDal.UpdateOnly(entity,
                    m => new {
                        m.Title,
                        m.Category,
                        m.Content,
                        m.UpdateTime,
                        m.Remark,
                    },
                    m => m.ID == blogID);
            }

            resultDTO.BlogID = blogID;

            return resultDTO;
        }
    }
}
