#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace SteamCleaner.Clients
{
    internal class Battlenet
    {
        public static bool Exist()
        {
            return
                Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                                 "\\Battle.net");
        }


        //There has to be a better way, my god what have I done.
        public static List<string> GetGames()
        {
            var paths = new List<string>();
            var productPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                              "\\Battle.net\\Agent\\product.db";
            if (File.Exists(productPath))
            {
                var data = File.ReadAllText(productPath);
                var rgx = new Regex(@"[^\u0020-\u007E]");
                data = rgx.Replace(data, Environment.NewLine);
                using (var reader = new StringReader(data))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        line = line.Trim().Replace("!", "");
                        line = line.Trim().Replace("!", "\"");
                        if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line) || !line.Contains("/"))
                            continue;
                        if (line.StartsWith("\""))
                        {
                            line = line.Remove(0, 1);
                        }
                        if (Directory.Exists(line))
                        {
                            paths.Add(line);
                        }
                    }
                }
            }
            return paths;
        }
    }
}