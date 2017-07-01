using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.DTO.UserPurview;
using TonyBlogs.Entity;

namespace TonyBlogs.IRepository
{
    public interface IUserPurviewRepository : IBaseRepository<PurviewEntity>
    {
        List<PurviewEntity> GetPurviewList(UserPurviewSearchDTO searchDTO, out long totalCount);
    }
}
