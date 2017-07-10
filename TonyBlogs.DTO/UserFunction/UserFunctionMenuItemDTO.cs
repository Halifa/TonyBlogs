using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserFunction
{
    public class UserFunctionMenuItemDTO
    {
        public long ID { get; set; }

        public long ParentID { get; set; }

        public string FucTitle { get; set; }

        public int FuncLevel { get; set; }

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
    }
}
