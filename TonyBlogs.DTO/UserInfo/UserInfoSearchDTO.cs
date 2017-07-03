using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserInfo
{
    public class UserInfoSearchDTO : JQueryDataTableSearchDTO
    {
        public UserInfoSearchDTO() 
        {
            this.PurviewMap = new Dictionary<long, string>();
        }

        public long? UserID { get; set; }


        public string LoginName { get; set; }

        public string RealName { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public long PurviewID { get; set; }

        /// <summary>
        /// 权限集合<PurviewID, PurviewTitle>
        /// </summary>
        public Dictionary<long, string> PurviewMap { get; set; }
    }
}
