using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using ServiceStack.OrmLite;

namespace TonyBlogs.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfoEntity>, IUserInfoRepository
    {
        public List<UserInfoEntity> GetUserInfoList(UserInfoSearchDTO searchDTO, out long totalCount)
        {
            var sqlExp = db.From<UserInfoEntity>();

            if (searchDTO.UserID.HasValue)
            {
                sqlExp.Where(m => m.UserID == searchDTO.UserID);
            }

            if (!string.IsNullOrEmpty(searchDTO.LoginName))
            {
                sqlExp.Where(m => m.LoginName == searchDTO.LoginName);
            }

            if (!string.IsNullOrEmpty(searchDTO.RealName))
            {
                sqlExp.Where(m => m.RealName == searchDTO.RealName);
            }

            if (searchDTO.PurviewID.HasValue)
            {
                sqlExp.Where(m => m.PurviewID == searchDTO.PurviewID);
            }

            totalCount = base.Count(sqlExp);

            sqlExp.Limit(searchDTO.PageIndex - 1, searchDTO.iDisplayLength);

            var list = base.QueryWhere(sqlExp);

            return list;
        }
    }
}
