#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace SteamCleaner.Model
{
    public class AnalyzeResult
    {
        public AnalyzeResult(List<FileInfo> files, List<string> usedAnalyers)
        {
            Files = files;
            UsedAnalyzers = usedAnalyers;
            TotalSize = files.Sum(f => f.Length);
        }

        public List<FileInfo> Files { get; private set; }
        public long TotalSize { get; private set; }
        public List<string> UsedAnalyzers { get; private set; }
    }
}