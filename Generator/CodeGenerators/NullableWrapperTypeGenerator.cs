using System.Text;

namespace Generator.CodeGenerators
{
    public static class NullableWrapperTypeGenerator
    {
        public static string GetAllNullableClasses(string szNamespace)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using " + szNamespace + ".Infrastructure;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace " + szNamespace + ".Entity");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("byte"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("short"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("int"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("long"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("float"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("double"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("decimal"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("bool"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(NullableWrapperTypeGenerator.GetNullableClass("DateTime"));
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private static string GetNullableClass(string szType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("   public class Nullable" + szType + " : NullabletypeBase");
            stringBuilder.AppendLine("   {");
            stringBuilder.AppendLine("       private " + szType + " _Value;");
            stringBuilder.AppendLine("       public " + szType + " Value");
            stringBuilder.AppendLine("       {");
            stringBuilder.AppendLine("           get");
            stringBuilder.AppendLine("           {");
            stringBuilder.AppendLine("               if (this._HasValue)");
            stringBuilder.AppendLine("               {");
            stringBuilder.AppendLine("                  return this._Value;");
            stringBuilder.AppendLine("               }");
            stringBuilder.AppendLine("               else");
            stringBuilder.AppendLine("               {");
            stringBuilder.AppendLine("                   throw new InvalidOperationException(\"Value has not been set.\");");
            stringBuilder.AppendLine("               }");
            stringBuilder.AppendLine("           }");
            stringBuilder.AppendLine("           set");
            stringBuilder.AppendLine("           {");
            stringBuilder.AppendLine("               this._Value = value;");
            stringBuilder.AppendLine("               this._ObjectValue = value;");
            stringBuilder.AppendLine("               this._HasValue = true;");
            stringBuilder.AppendLine("           }");
            stringBuilder.AppendLine("       }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("       public static implicit operator Nullable" + szType + "(" + szType + " t)");
            stringBuilder.AppendLine("       {");
            stringBuilder.AppendLine("           Nullable" + szType + " n = new Nullable" + szType + "();");
            stringBuilder.AppendLine("           n.Value = t;");
            stringBuilder.AppendLine("           return n;");
            stringBuilder.AppendLine("       }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("       public static implicit operator " + szType + "(Nullable" + szType + " t)");
            stringBuilder.AppendLine("       {");
            stringBuilder.AppendLine("           return t.Value;");
            stringBuilder.AppendLine("       }");
            stringBuilder.AppendLine("   }");
            return stringBuilder.ToString();
        }

        public static string GetNullableAbstractBase(string szNamespace)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace " + szNamespace + ".Infrastructure");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("   public abstract class NullabletypeBase");
            stringBuilder.AppendLine("   {");
            stringBuilder.AppendLine("       protected bool _HasValue;");
            stringBuilder.AppendLine("       public bool HasValue");
            stringBuilder.AppendLine("       {");
            stringBuilder.AppendLine("           get { return this._HasValue; }");
            stringBuilder.AppendLine("       }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("       protected object _ObjectValue;");
            stringBuilder.AppendLine("       public object ValueBoxed");
            stringBuilder.AppendLine("       {");
            stringBuilder.AppendLine("           get");
            stringBuilder.AppendLine("           {");
            stringBuilder.AppendLine("               if (this._HasValue) return this._ObjectValue;");
            stringBuilder.AppendLine("               return null;");
            stringBuilder.AppendLine("           }");
            stringBuilder.AppendLine("       }");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("   }");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}
