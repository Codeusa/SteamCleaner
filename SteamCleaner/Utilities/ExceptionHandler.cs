#region

using System;
using System.IO;
using Newtonsoft.Json;

#endregion

namespace SteamCleaner.Utilities
{
    public static class ExceptionHandler
    {
        private static readonly string LogsPath = Path.Combine("", "Logs");

        public static void AddGlobalHandlers()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                try
                {
                    if (!Directory.Exists(LogsPath))
                        Directory.CreateDirectory(LogsPath);

                    var filePath = Path.Combine(LogsPath,
                        $"UnhandledException_{DateTime.Now.ToShortDateString().Replace("/", "-")}.json");

                    File.AppendAllText(filePath,
                        JsonConvert.SerializeObject(args.ExceptionObject, Formatting.Indented) + "\r\n\r\n");

                    Console.WriteLine("An Unhandled Exception was Caught and Logged to:\r\n{0}", filePath);
                }
                catch
                {
                    // ignored
                }
            };
        }
    }
}