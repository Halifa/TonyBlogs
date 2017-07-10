using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;

namespace TonyBlogs.IService
{
    public interface IUserInfoService : IBaseServices<UserInfoEntity>
    {
        UserInfoSearchDTO GetUserInfoSearchDTO();

        UserInfoListDTO GetUserInfoList(UserInfoSearchDTO searchDTO);

        UserObj GetUserObj(long userID, bool isFromCache = true);

        ExecuteResult AddOrEditUserInfo(UserInfoEditDTO dto);

        UserInfoEditDTO GetUserInfoEditDTO(long userID);

        ExecuteResult DeleteUserInfo(long userID);
    }
}
