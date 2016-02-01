#region

using System.Collections.Generic;

#endregion

namespace SteamCleaner.Model
{
    public class CleanResult
    {
        public CleanResult(List<string> failures)
        {
            Failures = failures;
        }

        public List<string> Failures { get; internal set; }
    }
}