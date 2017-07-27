using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace TonyBlogs.Repository
{
    internal class DbTransactionContext
    {
        private static string CONTEXT_TRANSACTIONS_COLLECTION = "CONTEXT_TRANSACTIONS_COLLECTION";

        /// <summary>
        /// 添加事物
        /// </summary>
        /// <param name="key"></param>
        /// <param name="transaction"></param>
        internal static void AddTransaction(string key, MyTransaction transaction)
        {
            var obj = CallContext.LogicalGetData(CONTEXT_TRANSACTIONS_COLLECTION);

            List<MyTransaction> MyTransactions = null;
            if (obj == null)
            {
                MyTransactions=new List<MyTransaction>();
                MyTransactions.Add(transaction);
            }
            else
            {
                if (obj is List<MyTransaction>)
                {
                    MyTransactions = obj as List<MyTransaction>;
                    if (MyTransactions.SingleOrDefault(t => t.DataBase == key) == null)
                    {
                        MyTransactions.Add(transaction);
                    }
                }
                else
                {
                    throw new InvalidCastException("字典转换失败");
                }
            }

            CallContext.LogicalSetData(CONTEXT_TRANSACTIONS_COLLECTION, MyTransactions);
        }

        /// <summary>
        /// 是否包含事物
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static bool HasTransaction(string key)
        {
            var obj = CallContext.LogicalGetData(CONTEXT_TRANSACTIONS_COLLECTION);
            if (obj == null)
                return false;
            if (obj is List<MyTransaction>)
            {
                if ((obj as List<MyTransaction>).SingleOrDefault(t => t.DataBase == key) != null)
                    return true;
                return false;
            }
            throw new InvalidCastException("类型转换失败");
        }

        /// <summary>
        /// 获取事物
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static MyTransaction GetTransaction(string key)
        {
            var obj = CallContext.LogicalGetData(CONTEXT_TRANSACTIONS_COLLECTION);
            if (obj == null)
                return null;

            if (obj is List<MyTransaction>)
            {
                var trans = (obj as List<MyTransaction>).SingleOrDefault(t => t.DataBase == key);

                return trans;
            }

            throw new InvalidCastException("类型转换失败");
        }

        /// <summary>
        /// 移除事物
        /// </summary>
        /// <param name="key"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        internal static bool RemoveTransaction(string key)
        {
            var obj = CallContext.LogicalGetData(CONTEXT_TRANSACTIONS_COLLECTION);
            if (obj == null)
            {
                throw new ArgumentException("字典不能为空");
            }

            if (obj is List<MyTransaction>)
            {
                var transList = obj as List<MyTransaction>;

                var trans = transList.SingleOrDefault(t => t.DataBase == key);
                if (trans != null)
                {
                    transList.Remove(trans);
                    if (transList.Count > 0)
                        CallContext.LogicalSetData(CONTEXT_TRANSACTIONS_COLLECTION, transList);
                    else
                        CallContext.LogicalSetData(CONTEXT_TRANSACTIONS_COLLECTION, null);

                    return true;
                }

                return false;
            }
            else
            {
                throw new InvalidCastException("字典转换失败");
            }
        }
    }
}
