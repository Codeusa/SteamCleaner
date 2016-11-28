using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            if (File.Exists(productPath))
            {
                var data = File.ReadAllText(productPath);
                foreach (Match match in Regex.Matches(data, @"(\u0021|\u001F)(.:/.*?)(\u0012\u0002)"))
                {
                    if (match.Groups.Count != 4)
                    {
                        continue;
                    }
                    var value = match.Groups[2].Value;
                    if (Directory.Exists(value))
                    {
                        paths.Add(value);
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
                                       "\\Battle.net";
        }

    }
}
