using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.FileFinders
{
    public class RenPyRedisFileFinder : IFileFinder
    {

        string[] filters = new string[] { "darwin", "linux" };

        public IEnumerable<string> FindFiles(IEnumerable<string> paths)
        {
            List<string> files = new List<string>();
            Search(files, paths);
            return files;
        }

        public void Search(List<string> files, IEnumerable<string> paths)
        {
            foreach (string path in paths)
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
