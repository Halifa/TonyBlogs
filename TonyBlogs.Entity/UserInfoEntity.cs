using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.DataAnnotations;
using TonyBlogs.Enum.User;

namespace TonyBlogs.Entity
{
    public class UserInfoEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public long UserID { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPWD { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public UserStatusEnum UserStatus { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public long PurviewID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime InsertTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
