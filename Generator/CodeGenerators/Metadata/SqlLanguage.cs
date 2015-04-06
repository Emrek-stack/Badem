using System.Collections.Generic;
using System.IO;
using System.Xml;
using Generator.Properties;

namespace Generator.CodeGenerators.Metadata
{
    public static class SqlLanguage
    {
        public static List<string> SQLKeywordList;

        static SqlLanguage()
        {
            SqlLanguage.InitSQLKeywordListWithXmlresource();
        }

        private static void InitSQLKeywordListWithXmlresource()
        {
            SqlLanguage.SQLKeywordList = new List<string>();
            XmlTextReader xmlTextReader = new XmlTextReader((TextReader)new StringReader(Resources.SQLKeywords));
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlTextReader.Read();
            xmlTextReader.Read();
            xmlTextReader.Read();
            do
            {
                SqlLanguage.SQLKeywordList.Add(xmlTextReader.ReadElementString("Keyword"));
            }
            while (xmlTextReader.ReadToNextSibling("Keyword"));
        }

        public static string GetSqlSafeTableOrColumnName(string szOriginalName, bool checkForKeyword)
        {
            return "[" + szOriginalName.Trim().Replace("(", "").Replace(")", "").Replace("]", "").Replace("[", "") + "]";
        }
    }
}