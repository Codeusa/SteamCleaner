#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SteamCleaner.Model;

#endregion

namespace SteamCleaner.Cleaner
{
    public class CleanerService
    {
        public Task<CleanResult> CleanAsync(AnalyzeResult result, IProgress<int> callback)
            => Task.Run(() => Clean(result, callback));

        private CleanResult Clean(AnalyzeResult analyzeResult, IProgress<int> callback)
        {
            var failures = new List<string>();
            for (var i = 0; i < analyzeResult.Files.Count; i++)
            {
                var file = analyzeResult.Files[i];
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