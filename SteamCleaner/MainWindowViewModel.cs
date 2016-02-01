#region

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SquaredInfinity.Foundation.Extensions;
using SteamCleaner.Analyzer;
using SteamCleaner.Cleaner;
using SteamCleaner.Model;
using SteamCleaner.Utilities;

#endregion

namespace SteamCleaner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AnalyzerService analyzerService;
        private readonly CleanerService cleanerService;

        private bool _canRefresh;
        private string _statistics;

        private AnalyzeResult currentResult;

        public MainWindowViewModel()
        {
            Paths = new ObservableCollection<string>();
            Files = new ObservableCollection<FileInfo>();

            CleanCommand = new ActionCommand(o => RunClean(), o => CanRefresh);
            RefreshCommand = new ActionCommand(async o => await RunRefresh(), o => CanRefresh);

            analyzerService = new AnalyzerService();
            cleanerService = new CleanerService();

            //TODO run on a background thread, add spinner etc
            Init();
        }

        public ObservableCollection<string> Paths { get; }

        public ObservableCollection<FileInfo> Files { get; }

        public ActionCommand RefreshCommand { get; }

        public ActionCommand CleanCommand { get; }

        public bool CanRefresh
        {
            get { return _canRefresh; }
            set
            {
                _canRefresh = value;
                CleanCommand.Refresh();
                RefreshCommand.Refresh();
            }
        }

        public string Statistics
        {
            get { return _statistics; }
            set
            {
                if (_statistics == value) return;
                _statistics = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Init()
        {
            await RunRefresh();
        }

        private async Task RunRefresh()
        {
            CanRefresh = false;
            Paths.Clear();
            Files.Clear();
            var callback = new Progress<Tuple<string, int>>(UpdateProgress);
            var result = await analyzerService.AnalyzeAsync(callback);
            Files.AddRange(result.Files);
            Paths.AddRange(result.UsedAnalyzers);
            Statistics = string.Format("{0} files found ({1})", result.Files.Count,
                StringUtilities.GetBytesReadable(result.TotalSize));
            currentResult = result;
            CanRefresh = true;
        }

        private void UpdateProgress(Tuple<string, int> progress)
        {
            Statistics = string.Format("{0} ({1})", progress.Item1, progress.Item2);
        }

        private async void RunClean()
        {
            //if someone runs two refreshes at the same time there could be issues here
            if (currentResult == null)
            {
                await RunRefresh();
            }
            CanRefresh = false;

            //really should not be done here..
            if (await Confirm())
            {
                var progressBar = new ProgressBar
                {
                    Maximum = currentResult.Files.Count,
                    Width = 300,
                    Margin = new Thickness(32)
                };
                await
                    DialogHost.Show(progressBar,
                        (DialogOpenedEventHandler)
                            ((o, args) => StartCleaning(progressBar, args.Session)));
            }
            else
            {
                CanRefresh = true;
            }
        }

        private async void StartCleaning(ProgressBar progressBar, DialogSession session)
        {
            var callback =
                new Progress<int>(
                    i => { progressBar.Dispatcher.BeginInvoke(new Action(() => { progressBar.Value = i; })); });
            var result = await cleanerService.CleanAsync(currentResult, callback);
            if (result.Failures.Count > 0)
                await progressBar.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var failuresDialog = new FailuresDialog {FailuresListBox = {ItemsSource = result.Failures}};
                    session.UpdateContent(failuresDialog);
                }));
            else
                await progressBar.Dispatcher.BeginInvoke(new Action(session.Close));
            CanRefresh = true;
            await RunRefresh();
        }

        private async Task<bool> Confirm()
        {
            var dialog = new ConfirmationDialog
            {
                MessageTextBlock =
                {
                    Text = "Are you sure you wish to do this?  " + currentResult.Files.Count +
                           " files will be permanently deleted."
                }
            };
            var result = await DialogHost.Show(dialog);
            return "1".Equals(result);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}