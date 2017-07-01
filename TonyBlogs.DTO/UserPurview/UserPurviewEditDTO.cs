using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserFunction;

namespace TonyBlogs.DTO.UserPurview
{
    public class UserPurviewEditDTO
    {
        public UserPurviewEditDTO()
        {
            FuncList = new List<UserFunctionTreeItemDTO>();
        }

        public long PurviewID { get; set; }

        /// <summary>
        /// 权限标题
        /// </summary>
        public string PurviewTitle { get; set; }

        /// <summary>
        /// 权限内容
        /// </summary>
        public string PurviewContent { get; set; }

        /// <summary>
        /// 权限功能组
        /// </summary>
        public string PurviewFuncIDs { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<UserFunctionTreeItemDTO> FuncList { get; set; }
    }
}
