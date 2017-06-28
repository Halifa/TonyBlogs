using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TonyBlogs.Framework;

namespace TonyBlogs.IService
{
    public interface IBaseServices<TEntity> : IDependency where TEntity : class
    {
        #region 查询
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 编辑
        /// <summary>
        /// 通过传入的model加需要修改的数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertys"></param>
        void Update(TEntity model, Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="model"></param>
        void UpdateOnly<TKey>(TEntity model, Expression<Func<TEntity, TKey>> onlyFields, Expression<Func<TEntity, bool>> where);
        #endregion

        #region 删除
        void Delete(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region 新增
        void Add(TEntity model);

        long Add(TEntity model, bool selectIdentity);
        #endregion
    }
}
