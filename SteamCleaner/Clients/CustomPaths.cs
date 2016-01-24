#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace SteamCleaner.Clients
{
    internal class Custom
    {
        public static bool Exist()
        {
            try
            {
                var lines = File.ReadLines("custom.txt");
                return lines.Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static List<string> GetGames()
        {
            var lines = File.ReadAllLines("custom.txt");
            var games = new List<string>(lines);
            return games;
        }
    }
}