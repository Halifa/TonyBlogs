using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.Account
{
    /// <summary>
    /// 用户信息注册DTO
    /// </summary>
    public class AccountRegisterDTO
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 账号密码
        /// </summary>
        public string LoginPWD { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
    }
}
