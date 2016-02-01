#region

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

#endregion

namespace SteamCleaner.Analyzer.Analyzers
{
    public class NexonAnalyzer : IAnalyzer
    {
        public string Name => "Nexon";

        public bool CheckExists()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit
                ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall\Nexon Nexon Launcher"
                : @"Software\Microsoft\Windows\CurrentVersion\Uninstall\Nexon Nexon Launcher";
            using (var key = Registry.LocalMachine.OpenSubKey(regPath))
            {
                return key?.GetValue("InstallLocation") != null;
            }
        }

        public IEnumerable<string> FindPaths()
        {
            var paths = new List<string>();
            var settingsDb = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData), @"NexonLauncher\apps-settings.db");
            var jsonData = JObject.Parse(File.ReadAllText(settingsDb)); // parse as array  

            foreach (var pair in (JObject) jsonData["installedApps"])
            {
                var name = pair.Key;
                var child = pair.Value;
                var installPath = child["installPath"].ToString();
                if (!string.IsNullOrEmpty(installPath))
                {
                    paths.Add(installPath);
                }
            }
            return paths;
        }
    }
}