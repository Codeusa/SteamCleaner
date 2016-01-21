#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Clients
{
    internal class Desura
    {
        public static bool Exist()
        {
            var regPath = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = @"SOFTWARE\Wow6432Node\Desura";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = @"SOFTWARE\Desura";
            }

            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root != null;
        }

        public static List<string> GetGames()
        {
            var strs = new List<string>();
            foreach (var lower in from registryKey in new List<RegistryKey>
            {
                Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall"),
                Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall")
            } let subKeyNames = registryKey.GetSubKeyNames() from lower in (from str in subKeyNames where str.StartsWith("Desura_", StringComparison.Ordinal) let openSubKey = registryKey.OpenSubKey(str) where openSubKey != null select openSubKey.GetValue("InstallLocation") into value where value != null select value.ToString().ToLower() into lower where !strs.Contains(lower) select lower) select lower)
            {
                strs.Add(lower);
            }
            return strs;
        }
    }
}