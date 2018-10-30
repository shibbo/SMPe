namespace SMPe
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.perspectiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addSpaceButton = new System.Windows.Forms.ToolStripButton();
            this.deleteSpaceButton = new System.Windows.Forms.ToolStripButton();
            this.spaceInfoGrid = new System.Windows.Forms.PropertyGrid();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.zoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom90 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom80 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom70 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom60 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.zoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.perspectiveToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // perspectiveToolStripMenuItem
            // 
            this.perspectiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upToolStripMenuItem,
            this.sideToolStripMenuItem});
            this.perspectiveToolStripMenuItem.Name = "perspectiveToolStripMenuItem";
            this.perspectiveToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.perspectiveToolStripMenuItem.Text = "Perspective";
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.upToolStripMenuItem.Text = "Up";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.upToolStripMenuItem_Click);
            // 
            // sideToolStripMenuItem
            // 
            this.sideToolStripMenuItem.Name = "sideToolStripMenuItem";
            this.sideToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.sideToolStripMenuItem.Text = "Side";
            this.sideToolStripMenuItem.Click += new System.EventHandler(this.sideToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.spaceInfoGrid);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 426);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSpaceButton,
            this.deleteSpaceButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addSpaceButton
            // 
            this.addSpaceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addSpaceButton.Enabled = false;
            this.addSpaceButton.Image = ((System.Drawing.Image)(resources.GetObject("addSpaceButton.Image")));
            this.addSpaceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSpaceButton.Name = "addSpaceButton";
            this.addSpaceButton.Size = new System.Drawing.Size(67, 22);
            this.addSpaceButton.Text = "Add Space";
            // 
            // deleteSpaceButton
            // 
            this.deleteSpaceButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteSpaceButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteSpaceButton.Image")));
            this.deleteSpaceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteSpaceButton.Name = "deleteSpaceButton";
            this.deleteSpaceButton.Size = new System.Drawing.Size(78, 22);
            this.deleteSpaceButton.Text = "Delete Space";
            this.deleteSpaceButton.Click += new System.EventHandler(this.deleteSpaceButton_Click);
            // 
            // spaceInfoGrid
            // 
            this.spaceInfoGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.spaceInfoGrid.Location = new System.Drawing.Point(0, 264);
            this.spaceInfoGrid.Name = "spaceInfoGrid";
            this.spaceInfoGrid.Size = new System.Drawing.Size(198, 140);
            this.spaceInfoGrid.TabIndex = 2;
            this.spaceInfoGrid.ToolbarVisible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStrip,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(89, 17);
            this.statusStrip.Text = "No File Loaded.";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 22);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(198, 242);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoom200,
            this.zoom150,
            this.zoom125,
            this.zoom100,
            this.zoom90,
            this.zoom80,
            this.zoom70,
            this.zoom60,
            this.zoom50});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(55, 20);
            this.toolStripSplitButton1.Text = "Zoom";
            this.toolStripSplitButton1.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripSplitButton1_DropDownItemClicked);
            // 
            // zoom100
            // 
            this.zoom100.Name = "zoom100";
            this.zoom100.Size = new System.Drawing.Size(180, 22);
            this.zoom100.Tag = "100";
            this.zoom100.Text = "100%";
            // 
            // zoom90
            // 
            this.zoom90.Name = "zoom90";
            this.zoom90.Size = new System.Drawing.Size(180, 22);
            this.zoom90.Tag = "90";
            this.zoom90.Text = "90%";
            // 
            // zoom80
            // 
            this.zoom80.Name = "zoom80";
            this.zoom80.Size = new System.Drawing.Size(180, 22);
            this.zoom80.Tag = "80";
            this.zoom80.Text = "80%";
            // 
            // zoom70
            // 
            this.zoom70.Name = "zoom70";
            this.zoom70.Size = new System.Drawing.Size(180, 22);
            this.zoom70.Tag = "70";
            this.zoom70.Text = "70%";
            // 
            // zoom60
            // 
            this.zoom60.Name = "zoom60";
            this.zoom60.Size = new System.Drawing.Size(180, 22);
            this.zoom60.Tag = "60";
            this.zoom60.Text = "60%";
            // 
            // zoom50
            // 
            this.zoom50.Name = "zoom50";
            this.zoom50.Size = new System.Drawing.Size(180, 22);
            this.zoom50.Tag = "50";
            this.zoom50.Text = "50%";
            // 
            // zoom125
            // 
            this.zoom125.Name = "zoom125";
            this.zoom125.Size = new System.Drawing.Size(180, 22);
            this.zoom125.Tag = "125";
            this.zoom125.Text = "125%";
            // 
            // zoom150
            // 
            this.zoom150.Name = "zoom150";
            this.zoom150.Size = new System.Drawing.Size(180, 22);
            this.zoom150.Tag = "150";
            this.zoom150.Text = "150%";
            // 
            // zoom200
            // 
            this.zoom200.Name = "zoom200";
            this.zoom200.Size = new System.Drawing.Size(180, 22);
            this.zoom200.Tag = "200";
            this.zoom200.Text = "200%";
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "SMPe v0.1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStrip;
        private System.Windows.Forms.PropertyGrid spaceInfoGrid;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem perspectiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sideToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addSpaceButton;
        private System.Windows.Forms.ToolStripButton deleteSpaceButton;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem zoom100;
        private System.Windows.Forms.ToolStripMenuItem zoom90;
        private System.Windows.Forms.ToolStripMenuItem zoom80;
        private System.Windows.Forms.ToolStripMenuItem zoom70;
        private System.Windows.Forms.ToolStripMenuItem zoom200;
        private System.Windows.Forms.ToolStripMenuItem zoom150;
        private System.Windows.Forms.ToolStripMenuItem zoom125;
        private System.Windows.Forms.ToolStripMenuItem zoom60;
        private System.Windows.Forms.ToolStripMenuItem zoom50;
    }
}

