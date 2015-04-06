using System;
using System.Data;
using Bade.Data.Contract;
using Bade.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Bade.Data.Dapper.Dapper;
using Dapper;

namespace Bade.Data.Dapper
{
    public abstract class Repository : IDisposable, IRepository
    {
        private bool _isDisposed;

        private readonly IConnectionFactory _connectionFactory;

        private readonly string _connectionStringName;

        protected Repository(IConnectionFactory connectionFactory, string connectionStringName)
        {
            _connectionFactory = connectionFactory;
            _connectionStringName = connectionStringName;
        }

        //private string _tableName
        //{
        //    get
        //    {
        //        TableAttribute attribute = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
        //        return attribute != null ? attribute.Name : typeof(T).Name;
        //    }
        //}

        public virtual long Add<T>(T item) where T : class
        {
            return GetConnection(c => c.Insert(item));
        }

        public virtual bool Update<T>(T item) where T : class
        {
            return GetConnection(c => c.Update(item));
        }

        public virtual T GetById<T>(object id) where T : class
        {
            return GetConnection(c => c.Get<T>(id));
        }

        public virtual bool Remove<T>(T item) where T : class
        {
            return GetConnection(c => c.Delete(item));
        }



        //public virtual T Get(Expression<Func<T, bool>> predicate)
        //{
        //    QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
        //    return GetConnection(c => c.Query<T>(result.Sql, (object)result.Param, commandType: CommandType.Text).FirstOrDefault());
        //}

        //public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate)
        //{
        //    QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
        //    return GetConnection(c => c.Query<T>(result.Sql, (object)result.Param, commandType: CommandType.Text));
        //}

        //public virtual IEnumerable<T> FindAll()
        //{
        //    return GetConnection(c => c.Query<T>("SELECT * FROM " + _tableName));
        //}

        public T GetStoredProcedure<T>(string spName, object param = null)
        {
            return GetConnection(c => c.Query<T>(spName, param, commandType: CommandType.StoredProcedure).FirstOrDefault());
        }

        public IEnumerable<T> GetManyStoredProcedure<T>(string spName, object param = null)
        {
            return GetConnection(c => c.Query<T>(spName, param, commandType: CommandType.StoredProcedure));
        }

        protected T GetConnection<T>(Func<IDbConnection, T> getData)
        {
            using (var connection = _connectionFactory.Create(_connectionStringName))
            {
                try
                {
                    connection.Open();
                    return getData(connection);
                }
                catch (Exception ex)
                {
                    throw new DataAccessLayerException("Veri Tabani Baglantisi Acilirken Hata. Hata Mesaji : " + ex.Message, ex);
                }
            }
        }

        protected TResult GetConnection<TRead, TResult>(Func<IDbConnection, TRead> getData, Func<TRead, TResult> process)
        {
            using (var connection = _connectionFactory.Create(_connectionStringName))
            {
                try
                {
                    connection.Open();
                    var data = getData(connection);
                    return process(data);
                }
                catch (Exception ex)
                {
                    throw new DataAccessLayerException("Veri Tabani Baglantisi Acilirken Hata. Hata Mesaji : " + ex.Message, ex);
                }
            }
        }

        ~Repository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {

            }

            _isDisposed = true;
        }
    }
}
