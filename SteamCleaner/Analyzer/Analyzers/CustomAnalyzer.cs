#region

using System.Collections.Generic;
using System.IO;

#endregion

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