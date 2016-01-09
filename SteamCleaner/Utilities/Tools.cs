#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows;
using System.Xml;
using MaterialDesignThemes.Wpf;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

#endregion

namespace SteamCleaner.Utilities
{
    public static class Tools
    {
        // A sort of nullable boolean
        public enum Boolstate
        {
            True,
            False,
            Indeterminate
        }

        private static bool HasInternetConnection
        {
            // There is no way you can reliably check if there is an internet connection, but we can come close
            get
            {
                var result = false;

                try
                {
                    if (NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (var p = new Ping())
                        {
                            result = (p.Send("8.8.8.8", 15000).Status == IPStatus.Success) ||
                                     (p.Send("8.8.4.4", 15000).Status == IPStatus.Success) ||
                                     (p.Send("4.2.2.1", 15000).Status == IPStatus.Success);
                        }
                    }
                }
                catch
                {
                }

                return result;
            }
        }


        public static List<string> StartupParameters
        {
            get
            {
                try
                {
                    var startup_parameters_mixed = new List<string>();
                    startup_parameters_mixed.AddRange(Environment.GetCommandLineArgs());

                    var startup_parameters_lower = new List<string>();
                    foreach (var s in startup_parameters_mixed)
                        startup_parameters_lower.Add(s.Trim().ToLower());

                    startup_parameters_mixed.Clear();

                    return startup_parameters_lower;
                }
                catch
                {
                    try
                    {
                        return new List<string>(Environment.GetCommandLineArgs());
                    }
                    catch
                    {
                    }
                }

                return new List<string>();
            }
        }

        public static void GotoSite(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
            }
        }

        public static async void CheckForUpdates()
        {
            if (HasInternetConnection)
            {
                try
                {
                    var _releasePageURL = "";
                    Version _newVersion = null;
                    const string _versionConfig = "https://raw.github.com/Codeusa/SteamCleaner/master/version.xml";
                    var _reader = new XmlTextReader(_versionConfig);
                    _reader.MoveToContent();
                    var _elementName = "";
                    try
                    {
                        if ((_reader.NodeType == XmlNodeType.Element) && (_reader.Name == "steamcleaner"))
                        {
                            while (_reader.Read())
                            {
                                switch (_reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        _elementName = _reader.Name;
                                        break;
                                    default:
                                        if ((_reader.NodeType == XmlNodeType.Text) && _reader.HasValue)
                                        {
                                            switch (_elementName)
                                            {
                                                case "version":
                                                    _newVersion = new Version(_reader.Value);
                                                    break;
                                                case "url":
                                                    _releasePageURL = _reader.Value;
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        _reader.Close();
                    }

                    var applicationVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    if (applicationVersion.CompareTo(_newVersion) < 0)
                    {
                        var dialog = new ConfirmationDialog
                        {
                            MessageTextBlock =
                            {
                                Text = "A new Steam CLeaner update is available, update now?"
                            }
                        };
                        var result = await DialogHost.Show(dialog);
                        if ("1".Equals(result))
                            GotoSite(_releasePageURL);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///     Gets the smallest Rectangle containing two input Rectangles
        /// </summary>
        public static Rectangle GetContainingRectangle(Rectangle a, Rectangle b)
        {
            var amin = new Point(a.X, a.Y);
            var amax = new Point(a.X + a.Width, a.Y + a.Height);
            var bmin = new Point(b.X, b.Y);
            var bmax = new Point(b.X + b.Width, b.Y + b.Height);
            var nmin = new Point(0, 0);
            var nmax = new Point(0, 0);

            nmin.X = amin.X < bmin.X ? amin.X : bmin.X;
            nmin.Y = amin.Y < bmin.Y ? amin.Y : bmin.Y;
            nmax.X = amax.X > bmax.X ? amax.X : bmax.X;
            nmax.Y = amax.Y > bmax.Y ? amax.Y : bmax.Y;

            return new Rectangle(nmin, new Size(nmax.X - nmin.X, nmax.Y - nmin.Y));
        }

        public static string GetDataPath()
        {
            try
            {
                // No version!
                var companyAttribute = (AssemblyCompanyAttribute)Application.ResourceAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0];

                return Environment.GetEnvironmentVariable("AppData").Trim() + "\\" + companyAttribute.Company + "\\" +
                       Application.ResourceAssembly.FullName;
            }
            catch
            {
            }

            try
            {
                // Version, but chopped out
                return GetUserAppDataPath().Substring(0, GetUserAppDataPath().LastIndexOf("\\"));
            }
            catch
            {
                try
                {
                    // App launch folder
                    return Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf("\\",  StringComparison.InvariantCultureIgnoreCase));
                }
                catch
                {
                    try
                    {
                        // Current working folder
                        return Environment.CurrentDirectory;
                    }
                    catch
                    {
                        try
                        {
                            // Desktop
                            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        }
                        catch
                        {
                            // Also current working folder
                            return ".";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// APF version of user app data path
        /// </summary>
        /// <returns></returns>
        private static string GetUserAppDataPath()
        {
            var assm = Assembly.GetEntryAssembly();
            var at = typeof(AssemblyCompanyAttribute);
            var r = assm.GetCustomAttributes(at, false);
            var ct = ((AssemblyCompanyAttribute) (r[0]));
            var path = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);
            path += @"\" + ct.Company;
            path += @"\" + assm.GetName().Version;

            return path;
        }
    }
}