using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserFunction;

namespace TonyBlogs.DTO.UserInfo
{
    public class UserObj
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public long PurviewID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PurviewTitle { get; set; }

        /// <summary>
        /// 权限功能组
        /// </summary>
        public string PurviewFuncIDs { get; set; }

        public List<UserFunctionMenuTreeDTO> UserMenuList { get; set; }
    }
}
