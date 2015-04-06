
using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Generator
{
    public class UcSqlConnection : UserControl, IDbConnectionProvider, IMessageTextUser
    {
        private DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        private IContainer components;
        private GroupBox gbxServer;
        private Label label7;
        private TextBox txtDatabaseName;
        private Label label3;
        private TextBox txtUserName;
        private Label label2;
        private TextBox txtPassword;
        private Label label1;
        private TextBox txtServerName;
        private Label lblConnectionStateInfo;
        private Button btnTest;
        private bool blStringChanged;
        private IDbConnection _DbConnection;
        private IMessageTextProvider messageTextProvider;

        public IMessageTextProvider MessageTextProvider
        {
            get
            {
                return this.messageTextProvider;
            }
            set
            {
                this.messageTextProvider = value;
            }
        }

        public UcSqlConnection()
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
            this.gbxServer = new GroupBox();
            this.btnTest = new Button();
            this.lblConnectionStateInfo = new Label();
            this.label7 = new Label();
            this.txtDatabaseName = new TextBox();
            this.label3 = new Label();
            this.txtUserName = new TextBox();
            this.label2 = new Label();
            this.txtPassword = new TextBox();
            this.label1 = new Label();
            this.txtServerName = new TextBox();
            this.gbxServer.SuspendLayout();
            this.SuspendLayout();
            this.gbxServer.Anchor = AnchorStyles.None;
            this.gbxServer.Controls.Add((Control)this.btnTest);
            this.gbxServer.Controls.Add((Control)this.lblConnectionStateInfo);
            this.gbxServer.Controls.Add((Control)this.label7);
            this.gbxServer.Controls.Add((Control)this.txtDatabaseName);
            this.gbxServer.Controls.Add((Control)this.label3);
            this.gbxServer.Controls.Add((Control)this.txtUserName);
            this.gbxServer.Controls.Add((Control)this.label2);
            this.gbxServer.Controls.Add((Control)this.txtPassword);
            this.gbxServer.Controls.Add((Control)this.label1);
            this.gbxServer.Controls.Add((Control)this.txtServerName);
            this.gbxServer.Location = new Point(39, 42);
            this.gbxServer.Name = "gbxServer";
            this.gbxServer.Size = new Size(417, 163);
            this.gbxServer.TabIndex = 18;
            this.gbxServer.TabStop = false;
            this.gbxServer.Text = "MS SQL Server Info";
            this.btnTest.Location = new Point(101, 123);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new Size(75, 23);
            this.btnTest.TabIndex = 23;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new EventHandler(this.btnTest_Click);
            this.lblConnectionStateInfo.AutoSize = true;
            this.lblConnectionStateInfo.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Bold, GraphicsUnit.Point, (byte)162);
            this.lblConnectionStateInfo.Location = new Point(182, 126);
            this.lblConnectionStateInfo.Name = "lblConnectionStateInfo";
            this.lblConnectionStateInfo.Size = new Size(87, 17);
            this.lblConnectionStateInfo.TabIndex = 22;
            this.lblConnectionStateInfo.Text = "State : N/A";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(11, 100);
            this.label7.Name = "label7";
            this.label7.Size = new Size(84, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Database Name";
            this.txtDatabaseName.Location = new Point(101, 97);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new Size(302, 20);
            this.txtDatabaseName.TabIndex = 3;
            this.txtDatabaseName.Text = "BadeDB";
            this.txtDatabaseName.TextChanged += new EventHandler(this.txtDatabaseName_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(11, 48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "User";
            this.txtUserName.Location = new Point(101, 45);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new Size(302, 20);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "sa";
            this.txtUserName.TextChanged += new EventHandler(this.txtUserName_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(11, 76);
            this.label2.Name = "label2";
            this.label2.Size = new Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            this.txtPassword.Location = new Point(101, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(302, 20);
            this.txtPassword.TabIndex = 2;
            txtPassword.Text = "09764309";
            this.txtPassword.TextChanged += new EventHandler(this.txtPassword_TextChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Address";
            this.txtServerName.Location = new Point(101, 19);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new Size(302, 20);
            this.txtServerName.TabIndex = 0;
            this.txtServerName.Text = "(local)";
            this.txtServerName.TextChanged += new EventHandler(this.txtServerName_TextChanged);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.gbxServer);
            this.Name = "UcSqlConnection";
            this.Size = new Size(494, 246);
            this.gbxServer.ResumeLayout(false);
            this.gbxServer.PerformLayout();
            this.ResumeLayout(false);
        }

        public IDbConnection GetDbConnection()
        {
            if (this.blStringChanged || this._DbConnection == null)
            {
                string str = "server=" + CommonMethods.AntiSqlInjection(this.txtServerName.Text) + ";database=" + CommonMethods.AntiSqlInjection(this.txtDatabaseName.Text) + ";" + (string.IsNullOrEmpty(this.txtUserName.Text) ? "Integrated Security=SSPI;" : "uid=" + CommonMethods.AntiSqlInjection(this.txtUserName.Text) + ";") + (string.IsNullOrEmpty(this.txtPassword.Text) ? "" : "pwd=" + CommonMethods.AntiSqlInjection(this.txtPassword.Text) + ";");
                this._DbConnection = (IDbConnection)this.dbProviderFactory.CreateConnection();
                this._DbConnection.ConnectionString = str;
                DbConnection dbConnection = this._DbConnection as DbConnection;
                if (dbConnection != null)
                    dbConnection.StateChange += (StateChangeEventHandler)((o, ev) => this.lblConnectionStateInfo.Text = ev.CurrentState.ToString());
                this.blStringChanged = false;
            }
            return this._DbConnection;
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            this.blStringChanged = true;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            this.blStringChanged = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.blStringChanged = true;
        }

        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            this.blStringChanged = true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetDbConnection().Open();
                if (this._DbConnection.State == ConnectionState.Open)
                {
                    int num1 = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.ConnectionSuccesful), this.messageTextProvider.GetMessage(MessageTypes.ConnectionTest), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    int num2 = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.ConnectionFailed), this.messageTextProvider.GetMessage(MessageTypes.ConnectionTest), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (SqlException ex)
            {
                int num = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.ConnectionFailed) + "\n\n" + ex.Message, this.messageTextProvider.GetMessage(MessageTypes.ConnectionTest), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(this.messageTextProvider.GetMessage(MessageTypes.ConnectionFailed) + "\n\n" + ex.Message, this.messageTextProvider.GetMessage(MessageTypes.ConnectionTest), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                if (this.GetDbConnection().State != ConnectionState.Closed)
                    this.GetDbConnection().Close();
            }
        }
    }
}
