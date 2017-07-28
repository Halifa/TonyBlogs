using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using TonyBlogs.Framework;
using TonyBlogs.Framework.Data;
using TonyBlogs.IRepository;

namespace TonyBlogs.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["tonyblogs"].ConnectionString;
        private static IDbConnectionFactory connFactory = new OrmLiteConnectionFactory(connStr, TonyMySqlOrmLiteDialectProvider.Current);

        protected IDbConnection db
        {
            get
            {
                //先从线程缓存CallContext中根据key查找EF容器对象，如果没有则创建,同时保存到缓存中
                object obj = CallContext.GetData(typeof(IDbConnection).FullName);
                if (obj == null)
                {
                    //例化EF的上下文容器对象
                    obj = connFactory.Open();
                    //将EF的上下文容器对象存入线程缓存CallContext中
                    CallContext.SetData(typeof(IDbConnection).FullName, obj);
                }
                //将当前的EF上下文对象返回
                var result = obj as IDbConnection;

                if (result != null && result.State == ConnectionState.Closed)
                {
                    result.Open();
                }

                return result;

            }
        }

        #region 查询
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> QueryWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return ExecRead(conn => conn.Select(predicate));
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate) 
        { 
            return ExecRead(conn => conn.Single(predicate));
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 通过传入的model加需要修改的数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="propertys"></param>
        public void Update(TEntity model, Expression<Func<TEntity, bool>> where)
        {
            ExecWrite(conn => conn.Update(model, where));
        }

        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="model"></param>
        public void UpdateOnly(TEntity model, Expression<Func<TEntity, object>> onlyFields, Expression<Func<TEntity, bool>> where)
        {
            ExecWrite(conn => conn.UpdateOnly(model, onlyFields, where));
        }
        #endregion

        #region 删除
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
             ExecWrite(conn => conn.Delete(predicate));
        }
        #endregion

        #region 新增
        public void Add(TEntity model)
        {
            this.Add(model, false);
        }

        public long Add(TEntity model, bool selectIdentity)
        {
            return ExecWrite(conn=> conn.Insert(model, selectIdentity));
        }
        #endregion

        protected SqlExpression<TEntity> GetSqlExp()
        {
            return db.From<TEntity>();
        }

        protected List<TEntity> QueryWhere(SqlExpression<TEntity> sqlExp)
        {
            return ExecRead(conn => conn.Select(sqlExp));
        }

        protected List<To> Query<To>(SqlExpression<TEntity> sqlExp)
        {
            return ExecRead(conn => conn.LoadSelect<To, TEntity>(sqlExp));
        }

        protected TKey Scala<TKey>(SqlExpression<TEntity> sqlExp)
        {
            return ExecRead(conn => conn.Scalar<TKey>(sqlExp));
        }

        protected long Count(SqlExpression<TEntity> sqlExp)
        { 
            return ExecRead(conn => conn.Count(sqlExp));
        }

        protected bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return ExecRead(conn => conn.Exists(predicate));
        }

        private T ExecWrite<T>(Func<IDbConnection, T> func, IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = db;
            }

            try
            {
                var result = func(connection);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        private  T ExecRead<T>(Func<IDbConnection, T> func, IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = db;
            }

            try
            {
                var result = func(connection);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="IsReadConnection">是否读链接 默认不是</param>
        protected  void CloseConnection(IDbConnection connection)
        {
            if (!DbTransactionContext.HasTransaction(connection.Database))
            {
                connection.Close();
            }
        }

        public MyTransaction OpenTransaction()
        {
            MyTransaction transaction = null;
            if (!DbTransactionContext.HasTransaction(db.Database))
            {
                transaction = new MyTransaction(db.BeginTransaction());
                DbTransactionContext.AddTransaction(db.Database, transaction);
            }
            else
            {
                transaction = DbTransactionContext.GetTransaction(db.Database);
            }
            transaction.AddCount();
            return transaction;
        }
    }

    public class TonyMySqlOrmLiteDialectProvider : MySqlDialectProvider
    {
        public static TonyMySqlOrmLiteDialectProvider Current;

        static TonyMySqlOrmLiteDialectProvider()
        {
            Current = new TonyMySqlOrmLiteDialectProvider();
        }

        public override string GetTableName(string table, string schema = null)
        {
            string tableName = base.GetTableName(table, schema).ToLower();

            tableName = tableName.Replace("entity", "");

            return tableName;
        }
    
    }
}
