using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Generator.CodeGenerators.TableModule;
using Generator.CodeGenerators.TableModuleWSL;

namespace Generator
{
    public class FrmMain : Form
    {
        private Dictionary<string, UserControl> instantiatedUserControls = new Dictionary<string, UserControl>();
        private IContainer components;
        private ToolStripContainer toolStripContainer1;
        private MenuStrip leftSideMenu;
        private ToolStripMenuItem menuItemDatabaseSetup;
        private ToolStripMenuItem menuItemQueryCommander;
        private ToolStripMenuItem menuItemDuplicateDestroyer;
        private ToolStripMenuItem codeGenerationToolStripMenuItem;
        private ToolStripMenuItem menuItemTableModule;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem menuItemTMWServiceLayer;
        public IDbConnectionProvider connectionProvider;
        public IMessageTextProvider messageTextProvider;

        public FrmMain()
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FrmMain));
            this.toolStripContainer1 = new ToolStripContainer();
            this.statusStrip1 = new StatusStrip();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.leftSideMenu = new MenuStrip();
            this.menuItemDatabaseSetup = new ToolStripMenuItem();
            this.menuItemQueryCommander = new ToolStripMenuItem();
            this.menuItemDuplicateDestroyer = new ToolStripMenuItem();
            this.codeGenerationToolStripMenuItem = new ToolStripMenuItem();
            this.menuItemTableModule = new ToolStripMenuItem();
            this.menuItemTMWServiceLayer = new ToolStripMenuItem();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.leftSideMenu.SuspendLayout();
            this.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add((Control)this.statusStrip1);
            this.toolStripContainer1.ContentPanel.Size = new Size(742, 499);
            this.toolStripContainer1.Dock = DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add((Control)this.leftSideMenu);
            this.toolStripContainer1.Location = new Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new Size(878, 546);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.statusStrip1.Dock = DockStyle.None;
            this.statusStrip1.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.toolStripStatusLabel1
      });
            this.statusStrip1.Location = new Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new Size(878, 22);
            this.statusStrip1.TabIndex = 0;
            //this.toolStripStatusLabel1.Image = (Image)componentResourceManager.GetObject("toolStripStatusLabel1.Image");
            this.toolStripStatusLabel1.ImageAlign = ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.ImageScaling = ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new Size(863, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Berke Sokhan";
            this.toolStripStatusLabel1.Click += new EventHandler(this.toolStripStatusLabel1_Click);
            this.leftSideMenu.AllowMerge = false;
            this.leftSideMenu.BackColor = Color.White;
            this.leftSideMenu.Dock = DockStyle.None;
            this.leftSideMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.menuItemDatabaseSetup,
        (ToolStripItem) this.menuItemQueryCommander,
        (ToolStripItem) this.menuItemDuplicateDestroyer,
        (ToolStripItem) this.codeGenerationToolStripMenuItem
      });
            this.leftSideMenu.LayoutStyle = ToolStripLayoutStyle.Table;
            this.leftSideMenu.Location = new Point(0, 0);
            this.leftSideMenu.Name = "leftSideMenu";
            this.leftSideMenu.RenderMode = ToolStripRenderMode.Professional;
            this.leftSideMenu.ShowItemToolTips = true;
            this.leftSideMenu.Size = new Size(136, 499);
            this.leftSideMenu.TabIndex = 0;
            this.leftSideMenu.Text = "menuStrip1";
            //this.menuItemDatabaseSetup.Image = (Image)componentResourceManager.GetObject("menuItemDatabaseSetup.Image");
            this.menuItemDatabaseSetup.ImageScaling = ToolStripItemImageScaling.None;
            this.menuItemDatabaseSetup.Name = "menuItemDatabaseSetup";
            this.menuItemDatabaseSetup.Size = new Size(112, 20);
            this.menuItemDatabaseSetup.Text = "Database Setup";
            this.menuItemDatabaseSetup.Click += new EventHandler(this.menuItemDatabaseSetup_Click);
            //this.menuItemQueryCommander.Image = (Image)componentResourceManager.GetObject("menuItemQueryCommander.Image");
            this.menuItemQueryCommander.Name = "menuItemQueryCommander";
            this.menuItemQueryCommander.Size = new Size(109, 20);
            this.menuItemQueryCommander.Text = "Sql Commander";
            this.menuItemQueryCommander.Click += new EventHandler(this.menuItemQueryCommander_Click);
            //this.menuItemDuplicateDestroyer.Image = (Image)componentResourceManager.GetObject("menuItemDuplicateDestroyer.Image");
            this.menuItemDuplicateDestroyer.Name = "menuItemDuplicateDestroyer";
            this.menuItemDuplicateDestroyer.Size = new Size(130, 20);
            this.menuItemDuplicateDestroyer.Text = "Duplicate Destroyer";
            this.menuItemDuplicateDestroyer.Click += new EventHandler(this.menuItemDuplicateDestroyer_Click);
            this.codeGenerationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.menuItemTableModule,
        (ToolStripItem) this.menuItemTMWServiceLayer
      });
            //this.codeGenerationToolStripMenuItem.Image = (Image)componentResourceManager.GetObject("codeGenerationToolStripMenuItem.Image");
            this.codeGenerationToolStripMenuItem.Name = "codeGenerationToolStripMenuItem";
            this.codeGenerationToolStripMenuItem.Size = new Size(117, 20);
            this.codeGenerationToolStripMenuItem.Text = "Code Generators";
            this.menuItemTableModule.Name = "menuItemTableModule";
            this.menuItemTableModule.Size = new Size(182, 22);
            this.menuItemTableModule.Text = "Table Module";
            this.menuItemTableModule.Click += new EventHandler(this.menuItemTableModule_Click);
            this.menuItemTMWServiceLayer.Name = "menuItemTMWServiceLayer";
            this.menuItemTMWServiceLayer.Size = new Size(182, 22);
            this.menuItemTMWServiceLayer.Text = "TM w/ Service Layer";
            this.menuItemTMWServiceLayer.Click += new EventHandler(this.menuItemTMWServiceLayer_Click);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(878, 546);
            this.Controls.Add((Control)this.toolStripContainer1);
            //this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.Name = "FrmMain";
            this.Text = "Sharp Generator 1.0.0.10";
            this.Load += new EventHandler(this.frmMdiParent_Load);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.leftSideMenu.ResumeLayout(false);
            this.leftSideMenu.PerformLayout();
            this.ResumeLayout(false);
        }

        private void frmMdiParent_Load(object sender, EventArgs e)
        {
            if (this.messageTextProvider == null)
                this.messageTextProvider = (IMessageTextProvider)new MessageTextProviderEnglish();
            IDbConnectionProvider connectionProvider = this.UtilityUserControlOpener(typeof(UcSqlConnection)) as IDbConnectionProvider;
            if (connectionProvider == null)
                return;
            this.connectionProvider = connectionProvider;
        }

        private UserControl UtilityUserControlOpener(Type typeOfUserControl)
        {
            UserControl userControl;
            if (!this.instantiatedUserControls.ContainsKey(typeOfUserControl.ToString()))
            {
                userControl = (UserControl)Assembly.GetExecutingAssembly().CreateInstance(typeOfUserControl.ToString());
                this.instantiatedUserControls.Add(typeOfUserControl.ToString(), userControl);
            }
            else
                userControl = this.instantiatedUserControls[typeOfUserControl.ToString()];
            this.toolStripContainer1.ContentPanel.Controls.Clear();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control)userControl);
            if (userControl is IDbConnectionUser)
                ((IDbConnectionUser)userControl).ConnectionProvider = this.connectionProvider;
            if (userControl is IMessageTextUser)
                ((IMessageTextUser)userControl).MessageTextProvider = this.messageTextProvider;
            userControl.Dock = DockStyle.Fill;
            return userControl;
        }

        private void menuItemDatabaseSetup_Click(object sender, EventArgs e)
        {
            this.UtilityUserControlOpener(typeof(UcSqlConnection));
        }

        private void menuItemQueryCommander_Click(object sender, EventArgs e)
        {
            this.UtilityUserControlOpener(typeof(UcSqlCommander));
        }

        private void menuItemDuplicateDestroyer_Click(object sender, EventArgs e)
        {
            this.UtilityUserControlOpener(typeof(UcDuplicateDestroyer));
        }

        private void menuItemTableModule_Click(object sender, EventArgs e)
        {
            this.UtilityUserControlOpener(typeof(UcTableModuleGenerator));
        }

        private void menuItemTMWServiceLayer_Click(object sender, EventArgs e)
        {
            this.UtilityUserControlOpener(typeof(UcTMwServiceLayerGenerator));
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo("iexplore", "www.berkesokhan.com");
            try
            {
                process.Start();
            }
            catch (Win32Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message);
            }
        }
    }
}
