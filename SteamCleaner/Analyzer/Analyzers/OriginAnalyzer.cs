using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.Analyzers
{
    public class OriginAnalyzer : IAnalyzer
    {
        public string Name => "Origin";

        public bool CheckExists()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"Software\Wow6432Node\Origin" : @"Software\Origin";
            using (var key = Registry.LocalMachine.OpenSubKey(regPath))
            {
                return key?.GetValue("ClientPath") != null;
            }
        }

        public IEnumerable<string> FindPaths()
        {
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"SOFTWARE\Wow6432Node\Electronic Arts" : @"SOFTWARE\Electronic Arts";
            using (var root = Registry.LocalMachine.OpenSubKey(regPath))
            {
                if (root != null)
                    paths.AddRange(
                        root.GetSubKeyNames()
                            .Select(keyname => root.OpenSubKey(keyname))
                            .Where(key => key != null)
                            .Select(key => key.GetValue(@"Install Dir"))
                            .Select(o => o?.ToString())
                            .Where(Directory.Exists));
            }
            regPath = is64Bit ? @"SOFTWARE\Wow6432Node\EA Games" : @"SOFTWARE\EA Games";
            using (var legacyRoot = Registry.LocalMachine.OpenSubKey(regPath))
            {
                if (legacyRoot != null)
                    paths.AddRange(legacyRoot.GetSubKeyNames()
                            .Select(keyname => legacyRoot.OpenSubKey(keyname))
                            .Where(key => key != null)
                            .Select(key => key.GetValue(@"Install Dir"))
                            .Select(o => o?.ToString())
                            .Where(Directory.Exists));
            }
            return paths;
        }
    }
}
