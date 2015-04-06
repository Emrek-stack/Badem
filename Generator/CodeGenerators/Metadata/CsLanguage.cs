using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Generator.Properties;

namespace Generator.CodeGenerators.Metadata
{
    public static class CsLanguage
    {
        public static char[] SqlValidCSharpInvalidIdentifierChars = new char[5]
    {
      '(',
      ')',
      '[',
      ']',
      ' '
    };
        public static List<string> CSharpKeywordList;

        static CsLanguage()
        {
            CsLanguage.InitCsKeywordListWithXmlresource();
        }

        private static void InitCsKeywordListWithXmlresource()
        {
            CsLanguage.CSharpKeywordList = new List<string>();
            XmlTextReader xmlTextReader = new XmlTextReader((TextReader)new StringReader(Resources.CSharpKeywords));
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            xmlTextReader.Read();
            xmlTextReader.Read();
            xmlTextReader.Read();
            do
            {
                CsLanguage.CSharpKeywordList.Add(xmlTextReader.ReadElementString("Keyword"));
            }
            while (xmlTextReader.ReadToNextSibling("Keyword"));
        }

        public static string GetCsSafeIdentifierName(string szOriginalName, bool checkForKeyword)
        {
            StringBuilder stringBuilder = new StringBuilder(szOriginalName);
            stringBuilder.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("]", "").Replace("[", "").Replace(" ", "").Replace(";", "").Replace(":", "").Replace("\\", "").Replace("/", "").Replace(",", "").Replace("\"", "").Replace("?", "").Replace("!", "").Replace("ı", "i").Replace("ğ", "g").Replace("ş", "s").Replace("ç", "c").Replace("ü", "u").Replace("ö", "o");
            string str = stringBuilder.ToString().Substring(0, 1).ToUpper().Replace("İ", "I") + stringBuilder.ToString().Remove(0, 1);
            if (checkForKeyword && CsLanguage.CSharpKeywordList.Contains(str))
                return str + "_";
            return str;
        }

        private static void InitCsKeywordList()
        {
            CsLanguage.CSharpKeywordList = new List<string>();
            CsLanguage.CSharpKeywordList.Add("abstract");
            CsLanguage.CSharpKeywordList.Add("as");
            CsLanguage.CSharpKeywordList.Add("base");
            CsLanguage.CSharpKeywordList.Add("bool");
            CsLanguage.CSharpKeywordList.Add("break");
            CsLanguage.CSharpKeywordList.Add("byte");
            CsLanguage.CSharpKeywordList.Add("case");
            CsLanguage.CSharpKeywordList.Add("catch");
            CsLanguage.CSharpKeywordList.Add("char");
            CsLanguage.CSharpKeywordList.Add("checked");
            CsLanguage.CSharpKeywordList.Add("class");
            CsLanguage.CSharpKeywordList.Add("const");
            CsLanguage.CSharpKeywordList.Add("continue");
            CsLanguage.CSharpKeywordList.Add("decimal");
            CsLanguage.CSharpKeywordList.Add("default");
            CsLanguage.CSharpKeywordList.Add("delegate");
            CsLanguage.CSharpKeywordList.Add("do");
            CsLanguage.CSharpKeywordList.Add("double");
            CsLanguage.CSharpKeywordList.Add("else");
            CsLanguage.CSharpKeywordList.Add("enum");
            CsLanguage.CSharpKeywordList.Add("event");
            CsLanguage.CSharpKeywordList.Add("explicit");
            CsLanguage.CSharpKeywordList.Add("extern");
            CsLanguage.CSharpKeywordList.Add("false");
            CsLanguage.CSharpKeywordList.Add("finally");
            CsLanguage.CSharpKeywordList.Add("fixed");
            CsLanguage.CSharpKeywordList.Add("float");
            CsLanguage.CSharpKeywordList.Add("for");
            CsLanguage.CSharpKeywordList.Add("foreach");
            CsLanguage.CSharpKeywordList.Add("goto");
            CsLanguage.CSharpKeywordList.Add("if");
            CsLanguage.CSharpKeywordList.Add("implicit");
            CsLanguage.CSharpKeywordList.Add("in");
            CsLanguage.CSharpKeywordList.Add("int");
            CsLanguage.CSharpKeywordList.Add("interface");
            CsLanguage.CSharpKeywordList.Add("internal");
            CsLanguage.CSharpKeywordList.Add("is");
            CsLanguage.CSharpKeywordList.Add("lock");
            CsLanguage.CSharpKeywordList.Add("long");
            CsLanguage.CSharpKeywordList.Add("namespace");
            CsLanguage.CSharpKeywordList.Add("new");
            CsLanguage.CSharpKeywordList.Add("null");
            CsLanguage.CSharpKeywordList.Add("object");
            CsLanguage.CSharpKeywordList.Add("operator");
            CsLanguage.CSharpKeywordList.Add("out");
            CsLanguage.CSharpKeywordList.Add("override");
            CsLanguage.CSharpKeywordList.Add("params");
            CsLanguage.CSharpKeywordList.Add("private");
            CsLanguage.CSharpKeywordList.Add("protected");
            CsLanguage.CSharpKeywordList.Add("public");
            CsLanguage.CSharpKeywordList.Add("readonly");
            CsLanguage.CSharpKeywordList.Add("ref");
            CsLanguage.CSharpKeywordList.Add("return");
            CsLanguage.CSharpKeywordList.Add("sbyte");
            CsLanguage.CSharpKeywordList.Add("sealed");
            CsLanguage.CSharpKeywordList.Add("this");
            CsLanguage.CSharpKeywordList.Add("throw");
            CsLanguage.CSharpKeywordList.Add("true");
            CsLanguage.CSharpKeywordList.Add("try");
            CsLanguage.CSharpKeywordList.Add("typeof");
            CsLanguage.CSharpKeywordList.Add("uint");
            CsLanguage.CSharpKeywordList.Add("ulong");
            CsLanguage.CSharpKeywordList.Add("unchecked");
            CsLanguage.CSharpKeywordList.Add("unsafe");
            CsLanguage.CSharpKeywordList.Add("ushort");
            CsLanguage.CSharpKeywordList.Add("using");
            CsLanguage.CSharpKeywordList.Add("short");
            CsLanguage.CSharpKeywordList.Add("sizeof");
            CsLanguage.CSharpKeywordList.Add("stackallac");
            CsLanguage.CSharpKeywordList.Add("static");
            CsLanguage.CSharpKeywordList.Add("string");
            CsLanguage.CSharpKeywordList.Add("struct");
            CsLanguage.CSharpKeywordList.Add("switch");
            CsLanguage.CSharpKeywordList.Add("virtual");
            CsLanguage.CSharpKeywordList.Add("void");
        }
    }
}
