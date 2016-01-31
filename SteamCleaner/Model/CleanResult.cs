using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
