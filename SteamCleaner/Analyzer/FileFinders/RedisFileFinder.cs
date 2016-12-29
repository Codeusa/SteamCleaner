#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SteamCleaner.Utilities.Files;

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
                var targetPath = path;
                if (SymbolicLink.IsSymbolic(path) && SymbolicLink.Exists(path))
                {
                    targetPath = SymbolicLink.GetTarget(path);
                }
                AddFiles(files, targetPath);
                Search(files, Directory.GetDirectories(targetPath));
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