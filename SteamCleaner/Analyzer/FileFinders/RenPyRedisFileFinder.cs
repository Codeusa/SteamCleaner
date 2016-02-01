#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace SteamCleaner.Analyzer.FileFinders
{
    public class RenPyRedisFileFinder : IFileFinder
    {
        private readonly string[] filters = {"darwin", "linux"};

        public IEnumerable<string> FindFiles(IEnumerable<string> paths)
        {
            var files = new List<string>();
            Search(files, paths);
            return files;
        }

        public void Search(List<string> files, IEnumerable<string> paths)
        {
            foreach (var path in paths)
            {
                if (!path.Contains("\\lib"))
                {
                    continue;
                }
                AddFiles(files, path);
                Search(files, Directory.GetDirectories(path));
            }
        }

        private void AddFiles(List<string> files, string path)
        {
            foreach (var filter in filters.Where(filter => path.ToLower().Contains(filter)))
            {
                files.AddRange(Directory.GetFiles(path).Select(f => f));
            }
        }
    }
}