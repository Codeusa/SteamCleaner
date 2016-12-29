#region

using System.Runtime.InteropServices;

#endregion

namespace SteamCleaner.Utilities.Files
{
    /// <remarks>
    ///     Refer to http://msdn.microsoft.com/en-us/library/windows/hardware/ff552012%28v=vs.85%29.aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct SymbolicLinkReparseData
    {
        // Not certain about this!
        private const int MaxUnicodePathLength = 260*2;

        public uint ReparseTag;
        public ushort ReparseDataLength;
        public ushort Reserved;
        public ushort SubstituteNameOffset;
        public ushort SubstituteNameLength;
        public ushort PrintNameOffset;
        public ushort PrintNameLength;
        public uint Flags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxUnicodePathLength)] public byte[] PathBuffer;
    }
}