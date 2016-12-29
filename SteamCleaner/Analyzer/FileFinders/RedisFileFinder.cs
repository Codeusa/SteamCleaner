#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace SteamCleaner.Analyzer.FileFinders
{
    public class RedisFileFinder : IFileFinder
    {
        private readonly Regex dirRegex = new Regex("(.*)(directx|redist|miles|support|installer)(.*)",
            RegexOptions.IgnoreCase);

      

        private readonly Regex fileRegex = new Regex("(cab|exe|msi|so)", RegexOptions.IgnoreCase);

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
                //These three games put game files in the support folders
                if (path.Contains("Heroes of the Storm") || path.Contains("StarCraft"))
                {
                    continue;
                }
                if (path.Contains("Penumbra Overture\\redist"))
                {
                    continue;
                    
                }
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