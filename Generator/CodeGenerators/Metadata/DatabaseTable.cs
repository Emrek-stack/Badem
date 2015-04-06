using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Generator.CodeGenerators.Metadata
{
    public class DatabaseTable
    {
        private readonly List<DatabaseTableColumn> _columns = new List<DatabaseTableColumn>();
        private readonly DatabaseTableColumn _primaryKeyColumn;
        private readonly bool _hasDeletedDisabled;
        private readonly bool _isOldSystemDataEnabled;
        private readonly string _allParametersWithTypeWopk;
        private readonly string _allParametersEqualFieldsCommaSeperatedWopk;
        private readonly string _allParametersCommaSeperatedWopk;
        private readonly string _allFieldsCommaSeperated;
        private readonly string _allFieldsCommaSeperatedWopk;
        private readonly string _sqlTableName;
        private readonly string _csEntityName;
        private  string _schemaName;

        public static string ListTablesCommandText
        {
            get
            {
                return "SELECT * FROM INFORMATION_SCHEMA.TABLES ORDER BY TABLE_NAME ASC";
            }
        }

        public List<DatabaseTableColumn> Columns
        {
            get
            {
                return this._columns;
            }
        }

        public DatabaseTableColumn PrimaryKeyColumn
        {
            get
            {
                return this._primaryKeyColumn;
            }
        }

        public bool HasDeletedDisabled
        {
            get
            {
                return this._hasDeletedDisabled;
            }
        }

        public bool IsOldSystemDataEnabled
        {
            get
            {
                return this._isOldSystemDataEnabled;
            }
        }

        public string AllParametersWithTypeWopk
        {
            get
            {
                return this._allParametersWithTypeWopk;
            }
        }

        public string AllParametersEqualFieldsCommaSeperatedWopk
        {
            get
            {
                return this._allParametersEqualFieldsCommaSeperatedWopk;
            }
        }

        public string AllParametersCommaSeparatedWopk
        {
            get
            {
                return this._allParametersCommaSeperatedWopk;
            }
        }

        public string AllFieldsCommaSeperated
        {
            get
            {
                return this._allFieldsCommaSeperated;
            }
        }

        public string AllFieldsCommaSeperatedWopk
        {
            get
            {
                return this._allFieldsCommaSeperatedWopk;
            }
        }

        public string SqlTableName
        {
            get
            {
                return this._sqlTableName;
            }
        }

        public string CsEntityName
        {
            get
            {
                return this._csEntityName;
            }
        }

        public string SchemaName
        {
            get { return _schemaName; }

        }

        public DatabaseTable(CommonGenerationOptions options, string szTableName, string szSchemaName, IDbConnection iDbConnection)
        {
            _sqlTableName = SqlLanguage.GetSqlSafeTableOrColumnName(szTableName, options.ValidationAndReplaceRequired);
            _csEntityName = CsLanguage.GetCsSafeIdentifierName(szTableName, options.ValidationAndReplaceRequired);
            _schemaName = szSchemaName;
            using (IDataReader dataReader = DatabaseTable.GetDbCommandReady(DatabaseTable.GetColumnMetaDataQuery(szTableName), iDbConnection).ExecuteReader())
            {
                bool flag1 = false;
                bool flag2 = false;
                int ordinal1 = dataReader.GetOrdinal("COLUMN_NAME");
                int ordinal2 = dataReader.GetOrdinal("ORDINAL_POSITION");
                int ordinal3 = dataReader.GetOrdinal("IS_NULLABLE");
                int ordinal4 = dataReader.GetOrdinal("DATA_TYPE");
                int ordinal5 = dataReader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
                int ordinal6 = dataReader.GetOrdinal("COLUMN_DEFAULT");
                int ordinal7 = dataReader.GetOrdinal("CONSTRAINT_TYPE");
                int ordinal8 = dataReader.GetOrdinal("REFERED_TO_COLUMN");
                int ordinal9 = dataReader.GetOrdinal("REFERED_TO_TABLE");
                int ordinal10 = dataReader.GetOrdinal("NUMERIC_PRECISION");
                int ordinal11 = dataReader.GetOrdinal("NUMERIC_SCALE");
                while (dataReader.Read())
                {
                    int? sqlCharLength = dataReader.GetValue(ordinal5) as int?;
                    byte? nNumericPrecision = dataReader.GetValue(ordinal10) as byte?;
                    int? nNumericScale = dataReader.GetValue(ordinal11) as int?;
                    DatabaseTableColumn databaseTableColumn = new DatabaseTableColumn(options.TargetPlatform, options.ValidationAndReplaceRequired, dataReader.GetString(ordinal3) == "YES", dataReader.GetString(ordinal1), dataReader.GetString(ordinal4), sqlCharLength, dataReader.GetValue(ordinal6).ToString(), nNumericPrecision, nNumericScale);
                    databaseTableColumn.TableColumnOrder = Convert.ToInt32(dataReader.GetValue(ordinal2));
                    switch (dataReader.GetValue(ordinal7) as string)
                    {
                        case "PRIMARY KEY":
                            databaseTableColumn.IsPrimaryKey = true;
                            databaseTableColumn.IsForeignKey = false;
                            this._primaryKeyColumn = databaseTableColumn;
                            break;
                        case "FOREIGN KEY":
                            databaseTableColumn.IsPrimaryKey = false;
                            databaseTableColumn.IsForeignKey = true;
                            databaseTableColumn.ReferedToColumnName = dataReader.GetString(ordinal8);
                            databaseTableColumn.ReferedToTableName = dataReader.GetString(ordinal9);
                            break;
                    }

                    //_schemaName = dataReader.GetString()
                    if (databaseTableColumn.SqlColumnName == options.IsDeletedColumn)
                        flag1 = true;
                    if (databaseTableColumn.SqlColumnName == options.IsDisabledColumn)
                        flag2 = true;
                    if (databaseTableColumn.SqlColumnName == "IsOldSystemData")
                        this._isOldSystemDataEnabled = true;
                    if (!this.Columns.Contains(databaseTableColumn))
                        this.Columns.Add(databaseTableColumn);
                }
                if (flag2)
                {
                    if (flag1)
                        this._hasDeletedDisabled = true;
                }
            }
            if (this._primaryKeyColumn == null)
                throw new Exception("Table " + szTableName + ", doesn't have a primary key!");
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            StringBuilder stringBuilder3 = new StringBuilder();
            StringBuilder stringBuilder4 = new StringBuilder();
            StringBuilder stringBuilder5 = new StringBuilder();
            stringBuilder5.Append(this.PrimaryKeyColumn.SqlColumnName + ", ");
            for (int index = 0; index < this.Columns.Count; ++index)
            {
                if (!this.Columns[index].IsPrimaryKey)
                {
                    if (index + 1 == this.Columns.Count)
                    {
                        stringBuilder1.Append(this.Columns[index].SqlParamAssignToColumnText);
                        stringBuilder2.Append(this.Columns[index].SqlParamNameWithCondensedType);
                        stringBuilder3.Append(this.Columns[index].SqlParamName);
                        stringBuilder4.Append(this.Columns[index].SqlColumnName);
                        stringBuilder5.Append(this.Columns[index].SqlColumnName);
                    }
                    else
                    {
                        stringBuilder1.Append(this.Columns[index].SqlParamAssignToColumnText + ", ");
                        stringBuilder2.Append(this.Columns[index].SqlParamNameWithCondensedType + ", ");
                        stringBuilder3.Append(this.Columns[index].SqlParamName + ", ");
                        stringBuilder4.Append(this.Columns[index].SqlColumnName + ", ");
                        stringBuilder5.Append(this.Columns[index].SqlColumnName + ", ");
                    }
                }
            }
            this._allParametersEqualFieldsCommaSeperatedWopk = stringBuilder1.ToString();
            this._allParametersWithTypeWopk = stringBuilder2.ToString();
            this._allParametersCommaSeperatedWopk = stringBuilder3.ToString();
            this._allFieldsCommaSeperatedWopk = stringBuilder4.ToString();
            this._allFieldsCommaSeperated = stringBuilder5.ToString();
        }

        private static string GetColumnMetaDataQuery(string szTableName)
        {
            return string.Format("SELECT c.TABLE_SCHEMA, C.COLUMN_NAME, C.ORDINAL_POSITION, C.IS_NULLABLE, C.DATA_TYPE, " +
                                 "C.CHARACTER_MAXIMUM_LENGTH, C.COLUMN_DEFAULT, C.NUMERIC_PRECISION,C.NUMERIC_SCALE,TC.CONSTRAINT_TYPE," +
                                 "CCU2.TABLE_NAME AS 'REFERED_TO_COLUMN', CCU2.COLUMN_NAME AS 'REFERED_TO_TABLE' " +
                                 "FROM INFORMATION_SCHEMA.COLUMNS AS C " +
                                 "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU ON CCU.COLUMN_NAME = C.COLUMN_NAME AND " +
                                 "CCU.TABLE_NAME = C.TABLE_NAME LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC ON TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME AND " +
                                 "TC.TABLE_NAME = CCU.TABLE_NAME LEFT OUTER JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC ON RC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME " +
                                 "LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE AS CCU2 ON CCU2.CONSTRAINT_NAME = RC.UNIQUE_CONSTRAINT_NAME  " +
                                 "WHERE  C.TABLE_NAME = '{0}' ", szTableName);
        }

        protected static IDbCommand GetDbCommandReady(string szCommandText, IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            return (IDbCommand)new SqlCommand(szCommandText, (SqlConnection)connection);
        }
    }
}
