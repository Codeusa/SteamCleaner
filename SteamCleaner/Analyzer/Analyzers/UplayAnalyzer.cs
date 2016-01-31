using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.Analyzers
{
    public class UplayAnalyzer : IAnalyzer
    {
        public string Name => "Uplay";

        public bool CheckExists()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"Software\Wow6432Node\Ubisoft\Launcher" : @"Software\Ubisoft\Launcher";
            using (var key = Registry.LocalMachine.OpenSubKey(regPath))
            {
                return key?.GetValue("InstallDir") != null;
            }
        }

        public IEnumerable<string> FindPaths()
        {
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"Software\Wow6432Node\Ubisoft\Launcher" : @"Software\Ubisoft\Launcher";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
                paths.AddRange(
                    root.GetSubKeyNames()
                        .Select(keyname => root.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue("InstallDir"))
                        .Select(o => o?.ToString())
                        .Where(Directory.Exists));
            return paths;
        }
    }
}
