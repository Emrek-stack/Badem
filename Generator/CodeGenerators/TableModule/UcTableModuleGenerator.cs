using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Generator.CodeGenerators.Metadata;
using Generator.Properties;
using ScintillaNET;

namespace Generator.CodeGenerators.TableModule
{
    public class UcTableModuleGenerator : DatabaseUtility
    {
        private Dictionary<string, string> _gloDictEntities = new Dictionary<string, string>();
        private Dictionary<string, string> _gloDictDataLayerInterface = new Dictionary<string, string>();
        private Dictionary<string, string> _gloDictDataLayer = new Dictionary<string, string>();
        private Dictionary<string, string> _gloDictBusinessLayerInterface = new Dictionary<string, string>();
        private Dictionary<string, string> _gloDictBusinessLayer = new Dictionary<string, string>();
        private Dictionary<string, string> _gloDictInfrastructure = new Dictionary<string, string>();
        private CodeGenerationOptions options = new CodeGenerationOptions();
        private IContainer components;
        private Scintilla txtDataLayer;
        private Scintilla txtSPs;
        private TabPage tabPageBusiness;
        private TextBox txtNamespace;
        private Button btnSave;
        private Button btnGenerate;
        private TreeView tvwTables;
        private ImageList imgListTablesTreeView;
        private SplitContainer splcntQueryAndResults;
        private TabControl tabControlOutput;
        private TabPage tabPageSPs;
        private TabPage tabPageData;
        private Label lblNamespaceInfo;
        private Label lblGenerationInfo;
        private Button btnFillTables;
        private Scintilla txtDomainLogic;
        private TabPage tabPageEntity;
        private Scintilla txtEntities;
        private TabPage tabPageBlockDiagram;
        private Label label1;
        private Label label6;
        private Label label7;
        private Label label4;
        private Label label5;
        private Label label3;
        private Label label2;
        private TabPage tabPageDSupertype;
        private Scintilla txtDataSupertype;
        private TabPage tabPageBSupertype;
        private Scintilla txtBusinessLayerSupertype;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button btnOptions;
        private TabPage tabPageInfrastructure;
        private Scintilla txtInfrastructure;
        private PictureBox pictureBox1;
        private string gloSQLScript;
        private string gloDataSupertype;
        private TabPage tabPageIBusiness;
        private Scintilla txtIDomainLogic;
        private TabPage tabPageIData;
        private Scintilla txtIDataLayer;
        private string _gloBusinessSupertype;

        public UcTableModuleGenerator()
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
            this.components = new System.ComponentModel.Container();
            this.txtDataLayer =new Scintilla();
            this.txtSPs = new Scintilla();
            this.tabPageBusiness = new System.Windows.Forms.TabPage();
            this.txtDomainLogic =new Scintilla();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tvwTables = new System.Windows.Forms.TreeView();
            this.imgListTablesTreeView = new System.Windows.Forms.ImageList(this.components);
            this.splcntQueryAndResults = new System.Windows.Forms.SplitContainer();
            this.btnFillTables = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.lblGenerationInfo = new System.Windows.Forms.Label();
            this.tabControlOutput = new System.Windows.Forms.TabControl();
            this.tabPageBlockDiagram = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageSPs = new System.Windows.Forms.TabPage();
            this.tabPageEntity = new System.Windows.Forms.TabPage();
            this.txtEntities =new Scintilla();
            this.tabPageData = new System.Windows.Forms.TabPage();
            this.tabPageIBusiness = new System.Windows.Forms.TabPage();
            this.txtIDomainLogic = new Scintilla();
            this.tabPageDSupertype = new System.Windows.Forms.TabPage();
            this.txtDataSupertype = new Scintilla();
            this.tabPageBSupertype = new System.Windows.Forms.TabPage();
            this.txtBusinessLayerSupertype = new Scintilla();
            this.tabPageInfrastructure = new System.Windows.Forms.TabPage();
            this.txtInfrastructure = new Scintilla();
            this.lblNamespaceInfo = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPageIData = new System.Windows.Forms.TabPage();
            this.txtIDataLayer =new Scintilla();
            this.tabPageBusiness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcntQueryAndResults)).BeginInit();
            this.splcntQueryAndResults.Panel1.SuspendLayout();
            this.splcntQueryAndResults.Panel2.SuspendLayout();
            this.splcntQueryAndResults.SuspendLayout();
            this.tabControlOutput.SuspendLayout();
            this.tabPageBlockDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPageSPs.SuspendLayout();
            this.tabPageEntity.SuspendLayout();
            this.tabPageData.SuspendLayout();
            this.tabPageIBusiness.SuspendLayout();
            this.tabPageDSupertype.SuspendLayout();
            this.tabPageBSupertype.SuspendLayout();
            this.tabPageInfrastructure.SuspendLayout();
            this.tabPageIData.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDataLayer
            // 
            this.txtDataLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDataLayer.Location = new System.Drawing.Point(3, 3);
            this.txtDataLayer.Name = "txtDataLayer";
            this.txtDataLayer.Size = new System.Drawing.Size(538, 346);
            this.txtDataLayer.TabIndex = 1;
            this.txtDataLayer.ConfigurationManager.Language = "cs";
            // 
            // txtSPs
            // 
            this.txtSPs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSPs.Location = new System.Drawing.Point(3, 3);
            this.txtSPs.Name = "txtSPs";
            this.txtSPs.Size = new System.Drawing.Size(538, 346);
            this.txtSPs.TabIndex = 0;
            this.txtSPs.ConfigurationManager.Language = "mssql";
            // 
            // tabPageBusiness
            // 
            this.tabPageBusiness.Controls.Add(this.txtDomainLogic);
            this.tabPageBusiness.Location = new System.Drawing.Point(4, 22);
            this.tabPageBusiness.Name = "tabPageBusiness";
            this.tabPageBusiness.Size = new System.Drawing.Size(544, 352);
            this.tabPageBusiness.TabIndex = 2;
            this.tabPageBusiness.Text = "Service Layer";
            this.tabPageBusiness.ToolTipText = "Table Module";
            this.tabPageBusiness.UseVisualStyleBackColor = true;
            // 
            // txtDomainLogic
            // 
            this.txtDomainLogic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDomainLogic.Location = new System.Drawing.Point(0, 0);
            this.txtDomainLogic.Name = "txtDomainLogic";
            this.txtDomainLogic.Size = new System.Drawing.Size(544, 352);
            this.txtDomainLogic.TabIndex = 2;
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(74, 5);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(223, 20);
            this.txtNamespace.TabIndex = 4;
            this.txtNamespace.Text = "Bade";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(368, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(303, 3);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(59, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tvwTables
            // 
            this.tvwTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvwTables.CheckBoxes = true;
            this.tvwTables.Location = new System.Drawing.Point(0, 32);
            this.tvwTables.Name = "tvwTables";
            this.tvwTables.Size = new System.Drawing.Size(161, 409);
            this.tvwTables.TabIndex = 0;
            this.tvwTables.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwTables_AfterCheck);
            // 
            // imgListTablesTreeView
            // 
            this.imgListTablesTreeView.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imgListTablesTreeView.ImageSize = new System.Drawing.Size(16, 16);
            this.imgListTablesTreeView.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splcntQueryAndResults
            // 
            this.splcntQueryAndResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splcntQueryAndResults.Location = new System.Drawing.Point(3, 3);
            this.splcntQueryAndResults.Name = "splcntQueryAndResults";
            // 
            // splcntQueryAndResults.Panel1
            // 
            this.splcntQueryAndResults.Panel1.Controls.Add(this.btnFillTables);
            this.splcntQueryAndResults.Panel1.Controls.Add(this.tvwTables);
            // 
            // splcntQueryAndResults.Panel2
            // 
            this.splcntQueryAndResults.Panel2.Controls.Add(this.btnOptions);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.lblGenerationInfo);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.txtNamespace);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.btnSave);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.btnGenerate);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.tabControlOutput);
            this.splcntQueryAndResults.Panel2.Controls.Add(this.lblNamespaceInfo);
            this.splcntQueryAndResults.Size = new System.Drawing.Size(725, 444);
            this.splcntQueryAndResults.SplitterDistance = 161;
            this.splcntQueryAndResults.TabIndex = 20;
            // 
            // btnFillTables
            // 
            this.btnFillTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFillTables.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFillTables.Location = new System.Drawing.Point(3, 3);
            this.btnFillTables.Name = "btnFillTables";
            this.btnFillTables.Size = new System.Drawing.Size(155, 25);
            this.btnFillTables.TabIndex = 6;
            this.btnFillTables.Text = "Fill Tables";
            this.btnFillTables.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFillTables.UseVisualStyleBackColor = true;
            this.btnFillTables.Click += new System.EventHandler(this.btnFillTables_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(434, 3);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(63, 23);
            this.btnOptions.TabIndex = 6;
            this.btnOptions.Text = "Options...";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // lblGenerationInfo
            // 
            this.lblGenerationInfo.Location = new System.Drawing.Point(8, 32);
            this.lblGenerationInfo.Name = "lblGenerationInfo";
            this.lblGenerationInfo.Size = new System.Drawing.Size(503, 27);
            this.lblGenerationInfo.TabIndex = 5;
            // 
            // tabControlOutput
            // 
            this.tabControlOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlOutput.Controls.Add(this.tabPageBlockDiagram);
            this.tabControlOutput.Controls.Add(this.tabPageSPs);
            this.tabControlOutput.Controls.Add(this.tabPageEntity);
            this.tabControlOutput.Controls.Add(this.tabPageIData);
            this.tabControlOutput.Controls.Add(this.tabPageData);
            this.tabControlOutput.Controls.Add(this.tabPageIBusiness);
            this.tabControlOutput.Controls.Add(this.tabPageBusiness);
            this.tabControlOutput.Controls.Add(this.tabPageDSupertype);
            this.tabControlOutput.Controls.Add(this.tabPageBSupertype);
            this.tabControlOutput.Controls.Add(this.tabPageInfrastructure);
            this.tabControlOutput.Location = new System.Drawing.Point(3, 63);
            this.tabControlOutput.Name = "tabControlOutput";
            this.tabControlOutput.SelectedIndex = 0;
            this.tabControlOutput.Size = new System.Drawing.Size(552, 378);
            this.tabControlOutput.TabIndex = 1;
            // 
            // tabPageBlockDiagram
            // 
            this.tabPageBlockDiagram.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBlockDiagram.Controls.Add(this.pictureBox1);
            this.tabPageBlockDiagram.Controls.Add(this.label6);
            this.tabPageBlockDiagram.Controls.Add(this.label7);
            this.tabPageBlockDiagram.Controls.Add(this.label4);
            this.tabPageBlockDiagram.Controls.Add(this.label5);
            this.tabPageBlockDiagram.Controls.Add(this.label3);
            this.tabPageBlockDiagram.Controls.Add(this.label2);
            this.tabPageBlockDiagram.Controls.Add(this.label1);
            this.tabPageBlockDiagram.Location = new System.Drawing.Point(4, 22);
            this.tabPageBlockDiagram.Name = "tabPageBlockDiagram";
            this.tabPageBlockDiagram.Size = new System.Drawing.Size(544, 352);
            this.tabPageBlockDiagram.TabIndex = 4;
            this.tabPageBlockDiagram.Text = "Block Diagram";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Location = new System.Drawing.Point(88, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(372, 248);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 305);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Data Transfer Object";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(179, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Entity Layer :";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(267, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Table Module";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(161, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Business Layer :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 331);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Table Data Gateway";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(184, 331);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Layer :";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(147, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table Module Layered Architecture";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPageSPs
            // 
            this.tabPageSPs.Controls.Add(this.txtSPs);
            this.tabPageSPs.Location = new System.Drawing.Point(4, 22);
            this.tabPageSPs.Name = "tabPageSPs";
            this.tabPageSPs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSPs.Size = new System.Drawing.Size(544, 352);
            this.tabPageSPs.TabIndex = 0;
            this.tabPageSPs.Text = "Stored Procs";
            this.tabPageSPs.UseVisualStyleBackColor = true;
            // 
            // tabPageEntity
            // 
            this.tabPageEntity.Controls.Add(this.txtEntities);
            this.tabPageEntity.Location = new System.Drawing.Point(4, 22);
            this.tabPageEntity.Name = "tabPageEntity";
            this.tabPageEntity.Size = new System.Drawing.Size(544, 352);
            this.tabPageEntity.TabIndex = 3;
            this.tabPageEntity.Text = "Entities";
            this.tabPageEntity.ToolTipText = "Data Transfer Objects";
            this.tabPageEntity.UseVisualStyleBackColor = true;
            // 
            // txtEntities
            // 
            this.txtEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEntities.Location = new System.Drawing.Point(0, 0);
            this.txtEntities.Name = "txtEntities";
            this.txtEntities.Size = new System.Drawing.Size(544, 352);
            this.txtEntities.TabIndex = 2;
            this.txtEntities.ConfigurationManager.Language = "cs";
            // 
            // tabPageData
            // 
            this.tabPageData.Controls.Add(this.txtDataLayer);
            this.tabPageData.Location = new System.Drawing.Point(4, 22);
            this.tabPageData.Name = "tabPageData";
            this.tabPageData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageData.Size = new System.Drawing.Size(544, 352);
            this.tabPageData.TabIndex = 1;
            this.tabPageData.Text = "Data Layer";
            this.tabPageData.ToolTipText = "Table Data Gateway";
            this.tabPageData.UseVisualStyleBackColor = true;
            // 
            // tabPageIBusiness
            // 
            this.tabPageIBusiness.Controls.Add(this.txtIDomainLogic);
            this.tabPageIBusiness.Location = new System.Drawing.Point(4, 22);
            this.tabPageIBusiness.Name = "tabPageIBusiness";
            this.tabPageIBusiness.Size = new System.Drawing.Size(544, 352);
            this.tabPageIBusiness.TabIndex = 8;
            this.tabPageIBusiness.Text = "IService Layer";
            this.tabPageIBusiness.UseVisualStyleBackColor = true;

            // 
            // txtIDomainLogic
            // 
            this.txtIDomainLogic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIDomainLogic.Location = new System.Drawing.Point(0, 0);
            this.txtIDomainLogic.Name = "txtIDomainLogic";
            this.txtIDomainLogic.Size = new System.Drawing.Size(544, 352);
            this.txtIDomainLogic.TabIndex = 3;
            this.txtIDomainLogic.ConfigurationManager.Language = "cs";
            // 
            // tabPageDSupertype
            // 
            this.tabPageDSupertype.Controls.Add(this.txtDataSupertype);
            this.tabPageDSupertype.Location = new System.Drawing.Point(4, 22);
            this.tabPageDSupertype.Name = "tabPageDSupertype";
            this.tabPageDSupertype.Size = new System.Drawing.Size(544, 352);
            this.tabPageDSupertype.TabIndex = 5;
            this.tabPageDSupertype.Text = "Data Layer Supertype";
            this.tabPageDSupertype.UseVisualStyleBackColor = true;

            // 
            // txtDataSupertype
            // 
            this.txtDataSupertype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDataSupertype.Location = new System.Drawing.Point(0, 0);
            this.txtDataSupertype.Name = "txtDataSupertype";
            this.txtDataSupertype.Size = new System.Drawing.Size(544, 352);
            this.txtDataSupertype.TabIndex = 3;
            this.txtDataSupertype.ConfigurationManager.Language = "cs";
            // 
            // tabPageBSupertype
            // 
            this.tabPageBSupertype.Controls.Add(this.txtBusinessLayerSupertype);
            this.tabPageBSupertype.Location = new System.Drawing.Point(4, 22);
            this.tabPageBSupertype.Name = "tabPageBSupertype";
            this.tabPageBSupertype.Size = new System.Drawing.Size(544, 352);
            this.tabPageBSupertype.TabIndex = 6;
            this.tabPageBSupertype.Text = "Business Layer Supertype";
            this.tabPageBSupertype.UseVisualStyleBackColor = true;
            // 
            // txtBusinessLayerSupertype
            // 
            this.txtBusinessLayerSupertype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBusinessLayerSupertype.Location = new System.Drawing.Point(0, 0);
            this.txtBusinessLayerSupertype.Name = "txtBusinessLayerSupertype";
            this.txtBusinessLayerSupertype.Size = new System.Drawing.Size(544, 352);
            this.txtBusinessLayerSupertype.TabIndex = 4;
            this.txtBusinessLayerSupertype.ConfigurationManager.Language = "cs";

            // 
            // tabPageInfrastructure
            // 
            this.tabPageInfrastructure.Controls.Add(this.txtInfrastructure);
            this.tabPageInfrastructure.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfrastructure.Name = "tabPageInfrastructure";
            this.tabPageInfrastructure.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfrastructure.Size = new System.Drawing.Size(544, 352);
            this.tabPageInfrastructure.TabIndex = 7;
            this.tabPageInfrastructure.Text = "Infrastructure";
            this.tabPageInfrastructure.UseVisualStyleBackColor = true;
            // 
            // txtInfrastructure
            // 
            this.txtInfrastructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfrastructure.Location = new System.Drawing.Point(3, 3);
            this.txtInfrastructure.Name = "txtInfrastructure";
            this.txtInfrastructure.Size = new System.Drawing.Size(538, 346);
            this.txtInfrastructure.TabIndex = 5;
            this.txtInfrastructure.ConfigurationManager.Language = "cs";
            // 
            // lblNamespaceInfo
            // 
            this.lblNamespaceInfo.AutoSize = true;
            this.lblNamespaceInfo.Location = new System.Drawing.Point(4, 8);
            this.lblNamespaceInfo.Name = "lblNamespaceInfo";
            this.lblNamespaceInfo.Size = new System.Drawing.Size(64, 13);
            this.lblNamespaceInfo.TabIndex = 0;
            this.lblNamespaceInfo.Text = "Namespace";
            // 
            // tabPageIData
            // 
            this.tabPageIData.Controls.Add(this.txtIDataLayer);
            this.tabPageIData.Location = new System.Drawing.Point(4, 22);
            this.tabPageIData.Name = "tabPageIData";
            this.tabPageIData.Size = new System.Drawing.Size(544, 352);
            this.tabPageIData.TabIndex = 9;
            this.tabPageIData.Text = "IData Layer";
            this.tabPageIData.UseVisualStyleBackColor = true;
            // 
            // txtIDataLayer
            // 
            this.txtIDataLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtIDataLayer.Location = new System.Drawing.Point(0, 0);
            this.txtIDataLayer.Name = "txtIDataLayer";
            this.txtIDataLayer.Size = new System.Drawing.Size(544, 352);
            this.txtIDataLayer.TabIndex = 2;
            this.txtIDataLayer.ConfigurationManager.Language = "cs";
            // 
            // UcTableModuleGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.splcntQueryAndResults);
            this.Name = "UcTableModuleGenerator";
            this.Size = new System.Drawing.Size(731, 447);
            this.tabPageBusiness.ResumeLayout(false);
            this.tabPageBusiness.PerformLayout();
            this.splcntQueryAndResults.Panel1.ResumeLayout(false);
            this.splcntQueryAndResults.Panel2.ResumeLayout(false);
            this.splcntQueryAndResults.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splcntQueryAndResults)).EndInit();
            this.splcntQueryAndResults.ResumeLayout(false);
            this.tabControlOutput.ResumeLayout(false);
            this.tabPageBlockDiagram.ResumeLayout(false);
            this.tabPageBlockDiagram.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPageSPs.ResumeLayout(false);
            this.tabPageSPs.PerformLayout();
            this.tabPageEntity.ResumeLayout(false);
            this.tabPageEntity.PerformLayout();
            this.tabPageData.ResumeLayout(false);
            this.tabPageData.PerformLayout();
            this.tabPageIBusiness.ResumeLayout(false);
            this.tabPageIBusiness.PerformLayout();
            this.tabPageDSupertype.ResumeLayout(false);
            this.tabPageDSupertype.PerformLayout();
            this.tabPageBSupertype.ResumeLayout(false);
            this.tabPageBSupertype.PerformLayout();
            this.tabPageInfrastructure.ResumeLayout(false);
            this.tabPageInfrastructure.PerformLayout();
            this.tabPageIData.ResumeLayout(false);
            this.tabPageIData.PerformLayout();
            this.ResumeLayout(false);

        }

        private void btnFillTables_Click(object sender, EventArgs e)
        {
            this.PopulateTablesTree();
        }

        private void tvwTables_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes == null)
                return;
            foreach (TreeNode treeNode in e.Node.Nodes)
                treeNode.Checked = e.Node.Checked;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (string.IsNullOrEmpty(this.txtNamespace.Text) || this.tvwTables.Nodes.Count == 0)
                    return;
                List<TreeNode> listTables = (from TreeNode treeNode in tvwTables.Nodes[0].Nodes where treeNode.Checked select treeNode).ToList();
                DatabaseTableCollection dbTableCollection = new DatabaseTableCollection(options, listTables, ConnectionProvider.GetDbConnection());
                string text = this.txtNamespace.Text;
                DateTime now = DateTime.Now;
                this.txtSPs.Text = this.gloSQLScript = new StoredProcedureGenerator(this.options).GenerateSpScripts(dbTableCollection, now);
                this.txtDataSupertype.Text = this.gloDataSupertype = new DataLayerGenerator(this.options).GenerateDataLayerSupertypeCode(this.txtNamespace.Text);
                this.txtBusinessLayerSupertype.Text = this._gloBusinessSupertype = new BusinessLayerGenerator(this.options).GenerateBusinessLayerSupertypeCode(this.txtNamespace.Text);
                this._gloDictEntities = new EntityLayerGenerator(this.options).GenerateEntityCode(text, dbTableCollection);


                StringBuilder stringBuilder1 = new StringBuilder();
                foreach (string str in this._gloDictEntities.Values)
                    stringBuilder1.AppendLine(str);
                this.txtEntities.Text = stringBuilder1.ToString();



                _gloDictDataLayer = new DataLayerGenerator(this.options).GenerateDataLayerCode(text, dbTableCollection);
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (string str in this._gloDictDataLayer.Values)
                    stringBuilder2.AppendLine(str);
                txtDataLayer.Text = stringBuilder2.ToString();



                _gloDictDataLayerInterface = new DataLayerInterfaceGenerator(this.options).GenerateDataLayerCode(text, dbTableCollection);
                StringBuilder stringBuilderIData = new StringBuilder();
                foreach (string str in this._gloDictDataLayerInterface.Values)
                    stringBuilderIData.AppendLine(str);
                this.txtIDataLayer.Text = stringBuilderIData.ToString();



                this._gloDictBusinessLayer = new BusinessLayerGenerator(this.options).GenerateBusinessLayerCode(text, dbTableCollection);
                StringBuilder stringBuilder3 = new StringBuilder();
                foreach (string str in this._gloDictBusinessLayer.Values)
                    stringBuilder3.AppendLine(str);
                this.txtDomainLogic.Text = stringBuilder3.ToString();


                this._gloDictBusinessLayerInterface = new BusinessLayerInterfaceGenerator(this.options).GenerateBusinessLayerCode(text, dbTableCollection);
                StringBuilder stringBuilderIService = new StringBuilder();
                foreach (string str in this._gloDictBusinessLayerInterface.Values)
                    stringBuilderIService.AppendLine(str);
                txtIDomainLogic.Text = stringBuilderIService.ToString();


                this._gloDictInfrastructure = new Dictionary<string, string>();
                this._gloDictInfrastructure.Add("NullabletypeBase", NullableWrapperTypeGenerator.GetNullableAbstractBase(text));
                StringBuilder stringBuilder4 = new StringBuilder();
                foreach (string str in this._gloDictInfrastructure.Values)
                    stringBuilder4.AppendLine(str);
                this.txtInfrastructure.Text = stringBuilder4.ToString();
                stopwatch.Stop();
                this.lblGenerationInfo.Text = "Processing Time: " + (object)stopwatch.ElapsedMilliseconds + "ms";
                this.tabControlOutput.SelectedTab = this.tabPageSPs;
            }
            catch (SqlException ex)
            {
                this.lblGenerationInfo.Text = ex.Message;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.messageTextProvider.GetMessage(MessageTypes.Error), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK != this.folderBrowserDialog1.ShowDialog() || string.IsNullOrEmpty(this.folderBrowserDialog1.SelectedPath))
                return;
            this.SaveCodes(this.folderBrowserDialog1.SelectedPath);
        }

        private void PopulateTablesTree()
        {
            this.tvwTables.Nodes.Clear();
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                TreeNode node = new TreeNode(this.ConnectionProvider.GetDbConnection().Database);
                using (IDataReader dataReader = this.GetDbCommandReady(DatabaseTable.ListTablesCommandText).ExecuteReader())
                {
                    int ordinal = dataReader.GetOrdinal("TABLE_NAME");
                    int ordSchema = dataReader.GetOrdinal("TABLE_SCHEMA");
                    while (dataReader.Read())
                    {
                        var nodea = new TreeNode(dataReader.GetString(ordinal));
                        node.Tag = dataReader.GetString(ordSchema);
                        node.Nodes.Add(nodea);
                        
                    }

                }
                this.tvwTables.Nodes.Add(node);
                stopwatch.Stop();
                this.lblGenerationInfo.Text = "Processing Time: " + (object)stopwatch.ElapsedMilliseconds + "ms";
                this.tvwTables.ExpandAll();
            }
            catch (SqlException ex)
            {
                this.lblGenerationInfo.Text = ex.Message;
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.messageTextProvider.GetMessage(MessageTypes.Error), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
        }

        private void SaveCodes(string szPath)
        {
            FileSaver fileSaver = new FileSaver();
            Directory.CreateDirectory(szPath + "\\Service\\Interface");
            Directory.CreateDirectory(szPath + "\\Service\\Impl");
            Directory.CreateDirectory(szPath + "\\Repo\\Interface");
            Directory.CreateDirectory(szPath + "\\Repo\\Impl");
            //Directory.CreateDirectory(szPath + "\\DAL");
            Directory.CreateDirectory(szPath + "\\Domain");
            //Directory.CreateDirectory(szPath + "\\Infrastructure");
            foreach (string index in this._gloDictEntities.Keys)
                fileSaver.SaveFileAs(this._gloDictEntities[index], szPath + "\\Domain", index, "cs");


            foreach (string index in this._gloDictDataLayerInterface.Keys)
                fileSaver.SaveFileAs(this._gloDictDataLayerInterface[index], string.Format("{0}\\Repo\\Interface", szPath), string.Format("I{0}{1}", index, options.DataLayerClassSuffix), "cs");

            foreach (string index in this._gloDictDataLayer.Keys)
                fileSaver.SaveFileAs(this._gloDictDataLayer[index], string.Format("{0}\\Repo\\Impl", szPath), index + options.DataLayerClassSuffix, "cs");


            foreach (string index in this._gloDictBusinessLayer.Keys)
                fileSaver.SaveFileAs(this._gloDictBusinessLayer[index], string.Format("{0}\\Service\\Interface", szPath), "I"+ index + options.DomainLogicLayerSuffix, "cs");


            foreach (string index in this._gloDictBusinessLayer.Keys)
                fileSaver.SaveFileAs(this._gloDictBusinessLayer[index], string.Format("{0}\\Service\\Impl", szPath), index + options.DomainLogicLayerSuffix, "cs");

            //fileSaver.SaveFileAs(this.gloDataSupertype, szPath + "\\Repo", this.options.DataLayerClassPrefix + "Base", "cs");

            //fileSaver.SaveFileAs(this.gloBusinessSupertype, szPath + "\\Service", this.options.DomainLogicLayerPrefix + "Base", "cs");

         //   foreach (string szFileName in this._gloDictInfrastructure.Keys)
          //      fileSaver.SaveFileAs(this._gloDictInfrastructure[szFileName], szPath + "\\Infrastructure", szFileName, "cs");

            //fileSaver.SaveFileAs(this.gloSQLScript, szPath, (string)(object)DateTime.Now.Year + (object)"-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + DateTime.Now.Day.ToString().PadLeft(2, '0'), "sql", 1 != 0);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            int num = (int)new FrmOptions(this.options).ShowDialog((IWin32Window)this);
        }
    }
}
