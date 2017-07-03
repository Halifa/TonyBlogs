using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;

namespace TonyBlogs.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfoEntity>
    {
        List<UserInfoEntity> GetUserInfoList(UserInfoSearchDTO searchDTO, out long totalCount);
    }
}
