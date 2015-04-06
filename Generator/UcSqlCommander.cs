using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Generator
{
    public class UcSqlCommander : DatabaseUtility
    {
        private IContainer components;
        private SplitContainer splcntQueryAndResults;
        private GroupBox gbxQuery;
        private SplitContainer splcntExecuteQuery;
        private TextBox txtQuery;
        private Button btnExecuteQuery;
        private TextBox txtOutput;
        private DataGridView dgvDuplicates;
        private OutputFormat outputFormat;

        private string QueryCommandText
        {
            get
            {
                if (string.IsNullOrEmpty(this.txtQuery.Text))
                    throw new Exception(this.messageTextProvider.GetMessage(MessageTypes.IncompleteQuery));
                return this.txtQuery.Text.Trim();
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

        public UcSqlCommander()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splcntQueryAndResults = new SplitContainer();
            this.gbxQuery = new GroupBox();
            this.splcntExecuteQuery = new SplitContainer();
            this.txtQuery = new TextBox();
            this.btnExecuteQuery = new Button();
            this.txtOutput = new TextBox();
            this.dgvDuplicates = new DataGridView();
            this.splcntQueryAndResults.Panel1.SuspendLayout();
            this.splcntQueryAndResults.Panel2.SuspendLayout();
            this.splcntQueryAndResults.SuspendLayout();
            this.gbxQuery.SuspendLayout();
            this.splcntExecuteQuery.Panel1.SuspendLayout();
            this.splcntExecuteQuery.Panel2.SuspendLayout();
            this.splcntExecuteQuery.SuspendLayout();
            ((ISupportInitialize)this.dgvDuplicates).BeginInit();
            this.SuspendLayout();
            this.splcntQueryAndResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.splcntQueryAndResults.Location = new Point(12, 4);
            this.splcntQueryAndResults.Name = "splcntQueryAndResults";
            this.splcntQueryAndResults.Orientation = Orientation.Horizontal;
            this.splcntQueryAndResults.Panel1.Controls.Add((Control)this.gbxQuery);
            this.splcntQueryAndResults.Panel2.Controls.Add((Control)this.dgvDuplicates);
            this.splcntQueryAndResults.Size = new Size(747, 558);
            this.splcntQueryAndResults.SplitterDistance = 261;
            this.splcntQueryAndResults.TabIndex = 19;
            this.gbxQuery.Controls.Add((Control)this.splcntExecuteQuery);
            this.gbxQuery.Dock = DockStyle.Fill;
            this.gbxQuery.Location = new Point(0, 0);
            this.gbxQuery.Name = "gbxQuery";
            this.gbxQuery.Size = new Size(747, 261);
            this.gbxQuery.TabIndex = 15;
            this.gbxQuery.TabStop = false;
            this.gbxQuery.Text = "Execute Query at Server";
            this.splcntExecuteQuery.Dock = DockStyle.Fill;
            this.splcntExecuteQuery.Location = new Point(3, 16);
            this.splcntExecuteQuery.Name = "splcntExecuteQuery";
            this.splcntExecuteQuery.Orientation = Orientation.Horizontal;
            this.splcntExecuteQuery.Panel1.Controls.Add((Control)this.txtQuery);
            this.splcntExecuteQuery.Panel1.Controls.Add((Control)this.btnExecuteQuery);
            this.splcntExecuteQuery.Panel2.Controls.Add((Control)this.txtOutput);
            this.splcntExecuteQuery.Size = new Size(741, 242);
            this.splcntExecuteQuery.SplitterDistance = 119;
            this.splcntExecuteQuery.TabIndex = 15;
            this.txtQuery.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtQuery.Font = new Font("Courier New", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte)162);
            this.txtQuery.Location = new Point(3, 6);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = ScrollBars.Vertical;
            this.txtQuery.Size = new Size(604, 110);
            this.txtQuery.TabIndex = 10;
            this.btnExecuteQuery.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnExecuteQuery.Location = new Point(613, 6);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new Size(106, 41);
            this.btnExecuteQuery.TabIndex = 11;
            this.btnExecuteQuery.Text = "Execute Query";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new EventHandler(this.btnExecuteQuery_Click);
            this.txtOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.txtOutput.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)162);
            this.txtOutput.Location = new Point(3, 3);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = ScrollBars.Vertical;
            this.txtOutput.Size = new Size(604, 113);
            this.txtOutput.TabIndex = 12;
            this.dgvDuplicates.AllowUserToAddRows = false;
            this.dgvDuplicates.AllowUserToOrderColumns = true;
            this.dgvDuplicates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDuplicates.Dock = DockStyle.Fill;
            this.dgvDuplicates.Location = new Point(0, 0);
            this.dgvDuplicates.Name = "dgvDuplicates";
            this.dgvDuplicates.Size = new Size(747, 293);
            this.dgvDuplicates.TabIndex = 13;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.splcntQueryAndResults);
            this.Name = "ucSqlCommander";
            this.Size = new Size(770, 577);
            this.splcntQueryAndResults.Panel1.ResumeLayout(false);
            this.splcntQueryAndResults.Panel2.ResumeLayout(false);
            this.splcntQueryAndResults.ResumeLayout(false);
            this.gbxQuery.ResumeLayout(false);
            this.splcntExecuteQuery.Panel1.ResumeLayout(false);
            this.splcntExecuteQuery.Panel1.PerformLayout();
            this.splcntExecuteQuery.Panel2.ResumeLayout(false);
            this.splcntExecuteQuery.Panel2.PerformLayout();
            this.splcntExecuteQuery.ResumeLayout(false);
            ((ISupportInitialize)this.dgvDuplicates).EndInit();
            this.ResumeLayout(false);
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            this.OutputFormat = OutputFormat.Normal;
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                StringBuilder stringBuilder = new StringBuilder();
                using (IDataReader reader = this.GetDbCommandReady(this.QueryCommandText).ExecuteReader())
                {
                    stringBuilder.AppendLine("RESULTSET: ");
                    do
                    {
                        stringBuilder.AppendLine("Records Affected: " + reader.RecordsAffected.ToString());
                        stringBuilder.AppendLine("Field Count: " + reader.FieldCount.ToString());
                        DataTable dataTable = this.GetDataTable(reader);
                        if (reader.Read())
                        {
                            do
                            {
                                DataRow row = dataTable.NewRow();
                                for (int i = 0; i < dataTable.Columns.Count; ++i)
                                    row[i] = reader.GetValue(i);
                                dataTable.Rows.Add(row);
                            }
                            while (reader.Read());
                            this.dgvDuplicates.DataSource = (object)dataTable;
                        }
                    }
                    while (reader.NextResult());
                }
                stopwatch.Stop();
                stringBuilder.AppendLine("Processing Time: " + (object)stopwatch.ElapsedMilliseconds + "ms");
                this.txtOutput.Text = stringBuilder.ToString();
            }
            catch (SqlException ex)
            {
                this.txtOutput.Text = ex.Message;
                this.OutputFormat = OutputFormat.Error;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.messageTextProvider.GetMessage(MessageTypes.Error), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
