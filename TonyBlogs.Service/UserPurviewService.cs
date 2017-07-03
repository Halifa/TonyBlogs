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

            IEnumerable<PurviewEntity> list = GetAllFromCache();

            if (searchDTO.PurviewID.HasValue)
            {
                list = list.Where(m => m.PurviewID == searchDTO.PurviewID);
            }

            if (!string.IsNullOrEmpty(searchDTO.PurviewTitle))
            {
                list = list.Where(m => m.PurviewTitle == searchDTO.PurviewTitle);
            }

            var pagedList = list.Skip((searchDTO.PageIndex - 1) * searchDTO.iDisplayLength).Take(searchDTO.iDisplayLength);

            result.TotalRecords = list.Count();
            result.List = pagedList.Select(m => Mapper.DynamicMap<UserPurviewListItemDTO>(m)).ToList();

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

            RemoveAllCache();

            return result;
        }

        public UserPurviewEditDTO GetPurviewEditDTO(long purviewID)
        {
            UserPurviewEditDTO dto = new UserPurviewEditDTO();

            if (purviewID <= 0)
            {
                dto.FuncList = _funcService.GetAllValidFunciton();
                return dto;
            }

            var entity = GetAllFromCache().FirstOrDefault(m => m.PurviewID == purviewID);
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
            RemoveAllCache();

            return new ExecuteResult() { IsSuccess = true };
        }

        public Dictionary<long, string> GetPurviewMap()
        {
            var purviewList = GetAllFromCache();

            return purviewList.ToDictionary<PurviewEntity,long, string>(m => m.PurviewID, m => m.PurviewTitle);
        }

        private List<PurviewEntity> GetAllFromCache()
        {
            return Cache.GetOrAdd<List<PurviewEntity>>(
                AllEnityCacheKey, 120,
                () => {
                    return baseDal.QueryWhere(m => m.PurviewID > 0);
                });
        }

        private void RemoveAllCache()
        {
            Cache.Remove(AllEnityCacheKey);
        }
    }
}
