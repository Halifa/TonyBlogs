using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.BlogComment;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;
using TonyBlogs.Framework.Data;
using TonyBlogs.IRepository;
using TonyBlogs.IService;

namespace TonyBlogs.Service
{
    public class BlogCommentService : BaseService<BlogCommentEntity>, IBlogCommentService
    {
        private IBlogCommentRepository _commentDal;
        private IBlogArticleRepository _blogDal;

        public BlogCommentService(IBlogCommentRepository commentDal,
            IBlogArticleRepository blogDal)
        {
            this._commentDal = commentDal;
            this.baseDal = commentDal;
            this._blogDal = blogDal;
        }

        public ExecuteResult AddComment(BlogCommentAddPageDTO dto, IUserBasicInfo userInfo)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true};

            BlogCommentEntity commentEntity = Mapper.DynamicMap<BlogCommentEntity>(dto);
            commentEntity.InsertTime = DateTime.Now;
            commentEntity.UserID = userInfo.UserID;
            commentEntity.RealName = userInfo.RealName;

            MyTransaction transaction = this._commentDal.OpenTransaction();
            try
            {
                this._commentDal.Add(commentEntity);

                this._blogDal.UpdateBlogComment(dto.BlogID, 1);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return result;
        }


        public List<BlogCommentListItemPageDTO> GetCommentList(long blogID)
        {
            List<BlogCommentEntity> entityList = this._commentDal.QueryWhere(m => m.ID == blogID);

            var rootEntityList = entityList.Where(m=>m.ParentID == 0);

            foreach (var firstLevel in rootEntityList)
            {
                var firDTO = Mapper.DynamicMap<BlogCommentListItemPageDTO>(firstLevel);
                firDTO.ChildrenList = entityList
                    .Where(m => m.ParentID == firDTO.ID)
                    .Select(m => Mapper.DynamicMap<BlogCommentListItemPageDTO>(m))
                    .ToList();
            }

            return List;
        }
    }
}
