using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO;
using TonyBlogs.DTO.UserFunction;
using TonyBlogs.Entity;

namespace TonyBlogs.IService
{
    public interface IUserFunctionService : IBaseServices<UserFunctionEntity>
    {
        List<UserFunctionTreeItemDTO> GetAllValidFunciton();

        UserFunctionEditDTO GetFunctionEditDTO(long funcID, long parentID);

        ExecuteResult AddOrEditFunction(UserFunctionEditDTO dto);

        ExecuteResult DeleteFunction(long funcID);
    }
}
