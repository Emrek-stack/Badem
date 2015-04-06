using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Generator.CodeGenerators.TableModuleWSL
{
    public class FrmOptions : Form
    {
        private IContainer components;
        private Button btnCancel;
        private RadioButton radioNF11;
        private GroupBox groupBox1;
        private RadioButton radioNF20;
        private GroupBox groupBox2;
        private TextBox txtDeletePrefix;
        private Label label3;
        private TextBox txtUpdatePrefix;
        private Label label2;
        private TextBox txtInsertPrefix;
        private Label label1;
        private TextBox txtBLLClassPrefix;
        private Label label4;
        private TextBox txtDLClassPrefix;
        private Label label5;
        private TextBox txtSelectPrefix;
        private Label label6;
        private Button btnOK;
        private TextBox txtAllSPPrefix;
        private Label label7;
        private TextBox txtWithMarkPostfix;
        private Label label12;
        private TextBox txtIsDeletedColumn;
        private Label label11;
        private TextBox txtIsDisabledColumn;
        private Label label10;
        private TextBox txtNotDeletedPhrase;
        private Label label9;
        private TextBox txtNotDisabledPhrase;
        private Label label8;
        private TextBox txtMarkPrefix;
        private Label label13;
        private CheckBox chkValidationRequired;
        private GroupBox groupBox3;
        private TextBox txtDeletedPhrase;
        private Label label14;
        private TextBox txtDisabledPhrase;
        private Label label15;
        private CodeGenerationOptions gloOptions;

        public CodeGenerationOptions Options
        {
            get
            {
                return this.gloOptions;
            }
        }

        public FrmOptions()
        {
            this.InitializeComponent();
        }

        public FrmOptions(CodeGenerationOptions options)
            : this()
        {
            this.gloOptions = options;
            this.UpdateGUIFromOptions(options);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.radioNF11 = new RadioButton();
            this.groupBox1 = new GroupBox();
            this.radioNF20 = new RadioButton();
            this.groupBox2 = new GroupBox();
            this.txtMarkPrefix = new TextBox();
            this.label13 = new Label();
            this.txtWithMarkPostfix = new TextBox();
            this.label12 = new Label();
            this.txtIsDeletedColumn = new TextBox();
            this.txtDLClassPrefix = new TextBox();
            this.label5 = new Label();
            this.txtIsDisabledColumn = new TextBox();
            this.label11 = new Label();
            this.label10 = new Label();
            this.txtBLLClassPrefix = new TextBox();
            this.label4 = new Label();
            this.txtNotDeletedPhrase = new TextBox();
            this.label9 = new Label();
            this.txtNotDisabledPhrase = new TextBox();
            this.label8 = new Label();
            this.txtAllSPPrefix = new TextBox();
            this.label7 = new Label();
            this.txtSelectPrefix = new TextBox();
            this.label6 = new Label();
            this.txtDeletePrefix = new TextBox();
            this.label3 = new Label();
            this.txtUpdatePrefix = new TextBox();
            this.label2 = new Label();
            this.txtInsertPrefix = new TextBox();
            this.label1 = new Label();
            this.btnOK = new Button();
            this.chkValidationRequired = new CheckBox();
            this.groupBox3 = new GroupBox();
            this.txtDeletedPhrase = new TextBox();
            this.label14 = new Label();
            this.txtDisabledPhrase = new TextBox();
            this.label15 = new Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(354, 414);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.radioNF11.AutoSize = true;
            this.radioNF11.Location = new Point(15, 19);
            this.radioNF11.Name = "radioNF11";
            this.radioNF11.Size = new Size(123, 17);
            this.radioNF11.TabIndex = 1;
            this.radioNF11.TabStop = true;
            this.radioNF11.Text = ".NET Framework 1.1";
            this.radioNF11.UseVisualStyleBackColor = true;
            this.groupBox1.Controls.Add((Control)this.radioNF20);
            this.groupBox1.Controls.Add((Control)this.radioNF11);
            this.groupBox1.Location = new Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(499, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Platform";
            this.radioNF20.AutoSize = true;
            this.radioNF20.Location = new Point(15, 42);
            this.radioNF20.Name = "radioNF20";
            this.radioNF20.Size = new Size(123, 17);
            this.radioNF20.TabIndex = 2;
            this.radioNF20.TabStop = true;
            this.radioNF20.Text = ".NET Framework 2.0";
            this.radioNF20.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add((Control)this.txtDeletedPhrase);
            this.groupBox2.Controls.Add((Control)this.label14);
            this.groupBox2.Controls.Add((Control)this.txtDisabledPhrase);
            this.groupBox2.Controls.Add((Control)this.label15);
            this.groupBox2.Controls.Add((Control)this.txtMarkPrefix);
            this.groupBox2.Controls.Add((Control)this.label13);
            this.groupBox2.Controls.Add((Control)this.txtWithMarkPostfix);
            this.groupBox2.Controls.Add((Control)this.label12);
            this.groupBox2.Controls.Add((Control)this.txtIsDeletedColumn);
            this.groupBox2.Controls.Add((Control)this.txtDLClassPrefix);
            this.groupBox2.Controls.Add((Control)this.label5);
            this.groupBox2.Controls.Add((Control)this.txtIsDisabledColumn);
            this.groupBox2.Controls.Add((Control)this.label11);
            this.groupBox2.Controls.Add((Control)this.label10);
            this.groupBox2.Controls.Add((Control)this.txtBLLClassPrefix);
            this.groupBox2.Controls.Add((Control)this.label4);
            this.groupBox2.Controls.Add((Control)this.txtNotDeletedPhrase);
            this.groupBox2.Controls.Add((Control)this.label9);
            this.groupBox2.Controls.Add((Control)this.txtNotDisabledPhrase);
            this.groupBox2.Controls.Add((Control)this.label8);
            this.groupBox2.Controls.Add((Control)this.txtAllSPPrefix);
            this.groupBox2.Controls.Add((Control)this.label7);
            this.groupBox2.Controls.Add((Control)this.txtSelectPrefix);
            this.groupBox2.Controls.Add((Control)this.label6);
            this.groupBox2.Controls.Add((Control)this.txtDeletePrefix);
            this.groupBox2.Controls.Add((Control)this.label3);
            this.groupBox2.Controls.Add((Control)this.txtUpdatePrefix);
            this.groupBox2.Controls.Add((Control)this.label2);
            this.groupBox2.Controls.Add((Control)this.txtInsertPrefix);
            this.groupBox2.Controls.Add((Control)this.label1);
            this.groupBox2.Location = new Point(12, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(499, 261);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Namenclature";
            this.txtMarkPrefix.Location = new Point(356, 192);
            this.txtMarkPrefix.Name = "txtMarkPrefix";
            this.txtMarkPrefix.Size = new Size(116, 20);
            this.txtMarkPrefix.TabIndex = 25;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(249, 195);
            this.label13.Name = "label13";
            this.label13.Size = new Size(60, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Mark Prefix";
            this.txtWithMarkPostfix.Location = new Point(112, 192);
            this.txtWithMarkPostfix.Name = "txtWithMarkPostfix";
            this.txtWithMarkPostfix.Size = new Size(116, 20);
            this.txtWithMarkPostfix.TabIndex = 23;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(6, 195);
            this.label12.Name = "label12";
            this.label12.Size = new Size(90, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "With Mark Postfix";
            this.txtIsDeletedColumn.Location = new Point(356, 166);
            this.txtIsDeletedColumn.Name = "txtIsDeletedColumn";
            this.txtIsDeletedColumn.Size = new Size(116, 20);
            this.txtIsDeletedColumn.TabIndex = 21;
            this.txtDLClassPrefix.Location = new Point(356, 235);
            this.txtDLClassPrefix.Name = "txtDLClassPrefix";
            this.txtDLClassPrefix.Size = new Size(116, 20);
            this.txtDLClassPrefix.TabIndex = 9;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(249, 238);
            this.label5.Name = "label5";
            this.label5.Size = new Size(59, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Data Prefix";
            this.txtIsDisabledColumn.Location = new Point(112, 166);
            this.txtIsDisabledColumn.Name = "txtIsDisabledColumn";
            this.txtIsDisabledColumn.Size = new Size(116, 20);
            this.txtIsDisabledColumn.TabIndex = 19;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(249, 169);
            this.label11.Name = "label11";
            this.label11.Size = new Size(90, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "IsDeleted Column";
            this.label10.AutoSize = true;
            this.label10.Location = new Point(6, 169);
            this.label10.Name = "label10";
            this.label10.Size = new Size(94, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "IsDisabled Column";
            this.txtBLLClassPrefix.Location = new Point(112, 235);
            this.txtBLLClassPrefix.Name = "txtBLLClassPrefix";
            this.txtBLLClassPrefix.Size = new Size(116, 20);
            this.txtBLLClassPrefix.TabIndex = 7;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(6, 238);
            this.label4.Name = "label4";
            this.label4.Size = new Size(78, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Business Prefix";
            this.txtNotDeletedPhrase.Location = new Point(356, 140);
            this.txtNotDeletedPhrase.Name = "txtNotDeletedPhrase";
            this.txtNotDeletedPhrase.Size = new Size(116, 20);
            this.txtNotDeletedPhrase.TabIndex = 17;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(249, 143);
            this.label9.Name = "label9";
            this.label9.Size = new Size(100, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Not Deleted Phrase";
            this.txtNotDisabledPhrase.Location = new Point(112, 140);
            this.txtNotDisabledPhrase.Name = "txtNotDisabledPhrase";
            this.txtNotDisabledPhrase.Size = new Size(116, 20);
            this.txtNotDisabledPhrase.TabIndex = 15;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(6, 143);
            this.label8.Name = "label8";
            this.label8.Size = new Size(104, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Not Disabled Phrase";
            this.txtAllSPPrefix.Location = new Point(112, 71);
            this.txtAllSPPrefix.Name = "txtAllSPPrefix";
            this.txtAllSPPrefix.Size = new Size(116, 20);
            this.txtAllSPPrefix.TabIndex = 13;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(6, 74);
            this.label7.Name = "label7";
            this.label7.Size = new Size(71, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "All SP's Prefix";
            this.txtSelectPrefix.Location = new Point(356, 45);
            this.txtSelectPrefix.Name = "txtSelectPrefix";
            this.txtSelectPrefix.Size = new Size(116, 20);
            this.txtSelectPrefix.TabIndex = 11;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(249, 48);
            this.label6.Name = "label6";
            this.label6.Size = new Size(90, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Select SP's Prefix";
            this.txtDeletePrefix.Location = new Point(112, 45);
            this.txtDeletePrefix.Name = "txtDeletePrefix";
            this.txtDeletePrefix.Size = new Size(116, 20);
            this.txtDeletePrefix.TabIndex = 5;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new Size(91, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Delete SP's Prefix";
            this.txtUpdatePrefix.Location = new Point(356, 19);
            this.txtUpdatePrefix.Name = "txtUpdatePrefix";
            this.txtUpdatePrefix.Size = new Size(116, 20);
            this.txtUpdatePrefix.TabIndex = 3;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(249, 22);
            this.label2.Name = "label2";
            this.label2.Size = new Size(95, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Update SP's Prefix";
            this.txtInsertPrefix.Location = new Point(112, 19);
            this.txtInsertPrefix.Name = "txtInsertPrefix";
            this.txtInsertPrefix.Size = new Size(116, 20);
            this.txtInsertPrefix.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Insert SP's Prefix";
            this.btnOK.Location = new Point(436, 414);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.chkValidationRequired.AutoSize = true;
            this.chkValidationRequired.Location = new Point(15, 22);
            this.chkValidationRequired.Name = "chkValidationRequired";
            this.chkValidationRequired.Size = new Size(118, 17);
            this.chkValidationRequired.TabIndex = 25;
            this.chkValidationRequired.Text = "Validation Required";
            this.chkValidationRequired.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add((Control)this.chkValidationRequired);
            this.groupBox3.Location = new Point(12, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(499, 56);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Miscellenaus";
            this.txtDeletedPhrase.Location = new Point(356, 114);
            this.txtDeletedPhrase.Name = "txtDeletedPhrase";
            this.txtDeletedPhrase.Size = new Size(116, 20);
            this.txtDeletedPhrase.TabIndex = 29;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(249, 117);
            this.label14.Name = "label14";
            this.label14.Size = new Size(80, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Deleted Phrase";
            this.txtDisabledPhrase.Location = new Point(112, 114);
            this.txtDisabledPhrase.Name = "txtDisabledPhrase";
            this.txtDisabledPhrase.Size = new Size(116, 20);
            this.txtDisabledPhrase.TabIndex = 27;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(6, 117);
            this.label15.Name = "label15";
            this.label15.Size = new Size(84, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Disabled Phrase";
            this.AcceptButton = (IButtonControl)this.btnOK;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = (IButtonControl)this.btnCancel;
            this.ClientSize = new Size(523, 448);
            this.Controls.Add((Control)this.groupBox3);
            this.Controls.Add((Control)this.btnOK);
            this.Controls.Add((Control)this.groupBox2);
            this.Controls.Add((Control)this.groupBox1);
            this.Controls.Add((Control)this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOptions";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        private void UpdateGUIFromOptions(CodeGenerationOptions options)
        {
            this.txtInsertPrefix.Text = options.InsertPrefix;
            this.txtDeletePrefix.Text = options.DeletePrefix;
            this.txtUpdatePrefix.Text = options.UpdatePrefix;
            this.txtSelectPrefix.Text = options.SelectPrefix;
            this.txtAllSPPrefix.Text = options.AllGeneratedSpPrefix;
            this.txtDeletedPhrase.Text = options.DeletedPhrase;
            this.txtDisabledPhrase.Text = options.DisabledPhrase;
            this.txtNotDisabledPhrase.Text = options.NotDisabledPhrase;
            this.txtNotDeletedPhrase.Text = options.NotDeletedPhrase;
            this.txtIsDisabledColumn.Text = options.IsDisabledColumn;
            this.txtIsDeletedColumn.Text = options.IsDeletedColumn;
            this.txtWithMarkPostfix.Text = options.WithMarkPostfix;
            this.txtMarkPrefix.Text = options.MarkPrefix;
            this.txtBLLClassPrefix.Text = options.DomainLogicLayerPrefix;
            this.txtDLClassPrefix.Text = options.DataLayerClassPrefix;
            switch (options.TargetPlatform)
            {
                case Platform.netFramework11:
                    this.radioNF11.Checked = true;
                    break;
                case Platform.netFramework20:
                    this.radioNF20.Checked = true;
                    break;
            }
        }

        private void UpdateOptionsFromGUI(CodeGenerationOptions options)
        {
            options.InsertPrefix = this.txtInsertPrefix.Text;
            options.DeletePrefix = this.txtDeletePrefix.Text;
            options.UpdatePrefix = this.txtUpdatePrefix.Text;
            options.SelectPrefix = this.txtSelectPrefix.Text;
            options.AllGeneratedSpPrefix = this.txtAllSPPrefix.Text;
            options.DeletedPhrase = this.txtDeletedPhrase.Text;
            options.DisabledPhrase = this.txtDisabledPhrase.Text;
            options.NotDisabledPhrase = this.txtNotDisabledPhrase.Text;
            options.NotDeletedPhrase = this.txtNotDeletedPhrase.Text;
            options.IsDisabledColumn = this.txtIsDisabledColumn.Text;
            options.IsDeletedColumn = this.txtIsDeletedColumn.Text;
            options.WithMarkPostfix = this.txtWithMarkPostfix.Text;
            options.MarkPrefix = this.txtMarkPrefix.Text;
            options.DomainLogicLayerPrefix = this.txtBLLClassPrefix.Text;
            options.DataLayerClassPrefix = this.txtDLClassPrefix.Text;
            if (this.radioNF11.Checked)
                options.TargetPlatform = Platform.netFramework11;
            if (this.radioNF20.Checked)
                options.TargetPlatform = Platform.netFramework20;
            options.GenerateDeleteByForeignKey = true;
            options.GenerateDropAllTableSPs = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.UpdateOptionsFromGUI(this.gloOptions);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
