using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamCleaner.Analyzer
{
    public interface IFileFinder
    {

        IEnumerable<string> FindFiles(IEnumerable<string> paths);

    }
}
