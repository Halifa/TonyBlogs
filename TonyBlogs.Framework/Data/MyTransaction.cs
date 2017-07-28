using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TonyBlogs.Framework.Data
{
    public class MyTransaction
    {
        public MyTransaction(IDbTransaction transaction)
        {
            this.Transaction = transaction;
            this.Connection = transaction.Connection;
            this.DataBase = this.Connection.Database;
        }

        private int _count = 0;

        private readonly IDbConnection Connection;

        internal readonly Guid Distinct = Guid.NewGuid();

        private readonly IDbTransaction Transaction;

        internal readonly string DataBase;


        public void AddCount()
        {
            _count++;
        }

        public IDbConnection GetConnection()
        {
            return Connection;
        }

        public void Commit()
        {
            _count--;

            if (_count == 0)
            {

                Transaction.Commit();

                DbTransactionContext.RemoveTransaction(DataBase);

                if (!DbTransactionContext.HasTransaction(DataBase) && Connection != null)
                    Connection.Close();

            }
        }

        public void Rollback()
        {
            _count--;
            if (_count == 0)
            {
                Transaction.Rollback();

                DbTransactionContext.RemoveTransaction(DataBase);

                if (!DbTransactionContext.HasTransaction(DataBase) && Connection != null)
                    CloseConnection(Connection);
            }
        }

        public override int GetHashCode()
        {
            byte[] buffer = Distinct.ToByteArray();
            return BitConverter.ToInt32(buffer, 0);
        }

        private void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }
    }
}
