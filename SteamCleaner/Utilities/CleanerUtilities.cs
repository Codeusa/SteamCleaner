#region

using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

#endregion

namespace SteamCleaner.Utilities
{
    internal class CleanerUtilities
    {
        private static List<Redistributables> cachedRedistributables;
        public static bool updateRedistributables = true;


        private static List<string> DirectoryWalk(string sDir)
        {
            var files = new List<string>();

            foreach (var d in from d in Directory.GetDirectories(sDir)
                let regex = new Regex("(.*)(directx|redist|miles)(.*)", RegexOptions.IgnoreCase)
                let match = regex.Match(d)
                where match.Success
                select d)
            {
                files.AddRange(DirectoryWalk(d));
                files.AddRange(from f in Directory.GetFiles(d)
                    let filePathRegex = new Regex("(cab|exe|msi)", RegexOptions.IgnoreCase)
                    let fileMatch = filePathRegex.Match(f)
                    where fileMatch.Success
                    select f);
            }
            return files;
        }
            
        public static List<Redistributables> FindRedistributables()
        {
            if (cachedRedistributables != null && !updateRedistributables)
            {
                return cachedRedistributables;
            }
            var steamPaths = SteamUtilities.SteamPaths();
            var crawlableDirs = steamPaths.Select(path => SteamUtilities.FixPath(path)).Where(appPath => Directory.Exists(appPath)).ToList();
            var gameDirs =
                crawlableDirs.Select(Directory.GetDirectories).SelectMany(directories => directories).ToList();
            var redistFiles = new List<string>();
            foreach (var dir in gameDirs)
            {
                redistFiles.AddRange(DirectoryWalk(dir));
            }
            var cleanAbleFiles = redistFiles.Select(file => new Redistributables
            {
                Size = new FileInfo(file).Length,
                Path = file
            }).ToList();
            cachedRedistributables = cleanAbleFiles;
            updateRedistributables = false;
            return cleanAbleFiles;
        }

        public static int TotalFiles()
        {
            var redistributables = FindRedistributables();
            return redistributables.Count;
        }

        public static string TotalTakenSpace()
        {
            var redistributables = FindRedistributables();
            var totalTakenSpace = StringUtilities.GetBytesReadable(redistributables.Sum(file => file.Size));
            return totalTakenSpace;
        }

        public static async Task CleanData()
        {
            var redistributables = FindRedistributables();
            var totalFiles = redistributables.Count;

            var dialog = new ConfirmationDialog
            {
                MessageTextBlock =
                {
                    Text = "Are you sure you wish to do this?  " + totalFiles +
                           " files will be permanently deleted."
                }
            };
            var result = await DialogHost.Show(dialog);
            if (!"1".Equals(result)) return;

            var progressBar = new ProgressBar
            {
                Maximum = redistributables.Count,
                Width = 300,
                Margin = new Thickness(32)
            };
            await
                DialogHost.Show(progressBar,
                    (DialogOpenedEventHandler)
                        ((o, args) => DeleteFiles(redistributables, progressBar, args.Session)));
        }

        private static void DeleteFiles(IEnumerable<Redistributables> redistributables, ProgressBar progressBar, DialogSession dialogSession)
        {
            Task.Factory.StartNew(() =>
            {
                var failures = new List<string>();

                foreach (var item in redistributables.Select((red, idx) => new {red, idx}))
                {
                    progressBar.Dispatcher.BeginInvoke(new Action(() => progressBar.Value = item.idx));
                    try
                    {
                        throw new ApplicationException("cunt");
                        if (File.Exists(item.red.Path))
                            File.Delete(item.red.Path);
                    }
                    catch
                    {
                        failures.Add(item.red.Path);                        
                    }
                }

                if (failures.Count > 0)
                    progressBar.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        var failuresDialog = new FailuresDialog {FailuresListBox = {ItemsSource = failures}};
                        dialogSession.UpdateContent(failuresDialog);
                    }));
                else
                    progressBar.Dispatcher.BeginInvoke(new Action(dialogSession.Close));                
            });            
        }

        public class Redistributables
        {
            public string Path { get; set; }
            public long Size { get; set; }
        }
    }
}