using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.Account;
using TonyBlogs.Framework;

namespace TonyBlogs.IService
{
    public interface IAccountService : IDependency
    {
        AccountLoginResultDTO Login(AccountLoginDTO dto);

        ExecuteResult RegisterBlogUser(AccountRegisterDTO dto);

        void Logout(string cookieCacheKey);
    }
}
