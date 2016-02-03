#region

using System;
using System.Globalization;
using System.Windows.Data;
using SteamCleaner.Utilities;

#endregion

namespace SteamCleaner
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var size = (long) value;
            return StringUtilities.GetBytesReadable(size);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}