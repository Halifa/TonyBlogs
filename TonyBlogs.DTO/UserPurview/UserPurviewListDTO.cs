using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.UserPurview
{
    public class UserPurviewListDTO
    {
        public List<UserPurviewListItemDTO> List { get; set; }

        public long TotalRecords { get; set; }
    }
}
