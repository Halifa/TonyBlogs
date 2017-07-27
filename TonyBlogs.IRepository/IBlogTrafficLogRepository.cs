using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;

namespace TonyBlogs.IRepository
{
    public interface IBlogTrafficLogRepository : IBaseRepository<BlogTrafficLogEntity>
    {
        bool ExistView(long blogID, string ip);
    }
}
