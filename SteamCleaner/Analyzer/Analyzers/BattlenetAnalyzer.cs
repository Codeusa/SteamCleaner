#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

#endregion

namespace SteamCleaner.Analyzer.Analyzers
{
    public class BattlenetAnalyzer : IAnalyzer
    {
        public string Name => "Battle.net";

        public bool CheckExists()
        {
            return Directory.Exists(GetBattleNetPath());
        }

        public IEnumerable<string> FindPaths()
        {
            var paths = new List<string>();
            var productPath = GetProductDbPath();
            if (!File.Exists(productPath)) return paths;
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
                    if (Directory.Exists(line) && !line.Contains("Support"))
                    {
                        paths.Add(line);
                    }
                }
            }
            return paths;
        }

        private string GetProductDbPath()
        {
            return Path.Combine(GetBattleNetPath(),
                "Agent\\product.db");
        }

        private string GetBattleNetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) +
                   @"\Battle.net";
        }
    }
}