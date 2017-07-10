using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;
using ServiceStack.OrmLite;
using TonyBlogs.Enum.User;

namespace TonyBlogs.Repository
{
    public class UserFunctionRepository : BaseRepository<UserFunctionEntity>, IUserFunctionRepository
    {

        public List<UserFunctionEntity> GetFunctionList(UserFunctionSearchDTO searchDTO)
        {
            var sqlExp = db.From<UserFunctionEntity>();

            sqlExp.Where(m => m.ID > 0);

            if (searchDTO.FuncLevel.HasValue)
            {
                sqlExp.Where(m => m.FuncLevel == searchDTO.FuncLevel.Value);
            }

            if (searchDTO.FuncStatus.HasValue)
            {
                sqlExp.Where(m => m.FuncStatus == searchDTO.FuncStatus.Value);
            }

            if (searchDTO.FuncType.HasValue)
            {
                sqlExp.Where(m => m.FuncType == searchDTO.FuncType.Value);
            }

            var list = base.QueryWhere(sqlExp);

            return list;
        }

        public List<UserFunctionEntity> GetAllValidFunctions()
        {
            return GetFunctionList(new UserFunctionSearchDTO() { FuncStatus = UserFuncStatusEnum.Valid});
        }

        public int GetMaxSortNum(long parentID)
        {
            var sqlExp = db.From<UserFunctionEntity>();

            sqlExp.Select(m => Sql.Max<int>(m.SortNum)).Where(q => q.ParentID == parentID);

            return base.Scala<int>(sqlExp);
        }
    }
}
