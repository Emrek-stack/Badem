using System;
using System.Data;
using System.IO;
using System.Text;

namespace Generator.CodeGenerators.InfrastructureGenerator
{
    public class FileSystemAccess
    {
        public static string FileSystemSeparator
        {
            get
            {
                return "\\";
            }
        }

        public string WriteDataTableTabDelimeted(string path, DataTable data)
        {
            return this.WriteDataTableDelimited(path, "\t", data, false);
        }

        public string WriteDataTableSpaceDelimeted(string path, DataTable data)
        {
            return this.WriteDataTableDelimited(path, " ", data, false);
        }

        public string WriteDataTableDelimeted(string path, string delimiter, DataTable data)
        {
            return this.WriteDataTableDelimited(path, delimiter, data, false);
        }

        public string AppendDataTableTabDelimited(string path, DataTable data)
        {
            return this.WriteDataTableDelimited(path, "\t", data, true);
        }

        public string AppendDataTableSpaceDelimited(string path, DataTable data)
        {
            return this.WriteDataTableDelimited(path, " ", data, true);
        }

        public string AppendDataTableDelimited(string path, string delimiter, DataTable data)
        {
            return this.WriteDataTableDelimited(path, delimiter, data, true);
        }

        private string WriteDataTableDelimited(string path, string delimiter, DataTable data, bool append)
        {
            StreamWriter streamWriter = (StreamWriter)null;
            try
            {
                if (!append && File.Exists(path))
                    File.Delete(path);
                streamWriter = new StreamWriter(path);
                foreach (DataRowView dataRowView in data.DefaultView)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (DataColumn dataColumn in (InternalDataCollectionBase)data.Columns)
                    {
                        if (dataRowView[dataColumn.Ordinal] != DBNull.Value)
                            stringBuilder.Append(dataRowView[dataColumn.Ordinal].ToString() + delimiter);
                    }
                    streamWriter.WriteLine(stringBuilder.ToString());
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Hata Mesaji : " + ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
            }
            return "";
        }

        private bool CheckFileFolderExists(string filePath)
        {
            if (filePath.EndsWith(FileSystemAccess.FileSystemSeparator))
                return this.CheckFolderExists(filePath);
            return this.CheckFolderExists(filePath.Remove(filePath.LastIndexOf(FileSystemAccess.FileSystemSeparator)));
        }

        public bool CheckFolderExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }
    }
}
