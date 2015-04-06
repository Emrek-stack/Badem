using System;
using System.Data;
using System.Data.Common;

namespace Generator.CodeGenerators.TableModule
{
    public class DbConnector
    {
        private readonly DbProviderFactory insDbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        private string _ConnectionString;
        private DbConnection _Connection;
        private DbTransaction _Transaction;

        public string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
            set
            {
                this._ConnectionString = value;
            }
        }

        public DbConnector()
        {
            this._ConnectionString = "";
        }

        public DbConnector(string parConnectionString)
        {
            this._ConnectionString = parConnectionString;
        }

        public void OpenConnection()
        {
            if (this._Connection == null)
            {
                this._Connection = this.insDbProviderFactory.CreateConnection();
                this._Connection.ConnectionString = this._ConnectionString;
            }
            if (this._Connection.State != ConnectionState.Closed)
                return;
            try
            {
                this._Connection.Open();
            }
            catch (Exception ex)
            {
            }
        }

        public void CloseConnecion()
        {
            if (this._Transaction != null)
                return;
            if (this._Connection == null)
                return;
            try
            {
                this._Connection.Close();
            }
            catch (Exception ex)
            {
            }
            this._Connection.Dispose();
            this._Connection = (DbConnection)null;
        }

        public void BeginTransaction()
        {
            if (this._Connection == null || this._Connection.State == ConnectionState.Closed)
                this.OpenConnection();
            if (this._Transaction != null)
                return;
            try
            {
                this._Transaction = this._Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
            }
        }

        public void CommitTransaction()
        {
            if (this._Transaction == null)
                return;
            try
            {
                this._Transaction.Commit();
            }
            catch (Exception ex)
            {
            }
            this._Transaction.Dispose();
            this._Transaction = (DbTransaction)null;
            this.CloseConnecion();
        }

        public void RollbackTransaction()
        {
            if (this._Transaction == null)
                return;
            try
            {
                this._Transaction.Rollback();
            }
            catch (Exception ex)
            {
            }
            this._Transaction.Dispose();
            this._Transaction = (DbTransaction)null;
            this.CloseConnecion();
        }

        public DbCommand CreateCommand(string parCommandText, DbParamCollection parDbParameters)
        {
            DbCommand command = this.insDbProviderFactory.CreateCommand();
            command.CommandText = parCommandText;
            command.CommandType = CommandType.StoredProcedure;
            if (parDbParameters != null)
                command.Parameters.AddRange((Array)parDbParameters.ToArray());
            command.Connection = this._Connection;
            if (this._Transaction != null)
                command.Transaction = this._Transaction;
            return command;
        }

        public DataTable ExecuteDataTable(string parCommandText, DbParamCollection parDbParameters)
        {
            return this.ExecuteDataTable(parCommandText, parDbParameters, false);
        }

        public DataTable ExecuteDataTable(string parCommandText, DbParamCollection parDbParameters, bool parIsCached)
        {
            DataTable dataTable = new DataTable();
            if (parIsCached)
            {
                if (AppDomain.CurrentDomain.GetData(parCommandText) == null)
                {
                    this.OpenConnection();
                    DbDataAdapter dataAdapter = this.insDbProviderFactory.CreateDataAdapter();
                    dataAdapter.SelectCommand = this.CreateCommand(parCommandText, parDbParameters);
                    try
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    catch (Exception ex)
                    {
                    }
                    this.CloseConnecion();
                    AppDomain.CurrentDomain.SetData(parCommandText, (object)dataTable);
                }
                else
                    dataTable = (DataTable)AppDomain.CurrentDomain.GetData(parCommandText);
            }
            else
            {
                this.OpenConnection();
                DbDataAdapter dataAdapter = this.insDbProviderFactory.CreateDataAdapter();
                dataAdapter.SelectCommand = this.CreateCommand(parCommandText, parDbParameters);
                try
                {
                    dataAdapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                }
                this.CloseConnecion();
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string parCommandText, DbParamCollection parDbParameters)
        {
            this.OpenConnection();
            int num = 0;
            try
            {
                num = this.CreateCommand(parCommandText, parDbParameters).ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            this.CloseConnecion();
            return num;
        }

        public object ExecuteScalar(string parCommandText, DbParamCollection parDbParameters)
        {
            this.OpenConnection();
            object obj = (object)null;
            try
            {
                obj = this.CreateCommand(parCommandText, parDbParameters).ExecuteScalar();
            }
            catch (Exception ex)
            {
            }
            this.CloseConnecion();
            return obj;
        }
    }
}
