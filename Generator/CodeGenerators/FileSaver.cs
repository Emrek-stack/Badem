using System.IO;

namespace Generator.CodeGenerators
{
    public class FileSaver
    {
        public void SaveFileAs(string szFileData, string szPath, string szFileName, string szFileExtension)
        {
            this.SaveFileAs(szFileData, szPath, szFileName, szFileExtension, false);
        }

        public void SaveFileAs(string szFileData, string szPath, string szFileName, string szFileExtension, bool versioned)
        {
            try
            {
                int num = 1;
                string path;
                if (versioned)
                {
                    do
                    {
                        path = szPath + "\\" + szFileName + "_" + num.ToString().PadLeft(3, '0') + "." + szFileExtension;
                        ++num;
                    }
                    while (File.Exists(path));
                }
                else
                    path = szPath + "\\" + szFileName + "." + szFileExtension;
                StreamWriter streamWriter = new StreamWriter(path, false);
                streamWriter.Write(szFileData);
                streamWriter.Close();
            }
            catch (IOException ex)
            {
            }
        }
    }
}
