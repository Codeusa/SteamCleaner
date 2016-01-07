#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

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

        public static void CheckForUpdates()
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
                        if (MessageBox.Show("A new version of Steam Cleaner is available", "A new update is available",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            GotoSite(_releasePageURL);
                        }
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
                return Environment.GetEnvironmentVariable("AppData").Trim() + "\\" + Application.CompanyName + "\\" +
                       Application.ProductName;
            }
            catch
            {
            }

            try
            {
                // Version, but chopped out
                return Application.UserAppDataPath.Substring(0, Application.UserAppDataPath.LastIndexOf("\\"));
            }
            catch
            {
                try
                {
                    // App launch folder
                    return Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
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
    }
}