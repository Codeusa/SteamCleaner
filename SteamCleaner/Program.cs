#region

using System;
using System.Windows;
using SteamCleaner.Utilities;
using Application = System.Windows.Forms.Application;

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

            //TODO restore
            //Tools.CheckForUpdates();
            //Application.Run(new SteamCleaner());


            var app = new App { ShutdownMode = ShutdownMode.OnLastWindowClose };
            //app.InitializeComponent();

            app.Run(new MainWindow());
        }
    }
}