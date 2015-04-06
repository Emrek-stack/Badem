using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Generator.Properties;

namespace Generator.CodeGenerators.Metadata
{
    public class DatabaseTableColumn : IEquatable<DatabaseTableColumn>
    {
        private static Dictionary<string, string> dictSqlTypeNameCsTypeMap;
        private static Dictionary<string, string> dictSqlTypeNameCs20NullableTypeMap;
        private static Dictionary<string, string> dictSqlTypeNameCs11NullableTypeMap;
        private static Dictionary<string, string> dictSqlTypeNameCsConverterMap;
        private bool validationAndReplaceRequired;
        private Platform currentPlatform;
        private bool isNullable;
        private int tableColumnOrder;
        private bool isPrimaryKey;
        private bool isForeignKey;
        private string referedToColumnName;
        private string referedToTableName;
        private string sqlColumnName;
        private string sqlParamName;
        private string sqlTypeName;
        private string sqlCharLength;
        private string sqlCondensedTypeName;
        private string sqlNumericPrecisionAndScale;
        private string sqlParamNameWithCondensedType;
        private string sqlParamAssignToColumnText;
        private string sqlDefaultValue;
        private string csTypeName;
        private string csMemberName;
        private string csPropertyName;
        private string csConversionMethodName;
        private string csDefaultValue;

        public bool ValidationAndReplaceRequired
        {
            get
            {
                return this.validationAndReplaceRequired;
            }
        }

        public Platform CurrentPlatform
        {
            get
            {
                return this.currentPlatform;
            }
        }

        public bool IsNullable
        {
            get
            {
                return this.isNullable;
            }
        }

        public int TableColumnOrder
        {
            get
            {
                return this.tableColumnOrder;
            }
            set
            {
                this.tableColumnOrder = value;
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return this.isPrimaryKey;
            }
            set
            {
                this.isPrimaryKey = value;
            }
        }

        public bool IsForeignKey
        {
            get
            {
                return this.isForeignKey;
            }
            set
            {
                this.isForeignKey = value;
            }
        }

        public string ReferedToColumnName
        {
            get
            {
                return this.referedToColumnName;
            }
            set
            {
                this.referedToColumnName = value;
            }
        }

        public string ReferedToTableName
        {
            get
            {
                return this.referedToTableName;
            }
            set
            {
                this.referedToTableName = value;
            }
        }

        public bool NeedsInitialisation
        {
            get
            {
                if (!string.IsNullOrEmpty(this.SqlDefaultValue))
                    return true;
                if (this.currentPlatform == Platform.netFramework11)
                    return this.IsNullable;
                return false;
            }
        }

        public string SqlColumnName
        {
            get
            {
                return this.sqlColumnName;
            }
        }

        public string SqlParamName
        {
            get
            {
                return this.sqlParamName;
            }
        }

        public string SqlTypeName
        {
            get
            {
                return this.sqlTypeName;
            }
        }

        public string SqlCharLength
        {
            get
            {
                return this.sqlCharLength;
            }
        }

        public string SqlCondensedTypeName
        {
            get
            {
                return this.sqlCondensedTypeName;
            }
        }

        public string SqlNumericPrecisionAndScale
        {
            get
            {
                return this.sqlNumericPrecisionAndScale;
            }
        }

        public string SqlParamNameWithCondensedType
        {
            get
            {
                return this.sqlParamNameWithCondensedType;
            }
        }

        public string SqlParamAssignToColumnText
        {
            get
            {
                return this.sqlParamAssignToColumnText;
            }
        }

        public string SqlDefaultValue
        {
            get
            {
                return this.sqlDefaultValue;
            }
        }

        public string CsTypeName
        {
            get
            {
                return this.csTypeName;
            }
        }

        public string CSMemberName
        {
            get
            {
                return this.csMemberName;
            }
        }

        public string CSPropertyName
        {
            get
            {
                return this.csPropertyName;
            }
        }

        public string CSConversionMethodName
        {
            get
            {
                return this.csConversionMethodName;
            }
        }

        public string CsDefaultValue
        {
            get
            {
                return this.csDefaultValue;
            }
        }

        static DatabaseTableColumn()
        {
            DatabaseTableColumn.LoadTypeMapXmlResource();
        }

        public DatabaseTableColumn(Platform platform, bool validationAndReplaceRequired, bool isNullable, string name, string sqlTypeName, int? sqlCharLength, string sqlDefaultValue, byte? nNumericPrecision, int? nNumericScale)
        {
            this.currentPlatform = platform;
            this.validationAndReplaceRequired = validationAndReplaceRequired;
            this.isNullable = isNullable;
            this.sqlColumnName = name;
            this.sqlTypeName = sqlTypeName;
            this.SetCharLength(sqlCharLength);
            byte? nullable = nNumericPrecision;
            this.SetSqlNumericPrecisionAndScale(nullable.HasValue ? new int?((int)nullable.GetValueOrDefault()) : new int?(), nNumericScale);
            this.sqlParamName = "@" + this.sqlColumnName;
            this.sqlCondensedTypeName = this.sqlTypeName + this.GetSqlTypeLengthOrPrecisionScaleText(this.SqlCharLength, this.SqlTypeName, this.SqlNumericPrecisionAndScale);
            this.sqlParamNameWithCondensedType = this.sqlParamName + " " + this.sqlCondensedTypeName;
            this.sqlParamAssignToColumnText = this.sqlColumnName + " = " + this.sqlParamName;
            this.sqlDefaultValue = sqlDefaultValue;
            this.csPropertyName = CsLanguage.GetCsSafeIdentifierName(name, validationAndReplaceRequired);
            this.csMemberName = "_" + this.csPropertyName;
            this.SetCsTypeNameBasedOnSqlTypeName();
            this.SetConversionMethodNameBasedOnSqlTypeName();
            if (!this.NeedsInitialisation)
                return;
            this.SetCsDefaultValueBasedOnSqlDefaultType();
        }

        private void SetSqlNumericPrecisionAndScale(int? nNumericPrecision, int? nNumericScale)
        {
            if (!nNumericPrecision.HasValue || !nNumericScale.HasValue || nNumericScale.Value == 0)
                return;
            this.sqlNumericPrecisionAndScale = "(" + nNumericPrecision.Value.ToString() + "," + nNumericScale.Value.ToString() + ")";
        }

        private void SetCharLength(int? nSqlCharLength)
        {
            if (!nSqlCharLength.HasValue)
                return;
            if (nSqlCharLength.Value == -1)
                this.sqlCharLength = "MAX";
            else
                this.sqlCharLength = nSqlCharLength.Value.ToString();
        }

        private static void LoadTypeMapXmlResource()
        {
            DatabaseTableColumn.dictSqlTypeNameCsTypeMap = new Dictionary<string, string>();
            DatabaseTableColumn.dictSqlTypeNameCs20NullableTypeMap = new Dictionary<string, string>();
            DatabaseTableColumn.dictSqlTypeNameCs11NullableTypeMap = new Dictionary<string, string>();
            DatabaseTableColumn.dictSqlTypeNameCsConverterMap = new Dictionary<string, string>();
            XmlTextReader xmlTextReader = new XmlTextReader((TextReader)new StringReader(Resources.Mappings));
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlTextReader.Read();
            xmlTextReader.Read();
            while (xmlTextReader.Read() && xmlTextReader.IsStartElement())
            {
                xmlTextReader.Read();
                string key = xmlTextReader.ReadElementString("SqlType");
                DatabaseTableColumn.dictSqlTypeNameCsTypeMap.Add(key, xmlTextReader.ReadElementString("CsType"));
                DatabaseTableColumn.dictSqlTypeNameCs20NullableTypeMap.Add(key, xmlTextReader.ReadElementString("Cs20NullableType"));
                DatabaseTableColumn.dictSqlTypeNameCs11NullableTypeMap.Add(key, xmlTextReader.ReadElementString("Cs11NullableType"));
                DatabaseTableColumn.dictSqlTypeNameCsConverterMap.Add(key, xmlTextReader.ReadElementString("CsConverter"));
            }
        }

        private void SetCsTypeNameBasedOnSqlTypeName()
        {
            if (this.isNullable && this.currentPlatform == Platform.netFramework20)
                this.csTypeName = DatabaseTableColumn.dictSqlTypeNameCs20NullableTypeMap[this.sqlTypeName];
            if (this.isNullable && this.currentPlatform == Platform.netFramework11)
                this.csTypeName = DatabaseTableColumn.dictSqlTypeNameCs11NullableTypeMap[this.sqlTypeName];
            if (this.isNullable)
                return;
            this.csTypeName = DatabaseTableColumn.dictSqlTypeNameCsTypeMap[this.sqlTypeName];
        }

        private void SetConversionMethodNameBasedOnSqlTypeName()
        {
            this.csConversionMethodName = DatabaseTableColumn.dictSqlTypeNameCsConverterMap[this.sqlTypeName];
        }

        private void SetCsDefaultValueBasedOnSqlDefaultType()
        {
            if (this.sqlDefaultValue.ToLower(new CultureInfo("en-US")).Contains("newid()"))
                this.csDefaultValue = "Guid.NewGuid().ToString()";
            else if (this.sqlDefaultValue.Contains("getdate()") || this.sqlDefaultValue.Contains("GETDATE()"))
            {
                this.csDefaultValue = "DateTime.Now";
            }
            else
            {
                string str = this.sqlDefaultValue.Replace("(", "").Replace(")", "").Trim();
                if (this.CsTypeName == "string" && string.IsNullOrEmpty(str))
                {
                    this.csDefaultValue = "\"" + str + "\"";
                }
                else
                {
                    if (this.SqlTypeName.Contains("date") && !string.IsNullOrEmpty(str))
                        str = this.CSConversionMethodName + "(\"" + str + "\")";
                    if (this.SqlTypeName == "bit" && !string.IsNullOrEmpty(str))
                        str = str == "1" ? "true" : "false";
                    if (this.currentPlatform == Platform.netFramework11 && this.IsNullable)
                        this.sqlDefaultValue = "new " + this.CsTypeName + "(" + this.CSConversionMethodName + "(\"" + str + "\"))";
                    else
                        this.csDefaultValue = str;
                }
            }
        }

        public string GetMemberText(Platform platform)
        {
            return "private " + this.CsTypeName + " " + this.CSMemberName + (this.NeedsInitialisation ? " = " + this.CsDefaultValue : "") + ";";
        }

        public string GetPropertyText(Platform platform)
        {
            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine("        " + this.GetMemberText(platform));
            stringBuilder.AppendLine("        public " + this.CsTypeName + " " + this.CSPropertyName + " { get; set; }");
            //stringBuilder.Append(" { get; set; }");
            //stringBuilder.AppendLine("            get { return this." + this.CSMemberName + "; }");
            //stringBuilder.AppendLine("            set { this." + this.CSMemberName + " = value; }");
            //stringBuilder.AppendLine("        }");
            return stringBuilder.ToString();
        }

        public string GetSqlTypeLengthOrPrecisionScaleText(string parSqlCharLength, string parSqlTypeName, string parNumericPrecisionAndScale)
        {
            if (parSqlCharLength != null && parSqlTypeName != "text" && parSqlTypeName != "ntext")
                return "(" + parSqlCharLength + ")";
            if (!string.IsNullOrEmpty(parNumericPrecisionAndScale))
                return parNumericPrecisionAndScale;
            return "";
        }

        public bool Equals(DatabaseTableColumn other)
        {
            return this.sqlColumnName == other.SqlColumnName;
        }
    }
}
