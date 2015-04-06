using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Generator
{
    public class UcDuplicateDestroyer : DatabaseUtility
    {
        private int pageSize = 1000;
        private ulong totalCount;
        private int pageNumber;
        private string DeleteDuplicatesCommandText;
        private List<string> lstszOrderBys;
        private string szCommand;
        private string[] aszDuplicateColumns;
        private List<string> lstszColumnValues;
        private List<long> lstlDuplicateRecordPrimaryKeys;
        private string szDuplicateIDs;
        private OutputFormat outputFormat;
        private IContainer components;
        private GroupBox gbxTable;
        private Label label8;
        private Button btnSaveDuplicates;
        private TextBox txtPrimaryKey;
        private Label label5;
        private Label label4;
        private Button btnDeleteDuplicates;
        private TextBox txtOrderBy;
        private TextBox txtDuplicateColumns;
        private Label label6;
        private Button btnListDuplicates;
        private TextBox txtTableName;
        private TextBox txtOutput;
        private DataGridView dgvDuplicates;
        private SplitContainer splitContainer1;
        private BackgroundWorker bwDataFetcher;
        private Label lblLeftInfo;
        private Label lblProcessesInfo;
        private Label lblLeft;
        private Label lblProcessed;
        private Label lblTotalInfo;
        private Label lblTotal;

        public string TableName
        {
            get
            {
                return this.txtTableName.Text.Trim().Replace("'", "").Replace(";", "");
            }
        }

        public string PrimaryKey
        {
            get
            {
                return this.txtPrimaryKey.Text.Trim().Replace("'", "").Replace(";", "");
            }
        }

        public ulong TotalCount
        {
            get
            {
                return this.totalCount;
            }
        }

        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
        }

        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }
        }

        public OutputFormat OutputFormat
        {
            get
            {
                return this.outputFormat;
            }
            set
            {
                switch (value)
                {
                    case OutputFormat.Error:
                        this.txtOutput.BackColor = Color.RosyBrown;
                        this.txtOutput.ForeColor = Color.White;
                        break;
                    case OutputFormat.Normal:
                        this.txtOutput.BackColor = SystemColors.Window;
                        this.txtOutput.ForeColor = SystemColors.WindowText;
                        break;
                }
                this.outputFormat = value;
            }
        }

        public UcDuplicateDestroyer()
        {
            this.InitializeComponent();
        }

        private string GetTotalRecordCountQuery(string tableName, string primaryKey)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(" SELECT");
            stringBuilder.AppendLine("     COUNT(DISTINCT " + primaryKey + ") AS 'TotalCount'");
            stringBuilder.AppendLine(" FROM " + tableName);
            return stringBuilder.ToString();
        }

        private string GetRecordPageQuery(int currentPageNumber, int currentPageSize, string tableName, string primaryKey)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(" SELECT TOP(" + (object)(currentPageNumber * currentPageSize) + ")");
            stringBuilder.AppendLine("     ROW_COUNT() OVER (ORDER BY " + primaryKey + " ASC) AS 'TotalCount',");
            foreach (string str in this.aszDuplicateColumns)
                stringBuilder.AppendLine("     " + str + ",");
            stringBuilder.AppendLine("     " + primaryKey);
            stringBuilder.AppendLine(" FROM " + tableName);
            return stringBuilder.ToString();
        }

        private string GetDeleteDuplicatesCommandText()
        {
            if (string.IsNullOrEmpty(this.TableName) || string.IsNullOrEmpty(this.PrimaryKey) || string.IsNullOrEmpty(this.szDuplicateIDs))
                throw new Exception(this.messageTextProvider.GetMessage(MessageTypes.IncompleteDeleteDuplicateInfo));
            return "DELETE FROM " + this.TableName + " WHERE " + this.PrimaryKey + " IN (" + this.szDuplicateIDs + ")";
        }

        private string GetListDuplicateCommandText()
        {
            if (string.IsNullOrEmpty(this.TableName))
                throw new Exception(this.messageTextProvider.GetMessage(MessageTypes.IncompleteDuplicateInfo));
            this.szCommand = "SELECT * FROM " + this.TableName;
            this.lstszOrderBys = new List<string>();
            if (!string.IsNullOrEmpty(this.txtOrderBy.Text))
            {
                this.lstszOrderBys.AddRange((IEnumerable<string>)this.txtOrderBy.Text.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries));
                if (this.lstszOrderBys.Count != 0)
                {
                    this.szCommand += " ORDER BY ";
                    for (int index = 0; index < this.lstszOrderBys.Count; ++index)
                    {
                        if (index == this.lstszOrderBys.Count - 1)
                        {
                            UcDuplicateDestroyer duplicateDestroyer = this;
                            string str = duplicateDestroyer.szCommand + this.lstszOrderBys[index].Trim() + " ASC";
                            duplicateDestroyer.szCommand = str;
                        }
                        else
                        {
                            UcDuplicateDestroyer duplicateDestroyer = this;
                            string str = duplicateDestroyer.szCommand + this.lstszOrderBys[index].Trim() + " ASC ,";
                            duplicateDestroyer.szCommand = str;
                        }
                    }
                }
            }
            return this.szCommand;
        }

        private void btnListDuplicates_Click(object sender, EventArgs e)
        {
            this.OutputFormat = OutputFormat.Normal;
            try
            {
                using (IDataReader reader = this.GetDbCommandReady(this.GetListDuplicateCommandText()).ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        DataTable dataTable = this.GetDataTable(reader);
                        this.lstlDuplicateRecordPrimaryKeys = new List<long>();
                        this.lstszColumnValues = new List<string>();
                        this.aszDuplicateColumns = this.txtDuplicateColumns.Text.Split(',');
                        do
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (string str in this.aszDuplicateColumns)
                                stringBuilder.Append(reader[str.Trim()].ToString());
                            if (!this.lstszColumnValues.Contains(stringBuilder.ToString()))
                            {
                                this.lstszColumnValues.Add(stringBuilder.ToString());
                            }
                            else
                            {
                                this.lstlDuplicateRecordPrimaryKeys.Add(Convert.ToInt64(reader[this.PrimaryKey]));
                                DataRow row = dataTable.NewRow();
                                for (int i = 0; i < dataTable.Columns.Count; ++i)
                                    row[i] = reader.GetValue(i);
                                dataTable.Rows.Add(row);
                            }
                        }
                        while (reader.Read());
                        this.dgvDuplicates.DataSource = (object)dataTable;
                        StringBuilder stringBuilder1 = new StringBuilder();
                        foreach (long num in this.lstlDuplicateRecordPrimaryKeys)
                            stringBuilder1.Append(num.ToString() + ", ");
                        this.szDuplicateIDs = stringBuilder1.ToString();
                        if (this.szDuplicateIDs.Length != 0)
                        {
                            this.txtOutput.Text = this.szDuplicateIDs = this.szDuplicateIDs.Remove(this.szDuplicateIDs.Length - 2);
                        }
                        else
                        {
                            int num1 = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.NoDuplicates), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        int num2 = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.NoRecordInTable), "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    }
                }
            }
            catch (SqlException ex)
            {
                this.txtOutput.Text = ex.Message;
                this.OutputFormat = OutputFormat.Error;
            }
        }

        private void btnDeleteDuplicates_Click(object sender, EventArgs e)
        {
            this.OutputFormat = OutputFormat.Normal;
            try
            {
                using (IDbCommand dbCommandReady = this.GetDbCommandReady(this.DeleteDuplicatesCommandText))
                {
                    this.txtOutput.Text = DateTime.Now.ToString() + ": " + dbCommandReady.ExecuteNonQuery().ToString() + " row(s) effected.";
                    dbCommandReady.Connection.Close();
                }
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.messageTextProvider.GetMessage(MessageTypes.Error), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                this.txtOutput.Text = ex.Message;
                this.OutputFormat = OutputFormat.Error;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSaveDuplicates_Click(object sender, EventArgs e)
        {
            try
            {
                int num = (int)MessageBox.Show("Coming Soon! - Berke Sokhan");
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gbxTable = new GroupBox();
            this.splitContainer1 = new SplitContainer();
            this.txtOutput = new TextBox();
            this.dgvDuplicates = new DataGridView();
            this.label8 = new Label();
            this.btnSaveDuplicates = new Button();
            this.txtPrimaryKey = new TextBox();
            this.label5 = new Label();
            this.label4 = new Label();
            this.btnDeleteDuplicates = new Button();
            this.txtOrderBy = new TextBox();
            this.txtDuplicateColumns = new TextBox();
            this.label6 = new Label();
            this.btnListDuplicates = new Button();
            this.txtTableName = new TextBox();
            this.bwDataFetcher = new BackgroundWorker();
            this.lblTotal = new Label();
            this.lblTotalInfo = new Label();
            this.lblProcessed = new Label();
            this.lblLeft = new Label();
            this.lblProcessesInfo = new Label();
            this.lblLeftInfo = new Label();
            this.gbxTable.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize)this.dgvDuplicates).BeginInit();
            this.SuspendLayout();
            this.gbxTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.gbxTable.Controls.Add((Control)this.lblLeftInfo);
            this.gbxTable.Controls.Add((Control)this.lblProcessesInfo);
            this.gbxTable.Controls.Add((Control)this.lblLeft);
            this.gbxTable.Controls.Add((Control)this.lblProcessed);
            this.gbxTable.Controls.Add((Control)this.lblTotalInfo);
            this.gbxTable.Controls.Add((Control)this.lblTotal);
            this.gbxTable.Controls.Add((Control)this.splitContainer1);
            this.gbxTable.Controls.Add((Control)this.label8);
            this.gbxTable.Controls.Add((Control)this.btnSaveDuplicates);
            this.gbxTable.Controls.Add((Control)this.txtPrimaryKey);
            this.gbxTable.Controls.Add((Control)this.label5);
            this.gbxTable.Controls.Add((Control)this.label4);
            this.gbxTable.Controls.Add((Control)this.btnDeleteDuplicates);
            this.gbxTable.Controls.Add((Control)this.txtOrderBy);
            this.gbxTable.Controls.Add((Control)this.txtDuplicateColumns);
            this.gbxTable.Controls.Add((Control)this.label6);
            this.gbxTable.Controls.Add((Control)this.btnListDuplicates);
            this.gbxTable.Controls.Add((Control)this.txtTableName);
            this.gbxTable.Location = new Point(9, 9);
            this.gbxTable.Name = "gbxTable";
            this.gbxTable.Size = new Size(727, 569);
            this.gbxTable.TabIndex = 5;
            this.gbxTable.TabStop = false;
            this.gbxTable.Text = "Duplicate Destroyer";
            this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.splitContainer1.Location = new Point(9, (int)sbyte.MaxValue);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add((Control)this.txtOutput);
            this.splitContainer1.Panel2.Controls.Add((Control)this.dgvDuplicates);
            this.splitContainer1.Size = new Size(712, 436);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 15;
            this.txtOutput.Dock = DockStyle.Fill;
            this.txtOutput.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)162);
            this.txtOutput.Location = new Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = ScrollBars.Vertical;
            this.txtOutput.Size = new Size(712, 218);
            this.txtOutput.TabIndex = 13;
            this.dgvDuplicates.AllowUserToAddRows = false;
            this.dgvDuplicates.AllowUserToOrderColumns = true;
            this.dgvDuplicates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicates.Dock = DockStyle.Fill;
            this.dgvDuplicates.Location = new Point(0, 0);
            this.dgvDuplicates.Name = "dgvDuplicates";
            this.dgvDuplicates.Size = new Size(712, 214);
            this.dgvDuplicates.TabIndex = 14;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(6, 48);
            this.label8.Name = "label8";
            this.label8.Size = new Size(100, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Primary Key Column";
            this.btnSaveDuplicates.Location = new Point(309, 91);
            this.btnSaveDuplicates.Name = "btnSaveDuplicates";
            this.btnSaveDuplicates.Size = new Size(106, 30);
            this.btnSaveDuplicates.TabIndex = 13;
            this.btnSaveDuplicates.Text = "Save IDs";
            this.btnSaveDuplicates.UseVisualStyleBackColor = true;
            this.btnSaveDuplicates.Click += new EventHandler(this.btnSaveDuplicates_Click);
            this.txtPrimaryKey.Location = new Point(124, 45);
            this.txtPrimaryKey.Name = "txtPrimaryKey";
            this.txtPrimaryKey.Size = new Size(179, 20);
            this.txtPrimaryKey.TabIndex = 5;
            this.txtPrimaryKey.Text = "AdvertLabelID";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(6, 102);
            this.label5.Name = "label5";
            this.label5.Size = new Size(112, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "ORDER BY Column(s)";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new Size(101, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Duplicate Column(s)";
            this.btnDeleteDuplicates.Location = new Point(310, 55);
            this.btnDeleteDuplicates.Name = "btnDeleteDuplicates";
            this.btnDeleteDuplicates.Size = new Size(106, 30);
            this.btnDeleteDuplicates.TabIndex = 9;
            this.btnDeleteDuplicates.Text = "Delete Duplicates";
            this.btnDeleteDuplicates.UseVisualStyleBackColor = true;
            this.btnDeleteDuplicates.Click += new EventHandler(this.btnDeleteDuplicates_Click);
            this.txtOrderBy.Location = new Point(124, 99);
            this.txtOrderBy.Name = "txtOrderBy";
            this.txtOrderBy.Size = new Size(179, 20);
            this.txtOrderBy.TabIndex = 7;
            this.txtOrderBy.Text = "AdvertLabelID";
            this.txtDuplicateColumns.Location = new Point(124, 73);
            this.txtDuplicateColumns.Name = "txtDuplicateColumns";
            this.txtDuplicateColumns.Size = new Size(179, 20);
            this.txtDuplicateColumns.TabIndex = 6;
            this.txtDuplicateColumns.Text = "AdvertID,LabelID";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new Size(65, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Table Name";
            this.btnListDuplicates.Location = new Point(310, 19);
            this.btnListDuplicates.Name = "btnListDuplicates";
            this.btnListDuplicates.Size = new Size(106, 30);
            this.btnListDuplicates.TabIndex = 8;
            this.btnListDuplicates.Text = "List Duplicates";
            this.btnListDuplicates.UseVisualStyleBackColor = true;
            this.btnListDuplicates.Click += new EventHandler(this.btnListDuplicates_Click);
            this.txtTableName.Location = new Point(124, 19);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new Size(179, 20);
            this.txtTableName.TabIndex = 4;
            this.txtTableName.Text = "AdvertLabel";
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new Point(433, 22);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(34, 13);
            this.lblTotal.TabIndex = 16;
            this.lblTotal.Text = "Total:";
            this.lblTotalInfo.AutoSize = true;
            this.lblTotalInfo.Location = new Point(549, 22);
            this.lblTotalInfo.Name = "lblTotalInfo";
            this.lblTotalInfo.Size = new Size(13, 13);
            this.lblTotalInfo.TabIndex = 17;
            this.lblTotalInfo.Text = "0";
            this.lblProcessed.AutoSize = true;
            this.lblProcessed.Location = new Point(433, 47);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new Size(60, 13);
            this.lblProcessed.TabIndex = 18;
            this.lblProcessed.Text = "Processed:";
            this.lblLeft.AutoSize = true;
            this.lblLeft.Location = new Point(433, 72);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new Size(28, 13);
            this.lblLeft.TabIndex = 19;
            this.lblLeft.Text = "Left:";
            this.lblProcessesInfo.AutoSize = true;
            this.lblProcessesInfo.Location = new Point(549, 48);
            this.lblProcessesInfo.Name = "lblProcessesInfo";
            this.lblProcessesInfo.Size = new Size(13, 13);
            this.lblProcessesInfo.TabIndex = 20;
            this.lblProcessesInfo.Text = "0";
            this.lblLeftInfo.AutoSize = true;
            this.lblLeftInfo.Location = new Point(549, 72);
            this.lblLeftInfo.Name = "lblLeftInfo";
            this.lblLeftInfo.Size = new Size(13, 13);
            this.lblLeftInfo.TabIndex = 21;
            this.lblLeftInfo.Text = "0";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.gbxTable);
            this.Name = "UcDuplicateDestroyer";
            this.Size = new Size(747, 589);
            this.gbxTable.ResumeLayout(false);
            this.gbxTable.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((ISupportInitialize)this.dgvDuplicates).EndInit();
            this.ResumeLayout(false);
        }
    }
}
