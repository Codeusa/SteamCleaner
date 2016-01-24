#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Utilities
{
    internal class Gog
    {
        public static bool Exisit()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"SOFTWARE\Wow6432Node\GOG.com\Games" : @"SOFTWARE\GOG.com\Games";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root != null;
        }

        public static List<string> GetGames()
        {
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"SOFTWARE\Wow6432Node\GOG.com\Games" : @"SOFTWARE\GOG.com\Games";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
                paths.AddRange(
                    root.GetSubKeyNames()
                        .Select(keyname => root.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue("PATH"))
                        .Select(o => o?.ToString())
                        .Where(Directory.Exists));
            return paths;
        }
    }
}