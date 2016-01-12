#region

using System;

#endregion

namespace SteamCleaner
{
    public class FileViewModel
    {
        public FileViewModel(string path, string sizeDescription)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (sizeDescription == null) throw new ArgumentNullException(nameof(sizeDescription));

            Path = path;
            SizeDescription = sizeDescription;
        }

        public string Path { get; }

        public string SizeDescription { get; }
    }
}