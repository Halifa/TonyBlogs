using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IService;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.IRepository;
using AutoMapper;
using TonyBlogs.DTO;
using TonyBlogs.Common;
using TonyBlogs.Enum.User;

namespace TonyBlogs.Service
{
    public class UserInfoService : BaseService<UserInfoEntity>,IUserInfoService
    {
        private IUserInfoRepository _userInfoRepository;
        private IUserPurviewService _userPurviewService;
        private IUserFunctionService _userFunctionService;

        public UserInfoService(IUserInfoRepository userInfoRepository,
            IUserPurviewService userPurviewService, IUserFunctionService userFunctionService)
        {
            this._userInfoRepository = userInfoRepository;
            this.baseDal = userInfoRepository;
            this._userPurviewService = userPurviewService;
            this._userFunctionService = userFunctionService;
        }

        public UserInfoSearchDTO GetUserInfoSearchDTO()
        {
            UserInfoSearchDTO dto = new UserInfoSearchDTO();
            dto.PurviewMap = GetPurviewMap();

            return dto;
        }

        public UserInfoListDTO GetUserInfoList(UserInfoSearchDTO searchDTO)
        {
            UserInfoListDTO result = new UserInfoListDTO();
            searchDTO.UserStatus = UserStatusEnum.Valid;

            long totalCount = 0;
            var entityList = this._userInfoRepository.GetUserInfoList(searchDTO, out totalCount);

            result.TotalRecords = totalCount;
            var purviewMap = GetPurviewMap();
            result.List = entityList.Select(m => CreateUserInfoListItemDTO(m, purviewMap)).ToList();

            return result;
        }

        private UserInfoListItemDTO CreateUserInfoListItemDTO(UserInfoEntity entity, Dictionary<long, string> purviewMap)
        {
            var dto = Mapper.DynamicMap<UserInfoListItemDTO>(entity);
            if (purviewMap.ContainsKey(dto.PurviewID))
	        {
                dto.PurviewTitle = purviewMap[dto.PurviewID];
	        }

            return dto;
        }

        public UserObj GetUserObj(long userID, bool isFromCache = true)
        {
            if (isFromCache == false)
            {
                return InternalGetUserObj(userID);
            }

            UserCacheBiz userCacheBiz = new UserCacheBiz(userID);
            var userObj = userCacheBiz.GetUserCache();
            if (userObj == null)
            {
                userObj = InternalGetUserObj(userID);
                userCacheBiz.SetUserCache(userObj);
            }

            return userObj;
        }

        private UserObj InternalGetUserObj(long userID)
        {
            var userEntity = baseDal.Single(m => m.UserID == userID && m.UserStatus == Enum.User.UserStatusEnum.Valid);
            if (userEntity == null)
            {
                return null;
            }

            var purviewEntity = _userPurviewService.Single(m => m.PurviewID == userEntity.PurviewID);
            if (purviewEntity == null)
            {
                return null;
            }

            UserObj userObj = Mapper.DynamicMap<UserObj>(userEntity);
            userObj.PurviewTitle = purviewEntity.PurviewTitle;
            userObj.PurviewFuncIDs = purviewEntity.PurviewFuncIDs;
            userObj.UserMenuList = _userFunctionService.GetUserFunctionMenuList(purviewEntity.PurviewFuncIDs);

            return userObj;
        }

        public ExecuteResult AddOrEditUserInfo(UserInfoEditDTO dto)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true };

            var entity = Mapper.DynamicMap<UserInfoEntity>(dto);
            

            bool isAdd = dto.UserID == 0;
            if (isAdd)
            {
                if (this.ExistUserName(dto.LoginName))
                {
                    result.IsSuccess = false;
                    result.Message = "该用户名已经被注册";

                    return result;
                }

                entity.LoginPWD = EncryptHelper.Encrypt(dto.LoginPWD);
                entity.UserStatus = Enum.User.UserStatusEnum.Valid;
                entity.InsertTime = DateTime.Now;
                dto.UserID = baseDal.Add(entity,true);
            }
            else
            {
                entity.UpdateTime = DateTime.Now;

                baseDal.UpdateOnly(entity,
                    m => new {
                        m.RealName,
                        m.PurviewID,
                        m.UpdateTime},
                    m => m.UserID == dto.UserID);
            }

            new UserCacheBiz(dto.UserID).RemoveUserCache();

            return result;
        }
            
        public UserInfoEditDTO GetUserInfoEditDTO(long userID)
        {
            UserInfoEditDTO dto = new UserInfoEditDTO();

            if (userID <= 0)
            {
                dto.PurviewMap = GetPurviewMap();
                return dto;
            }

            var entity = baseDal.Single(m => m.UserID == userID);
            if (entity == null)
            {
                return dto;
            }

            dto = Mapper.DynamicMap<UserInfoEditDTO>(entity);
            dto.LoginPWD = string.Empty;
            dto.PurviewMap = GetPurviewMap();

            return dto;
        }

        public ExecuteResult DeleteUserInfo(long userID)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true };

            var entity = baseDal.Single(m => m.UserID == userID);

            if (entity == null)
            {
                result.IsSuccess = false;
                result.Message = "当前功能实体不存在";
            }

            entity.UserStatus = Enum.User.UserStatusEnum.Deleted;
            entity.UpdateTime = DateTime.Now;

            baseDal.UpdateOnly(entity, m => new { m.UserStatus, m.UpdateTime }, m => m.UserID == userID);
            new UserCacheBiz(userID).RemoveUserCache();

            return result;
        }

        private Dictionary<long, string> GetPurviewMap()
        {
            return _userPurviewService.GetPurviewMap();
        }

        public bool ExistUserName(string userName)
        {
            return this._userInfoRepository.Exist(m => m.LoginName == userName && m.UserStatus == UserStatusEnum.Valid);
        }
    }
}

public class usertest {
    public virtual void test() { }
}


public class usertest1 : usertest {
    public override void test()
    {
        base.test();
    }
}

public class usertest2 : usertest1 {
    public override void test()
    {
        int[] a = new int[] { 1,123,12,11};
        int temp = 0;
        for (int i = 0; i < a.Length-1; i++)
        {
            for (int j = i+1; j < a.Length; j++)
            {
                if (a[j]<a[i])
                {
                    temp = a[j];
                    a[j] = a[i];
                    a[i] = temp;
                }
            }
        }
    }

    public int getnum(int n)
    {

        if (n== 1 || n == 2)
        {
            return 1;
        }
        else
	    {
            return getnum(n - 1) * getnum(n - 2);
	    }
    }
}