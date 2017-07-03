using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Common.Cache;


public static class CacheExtension
{
    public static T GetOrAdd<T>(this ICacheManager cache, string key, int cacheMinutes, Func<T> factory)
    {
        if (cache.Contains(key))
        {
            return cache.Get<T>(key);
        }
        else
        {
            var data = factory();
            cache.Set(key, data, new TimeSpan(0,cacheMinutes,0));
            return data;
        }
    }
}

