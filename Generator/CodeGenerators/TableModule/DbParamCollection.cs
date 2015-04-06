using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Generator.CodeGenerators.TableModule
{
    public class DbParamCollection : List<DbParameter>
    {
        private readonly DbProviderFactory insDbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        private int _OutputParameterIndex;

        public void Add(string parameterName, object parameterValue)
        {
            DbParameter parameter = this.insDbProviderFactory.CreateParameter();
            parameter.ParameterName = parameterName;
            if (parameterValue == null)
                parameterValue = (object)DBNull.Value;
            parameter.Value = parameterValue;
            this.Add(parameter);
        }

        public void AddOutput(string parameterName, DbType parDbType)
        {
            DbParameter parameter = this.insDbProviderFactory.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.DbType = parDbType;
            parameter.Direction = ParameterDirection.Output;
            this.Add(parameter);
            this._OutputParameterIndex = this.Count - 1;
        }

        public DbParameter GetOutPutParameter()
        {
            return this[this._OutputParameterIndex];
        }
    }
}
