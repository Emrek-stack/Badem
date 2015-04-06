// Decompiled with JetBrains decompiler
// Type: SharpGenerator.CommonMethods
// Assembly: SharpGenerator 1.0.0.10, Version=1.0.0.9, Culture=neutral, PublicKeyToken=9bbaf80d42f25446
// MVID: 5B815D8B-5A31-4D34-97F8-7714676DA481
// Assembly location: D:\Tencere\SharpGenerator 1.0.0.10 - \SharpGenerator 1.0.0.10 - .exe

namespace Generator
{
    public static class CommonMethods
    {
        public static string AntiSqlInjection(string suspect)
        {
            return suspect.Replace("'", "").Replace(";", "");
        }
    }
}
