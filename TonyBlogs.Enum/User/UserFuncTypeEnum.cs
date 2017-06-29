using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TonyBlogs.Enum.User
{
    /// <summary>
    /// 功能类型
    /// </summary>
    [Description("功能类型")]
    [ServiceStack.DataAnnotations.EnumAsInt]
    public enum UserFuncTypeEnum
    {
        /// <summary>
        /// 一级菜单
        /// </summary>
        [Description("一级菜单")]
        FirstLevelMenu = 1,

        /// <summary>
        /// 二级菜单
        /// </summary>
        [Description("二级菜单")]
        SecondLevelMenu = 2,

        /// <summary>
        /// Tab页
        /// </summary>
        [Description("Tab页")]
        TabPage = 3,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 4,
    }
}
