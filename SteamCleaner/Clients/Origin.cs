#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Clients
{
    internal class Origin
    {
        public static bool Exist()
        {
            var regPath = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = @"Software\Wow6432Node\Origin";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = @"Software\Origin";
            }

            var key = Registry.LocalMachine.OpenSubKey(regPath);
            return key?.GetValue("ClientPath") != null;
        }

        public static List<string> GetGames()
        {
            if (!Exist()) return null;
            var paths = new List<string>();
            var is64Bit = Environment.Is64BitOperatingSystem;
            var regPath = is64Bit ? @"SOFTWARE\Wow6432Node\Electronic Arts" : @"SOFTWARE\Electronic Arts";
            var root = Registry.LocalMachine.OpenSubKey(regPath);
            if (root != null)
                paths.AddRange(
                    root.GetSubKeyNames()
                        .Select(keyname => root.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue(@"Install Dir"))
                        .Select(o => o?.ToString())
                        .Where(Directory.Exists));
            regPath = is64Bit ? @"SOFTWARE\Wow6432Node\EA Games" : @"SOFTWARE\EA Games";
            var legacyRoot = Registry.LocalMachine.OpenSubKey(regPath);
            if (legacyRoot != null)
                paths.AddRange(
                    legacyRoot.GetSubKeyNames()
                        .Select(keyname => legacyRoot.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue(@"Install Dir"))
                        .Select(o => o?.ToString())
                        .Where(Directory.Exists));
            return paths;
        }
    }
}