#region

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using SteamCleaner.Utilities;

#endregion

namespace SteamCleaner
{
    public partial class SteamCleaner : Form
    {
        public SteamCleaner()
        {
            InitializeComponent();
        }

        private void RefreshFiles()
        {
            foundFilesGridView.DataSource = null;
            foundFilesGridView.Rows.Clear();
            var data = CleanerUtilities.FindRedistributables();
            foreach (var file in data)
            {
                var rowId = foundFilesGridView.Rows.Add();
                var row = foundFilesGridView.Rows[rowId];

                row.Cells["FileColumn"].Value = file.Path;
                row.Cells["SizeColumn"].Value = StringUtilities.GetBytesReadable(file.Size);
            }
            statsLabel.Text = CleanerUtilities.TotalFiles() + " files have been found (" + CleanerUtilities.TotalTakenSpace() + ") ";
        }

        private void CleanerFormLoader(object sender, EventArgs e)
        {
            RefreshFiles();
            collectionPathsList.DataSource = SteamUtilities.SteamPaths();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            CleanerUtilities.updateRedistributables = true;
            RefreshFiles();
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            if (CleanerUtilities.TotalFiles() > 0)
            {
                CleanerUtilities.CleanData();
                CleanerUtilities.updateRedistributables = true;
                RefreshFiles();
            }
            else MessageBox.Show("No files selected for cleaning");
        }

        private void twitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/andrewmd5");
        }

        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Codeusa/SteamCleaner");
        }

        private void borderlessGamingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://store.steampowered.com/app/388080");
        }

        private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Codeusa/SteamCleaner/issues");
        }

    }
}