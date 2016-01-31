using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer
{
    public class DesuraAnalyzer : IAnalyzer
    {
        public string Name => "Desura";

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
                var is64Bit = Environment.Is64BitOperatingSystem;
                var regPath = is64Bit
                    ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall"
                    : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
                List<string> paths = new List<string>();
                paths.AddRange(from subkeyName in rootKey.GetSubKeyNames()
                               where subkeyName.StartsWith("Desura_", StringComparison.Ordinal)
                               select Registry.LocalMachine.OpenSubKey(regPath + "\\" + subkeyName)
                               into subKey
                               select subKey?.GetValue("InstallLocation").ToString());
                return paths;
            }
        }

        public RegistryKey GetRootKey()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit
                ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall"
                : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root;
        }
    }
}
