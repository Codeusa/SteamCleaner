using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer.Analyzers
{
    public class CustomAnalyzer : IAnalyzer
    {
        public string Name => "Custom";

        public bool CheckExists()
        {
            return File.Exists("custom.txt");
        }

        public IEnumerable<string> FindPaths()
        {
            return File.ReadAllLines("custom.txt");
        }
    }
}
