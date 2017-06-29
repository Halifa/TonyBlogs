using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Enum.User;

namespace TonyBlogs.Entity
{
    /// <summary>
    /// 用户功能表
    /// </summary>
    public class UserFunctionEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public long ID { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 功能数中的层级
        /// </summary>
        public int FuncLevel { get; set; }

        /// <summary>
        /// 父功能ID
        /// </summary>
        public long ParentID { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string FucTitle { get; set; }

        /// <summary>
        /// 功能内容
        /// </summary>
        public string FuncContent { get; set; }

        /// <summary>
        /// 区域名
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 控制器名
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 功能类型，1，一级菜单 2，二级菜单 ，3-Tab页，4-按钮
        /// </summary>
        public UserFuncTypeEnum FuncType { get; set; }

        /// <summary>
        /// 状态 1-正常 9-删除
        /// </summary>
        public UserFuncStatusEnum FuncStatus { get; set; }
    }
}
