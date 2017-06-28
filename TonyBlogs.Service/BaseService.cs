using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TonyBlogs.IRepository;
using TonyBlogs.IService;

namespace TonyBlogs.Service
{
    public class BaseService<TEntity> : IBaseServices<TEntity> where TEntity : class
    {
        public IBaseRepository<TEntity> baseDal;

        #region 查询
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return baseDal.QueryWhere(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return baseDal.Single(predicate);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertys"></param>
        public void Update(TEntity model, Expression<Func<TEntity, bool>> where)
        {
            baseDal.Update(model, where);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void UpdateOnly<TKey>(TEntity model, Expression<Func<TEntity, TKey>> onlyFields, Expression<Func<TEntity, bool>> where)
        {
            baseDal.UpdateOnly(model, onlyFields, where);
        }
        #endregion

        #region 删除
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            baseDal.Delete(predicate);
        }
        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            baseDal.Add(model);
        }

        public long Add(TEntity model, bool selectIdentity)
        {
            return baseDal.Add(model, selectIdentity);
        }
        #endregion
    }
}
