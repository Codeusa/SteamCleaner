#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Clients
{
    internal class Desura
    {

        public static bool Exist()
        {
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall" : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root != null;
        }

        public static List<string> GetGames()
        {
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall" : @"Software\Microsoft\Windows\CurrentVersion\Uninstall";          
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
            {
                paths.AddRange(from subkeyName in root.GetSubKeyNames() where subkeyName.StartsWith("Desura_", StringComparison.Ordinal) select Registry.LocalMachine.OpenSubKey(regPath + "\\" + subkeyName) into subKey select subKey?.GetValue("InstallLocation").ToString());
            } 
            return paths;
        }
    }
}