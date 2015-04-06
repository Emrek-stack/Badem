using System;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class StoredProcedureGenerator
    {
        private CodeGenerationOptions options;
        private readonly string _schemaName;

        public StoredProcedureGenerator(CodeGenerationOptions options)
        {
            this.options = options;
        }

        public string GenerateSpScripts(DatabaseTableCollection dbTableCollection, DateTime datetimeTimestamp)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DatabaseTable dbTable in dbTableCollection.DatabaseTables)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("--|");
                sb.AppendLine("--|");
                sb.AppendLine("--| " + dbTable.SqlTableName + "'s Stored Procedures");
                sb.AppendLine("--|");
                if (this.options.GenerateDropAllTableSPs)
                    this.AppendDropAllTableSPs(dbTable, sb);
                this.AppendDropMarkByIdSP(dbTable, sb);
                this.AppendDropMarkByForeignKeySPs(dbTable, sb);
                if (dbTable.HasDeletedDisabled)
                    this.AppendMarkByIdSP(dbTable, sb, datetimeTimestamp);
                this.AppendDropInsertSp(dbTable, sb);
                this.AppendInsertSP(dbTable, sb, datetimeTimestamp);
                this.AppendDropUpdateSp(dbTable, sb);
                this.AppendUpdateSP(dbTable, sb, datetimeTimestamp);
                this.AppendDropDeleteByIdSP(dbTable, sb);
                this.AppendDeleteByIdSP(dbTable, sb, datetimeTimestamp);
                if (this.options.GenerateDeleteByForeignKey)
                {
                    this.AppendDropDeleteByForeignKeySPs(dbTable, sb);
                    this.AppendDeleteByForeignKeySPs(dbTable, sb, datetimeTimestamp);
                }
                this.AppendDropSelectSP(dbTable, sb);
                this.AppendSelectSP(dbTable, sb, datetimeTimestamp);
                this.AppendDropSelectByIdSP(dbTable, sb);
                this.AppendSelectByIdSP(dbTable, sb, datetimeTimestamp);
                this.AppendDropSelectByForeignKeySPs(dbTable, sb);
                this.AppendSelectByForeignKeySPs(dbTable, sb, datetimeTimestamp);
            }
            return sb.ToString();
        }

        private void AppendDropInsertSp(DatabaseTable dbTable, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.InsertPrefix + dbTable.CsEntityName, sb);
        }

        private void AppendDropUpdateSp(DatabaseTable dbTable, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName, sb);
        }

        private void AppendDropMarkByIdSP(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendDropMarkByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendDropMarkByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbColumn in dbTable.Columns)
            {
                if (dbColumn.IsForeignKey)
                    this.AppendDropMarkByKeySP(dbTable, dbColumn, sb);
            }
        }

        private void AppendDropMarkByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbColumn, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.MarkPrefix + dbTable.CsEntityName + "By" + dbColumn.CSPropertyName, sb);
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.MarkPrefix + dbTable.CsEntityName + "By" + dbColumn.CSPropertyName + this.options.WithMarkPostfix, sb);
        }

        private void AppendDropDeleteByIdSP(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendDropDeleteByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendDropDeleteByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbColumn in dbTable.Columns)
            {
                if (dbColumn.IsForeignKey)
                    this.AppendDropDeleteByKeySP(dbTable, dbColumn, sb);
            }
        }

        private void AppendDropDeleteByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbColumn, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.DeletePrefix + dbTable.CsEntityName + "By" + dbColumn.CSPropertyName, sb);
        }

        private void AppendDropSelectSP(DatabaseTable dbTable, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName, sb);
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + this.options.WithMarkPostfix, sb);
        }

        private void AppendDropSelectByIdSP(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendDropSelectByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendDropSelectByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbColumn in dbTable.Columns)
            {
                if (dbColumn.IsForeignKey)
                    this.AppendDropSelectByKeySP(dbTable, dbColumn, sb);
            }
        }

        private void AppendDropSelectByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbColumn, StringBuilder sb)
        {
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + "By" + dbColumn.CSPropertyName, sb);
            StoredProcedureGenerator.AppendDropProcedure(this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + "By" + dbColumn.CSPropertyName + this.options.WithMarkPostfix, sb);
        }

        private static void AppendDropProcedure(string szTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = '" + szTable + "' AND ROUTINE_TYPE = 'PROCEDURE') <> 0");
            sb.AppendLine("     DROP PROCEDURE " + szTable);
            sb.AppendLine("GO");
        }

        private void AppendDropAllTableSPs(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME LIKE '" + this.options.AllGeneratedSpPrefix + "%" + dbTable.SqlTableName + "' AND ROUTINE_TYPE = 'PROCEDURE') <> 0");
            sb.AppendLine("IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME LIKE '" + this.options.AllGeneratedSpPrefix + "%" + dbTable.SqlTableName + "' AND ROUTINE_TYPE = 'PROCEDURE') <> 0");
            sb.AppendLine("BEGIN");
            sb.AppendLine("     SELECT 'DROP PROC ' + SPECIFIC_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME LIKE '" + this.options.AllGeneratedSpPrefix + "%" + dbTable.SqlTableName + "' AND ROUTINE_TYPE = 'PROCEDURE'");
            sb.AppendLine("END");
            sb.AppendLine("GO");
        }

        private void AppendInsertSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.InsertPrefix + dbTable.CsEntityName);
            sb.AppendLine(dbTable.AllParametersWithTypeWopk + ", " + dbTable.PrimaryKeyColumn.SqlParamNameWithCondensedType + " output");
            sb.AppendLine("AS");
            sb.AppendLine("     INSERT INTO " + dbTable.SqlTableName);
            sb.AppendLine("         (" + dbTable.AllFieldsCommaSeperatedWopk + ")");
            sb.AppendLine("     VALUES ");
            sb.AppendLine("         (" + dbTable.AllParametersCommaSeparatedWopk + ")");
            sb.AppendLine("     SET " + dbTable.PrimaryKeyColumn.SqlParamName + " = SCOPE_IDENTITY()");
            sb.AppendLine("GO");
        }

        private void AppendUpdateSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName);
            sb.AppendLine(dbTable.PrimaryKeyColumn.SqlParamNameWithCondensedType + ", " + dbTable.AllParametersWithTypeWopk);
            sb.AppendLine("AS");
            sb.AppendLine("     UPDATE " + dbTable.SqlTableName + " SET ");
            sb.AppendLine("         " + dbTable.AllParametersEqualFieldsCommaSeperatedWopk);
            sb.AppendLine("     WHERE " + dbTable.PrimaryKeyColumn.SqlParamAssignToColumnText);
            sb.AppendLine("GO");
        }

        private void AppendMarkByIdSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            this.AppendMarkByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb, datetimeTimestamp);
        }

        private void AppendMarkByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendMarkByKeySP(dbTable, dbtColumn, sb, datetimeTimestamp);
            }
        }

        private void AppendMarkByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.MarkPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName);
            sb.AppendLine(dbtColumn.SqlParamNameWithCondensedType + ",");
            sb.AppendLine("@" + this.options.IsDeletedColumn + " bit, @" + this.options.IsDisabledColumn + " bit");
            sb.AppendLine("AS");
            sb.AppendLine("     UPDATE " + dbTable.SqlTableName + " SET ");
            sb.AppendLine("         " + this.options.IsDeletedColumn + " = @" + this.options.IsDeletedColumn + ", " + this.options.IsDisabledColumn + " = @" + this.options.IsDisabledColumn);
            sb.AppendLine("     WHERE " + dbtColumn.SqlParamAssignToColumnText);
            sb.AppendLine("GO");
        }

        private void AppendDeleteByIdSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            this.AppendDeleteByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb, datetimeTimestamp);
        }

        private void AppendDeleteByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendDeleteByKeySP(dbTable, dbtColumn, sb, datetimeTimestamp);
            }
        }

        private void AppendDeleteByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.DeletePrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName);
            sb.AppendLine(dbtColumn.SqlParamNameWithCondensedType);
            sb.AppendLine("AS");
            sb.AppendLine("     DELETE FROM " + dbTable.SqlTableName + " WHERE " + dbtColumn.SqlParamAssignToColumnText);
            sb.AppendLine("GO");
            sb.AppendLine();
        }

        private void AppendSelectSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName);
            sb.AppendLine("AS");
            sb.AppendLine("     SELECT * FROM " + dbTable.SqlTableName + "(NOLOCK)");
            sb.AppendLine("GO");
            if (!dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + this.options.WithMarkPostfix);
            sb.AppendLine("@" + this.options.IsDeletedColumn + " bit = NULL, @" + this.options.IsDisabledColumn + " bit = NULL");
            sb.AppendLine("AS");
            sb.AppendLine("     SELECT * FROM " + dbTable.SqlTableName + "(NOLOCK) WHERE (" + this.options.IsDeletedColumn + " = @" + this.options.IsDeletedColumn + " OR @" + this.options.IsDeletedColumn + " IS NULL) AND (" + this.options.IsDisabledColumn + " = @" + this.options.IsDisabledColumn + " OR @" + this.options.IsDisabledColumn + " IS NULL)");
            sb.AppendLine("GO");
        }

        private void AppendSelectByIdSP(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            this.AppendSelectByKeySP(dbTable, dbTable.PrimaryKeyColumn, sb, datetimeTimestamp);
        }

        private void AppendSelectByForeignKeySPs(DatabaseTable dbTable, StringBuilder sb, DateTime datetimeTimestamp)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendSelectByKeySP(dbTable, dbtColumn, sb, datetimeTimestamp);
            }
        }

        private void AppendSelectByKeySP(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb, DateTime datetimeTimestamp)
        {
            sb.AppendLine();
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName);
            sb.AppendLine(dbtColumn.SqlParamNameWithCondensedType);
            sb.AppendLine("AS");
            sb.AppendLine("     SELECT * FROM " + dbTable.SqlTableName + "(NOLOCK) WHERE " + dbtColumn.SqlParamAssignToColumnText);
            sb.AppendLine("GO");
            sb.AppendLine();
            if (dbTable.PrimaryKeyColumn.SqlColumnName == dbtColumn.SqlColumnName || !dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine("-- Date Of Creation : " + (object)datetimeTimestamp);
            sb.AppendLine("CREATE PROC " + this.options.AllGeneratedSpPrefix + this.options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + this.options.WithMarkPostfix);
            sb.AppendLine(dbtColumn.SqlParamNameWithCondensedType + ",");
            sb.AppendLine("@" + this.options.IsDeletedColumn + " bit = NULL, @" + this.options.IsDisabledColumn + " bit = NULL");
            sb.AppendLine("AS");
            sb.AppendLine("     SELECT * FROM " + dbTable.SqlTableName + "(NOLOCK) WHERE " + dbtColumn.SqlParamAssignToColumnText + " AND (" + this.options.IsDeletedColumn + " = @" + this.options.IsDeletedColumn + " OR @" + this.options.IsDeletedColumn + " IS NULL) AND (" + this.options.IsDisabledColumn + " = @" + this.options.IsDisabledColumn + " OR @" + this.options.IsDisabledColumn + " IS NULL)");
            sb.AppendLine("GO");
        }
    }
}
