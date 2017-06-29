using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TonyBlogs.Framework;

namespace TonyBlogs.Common.Cache
{
    public interface ICacheManager : ISignleton
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        //设置
        void Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 设置永久有胸啊
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        void Set(string key, object value);

        //判断是否存在
        bool Contains(string key);

        //移除
        void Remove(string key);

        //清除
        void Clear();
    }
}
