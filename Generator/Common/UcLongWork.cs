using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpGenerator.Common
{
    public class UcLongWork : UserControl
    {
        private IContainer components;
        private BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar1;
        private Label lblWorkDescriptionInfo;
        private Button btnCancel;
        private Label lblProgressInfo;

        public UcLongWork()
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
            this.backgroundWorker1 = new BackgroundWorker();
            this.progressBar1 = new ProgressBar();
            this.lblWorkDescriptionInfo = new Label();
            this.btnCancel = new Button();
            this.lblProgressInfo = new Label();
            this.SuspendLayout();
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.progressBar1.Location = new Point(20, 34);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(371, 23);
            this.progressBar1.Step = 15;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.UseWaitCursor = true;
            this.progressBar1.Value = 10;
            this.lblWorkDescriptionInfo.AutoSize = true;
            this.lblWorkDescriptionInfo.Location = new Point(17, 18);
            this.lblWorkDescriptionInfo.Name = "lblWorkDescriptionInfo";
            this.lblWorkDescriptionInfo.Size = new Size(89, 13);
            this.lblWorkDescriptionInfo.TabIndex = 1;
            this.lblWorkDescriptionInfo.Text = "Work Description";
            this.btnCancel.Location = new Point(316, 97);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.lblProgressInfo.AutoSize = true;
            this.lblProgressInfo.Location = new Point(20, 64);
            this.lblProgressInfo.Name = "lblProgressInfo";
            this.lblProgressInfo.Size = new Size(10, 13);
            this.lblProgressInfo.TabIndex = 4;
            this.lblProgressInfo.Text = "-";
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Controls.Add((Control)this.lblProgressInfo);
            this.Controls.Add((Control)this.btnCancel);
            this.Controls.Add((Control)this.lblWorkDescriptionInfo);
            this.Controls.Add((Control)this.progressBar1);
            this.Name = "UcLongWork";
            this.Size = new Size(424, 139);
            this.Load += new EventHandler(this.UcLongWork_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void UcLongWork_Load(object sender, EventArgs e)
        {
            this.lblProgressInfo.Text = "";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.backgroundWorker1.ReportProgress(12, (object)"objecttorenAschn");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
