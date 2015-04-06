using System;
using System.Collections.Generic;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class DataLayerInterfaceGenerator
    {
        private readonly CodeGenerationOptions _options;
        private string _currentDataLayerInterfaceName;

        public DataLayerInterfaceGenerator(CodeGenerationOptions options)
        {
            this._options = options;
        }

        private void SetCurrentDataLayerClassName(string name)
        {
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
                sb.AppendLine();
                sb.AppendLine("using " + szNamespace + ".Lib.Domain;");
                sb.AppendLine();
                sb.AppendLine("namespace " + szNamespace + ".Repo.Interface");
                sb.AppendLine("{");
                sb.AppendLine(string.Format("    public interface {0} : I{1}<{2}>", _currentDataLayerInterfaceName, _options.DataLayerClassSuffix, dbTable.CsEntityName));
                sb.AppendLine("    {");
                if (this._options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #region Generated");
                if (this._options.TargetPlatform == Platform.netFramework11)
                    sb.AppendLine("        #endregion");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                dictionary.Add(dbTable.CsEntityName, sb.ToString());
            }
            return dictionary;
        }

    }
}
