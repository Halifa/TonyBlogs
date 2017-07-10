using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserFunction
{
    public class UserFunctionMenuTreeDTO : UserFunctionMenuItemDTO
    {
        public UserFunctionMenuTreeDTO() 
        {
            this.ChildList = new List<UserFunctionMenuTreeDTO>();
        }

        public List<UserFunctionMenuTreeDTO> ChildList { get; set; }

    }
}
