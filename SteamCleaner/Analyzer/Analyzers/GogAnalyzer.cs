using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.Analyzers
{
    public class GogAnalyzer : IAnalyzer
    {
        public string Name => "GOG";

        public bool CheckExists()
        {
            using (var rootKey = GetRootKey())
            {
                return rootKey != null;
            }
        }

        public IEnumerable<string> FindPaths()
        {
            using (var rootKey = GetRootKey())
            {
                if (rootKey == null)
                {
                    return null;
                }
                List<string> paths = new List<string>();
                paths.AddRange(rootKey.GetSubKeyNames()
                       .Select(keyname => rootKey.OpenSubKey(keyname))
                       .Where(key => key != null)
                       .Select(key => key.GetValue("PATH"))
                       .Select(o => o?.ToString())
                       .Where(Directory.Exists));
                return paths;
            }
        }

        public RegistryKey GetRootKey()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"SOFTWARE\Wow6432Node\GOG.com\Games" : @"SOFTWARE\GOG.com\Games";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root;
        }
    }
}
