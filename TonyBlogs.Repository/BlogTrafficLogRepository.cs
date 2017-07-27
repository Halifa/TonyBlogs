using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;

namespace TonyBlogs.Repository
{
    public class BlogTrafficLogRepository : BaseRepository<BlogTrafficLogEntity>, IBlogTrafficLogRepository
    {
        public bool ExistView(long blogID, string ip)
        {
            return base.Exist(m => m.BlogID == blogID && m.IP == ip);
        }
    }
}
