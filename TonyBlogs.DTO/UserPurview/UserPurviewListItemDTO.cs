using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserPurview
{
    public class UserPurviewListItemDTO
    {
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
        /// 录入时间
        /// </summary>
        public DateTime InsertTime { get; set; }
    }
}
