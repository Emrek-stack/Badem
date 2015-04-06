using System;
using System.Collections.Generic;
using System.Text;
using Generator.CodeGenerators.Metadata;

namespace Generator.CodeGenerators.TableModule
{
    public class EntityLayerGenerator
    {
        private readonly CodeGenerationOptions _gloOptions;

        public EntityLayerGenerator(CodeGenerationOptions options)
        {
            _gloOptions = options;
        }

        public Dictionary<string, string> GenerateEntityCode(string szNamespace, DatabaseTableCollection dbTableCollection)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (DatabaseTable dbTable in dbTableCollection.DatabaseTables)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("/*************************************************");
                sb.AppendLine(string.Format("*{0} Entity", dbTable.CsEntityName));
                sb.AppendLine(string.Format("* Generation Date: {0}", DateTime.Now));
                sb.AppendLine("*************************************************/");
                sb.AppendLine();
                sb.AppendLine("using System;");
                sb.AppendLine("using System.Text;");
                sb.AppendLine();
                sb.AppendLine("namespace " + szNamespace + ".Lib.Domain");
                sb.AppendLine("{");
                sb.AppendLine(string.Format("    public class {0}: EntityBase, IAggregateRoot", dbTable.CsEntityName));
                sb.AppendLine("    {");
                GeneratePropertiesCodeRegion(dbTable, sb);
                sb.AppendLine();
                GenerateCtorsCodeRegion(dbTable, sb);
                GenerateNestedClassMetadataCodeRegion(dbTable, sb);
                sb.AppendLine("    }");
                sb.AppendLine("}");
                dictionary.Add(dbTable.CsEntityName, sb.ToString());
            }
            if (_gloOptions.TargetPlatform == Platform.netFramework11)
                dictionary.Add("NullableWrappers", NullableWrapperTypeGenerator.GetAllNullableClasses(szNamespace));
            return dictionary;
        }

        private void GeneratePropertiesCodeRegion(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine("        #region Properties");
            foreach (DatabaseTableColumn databaseTableColumn in dbTable.Columns)
                sb.AppendLine(databaseTableColumn.GetPropertyText(_gloOptions.TargetPlatform));
            sb.AppendLine("        #endregion");
        }

        private static void GenerateCtorsCodeRegion(DatabaseTable dbTable, StringBuilder sb)
        {
            sb.AppendLine("        #region ctor");
            sb.AppendLine();
            sb.AppendLine("        public " + dbTable.CsEntityName + "(){}");
            //sb.AppendLine("        public " + dbTable.CsEntityName + "(" + dbTable.PrimaryKeyColumn.CsTypeName + " par" + dbTable.PrimaryKeyColumn.CSPropertyName + ")");
            //sb.AppendLine("        {");
            //sb.AppendLine("             this." + dbTable.PrimaryKeyColumn.CSMemberName + " = par" + dbTable.PrimaryKeyColumn.CSPropertyName + ";");
            //sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("        #endregion");
        }

        private void GenerateNestedClassMetadataCodeRegion(DatabaseTable dbTable, StringBuilder sb)
        {
            //sb.AppendLine("        #region Meta");
            //sb.AppendLine("        public static class Meta");
            //sb.AppendLine("        {");
            //foreach (DatabaseTableColumn databaseTableColumn in dbTable.Columns)
            //    sb.AppendLine("             public static string " + databaseTableColumn.CSPropertyName + " = \"" + databaseTableColumn.CSPropertyName + "\";");
            //sb.AppendLine("        }");
            //sb.AppendLine("        #endregion");
        }
    }
}
