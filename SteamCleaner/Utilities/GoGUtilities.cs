#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Utilities
{
    internal class GoGUtilities
    {
        public static bool GoGExisit()
        {
            var regPath = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = "SOFTWARE\\WOW6432Node\\GOG.com\\Games";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = "SOFTWARE\\GOG.com\\Games";
            }
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            return root != null;
        }

        public static List<string> GoGGamePaths()
        {
            var regPath = "";
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = "SOFTWARE\\WOW6432Node\\GOG.com\\Games";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = "SOFTWARE\\GOG.com\\Games";
            }

            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
                paths.AddRange(
                    root.GetSubKeyNames()
                        .Select(keyname => root.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue("PATH"))
                        .Select(o => o.ToString())
                        .Where(Directory.Exists));
            return paths;
        }
    }
}