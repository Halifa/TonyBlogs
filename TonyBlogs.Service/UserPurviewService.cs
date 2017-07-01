using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IService;
using TonyBlogs.DTO.UserPurview;
using TonyBlogs.IRepository;
using AutoMapper;
using TonyBlogs.DTO;

namespace TonyBlogs.Service
{
    public class UserPurviewService : BaseService<PurviewEntity>, IUserPurviewService
    {
        private IUserPurviewRepository _userPurviewDal;
        private IUserFunctionService _funcService;

        public UserPurviewService(IUserPurviewRepository userPurviewDal,
            IUserFunctionService funcService)
        {
            this._userPurviewDal = userPurviewDal;
            this.baseDal = userPurviewDal;
            this._funcService = funcService;
        }

        public UserPurviewListDTO GetPurviewList(UserPurviewSearchDTO searchDTO)
        {
            UserPurviewListDTO result = new UserPurviewListDTO();

            long totalCount = 0;
            var entityList = this._userPurviewDal.GetPurviewList(searchDTO, out totalCount);

            result.TotalRecords = totalCount;
            result.List = entityList.Select(m => Mapper.DynamicMap<UserPurviewListItemDTO>(m)).ToList();

            return result;
        }

        public ExecuteResult AddOrEditPurview(UserPurviewEditDTO dto)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true};

            PurviewEntity entity = Mapper.DynamicMap<PurviewEntity>(dto);

            bool isAdd = dto.PurviewID == 0;
            if (isAdd)
            {
                entity.InsertTime = DateTime.Now;
                baseDal.Add(entity);
            }
            else
            {
                baseDal.UpdateOnly(entity,
                    m => new { m.PurviewContent, m.PurviewFuncIDs, m.PurviewTitle },
                    m => m.PurviewID == dto.PurviewID);
            }

            return result;
        }

        public UserPurviewEditDTO GetPurviewEditDTO(long purviewID)
        {
            UserPurviewEditDTO dto = new UserPurviewEditDTO();

            if (purviewID <= 0)
            {
                return dto;
            }

            var entity = baseDal.Single(m => m.PurviewID == purviewID);
            if (entity == null)
            {
                return dto;
            }

            dto = Mapper.DynamicMap<UserPurviewEditDTO>(entity);
            dto.FuncList = _funcService.GetAllValidFunciton();

            return dto;
        }

        public ExecuteResult DeletePurview(long purviewID)
        {
            baseDal.Delete(m => m.PurviewID == purviewID);

            return new ExecuteResult() { IsSuccess = true };
        }
    }
}
