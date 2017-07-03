using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.Enum.User
{
    /// <summary>
    /// 用户状态
    /// </summary>
    [EnumAsInt]
    public enum UserStatusEnum
    {
        /// <summary>
        /// 有效
        /// </summary>
        Valid = 1,

        /// <summary>
        /// 删除
        /// </summary>
        Deleted = 9,
    }
}
