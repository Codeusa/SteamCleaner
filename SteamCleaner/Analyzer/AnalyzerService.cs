#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SteamCleaner.Analyzer.Analyzers;
using SteamCleaner.Analyzer.FileFinders;
using SteamCleaner.Model;

#endregion

namespace SteamCleaner.Analyzer
{
    public class AnalyzerService
    {
        private readonly List<IAnalyzer> analyzers;
        private readonly List<IFileFinder> fileFinders;

        public AnalyzerService()
        {
            analyzers = new List<IAnalyzer>
            {
                new SteamAnalyzer(),
                new BattlenetAnalyzer(),
                new DesuraAnalyzer(),
                new GamestopAnalyzer(),
                new GogAnalyzer(),
                new NexonAnalyzer(),
                new OriginAnalyzer(),
                new UplayAnalyzer()
            };
            fileFinders = new List<IFileFinder>
            {
                new RedisFileFinder(),
                new RenPyRedisFileFinder()
            };
        }

        public Task<AnalyzeResult> AnalyzeAsync(IProgress<Tuple<string, int>> callback)
            => Task.Run(() => Analyze(callback));

        private AnalyzeResult Analyze(IProgress<Tuple<string, int>> callback)
        {
            callback.Report(Tuple.Create("Starting", 0));
            var pathResult = FindPaths(callback);
            callback.Report(Tuple.Create("Checking nesting", 50));
            CheckNesting(pathResult.Item1);
            callback.Report(Tuple.Create("Finding files", 50));
            var files = FindFiles(pathResult.Item1, callback);
            callback.Report(Tuple.Create("Calculating", 90));
            var result = new AnalyzeResult(files, pathResult.Item2.Select(a => "Found paths for " + a.Name).ToList());
            callback.Report(Tuple.Create("Done", 100));
            return result;
        }

        private Tuple<List<string>, List<IAnalyzer>> FindPaths(IProgress<Tuple<string, int>> callback)
        {
            var allPaths = new List<string>();
            var usedAnalyzers = new List<IAnalyzer>();
            var progress = 0;
            var updateAmount = 50/analyzers.Count;
            foreach (var analyzer in analyzers)
            {
                progress += updateAmount;
                try
                {
                    IEnumerable<string> paths = null;
                    if (analyzer.CheckExists())
                    {
                        paths = analyzer.FindPaths();
                    }
                    if (paths == null || paths.Count() == 0)
                    {
                        callback.Report(Tuple.Create(string.Format("No paths for {0}", analyzer.GetType().Name),
                            progress));
                        continue;
                    }
                    allPaths.AddRange(paths);
                    usedAnalyzers.Add(analyzer);
                    callback.Report(Tuple.Create(string.Format("Found paths for {0}", analyzer.GetType().Name), progress));
                }
                catch (Exception e)
                {
                    callback.Report(Tuple.Create(FormatError(analyzer, e), progress));
                    Console.WriteLine(e.Message);
                }
            }
            return Tuple.Create(allPaths, usedAnalyzers);
        }

        private List<FileInfo> FindFiles(List<string> paths, IProgress<Tuple<string, int>> callback)
        {
            var allFiles = new List<FileInfo>();
            var progress = 50;
            var updateAmount = 40/fileFinders.Count;
            foreach (var finder in fileFinders)
            {
                progress += updateAmount;
                try
                {
                    var files = finder.FindFiles(paths);
                    if (paths == null)
                    {
                        callback.Report(Tuple.Create(string.Format("No files for {0}", finder.GetType().Name), progress));
                        continue;
                    }
                    allFiles.AddRange(files.Where(File.Exists).Select(f => new FileInfo(f)));
                    callback.Report(Tuple.Create(string.Format("Found files for {0}", finder.GetType().Name), progress));
                }
                catch (Exception e)
                {
                    callback.Report(Tuple.Create(FormatError(finder, e), progress));
                    Console.WriteLine(e.Message);
                }
            }
            return allFiles;
        }

        private void CheckNesting(List<string> paths)
        {
            //Check if this still works!
            var nested = paths.Where(Directory.Exists).Select(Directory.GetDirectories)
                .SelectMany(nestedGameFolders => nestedGameFolders)
                .ToList();
            paths.AddRange(nested);
        }

        private string FormatError(object obj, Exception e)
        {
            return string.Format("Error with analyzer: {0}. Message: {1}", obj.GetType().Name, e.Message);
        }
    }
}
