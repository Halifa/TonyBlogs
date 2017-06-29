using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserFunction
{
    public class UserFunctionTreeItemDTO
    {
        public UserFunctionTreeItemDTO() 
        {
            this.ChildList = new List<UserFunctionTreeItemDTO>();
        }

        public long ID { get; set; }

        public long ParentID { get; set; }

        public string FucTitle { get; set; }

        public int FuncLevel { get; set; }

        public List<UserFunctionTreeItemDTO> ChildList { get; set; }
    }
}
