using SteamCleaner.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Cleaner
{
    public class CleanerService
    {

        public Task<CleanResult> CleanAsync(AnalyzeResult result, IProgress<int> callback) => Task.Run(() => Clean(result, callback));

        private CleanResult Clean(AnalyzeResult analyzeResult, IProgress<int> callback)
        {
            List<string> failures = new List<string>();
            for (int i = 0; i < analyzeResult.Files.Count; i++)
            {
                FileInfo file = analyzeResult.Files[i];
                callback.Report(i);
                try
                {
                    if (File.Exists(file.FullName))
                        File.Delete(file.FullName);
                }
                catch
                {
                    failures.Add(file.FullName);
                }
            }
            return new CleanResult(failures);
        }
    }
}
