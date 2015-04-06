using System;
using System.Collections.Generic;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class DataLayerGenerator
    {
        private readonly CodeGenerationOptions _options;
        private string _currentDataLayerClassName;
        private string _currentDataLayerInterfaceName;
        private string _currentDataLayerInterfaceInstanceName;

        public DataLayerGenerator(CodeGenerationOptions options)
        {
            this._options = options;
        }

        private void SetCurrentDataLayerClassName(string name)
        {
            _currentDataLayerClassName = string.Format("{0}{1}", name, _options.DataLayerClassSuffix);
            _currentDataLayerInterfaceName = string.Format("I{0}{1}", name, _options.DataLayerClassSuffix);
        }

        public string GenerateDataLayerSupertypeCode(string szNamespace)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("using " + szNamespace + ".DAL;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(string.Format("namespace {0}.Repo.Impl", szNamespace));
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    public class " + this._options.DataLayerClassPrefix + "Base");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        public DbConnector gloDbConnector;");
            stringBuilder.AppendLine("        public static Hashtable conns = new Hashtable();");
            stringBuilder.AppendLine("        private Guid _tCode;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        public " + this._options.DataLayerClassPrefix + "Base()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            gloDbConnector = new DbConnector();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        public " + this._options.DataLayerClassPrefix + "Base(Guid tCode)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if (tCode != Guid.Empty)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                _tCode = tCode;");
            stringBuilder.AppendLine("                if (conns.ContainsKey(tCode))");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    this.gloDbConnector = (DbConnector) conns[tCode];");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("                else");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    gloDbConnector = new DbConnector();");
            stringBuilder.AppendLine("                    gloDbConnector.BeginTransaction();");
            stringBuilder.AppendLine("                    conns.Add(tCode, this.gloDbConnector);");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            else");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                gloDbConnector = new DbConnector();");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public void Commit()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            gloDbConnector.CommitTransaction();");
            stringBuilder.AppendLine("            conns.Remove(_tCode);");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public void Rollback()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            gloDbConnector.RollbackTransaction();");
            stringBuilder.AppendLine("            conns.Remove(_tCode);");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        public Dictionary<string, string> GenerateDataLayerCode(string szNamespace, DatabaseTableCollection dbTableCollection)
        {


            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DatabaseTable dbTable in dbTableCollection.DatabaseTables)
            {
                SetCurrentDataLayerClassName(dbTable.CsEntityName);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("/*");
                sb.AppendLine(string.Format("****** {0} Data", dbTable.CsEntityName));
                sb.AppendLine(string.Format("****** Generation Date: {0}", (object)DateTime.Now));
                sb.AppendLine("*/");
                sb.AppendLine();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Data;");
                sb.AppendLine("using System.Collections.Generic;");
                sb.AppendLine();
                sb.AppendLine("using " + szNamespace + ".Lib.Domain;");
                sb.AppendLine("using " + szNamespace + ".Lib.Repo.Interface;");
                sb.AppendLine("using " + szNamespace + ".Lib.Common.Constant;");
                sb.AppendLine();
                sb.AppendLine("namespace " + szNamespace + ".Repo.Impl");
                sb.AppendLine("{");
                sb.AppendLine(string.Format("    public class {0} : {1}<{2}>, {3}", _currentDataLayerClassName, _options.DataLayerClassSuffix, dbTable.CsEntityName, _currentDataLayerInterfaceName));
                sb.AppendLine("    {");
                if (this._options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #region Generated");
                this.AppendCtorsDataLayerCode(dbTable, sb);
                //AppendEntityFillerDataLayerCode(dbTable, sb);
                // AppendFirstEntityFillerDataLayerCode(dbTable, sb);
                // AppendEntityListFillerDataLayerCode(dbTable, sb);
                // if (dbTable.HasDeletedDisabled)
                //    this.AppendMarkByIdDataLayerCode(dbTable, sb);
                //this.AppendInsertDataLayerCode(dbTable, sb);
                //this.AppendUpdateDataLayerCode(dbTable, sb);
                //this.AppendDeleteByIdDataLayerCode(dbTable, sb);
                //if (this._options.GenerateDeleteByForeignKey)
                //this.AppendDeleteByForeignKeyDataLayerCodes(dbTable, sb);
                //this.AppendSelectDataLayerCode(dbTable, sb);
                //this.AppendSelectByIdDataLayerCode(dbTable, sb);
                //this.AppendSelectByForeignKeyDataLayerCodes(dbTable, sb);
                if (this._options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #endregion");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                dictionary.Add(dbTable.CsEntityName, sb.ToString());
            }
            return dictionary;
        }

        private void AppendCtorsDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine(
                string.Format(
                    "        public {0}(IConnectionFactory connectionFactory):base(connectionFactory, Constant.DefaultConnectionStringName){{}}",
                    _currentDataLayerClassName));
            sb.AppendLine();
        }

        private static void AppendEntityListFillerDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public List<" + dbTable.CsEntityName + "> GetListOf" + dbTable.CsEntityName + "(DataTable parSource)");
            sb.AppendLine("        {");
            sb.AppendLine("            List<" + dbTable.CsEntityName + "> lstRetval = new List<" + dbTable.CsEntityName + ">();");
            sb.AppendLine("            if (parSource != null)");
            sb.AppendLine("            {");
            sb.AppendLine("                 for(int i = 0; i < parSource.Rows.Count; i++)");
            sb.AppendLine("                 {");
            sb.AppendLine("                     " + dbTable.CsEntityName + " entity = new " + dbTable.CsEntityName + "();");
            sb.AppendLine("                     this.Fill" + dbTable.CsEntityName + "Entity(parSource, entity, i);");
            sb.AppendLine("                     lstRetval.Add(entity);");
            sb.AppendLine("                 }");
            sb.AppendLine("            }");
            sb.AppendLine("            return lstRetval;");
            sb.AppendLine("        }");
        }

        private static void AppendFirstEntityFillerDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void FillFirst" + dbTable.CsEntityName + "Entity(DataTable parSource, " + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            if (parSource.Rows.Count > 0)");
            sb.AppendLine("            {");
            sb.AppendLine("                 this.Fill" + dbTable.CsEntityName + "Entity(parSource, par" + dbTable.CsEntityName + ", 0);");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
        }

        private static void AppendEntityFillerDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void Fill" + dbTable.CsEntityName + "Entity(DataTable parSource, " + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ", int parPosition)");
            sb.AppendLine("        {");
            foreach (DatabaseTableColumn databaseTableColumn in dbTable.Columns)
            {
                sb.AppendLine("                if (parSource.Rows[parPosition][\"" + databaseTableColumn.SqlColumnName + "\"] != DBNull.Value)");
                sb.AppendLine("                {");
                sb.AppendLine("                    par" + dbTable.CsEntityName + "." + databaseTableColumn.CSPropertyName + " = " + databaseTableColumn.CSConversionMethodName + "(parSource.Rows[parPosition][\"" + databaseTableColumn.SqlColumnName + "\"]);");
                sb.AppendLine("                }");
            }
            sb.AppendLine("        }");
        }

        private void AppendInsertDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.InsertPrefix + dbTable.CsEntityName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            foreach (DatabaseTableColumn databaseTableColumn in dbTable.Columns)
            {
                if (!databaseTableColumn.IsPrimaryKey)
                    sb.AppendLine("            insDbParamCollection.Add(\"" + databaseTableColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + databaseTableColumn.CSPropertyName + ");");
            }
            sb.AppendLine("            insDbParamCollection.AddOutput(\"" + dbTable.PrimaryKeyColumn.SqlParamName + "\", System.Data.DbType.Int32);");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.InsertPrefix + dbTable.CsEntityName + "\", insDbParamCollection);");
            sb.AppendLine("            par" + dbTable.CsEntityName + "." + dbTable.PrimaryKeyColumn.CSPropertyName + " = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendUpdateDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            foreach (DatabaseTableColumn databaseTableColumn in dbTable.Columns)
                sb.AppendLine("            insDbParamCollection.Add(\"" + databaseTableColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + databaseTableColumn.CSPropertyName + ");");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendMarkByIdDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendMarkByKeyDataLayerCode(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendMarkByForeignKeyDataLayerCodes(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendMarkByKeyDataLayerCode(dbTable, dbtColumn, sb);
            }
        }

        private void AppendMarkByKeyDataLayerCode(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ", bool " + this._options.IsDeletedColumn + ", bool " + this._options.IsDisabledColumn + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", " + this._options.IsDeletedColumn + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", " + this._options.IsDisabledColumn + ");");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + this._options.DeletedPhrase + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", true);");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", true);");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + this._options.DisabledPhrase + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", false);");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", true);");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendDeleteByIdDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendDeleteByKeyDataLayerCode(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendDeleteByForeignKeyDataLayerCodes(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendDeleteByKeyDataLayerCode(dbTable, dbtColumn, sb);
            }
        }

        private void AppendDeleteByKeyDataLayerCode(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.DeletePrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            gloDbConnector.ExecuteNonQuery(\"" + this._options.AllGeneratedSpPrefix + this._options.DeletePrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "()");
            sb.AppendLine("        {");
            sb.AppendLine("            return gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + "\", null);");
            sb.AppendLine("        }");
            sb.AppendLine();
            if (!dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "(bool? " + this._options.IsDeletedColumn + ", bool? " + this._options.IsDisabledColumn + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", " + this._options.IsDeletedColumn + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", " + this._options.IsDisabledColumn + ");");
            sb.AppendLine("            return gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + this._options.WithMarkPostfix + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectByIdDataLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbTable.PrimaryKeyColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbTable.PrimaryKeyColumn.CSPropertyName + ");");
            sb.AppendLine("            DataTable insDataTable = new DataTable();");
            sb.AppendLine("            insDataTable = gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("            this.FillFirst" + dbTable.CsEntityName + "Entity(insDataTable, par" + dbTable.CsEntityName + ");");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectByForeignKeyDataLayerCodes(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dbtColumn in dbTable.Columns)
            {
                if (dbtColumn.IsForeignKey)
                    this.AppendSelectByKeyDataLayerCode(dbTable, dbtColumn, sb);
            }
        }

        private void AppendSelectByKeyDataLayerCode(DatabaseTable dbTable, DatabaseTableColumn dbtColumn, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            return gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            if (!dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + this._options.NotDeletedPhrase + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", false);");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", null);");
            sb.AppendLine("            return gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + this._options.WithMarkPostfix + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + this._options.NotDisabledPhrase + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            DbParamCollection insDbParamCollection = new DbParamCollection();");
            sb.AppendLine("            insDbParamCollection.Add(\"" + dbtColumn.SqlParamName + "\", par" + dbTable.CsEntityName + "." + dbtColumn.CSPropertyName + ");");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDeletedColumn + "\", false);");
            sb.AppendLine("            insDbParamCollection.Add(\"@" + this._options.IsDisabledColumn + "\", false);");
            sb.AppendLine("            return gloDbConnector.ExecuteDataTable(\"" + this._options.AllGeneratedSpPrefix + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbtColumn.CSPropertyName + this._options.WithMarkPostfix + "\", insDbParamCollection);");
            sb.AppendLine("        }");
            sb.AppendLine();
        }
    }
}
