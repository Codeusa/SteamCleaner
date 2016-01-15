#region

using System.Diagnostics;
using System.Windows;
using SteamCleaner.Utilities;

#endregion

namespace SteamCleaner
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs args)
        {
            if (!Debugger.IsAttached)
                ExceptionHandler.AddGlobalHandlers();

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
            mainWindow.Show();
            Tools.CheckForUpdates();
        }
    }
}