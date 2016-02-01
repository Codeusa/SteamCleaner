using SteamCleaner.Analyzer;
using SteamCleaner.Analyzer.Analyzers;
using SteamCleaner.Analyzer.FileFinders;
using SteamCleaner.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer
{
    public class AnalyzerService
    {
        private readonly List<IAnalyzer> analyzers;
        private readonly List<IFileFinder> fileFinders;

        public AnalyzerService()
        {
            analyzers = new List<IAnalyzer>()
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
            fileFinders = new List<IFileFinder>()
            {
                new RedisFileFinder(),
                new RenPyRedisFileFinder()
            };
        }

        public Task<AnalyzeResult> AnalyzeAsync(IProgress<Tuple<string, int>> callback) => Task.Run(() => Analyze(callback));

        private AnalyzeResult Analyze(IProgress<Tuple<string, int>> callback)
        {
            callback.Report(Tuple.Create("Starting", 0));
            var pathResult = FindPaths(callback);
            callback.Report(Tuple.Create("Checking nesting", 50));
            CheckNesting(pathResult.Item1);
            callback.Report(Tuple.Create("Finding files", 50));
            List<FileInfo> files = FindFiles(pathResult.Item1, callback);
            callback.Report(Tuple.Create("Calculating", 90));
            AnalyzeResult result = new AnalyzeResult(files, pathResult.Item2.Select(a => "Found paths for " + a.Name).ToList());
            callback.Report(Tuple.Create("Done", 100));
            return result;
        }
        
        private Tuple<List<string>, List<IAnalyzer>> FindPaths(IProgress<Tuple<string, int>> callback)
        {
            List<string> allPaths = new List<string>();
            List<IAnalyzer> usedAnalyzers = new List<IAnalyzer>();
            int progress = 0;
            int updateAmount = 50 / analyzers.Count;
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
                        callback.Report(Tuple.Create(string.Format("No paths for {0}", analyzer.GetType().Name), progress));
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
            List<FileInfo> allFiles = new List<FileInfo>();
            int progress = 50;
            int updateAmount = 40 / fileFinders.Count;
            foreach (var finder in fileFinders)
            {
                progress += updateAmount;
                try
                {
                    IEnumerable<string> files = finder.FindFiles(paths);
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
            var nested = paths.Select(gameDir => Directory.GetDirectories(gameDir))
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
