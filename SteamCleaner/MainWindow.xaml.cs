#region

using System.Diagnostics;
using System.Windows;

#endregion

namespace SteamCleaner
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
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