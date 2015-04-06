using System;
using System.Collections.Generic;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class BusinessLayerInterfaceGenerator
    {
        private string _currentBusinessLayerInterfaceName;
        private readonly CodeGenerationOptions _options;

        public BusinessLayerInterfaceGenerator(CodeGenerationOptions options)
        {
            this._options = options;
        }

        private void SetCurrentDataLayerClassName(string name)
        {
            
        }

        private void SetCurrentBusinessLayerClassName(string name)
        {
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
                sb.AppendLine(string.Format("***** {0}", _currentBusinessLayerInterfaceName));
                sb.AppendLine(string.Format("***** Generation Date: {0}", (object)DateTime.Now));
                sb.AppendLine("*/");
                sb.AppendLine();                
                sb.AppendLine("#region references");
                sb.AppendLine("using " + szNamespace + ".Lib.Domain;");
                sb.AppendLine("using " + szNamespace + ".Lib.Model;");
                sb.AppendLine("#endregion");
                sb.AppendLine();
                sb.AppendLine("namespace " + szNamespace + ".Lib.Service.Interface");
                sb.AppendLine("{");
                sb.AppendLine(string.Format("    public interface {0} : I{1}<{2}>",
                   _currentBusinessLayerInterfaceName, _options.DomainLogicLayerSuffix, dbTable.CsEntityName));
                sb.AppendLine("    {");          

                if (_options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #region Generated");

                if (_options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #endregion");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                dictionary.Add(dbTable.CsEntityName, sb.ToString());
            }
            return dictionary;
        }

    }
}
