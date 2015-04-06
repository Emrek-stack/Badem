//Coded Emre Karahan

using System;

namespace Bade.Data.Contract
{
    public interface IDBExceptionLogger
    {
        void LogException(Exception ex, string sqlCommand, params object[] parameters);
    }
}
