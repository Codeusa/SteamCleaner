using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SteamCleaner.Clients
{
    class Gamestop
    {
        public static bool Exist()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit
                ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall"
                : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root != null;
        }

        public static List<string> GetGames()
        {
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit
                ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall"
                : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
            {
                paths.AddRange(from subkeyName in root.GetSubKeyNames()
                               where subkeyName.StartsWith("Gamestop_", StringComparison.Ordinal)
                               select Registry.LocalMachine.OpenSubKey(regPath + "\\" + subkeyName)
                    into subKey
                               select subKey?.GetValue("Install_local").ToString());
            }
            return paths;
        }
    }
}
