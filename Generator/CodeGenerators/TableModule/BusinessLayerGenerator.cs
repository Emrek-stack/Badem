using System;
using System.Collections.Generic;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class BusinessLayerGenerator
    {
        private string _currentDataLayerInstanceName;
        private string _currentDataLayerClassName;
        private string _currentDataLayerInterfaceName;
        private string _currentDataLayerInterfaceInstanceName;
        private string _currentDataLayerInterfaceLocalInstanceName;
        private string _currentBusinessLayerClassName;
        private string _currentBusinessLayerInterfaceName;
        private readonly CodeGenerationOptions _options;

        public BusinessLayerGenerator(CodeGenerationOptions options)
        {
            this._options = options;
        }

        private void SetCurrentDataLayerClassName(string name)
        {
            _currentDataLayerClassName = name + _options.DataLayerClassSuffix;
            _currentDataLayerInterfaceName = string.Format("I{0}{1}", name, _options.DataLayerClassSuffix);
            _currentDataLayerInterfaceInstanceName = string.Format("_{0}{1}", name.ToLower(),
                _options.DataLayerClassSuffix);
            _currentDataLayerInterfaceLocalInstanceName = string.Format("{0}{1}", name.ToLower(),
                _options.DataLayerClassSuffix);
            _currentDataLayerInstanceName = _options.DataLayerClassPrefix.Substring(0, 1).ToLower() + _options.DataLayerClassPrefix.Remove(0, 1) + name;
        }

        private void SetCurrentBusinessLayerClassName(string name)
        {
            _currentBusinessLayerClassName = name + _options.DomainLogicLayerSuffix;
            _currentBusinessLayerInterfaceName = string.Format("I{0}{1}", name, _options.DomainLogicLayerSuffix);
        }

        public string GenerateBusinessLayerSupertypeCode(string szNamespace)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            if (this._options.TargetPlatform == Platform.netFramework11)
                stringBuilder.AppendLine("using System.Collections;");
            else
                stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Text;");
            stringBuilder.AppendLine("using System.Data.Common;");
            stringBuilder.AppendLine("using System.Collections;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("using " + szNamespace + ".Lib.Domain;");
            stringBuilder.AppendLine("using " + szNamespace + ".Lib.Repo.Interface;");
            stringBuilder.AppendLine("using " + szNamespace + ".Lib.Service.Interace;");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("namespace " + szNamespace + ".Lib.Service.Impl");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    public class " + this._options.DomainLogicLayerPrefix + "Base");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        protected bool IsInnerBusiness = false;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        private bool _HasErrors;");
            stringBuilder.AppendLine("        public bool HasErrors");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            get { return _HasErrors; }");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        private string _ErrorMessage = String.Empty;");
            stringBuilder.AppendLine("        public string ErrorMessage");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            get { return _ErrorMessage; }");
            stringBuilder.AppendLine("            set");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                _ErrorMessage = value;");
            stringBuilder.AppendLine("                _HasErrors = (value != null && value != \"\");");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        private Guid _TranGuid = Guid.Empty;");
            stringBuilder.AppendLine("        public Guid TranGuid");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            get { return _TranGuid; }");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        public static Hashtable conns = new Hashtable();");
            stringBuilder.AppendLine("        private Guid _tCode;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("        public " + this._options.DomainLogicLayerPrefix + "Base(){}");
            stringBuilder.AppendLine("        public " + this._options.DomainLogicLayerPrefix + "Base(Guid parTranGuid)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            this.StartATransactionIfNotAlreadyStarted(parTranGuid);");
            stringBuilder.AppendLine("            this.IsInnerBusiness = true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        protected bool IsATransaction()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            return this.StartATransactionIfNotAlreadyStarted(Guid.NewGuid());");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        private bool StartATransactionIfNotAlreadyStarted(Guid guid)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if (_TranGuid == Guid.Empty)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                _TranGuid = guid;");
            stringBuilder.AppendLine("                return true;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            return false;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        protected void CommitTransaction(" + this._options.DataLayerClassPrefix + "Base commiterBase)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if(!this.IsInnerBusiness) commiterBase.Commit();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        protected void ThrowIfNecessary(BusinessRuleException bre, " + this._options.DataLayerClassPrefix + "Base rollerBase)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if(this.IsInnerBusiness) throw bre;");
            stringBuilder.AppendLine("            rollerBase.Rollback();");
            stringBuilder.AppendLine("            this.ErrorMessage = bre.Message;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        protected void ThrowIfNecessary(DbConnectorException be, " + this._options.DataLayerClassPrefix + "Base rollerBase)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if(this.IsInnerBusiness) throw be;");
            stringBuilder.AppendLine("            rollerBase.Rollback();");
            stringBuilder.AppendLine("            this.ErrorMessage = be.Message;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        public Dictionary<string, string> GenerateBusinessLayerCode(string szNamespace, DatabaseTableCollection dbTableCollection)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DatabaseTable dbTable in dbTableCollection.DatabaseTables)
            {
                this.SetCurrentDataLayerClassName(dbTable.CsEntityName);
                this.SetCurrentBusinessLayerClassName(dbTable.CsEntityName);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("/*");
                sb.AppendLine(string.Format("***** {0}", _currentBusinessLayerClassName));
                sb.AppendLine(string.Format("***** Generation Date: {0}", (object)DateTime.Now));
                sb.AppendLine("*/");
                sb.AppendLine();                
                sb.AppendLine("#region references");
                sb.AppendLine("using " + szNamespace + ".Lib.Domain;");
                sb.AppendLine("using " + szNamespace + ".Lib.Model;");
                sb.AppendLine("using " + szNamespace + ".Repo.Interface;");
                sb.AppendLine("using " + szNamespace + ".Service.Interface;");
                sb.AppendLine("#endregion");
                sb.AppendLine();
                sb.AppendLine("namespace " + szNamespace + ".Lib.Service.Impl");
                sb.AppendLine("{");
                sb.AppendLine(string.Format("    public class {0} : {1}<{2}>, {3}",
                   _currentBusinessLayerClassName, _options.DomainLogicLayerSuffix, dbTable.CsEntityName, _currentBusinessLayerInterfaceName));
                sb.AppendLine("    {");
                sb.AppendLine(string.Format("    private readonly {0} {1};", _currentDataLayerInterfaceInstanceName, _currentDataLayerInterfaceInstanceName));

                if (_options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #region Generated");
                AppendCtorsBusinessLayerCode(sb);
                //AppendInsertBusinessLayerCode(dbTable, sb);
                //AppendUpdateBusinessLayerCode(dbTable, sb);
                //if (dbTable.HasDeletedDisabled)
                //    AppendMarkByIdBusinessLayerCode(dbTable, sb);
                //AppendDeleteByIdBusinessLayerCode(dbTable, sb);
                //if (_options.GenerateDeleteByForeignKey)
                //    AppendDeleteByForeignKeyBusinessLayerCodes(dbTable, sb);
                //AppendSelectBusinessLayerCode(dbTable, sb);
                //AppendSelectByIdBusinessLayerCode(dbTable, sb);
                //AppendSelectByForeignKeyBusinessLayerCodes(dbTable, sb);
                //AppendSampleTransactionalMethodCode(dbTable, sb);
                if (_options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #endregion");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                dictionary.Add(dbTable.CsEntityName, sb.ToString());
            }
            return dictionary;
        }

        private void AppendCtorsBusinessLayerCode(StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("#region ctor");
            sb.AppendLine(string.Format("        public {0} ({1} {2}):base({2})", _currentBusinessLayerClassName,
                _currentDataLayerInterfaceName, _currentDataLayerInterfaceLocalInstanceName));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("{0} = {1};", _currentDataLayerInterfaceInstanceName,
                _currentDataLayerInterfaceLocalInstanceName));
            sb.AppendLine("}");
            sb.AppendLine("#endregion");
            sb.AppendLine();
        }

        private void AppendInsertBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.InsertPrefix + dbTable.CsEntityName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.InsertPrefix + dbTable.CsEntityName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendUpdateBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.UpdatePrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendMarkByIdBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendMarkByKeyBusinessLayerCode(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendMarkByKeyBusinessLayerCode(DatabaseTable dbTable, DatabaseTableColumn dtc, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ", bool " + this._options.IsDeletedColumn + ", bool " + this._options.IsDisabledColumn + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.MarkPrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ", " + this._options.IsDeletedColumn + ", " + this._options.IsDisabledColumn + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + this._options.DeletedPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.MarkPrefix + this._options.DeletedPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.MarkPrefix + this._options.DisabledPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.MarkPrefix + this._options.DisabledPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendDeleteByIdBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            this.AppendDeleteByKeyBusinessLayerCode(dbTable, dbTable.PrimaryKeyColumn, sb);
        }

        private void AppendDeleteByForeignKeyBusinessLayerCodes(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dtc in dbTable.Columns)
            {
                if (dtc.IsForeignKey)
                    this.AppendDeleteByKeyBusinessLayerCode(dbTable, dtc, sb);
            }
        }

        private void AppendDeleteByKeyBusinessLayerCode(DatabaseTable dbTable, DatabaseTableColumn dtc, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.DeletePrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.DeletePrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "()");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                return " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + dbTable.CsEntityName + "();");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
            if (!dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "(bool? " + this._options.IsDeletedColumn + ", bool? " + this._options.IsDisabledColumn + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                return " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + dbTable.CsEntityName + "(" + this._options.IsDeletedColumn + ", " + this._options.IsDisabledColumn + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectByIdBusinessLayerCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public void " + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dbTable.PrimaryKeyColumn.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSelectByForeignKeyBusinessLayerCodes(DatabaseTable dbTable, StringBuilder sb)
        {
            foreach (DatabaseTableColumn dtc in dbTable.Columns)
            {
                if (dtc.IsForeignKey)
                    this.AppendSelectByKeyBusinessLayerCode(dbTable, dtc, sb);
            }
        }

        private void AppendSelectByKeyBusinessLayerCode(DatabaseTable dbTable, DatabaseTableColumn dtc, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                return " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
            if (!dbTable.HasDeletedDisabled)
                return;
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + this._options.NotDeletedPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                return " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + this._options.NotDeletedPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        public DataTable " + this._options.SelectPrefix + this._options.NotDisabledPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                return " + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + this._options.NotDisabledPhrase + dbTable.CsEntityName + "By" + dtc.CSPropertyName + "(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }

        private void AppendSampleTransactionalMethodCode(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendLine("        public DataTable SampleTransactional" + dbTable.CsEntityName + "Method(" + dbTable.CsEntityName + " par" + dbTable.CsEntityName + ")");
            sb.AppendLine("        {");
            sb.AppendLine("            " + this._currentDataLayerClassName + " " + this._currentDataLayerInstanceName + " = null;");
            sb.AppendLine("            try");
            sb.AppendLine("            {");
            sb.AppendLine("                this.IsATransaction();");
            sb.AppendLine("                " + this._currentDataLayerInstanceName + " = new " + this._currentDataLayerClassName + "(this.TranGuid);");
            sb.AppendLine("                //" + this._options.DomainLogicLayerPrefix + "Hede = new " + this._options.DomainLogicLayerPrefix + "Hede(this.TranGuid);");
            sb.AppendLine("                //" + this._currentDataLayerInstanceName + "." + this._options.SelectPrefix + dbTable.CsEntityName + "ByHodo(par" + dbTable.CsEntityName + ");");
            sb.AppendLine("                //" + this._currentDataLayerInstanceName + "." + this._options.DeletePrefix + "HedeByHedeId(parHede);");
            sb.AppendLine("                this.CommitTransaction(" + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(BusinessRuleException bre)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(bre, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            catch(DbConnectorException dce)");
            sb.AppendLine("            {");
            sb.AppendLine("                this.ThrowIfNecessary(dce, " + this._currentDataLayerInstanceName + ");");
            sb.AppendLine("            }");
            sb.AppendLine("            return null;");
            sb.AppendLine("        }");
            sb.AppendLine();
        }
    }
}
