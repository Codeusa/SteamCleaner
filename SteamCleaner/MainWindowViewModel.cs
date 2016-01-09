using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SteamCleaner.Utilities;

namespace SteamCleaner
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ObservableCollection<string> _pathsInternal = new ObservableCollection<string>();
        private readonly ObservableCollection<FileViewModel> _filesInternal = new ObservableCollection<FileViewModel>();
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

        private void RunRefresh()
        {
            //needs to be called to ensure we aren't loading a previously stored object.
            CleanerUtilities.updateRedistributables = true;
            _pathsInternal.Clear();
            foreach (var steamPath in SteamUtilities.SteamPaths())
                _pathsInternal.Add(steamPath);

            _filesInternal.Clear();
            foreach (var fileViewModel in CleanerUtilities.FindRedistributables().Select(r => new FileViewModel(r.Path, StringUtilities.GetBytesReadable(r.Size))))            
                _filesInternal.Add(fileViewModel);         
            
            Statistics = CleanerUtilities.TotalFiles() + " files have been found (" + CleanerUtilities.TotalTakenSpace() + ") ";

        }

        private async void RunClean()
        {
            await CleanerUtilities.CleanData();

            RunRefresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
