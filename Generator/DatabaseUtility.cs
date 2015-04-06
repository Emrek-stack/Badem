
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Generator
{
    public partial class DatabaseUtility : UserControl, IDbConnectionUser, IMessageTextUser
    {
        private IContainer components;
        protected IDbConnectionProvider _ConnectionProvider;
        protected IMessageTextProvider messageTextProvider;
        private IDbCommand command;

        public IDbConnectionProvider ConnectionProvider
        {
            get
            {
                return this._ConnectionProvider;
            }
            set
            {
                this._ConnectionProvider = value;
            }
        }

        public IMessageTextProvider MessageTextProvider
        {
            get
            {
                return this.messageTextProvider;
            }
            set
            {
                this.messageTextProvider = value;
            }
        }

        public DatabaseUtility()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            this.AutoScaleMode = AutoScaleMode.Font;
        }

        protected DataTable GetDataTable(IDataReader reader)
        {
            DataTable schemaTable = reader.GetSchemaTable();
            DataTable dataTable = new DataTable();
            if (schemaTable != null)
            {
                for (int index = 0; index < schemaTable.Rows.Count; ++index)
                {
                    DataRow dataRow = schemaTable.Rows[index];
                    DataColumn column = new DataColumn((string)dataRow["ColumnName"], (System.Type)dataRow["DataType"]);
                    dataTable.Columns.Add(column);
                }
            }
            return dataTable;
        }

        protected IDbCommand GetDbCommandReady(string szCommandText)
        {
            try
            {
                IDbConnection dbConnection = this._ConnectionProvider.GetDbConnection();
                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();
                this.command = (IDbCommand)new SqlCommand(szCommandText, (SqlConnection)dbConnection);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return this.command;
        }
    }
}
