#region

using System.Collections.Generic;

#endregion

namespace SteamCleaner.Analyzer
{
    public interface IAnalyzer
    {
        string Name { get; }

        bool CheckExists();

        IEnumerable<string> FindPaths();
    }
}