using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.Entity;

namespace TonyBlogs.IRepository
{
    public interface IUserFunctionRepository : IBaseRepository<UserFunctionEntity>
    {
        List<UserFunctionEntity> GetFunctionList(UserFunctionSearchDTO searchDTO);

        List<UserFunctionEntity> GetAllValidFunctions();

        int GetMaxSortNum(long parentID);
    }
}
