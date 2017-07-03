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

namespace TonyBlogs.Service
{
    public class UserInfoService : BaseService<UserInfoEntity>,IUserInfoService
    {
        private IUserInfoRepository _userInfoRepository;
        private IUserPurviewService _userPurviewService;

        public UserInfoService(IUserInfoRepository userInfoRepository, 
            IUserPurviewService userPurviewService)
        {
            this._userInfoRepository = userInfoRepository;
            this.baseDal = userInfoRepository;
            this._userPurviewService = userPurviewService;
        }

        public UserInfoSearchDTO GetUserInfoSearchDTO()
        {
            UserInfoSearchDTO dto = new UserInfoSearchDTO();
            dto.PurviewMap = GetPurviewMap();

            return dto;
        }

        public UserInfoListDTO GetUserInfoList(UserInfoSearchDTO searchDTO)
        {
            throw new NotImplementedException();
        }

        public ExecuteResult AddOrEditUserInfo(UserInfoEditDTO dto)
        {
            throw new NotImplementedException();
        }

        public UserInfoEditDTO GetUserInfoEditDTO(long userID)
        {
            throw new NotImplementedException();
        }

        public ExecuteResult DeleteUserInfo(long userID)
        {
            throw new NotImplementedException();
        }

        private Dictionary<long, string> GetPurviewMap()
        {
            return _userPurviewService.GetPurviewMap();
        }
    }
}
