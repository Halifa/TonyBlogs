using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO
{
    public class BaseListDTO<TItem> where TItem: class, new()
    {
        public List<TItem> List { get; set; }

        public long TotalRecords { get; set; }
    }
}
