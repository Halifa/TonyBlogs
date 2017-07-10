using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Common.Cache;
using TonyBlogs.DTO.UserInfo;
using TonyBlogs.Framework;

namespace TonyBlogs.Service
{
    internal class UserCacheBiz
    {
        private long _userID;

        public UserCacheBiz(long userID)
        {
            this._userID = userID;
        }

        public void SetUserCache(UserObj userObj)
        {
            if (userObj != null)
            {
                this.Cache.Set(UserObjCacheKey, userObj, TimeSpan.FromHours(1)); 
            }
        }

        public void RemoveUserCache()
        {
            this.Cache.Remove(UserObjCacheKey);
        }

        public UserObj GetUserCache()
        {
            return this.Cache.Get<UserObj>(UserObjCacheKey);
        }

        public string UserObjCacheKey {
            get {
                return string.Format("UserCacheBiz_UserObjCacheKey_{0}", this._userID.ToString());
            }
        }

        protected ICacheManager Cache {
            get {
                return ContainerManager.Resolve<ICacheManager>();
            }
        }
    }
}
