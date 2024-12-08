
namespace StudentManager.Screens.Branches
{
    partial class ManageBranchesForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddNewBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BranchesDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BranchesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseToolStripMenuItem,
            this.toolStripMenuItem1,
            this.AddNewBranchToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // CloseToolStripMenuItem
            // 
            this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
            this.CloseToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.CloseToolStripMenuItem.Text = "&Close";
            this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem1.Text = "|";
            // 
            // AddNewBranchToolStripMenuItem
            // 
            this.AddNewBranchToolStripMenuItem.Name = "AddNewBranchToolStripMenuItem";
            this.AddNewBranchToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.AddNewBranchToolStripMenuItem.Text = "Add New Branch";
            this.AddNewBranchToolStripMenuItem.Click += new System.EventHandler(this.AddNewBranchToolStripMenuItem_Click);
            // 
            // BranchesDataGridView
            // 
            this.BranchesDataGridView.AllowUserToAddRows = false;
            this.BranchesDataGridView.AllowUserToDeleteRows = false;
            this.BranchesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BranchesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BranchesDataGridView.Location = new System.Drawing.Point(-7, 23);
            this.BranchesDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.BranchesDataGridView.Name = "BranchesDataGridView";
            this.BranchesDataGridView.ReadOnly = true;
            this.BranchesDataGridView.Size = new System.Drawing.Size(809, 378);
            this.BranchesDataGridView.TabIndex = 6;
            this.BranchesDataGridView.DoubleClick += new System.EventHandler(this.BranchesDataGridView_DoubleClick);
            // 
            // ManageBranchesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 398);
            this.Controls.Add(this.BranchesDataGridView);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ManageBranchesForm";
            this.ShowInTaskbar = true;
            this.Text = "Manage Branches Screen";
            this.Load += new System.EventHandler(this.ManageBranchesForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BranchesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AddNewBranchToolStripMenuItem;
        private System.Windows.Forms.DataGridView BranchesDataGridView;
    }
}