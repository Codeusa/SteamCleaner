#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

#endregion

namespace SteamCleaner.Utilities
{
    internal class SteamUtilities
    {
        public static string FixPath(string dir)
        {
            if (!dir.Contains("SteamApps"))
                dir += Path.Combine("\\SteamApps", "common");
            return dir;
        }

        public static string GetSteamPath()
        {
            var regPath = "";
            var steamPath = "";
            var is64Bit = Environment.Is64BitOperatingSystem;
            if (is64Bit)
            {
                Console.WriteLine("64 Bit operating system detected");
                regPath = "SOFTWARE\\Wow6432Node\\Valve\\Steam";
            }
            else
            {
                Console.WriteLine("32 Bit operating system detected");
                regPath = "SOFTWARE\\Valve\\Steam";
            }

            var key = Registry.LocalMachine.OpenSubKey(regPath);
            if (key != null)
            {
                var o = key.GetValue("InstallPath");
                steamPath = o.ToString();
            }
            key?.Close();
            return steamPath;
        }

        public static List<string> SteamPaths()
        {
            return new List<string> {GetSteamPath(), FixPath(GetSecondarySteamInstallPath()).Replace("\\\\", "\\")};
        }


        public static string GetSecondarySteamInstallPath()
        {
            var configPath = GetSteamPath() + "\\config\\config.vdf";
            var data = File.ReadAllLines(configPath);
            foreach (
                var info in
                    data.Where(t => t.Contains("BaseInstallFolder_1"))
                        .Select(t => Regex.Replace(t, @"\s+", " ").Split(' '))
                        .Where(info => info[2] != null))
            {
                return info[2].TrimEnd('"').TrimStart('"');
            }
            return "";
        }
    }
}