#region

using System.Collections.Generic;

#endregion

namespace SteamCleaner.Analyzer
{
    public interface IFileFinder
    {
        IEnumerable<string> FindFiles(IEnumerable<string> paths);
    }
}