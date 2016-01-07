#region

using System;
using System.Windows.Forms;
using SteamCleaner.Utilities;

#endregion

namespace SteamCleaner
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Tools.CheckForUpdates();
            Application.Run(new SteamCleaner());
        }
    }
}