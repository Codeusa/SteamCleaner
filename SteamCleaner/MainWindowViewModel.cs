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

#endregion

namespace SteamCleaner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<FileViewModel> _filesInternal = new ObservableCollection<FileViewModel>();
        private readonly ObservableCollection<string> _pathsInternal = new ObservableCollection<string>();
        private string _statistics;

        public MainWindowViewModel()
        {
            Paths = new ReadOnlyObservableCollection<string>(_pathsInternal);
            Files = new ReadOnlyObservableCollection<FileViewModel>(_filesInternal);

            CleanCommand = new ActionCommand(RunClean);
            RefreshCommand = new ActionCommand(RunRefresh);

            //TODO run on a background thread, add spinner etc
            RunRefresh();
        }


        public ReadOnlyObservableCollection<string> Paths { get; }

        public ReadOnlyObservableCollection<FileViewModel> Files { get; }

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

        private void RunRefresh()
        {
            //needs to be called to ensure we aren't loading a previously stored object.
            CleanerUtilities.updateRedistributables = true;
            _pathsInternal.Clear();
            var steamPaths = Steam.SteamPaths();
             _pathsInternal.AddRange(steamPaths.Select(steamPath => Steam.FixPath(steamPath)).Where(path => Directory.Exists(path)).ToList());
            if (Gog.Exisit())
            {
                _pathsInternal.Add("GoG Games Detected");
            }
            if (Origin.Exist())
            {
                _pathsInternal.Add("Origin Games Detected");
            }
            if (Uplay.Exist())
            {
                _pathsInternal.Add("Uplay Games Detected");
            }
            if (Battlenet.Exist())
            {
                _pathsInternal.Add("Battle.net Games Detected");
            }
            if (Desura.Exist())
            {
                _pathsInternal.Add("Desura Games Detected");
            }
            _filesInternal.Clear();
            foreach (
                var fileViewModel in
                    CleanerUtilities.FindRedistributables()
                        .Select(r => new FileViewModel(r.Path, StringUtilities.GetBytesReadable(r.Size))))
                _filesInternal.Add(fileViewModel);

            Statistics = CleanerUtilities.TotalFiles() + " files have been found (" + CleanerUtilities.TotalTakenSpace() +
                         ") ";
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