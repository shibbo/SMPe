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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextNode0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextNode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextNode2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextNode3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aux2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeID,
            this.nextNode0,
            this.nextNode1,
            this.nextNode2,
            this.nextNode3,
            this.nodeType,
            this.userDef,
            this.aux2});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 426);
            this.dataGridView1.TabIndex = 1;
            // 
            // nodeID
            // 
            this.nodeID.HeaderText = "Node ID";
            this.nodeID.Name = "nodeID";
            // 
            // nextNode0
            // 
            this.nextNode0.HeaderText = "Next Node [0]";
            this.nextNode0.Name = "nextNode0";
            // 
            // nextNode1
            // 
            this.nextNode1.HeaderText = "Next Node [1]";
            this.nextNode1.Name = "nextNode1";
            // 
            // nextNode2
            // 
            this.nextNode2.HeaderText = "Next Node [2]";
            this.nextNode2.Name = "nextNode2";
            // 
            // nextNode3
            // 
            this.nextNode3.HeaderText = "Next Node [3]";
            this.nextNode3.Name = "nextNode3";
            // 
            // nodeType
            // 
            this.nodeType.HeaderText = "Node Type";
            this.nodeType.Name = "nodeType";
            this.nodeType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.nodeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nodeType.Width = 200;
            // 
            // userDef
            // 
            this.userDef.HeaderText = "User Defined Attributes";
            this.userDef.Name = "userDef";
            this.userDef.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.userDef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.userDef.Width = 150;
            // 
            // aux2
            // 
            this.aux2.HeaderText = "AUX 2";
            this.aux2.Name = "aux2";
            this.aux2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.aux2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
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
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SMPe v0.1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextNode0;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextNode1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextNode2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextNode3;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn userDef;
        private System.Windows.Forms.DataGridViewTextBoxColumn aux2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

