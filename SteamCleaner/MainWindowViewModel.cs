#region

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SquaredInfinity.Foundation.Extensions;
using SteamCleaner.Clients;
using SteamCleaner.Utilities;
using SteamCleaner.Analyzer;
using SteamCleaner.Model;
using System;
using System.Data;

#endregion

namespace SteamCleaner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AnalyzerService analyzerService;
        private string _statistics;

        public MainWindowViewModel()
        {
            Paths = new ObservableCollection<string>();
            Files = new ObservableCollection<FileInfo>();

            CleanCommand = new ActionCommand(RunClean);
            RefreshCommand = new ActionCommand(RunRefresh);

            analyzerService = new AnalyzerService();

            //TODO run on a background thread, add spinner etc
            RunRefresh();
        }


        public ObservableCollection<string> Paths { get; private set; }

        public ObservableCollection<FileInfo> Files { get; private set; }

        public ICommand RefreshCommand { get; }

        public ICommand CleanCommand { get; }

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
        
        private async void RunRefresh()
        {
            Paths.Clear();
            Files.Clear();
            var callback = new Progress<Tuple<string, int>>(UpdateProgress);
            AnalyzeResult result = await analyzerService.AnalyzeAsync(callback);
            Files.AddRange(result.Files);
            Paths.AddRange(result.UsedAnalyzers);            
            Statistics = string.Format("{0} files found ({1})", result.Files.Count(), result.TotalSize);
        }

        private void UpdateProgress(Tuple<string, int> progress)
        {
            Statistics = string.Format("{0} ({1})", progress.Item1, progress.Item2);
        }

        private async void RunClean()
        {
            await CleanerUtilities.CleanData();

            RunRefresh();
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}