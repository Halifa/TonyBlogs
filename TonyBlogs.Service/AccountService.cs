using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Common;
using TonyBlogs.Common.Cache;
using TonyBlogs.DTO;
using TonyBlogs.DTO.Account;
using TonyBlogs.Entity;
using TonyBlogs.IService;

namespace TonyBlogs.Service
{
    public class AccountService : IAccountService
    {
        private IUserInfoService _userService;
        private IUserPurviewService _userPurviewService;
        private ICacheManager _cache;

        public AccountService(IUserInfoService userService, 
            ICacheManager cache,
            IUserPurviewService userPurviewService)
        {
            this._userService = userService;
            this._cache = cache;
            this._userPurviewService = userPurviewService;
        }

        public AccountLoginResultDTO Login(AccountLoginDTO dto)
        {
            AccountLoginResultDTO result = new AccountLoginResultDTO();

            var loginUser = _userService.Single(m => m.LoginName == dto.UserName);
            string password = EncryptHelper.Encrypt(dto.Password);

            if (loginUser != null && loginUser.LoginPWD == password)
            {
                result.IsSuccess = true;
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "用户名或密码错误";
                return result;
            }


            string cookieValueToEncrypt = string.Format("{0}_{1}_{2}", loginUser.UserID, loginUser.LoginName, DateTime.Now);
            string encryptCookieValue = EncryptHelper.Encrypt(cookieValueToEncrypt);
            string cacheKey = string.Format("AccountService_Login_{0}", loginUser.UserID);
            _cache.Remove(cacheKey);
            _cache.Set(cacheKey, encryptCookieValue, TimeSpan.FromHours(1));

            result.CookieValue = encryptCookieValue;
            result.CookieValueCacheKey = Base64Helper.Base64Encode(cacheKey);

            return result;
        }

        public ExecuteResult RegisterBlogUser(AccountRegisterDTO dto)
        {
            ExecuteResult result = new ExecuteResult() { IsSuccess = true };

            if (this._userService.ExistUserName(dto.LoginName))
            {
                result.IsSuccess = false;
                result.Message = "该用户名已经被注册";

                return result;
            }

            var entity = Mapper.DynamicMap<UserInfoEntity>(dto);
            entity.PurviewID = _userPurviewService.GetPurviewMap().Single(m => m.Value == "博客作者").Key;
            entity.LoginPWD = EncryptHelper.Encrypt(dto.LoginPWD);
            entity.UserStatus = Enum.User.UserStatusEnum.Valid;
            entity.InsertTime = DateTime.Now;
            _userService.Add(entity, true);

            return result;
        }

        public void Logout(string cookieCacheKey)
        {
            if (string.IsNullOrEmpty(cookieCacheKey))
            {
                return;
            }

            string cacheKey = Base64Helper.Base64Decode(cookieCacheKey);
            _cache.Remove(cacheKey);
        }
    }
}
