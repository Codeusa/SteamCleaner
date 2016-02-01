using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.FileFinders
{
    public class RedisFileFinder : IFileFinder
    {
        Regex dirRegex = new Regex("(.*)(directx|redist|miles|support|installer)(.*)", RegexOptions.IgnoreCase);
        Regex fileRegex = new Regex("(cab|exe|msi|so)", RegexOptions.IgnoreCase);

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
                if (!dirRegex.IsMatch(path))
                {
                    continue;
                }
                AddFiles(files, path);
                Search(files, Directory.GetDirectories(path));
            }
        }

        private void AddFiles(List<string> files, string path)
        {
            files.AddRange(from f in Directory.GetFiles(path)
                           where fileRegex.IsMatch(f)
                           select f);
        }
    }
}
