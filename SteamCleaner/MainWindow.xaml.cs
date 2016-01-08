using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SteamCleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TwitterMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://twitter.com/andrewmd5");
        }

        private void GitHubMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Codeusa/SteamCleaner");
        }

        private void BorderlessGamingMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://store.steampowered.com/app/388080");
        }

        private void ReportABugMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/Codeusa/SteamCleaner/issues");
        }
    }


}
