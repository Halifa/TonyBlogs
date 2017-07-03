using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Entity;
using TonyBlogs.IRepository;

namespace TonyBlogs.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfoEntity>, IUserInfoRepository
    {
    }
}
