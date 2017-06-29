using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Enum.User;

namespace TonyBlogs.DTO.UserFunction
{
    public class UserFunctionSearchDTO
    {
        public UserFuncTypeEnum? FuncType { get; set; }

        public UserFuncStatusEnum? FuncStatus { get; set; }

        public int? FuncLevel { get; set; }

    }
}
