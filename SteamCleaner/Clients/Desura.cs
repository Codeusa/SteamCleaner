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
            var paths = new List<string>();
            var regPath = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = @"Software\Wow6432Node\Microsoft\\Windows\CurrentVersion\Uninstall";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            }

            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
            {
                paths.AddRange(from subkeyName in root.GetSubKeyNames() where subkeyName.StartsWith("Desura_", StringComparison.Ordinal) select Registry.LocalMachine.OpenSubKey(regPath + "\\" + subkeyName) into subKey select subKey?.GetValue("InstallLocation").ToString());
            } 
            return paths;
        }
    }
}