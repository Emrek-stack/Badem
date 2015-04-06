using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Generator.CodeGenerators.Metadata
{
    public class DatabaseTableCollection
    {
        private readonly List<DatabaseTable> databaseTables;

        public List<DatabaseTable> DatabaseTables
        {
            get
            {
                return this.databaseTables;
            }
        }

        public DatabaseTableCollection(CommonGenerationOptions options, List<TreeNode> listTables, IDbConnection iDbConnection)
        {
            this.databaseTables = new List<DatabaseTable>();
            foreach (TreeNode szTableName in listTables)
                this.databaseTables.Add(new DatabaseTable(options, szTableName.Text, string.Empty, iDbConnection));
        }
    }
}
