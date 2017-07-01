using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserPurview;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using ServiceStack.OrmLite;

namespace TonyBlogs.Repository
{
    public class UserPurviewRepository : BaseRepository<PurviewEntity>, IUserPurviewRepository
    {
        public List<PurviewEntity> GetPurviewList(UserPurviewSearchDTO searchDTO, out long totalCount)
        {
            var sqlExp = db.From<PurviewEntity>();

            if (searchDTO.PurviewID > 0)
            {
                sqlExp.Where(m=>m.PurviewID == searchDTO.PurviewID);
            }

            if (string.IsNullOrEmpty(searchDTO.PurviewTitle))
            {
                sqlExp.Where(m => m.PurviewTitle == searchDTO.PurviewTitle);
            }

            totalCount = base.Count(sqlExp);

            sqlExp.Limit(searchDTO.PageIndex - 1, searchDTO.iDisplayLength);

            var list = base.QueryWhere(sqlExp);

            return list;
        }
    }
}
