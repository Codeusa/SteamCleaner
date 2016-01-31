using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.Analyzers
{
    public class SteamAnalyzer : IAnalyzer
    {

        private Regex dataRegex = new Regex("\\\"(.*)\\\"(.*)\\\"", RegexOptions.IgnoreCase);

        public string Name => "Steam";

        public bool CheckExists() => FindSteamPath() != null;

        public IEnumerable<string> FindPaths()
        {
            string steamPath = FindSteamPath();
            //we should be able to ignore the null check here because this code should only ever
            //be called if CheckExists is true. Going to add it anyway just incase
            if (steamPath == null)
            {
                return null;
            }
            List<string> paths = new List<string>();
            paths.Add(FixPath(steamPath));
            IEnumerable<string> secondaryPaths = FindSecondaryInstallPaths(steamPath);
            if (secondaryPaths != null)
            {
                paths.AddRange(secondaryPaths);
            }
            return paths.Select(Directory.GetDirectories)
                        .SelectMany(directories => directories);
        }

        private IEnumerable<string> FindSecondaryInstallPaths(string steamPath)
        {
            var configPath = steamPath + "\\config\\config.vdf";
            if (!File.Exists(configPath))
            {
                return null;
            }
            var paths = new List<string>();
            var data = File.ReadAllText(configPath);
            var numberOfInstallPaths = CountOccurences("BaseInstallFolder", data);
            var dataArray = File.ReadAllLines(configPath);
            for (var i = 0; i < numberOfInstallPaths; i++)
            {
                var slot = i + 1;
                paths.AddRange(from t in dataArray
                               where t.Contains("BaseInstallFolder_" + slot)
                               select t.Trim()
                    into dataString
                               select dataRegex.Match(dataString)
                    into match
                               where match.Success
                               let path = FixPath(match.Groups[2].Value).Replace("\\\\", "\\")
                               where Directory.Exists(path)
                               select path);
            }
            return paths;
        }

        private string FindSteamPath(bool ignoreArchitecture = false, bool check64 = false)
        {
            bool is64Bit =  ignoreArchitecture ? check64 : Environment.Is64BitOperatingSystem;
            string regPath = is64Bit ? @"SOFTWARE\Wow6432Node\Valve\Steam" : @"SOFTWARE\Valve\Steam";
            var key = Registry.LocalMachine.OpenSubKey(regPath);
            string value = (string) key?.GetValue("InstallPath");
            if (key == null || value == null)
            {
                //not sure if this is ever possible, but whatever
                if (!ignoreArchitecture)
                {
                    return FindSteamPath(true, !is64Bit);
                }
                return null;
            }
            key.Close();
            if (!Directory.Exists(value))
            {
                return null;
            }
            return value;
        }
                
        public static int CountOccurences(string needle, string haystack)
        {
            return (haystack.Length - haystack.Replace(needle, "").Length) / needle.Length;
        }

        public static string FixPath(string dir)
        {
            if (!dir.Contains("SteamApps"))
                dir += Path.Combine("\\SteamApps", "common");
            return dir;
        }

    }
}
