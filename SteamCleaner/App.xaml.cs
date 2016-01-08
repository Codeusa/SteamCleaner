using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SteamCleaner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        void AppStartup(object sender, StartupEventArgs args)
        {
            
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
