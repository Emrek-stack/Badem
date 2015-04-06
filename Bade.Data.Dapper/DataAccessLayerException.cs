//Coded Emre Karahan

using System;

namespace Bade.Data.Dapper
{
    public class DataAccessLayerException:Exception
    {
        /// <summary>
        /// Error Code List
        /// 1001 : Specified Column not found in dataReader
        /// 1002 : Transaction Error
        /// </summary>               
        public Exception Exception;

        public DataAccessLayerException(string message, Exception exception):base(message)
        {

            Exception = exception;
        }
    }
}
