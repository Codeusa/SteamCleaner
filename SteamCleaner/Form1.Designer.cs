using SteamCleaner.Utilities;

namespace SteamCleaner
{
    partial class SteamCleaner
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SteamCleaner));
            this.detectListLabel = new System.Windows.Forms.Label();
            this.foundFilesGridView = new System.Windows.Forms.DataGridView();
            this.FileColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refreshButton = new System.Windows.Forms.Button();
            this.collectionPathsList = new System.Windows.Forms.ListBox();
            this.collectionLabel = new System.Windows.Forms.Label();
            this.cleanButton = new System.Windows.Forms.Button();
            this.statsSectionLabel = new System.Windows.Forms.Label();
            this.statsLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borderlessGamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportABugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.foundFilesGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // detectListLabel
            // 
            this.detectListLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detectListLabel.AutoSize = true;
            this.detectListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectListLabel.Location = new System.Drawing.Point(12, 118);
            this.detectListLabel.Name = "detectListLabel";
            this.detectListLabel.Size = new System.Drawing.Size(148, 24);
            this.detectListLabel.TabIndex = 1;
            this.detectListLabel.Text = "Redistributables:";
            // 
            // foundFilesGridView
            // 
            this.foundFilesGridView.AllowUserToAddRows = false;
            this.foundFilesGridView.AllowUserToDeleteRows = false;
            this.foundFilesGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foundFilesGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.foundFilesGridView.BackgroundColor = System.Drawing.Color.White;
            this.foundFilesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.foundFilesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileColumn,
            this.SizeColumn});
            this.foundFilesGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.foundFilesGridView.Location = new System.Drawing.Point(12, 163);
            this.foundFilesGridView.Name = "foundFilesGridView";
            this.foundFilesGridView.Size = new System.Drawing.Size(592, 258);
            this.foundFilesGridView.TabIndex = 2;
            // 
            // FileColumn
            // 
            this.FileColumn.HeaderText = "File";
            this.FileColumn.Name = "FileColumn";
            // 
            // SizeColumn
            // 
            this.SizeColumn.HeaderText = "Size";
            this.SizeColumn.Name = "SizeColumn";
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(492, 134);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(112, 23);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // collectionPathsList
            // 
            this.collectionPathsList.FormattingEnabled = true;
            this.collectionPathsList.HorizontalScrollbar = true;
            this.collectionPathsList.Location = new System.Drawing.Point(16, 72);
            this.collectionPathsList.Name = "collectionPathsList";
            this.collectionPathsList.Size = new System.Drawing.Size(238, 43);
            this.collectionPathsList.TabIndex = 4;
            // 
            // collectionLabel
            // 
            this.collectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.collectionLabel.AutoSize = true;
            this.collectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.collectionLabel.Location = new System.Drawing.Point(12, 35);
            this.collectionLabel.Name = "collectionLabel";
            this.collectionLabel.Size = new System.Drawing.Size(119, 24);
            this.collectionLabel.TabIndex = 5;
            this.collectionLabel.Text = "Steam Paths:";
            // 
            // cleanButton
            // 
            this.cleanButton.Location = new System.Drawing.Point(374, 134);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(112, 23);
            this.cleanButton.TabIndex = 6;
            this.cleanButton.Text = "Clean";
            this.cleanButton.UseVisualStyleBackColor = true;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // statsSectionLabel
            // 
            this.statsSectionLabel.AutoSize = true;
            this.statsSectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsSectionLabel.Location = new System.Drawing.Point(370, 35);
            this.statsSectionLabel.Name = "statsSectionLabel";
            this.statsSectionLabel.Size = new System.Drawing.Size(54, 24);
            this.statsSectionLabel.TabIndex = 7;
            this.statsSectionLabel.Text = "Stats:";
            // 
            // statsLabel
            // 
            this.statsLabel.AutoSize = true;
            this.statsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statsLabel.Location = new System.Drawing.Point(371, 72);
            this.statsLabel.Name = "statsLabel";
            this.statsLabel.Size = new System.Drawing.Size(222, 13);
            this.statsLabel.TabIndex = 8;
            this.statsLabel.Text = "A total of 2424 files have been found (245mb)";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(616, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.twitterToolStripMenuItem,
            this.githubToolStripMenuItem,
            this.borderlessGamingToolStripMenuItem,
            this.reportABugToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // twitterToolStripMenuItem
            // 
            this.twitterToolStripMenuItem.Name = "twitterToolStripMenuItem";
            this.twitterToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.twitterToolStripMenuItem.Text = "Twitter";
            this.twitterToolStripMenuItem.Click += new System.EventHandler(this.twitterToolStripMenuItem_Click);
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.githubToolStripMenuItem.Text = "Github";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.githubToolStripMenuItem_Click);
            // 
            // borderlessGamingToolStripMenuItem
            // 
            this.borderlessGamingToolStripMenuItem.Name = "borderlessGamingToolStripMenuItem";
            this.borderlessGamingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.borderlessGamingToolStripMenuItem.Text = "Borderless-Gaming";
            this.borderlessGamingToolStripMenuItem.Click += new System.EventHandler(this.borderlessGamingToolStripMenuItem_Click);
            // 
            // reportABugToolStripMenuItem
            // 
            this.reportABugToolStripMenuItem.Name = "reportABugToolStripMenuItem";
            this.reportABugToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.reportABugToolStripMenuItem.Text = "Report a Bug";
            this.reportABugToolStripMenuItem.Click += new System.EventHandler(this.reportABugToolStripMenuItem_Click);
            // 
            // SteamCleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 433);
            this.Controls.Add(this.statsLabel);
            this.Controls.Add(this.statsSectionLabel);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.collectionLabel);
            this.Controls.Add(this.collectionPathsList);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.foundFilesGridView);
            this.Controls.Add(this.detectListLabel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(632, 472);
            this.Name = "SteamCleaner";
            this.Text = "Steam Cleaner";
            this.Load += new System.EventHandler(this.CleanerFormLoader);
            ((System.ComponentModel.ISupportInitialize)(this.foundFilesGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label detectListLabel;
        private System.Windows.Forms.DataGridView foundFilesGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SizeColumn;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ListBox collectionPathsList;
        private System.Windows.Forms.Label collectionLabel;
        private System.Windows.Forms.Button cleanButton;
        private System.Windows.Forms.Label statsSectionLabel;
        private System.Windows.Forms.Label statsLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borderlessGamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportABugToolStripMenuItem;
    }
}

