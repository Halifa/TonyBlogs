using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO.Account
{
    public class AccountLoginResultDTO :ExecuteResult
    {
        /// <summary>
        /// cookie值
        /// </summary>
        public string CookieValue { get; set; }

        /// <summary>
        /// cookie值缓存键
        /// </summary>
        public string CookieValueCacheKey { get; set; }
    }
}
