using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Model
{
    public class AnalyzeResult
    {
        public AnalyzeResult(List<FileInfo> files, List<string> usedAnalyers)
        {
            this.Files = files;
            this.UsedAnalyzers = usedAnalyers;
            this.TotalSize = files.Sum(f => f.Length);
        }

        public List<FileInfo> Files { get; private set; }
        public long TotalSize { get; private set; }
        public List<string> UsedAnalyzers { get; private set; }
    }
}
