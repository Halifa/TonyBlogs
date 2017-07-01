using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserPurview
{
    public class UserPurviewSearchDTO : JQueryDataTableSearchDTO
    {
        public long PurviewID { get; set; }


        public string PurviewTitle { get; set; }
    }
}
