#region

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bade.Infrastructure;

#endregion

namespace Bade.Data.Contract
{
    public interface IRepository
    {
        long Add<T>(T entity) where T : class;

        bool Update<T>(T entity) where T : class;

        T GetById<T>(object id) where T : class;

        bool Remove<T>(T entity) where T : class;

        //T Get(Expression<Func<T, bool>> predicate);

        //IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate);

        //IEnumerable<T> FindAll();

        //T GetStoredProcedure(string spName, object param = null);

        //IEnumerable<T> GetManyStoredProcedure(string spName, object param = null);
    }
}