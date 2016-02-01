using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer
{
    public interface IAnalyzer
    {

        string Name { get; }

        bool CheckExists();

        IEnumerable<string> FindPaths();
        
    }
}
