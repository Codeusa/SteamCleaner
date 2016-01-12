#region

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
            var key = Registry.LocalMachine.OpenSubKey(@"Software\Origin");
            return key?.GetValue("ClientPath") != null;
        }

        public static List<string> GetGames()
        {
            if (!Exist()) return null;
            var paths = new List<string>();
            var root = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Electronic Arts");
            if (root != null)
                paths.AddRange(
                    root.GetSubKeyNames()
                        .Select(keyname => root.OpenSubKey(keyname))
                        .Where(key => key != null)
                        .Select(key => key.GetValue(@"Install Dir"))
                        .Select(o => o?.ToString())
                        .Where(Directory.Exists));

            var legacyRoot = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\EA Games");
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