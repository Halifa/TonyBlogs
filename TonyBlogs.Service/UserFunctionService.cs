using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IService;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.IRepository;
using AutoMapper;
using TonyBlogs.DTO;
using TonyBlogs.Common.Cache;

namespace TonyBlogs.Service
{
    public class UserFunctionService : BaseService<UserFunctionEntity>, IUserFunctionService
    {
        private IUserFunctionRepository _userFunDal;

        public UserFunctionService(IUserFunctionRepository userFunDal)
        {
            this._userFunDal = userFunDal;
            base.baseDal = userFunDal;
        }

        public List<UserFunctionTreeItemDTO> GetAllValidFunciton()
        {
            var entityList = GetAllFromCache();

            var rootEntityList = entityList.Where(m=>m.ParentID == 0);

            List<UserFunctionTreeItemDTO> list = new List<UserFunctionTreeItemDTO>();

            foreach (var rootEntity in rootEntityList)
            {
                UserFunctionTreeItemDTO rootItemDTO = new UserFunctionTreeItemDTO();

                rootItemDTO = Mapper.DynamicMap<UserFunctionTreeItemDTO>(rootEntity);

                CreateChildNode(rootItemDTO, entityList);

                list.Add(rootItemDTO);
            }

            return list;
        }

        public List<UserFunctionMenuItemDTO> GetAllFuncitonMenuList()
        {
            var entityList = GetAllFromCache();

            return entityList.Select(m => Mapper.DynamicMap<UserFunctionMenuItemDTO>(m)).ToList();
        }

        public List<UserFunctionMenuTreeDTO> GetUserFunctionMenuList(string funcIDs) 
        {
            var entityList = GetAllFromCache();
            var userFuncEntityList = entityList.Where(m => funcIDs.Contains(m.ID.ToString()));
            var rootEntityList = userFuncEntityList.Where(m => m.ParentID == 0);

            List<UserFunctionMenuTreeDTO> list = new List<UserFunctionMenuTreeDTO>();

            foreach (var firItem in rootEntityList)
            {
                UserFunctionMenuTreeDTO firItemDTO = new UserFunctionMenuTreeDTO();

                firItemDTO = Mapper.DynamicMap<UserFunctionMenuTreeDTO>(firItem);

                List<UserFunctionMenuTreeDTO> childList = new List<UserFunctionMenuTreeDTO>();
                foreach (var secItem in userFuncEntityList.Where(m=>m.ParentID == firItem.ID))
                {
                    UserFunctionMenuTreeDTO secItemDTO = new UserFunctionMenuTreeDTO();

                    secItemDTO = Mapper.DynamicMap<UserFunctionMenuTreeDTO>(secItem);
                    childList.Add(secItemDTO);
                }

                firItemDTO.ChildList = childList;

                list.Add(firItemDTO);
            }

            return list;
        }

        private void CreateChildNode(UserFunctionTreeItemDTO parentItemDTO, IEnumerable<UserFunctionEntity> funcEntityList)
        {
            var childEntityList = funcEntityList.Where(m => m.ParentID == parentItemDTO.ID);

            foreach (var item in childEntityList)
            {
                UserFunctionTreeItemDTO funcItem = new UserFunctionTreeItemDTO();
                funcItem = Mapper.DynamicMap<UserFunctionTreeItemDTO>(item);

                CreateChildNode(funcItem, funcEntityList);

                parentItemDTO.ChildList.Add(funcItem);
            }
        }

        public UserFunctionEditDTO GetFunctionEditDTO(long funcID, long parentID)
        {
            UserFunctionEditDTO dto = new UserFunctionEditDTO();

            var allFuncs = GetAllFromCache();

            var funcIDList = new List<long>(){funcID, parentID};
            var list = allFuncs.Where(m => funcIDList.Contains(m.ID));

            var parentFuncEntity = list.FirstOrDefault(m => m.ID == parentID);
            var funcEntity = list.FirstOrDefault(m => m.ID == funcID);

            if (funcEntity != null)
            {
                dto = Mapper.DynamicMap<UserFunctionEditDTO>(funcEntity);
                dto.CanDeleteFunc = true;
            }

            if (parentFuncEntity != null)
            {
                dto.ParentID = parentFuncEntity.ID;
                dto.ParentFuncTitle = parentFuncEntity.FucTitle;
            }
            else
            {
                dto.ParentID = 0;
                dto.ParentFuncTitle = "根节点";
            }
            
            return dto;
        }

        public ExecuteResult AddOrEditFunction(UserFunctionEditDTO dto)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true};

            long funcID = dto.ID;

            UserFunctionEntity funEntity = CreateFuncEntity(dto);

            if (funcID > 0)
            {
                baseDal.Update(funEntity, m => m.ID == funcID);
            }
            else
            {
                baseDal.Add(funEntity);
            }

            RemoveAllCache();

            return result;

        }

        private UserFunctionEntity CreateFuncEntity(UserFunctionEditDTO dto)
        {
            int sortNum = _userFunDal.GetMaxSortNum(dto.ParentID) +1;

            int funcLevel = 1;
            if (dto.ParentID > 0)
            {
                var funcEntity = baseDal.Single(m => m.ID == dto.ParentID);
                funcLevel += funcEntity.FuncLevel;
            }

            UserFunctionEntity entity = new UserFunctionEntity();
            entity = Mapper.DynamicMap<UserFunctionEntity>(dto);

            entity.SortNum = sortNum;
            entity.FuncLevel = funcLevel;
            entity.FuncStatus = Enum.User.UserFuncStatusEnum.Valid;
            entity.AreaName = entity.AreaName ?? string.Empty;
            entity.ControllerName = entity.ControllerName ?? string.Empty;
            entity.ActionName = entity.ActionName ?? string.Empty;

            return entity;
        }

        public ExecuteResult DeleteFunction(long funcID)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true};

            var entity = baseDal.Single(m => m.ID == funcID);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "当前功能实体不存在";
            }

            entity.FuncStatus = Enum.User.UserFuncStatusEnum.Deleted;

            baseDal.UpdateOnly(entity, m => m.FuncStatus, m => m.ID == funcID);

            RemoveAllCache();

            return result;
        }

        private List<UserFunctionEntity> GetAllFromCache()
        {
            return Cache.GetOrAdd<List<UserFunctionEntity>>(
                AllEnityCacheKey, 120, _userFunDal.GetAllValidFunctions);
        }

        private void RemoveAllCache()
        {
            Cache.Remove(AllEnityCacheKey);
        }
    }
}
