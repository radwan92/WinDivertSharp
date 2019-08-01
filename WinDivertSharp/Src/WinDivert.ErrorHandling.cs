using System;
using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        static readonly string[] ErrorDescriptions = BuildErrorDescriptionsTable();

        public static ErrorInfo GetLastError()
            => new ErrorInfo(Marshal.GetLastWin32Error());

        public enum Error
        {
            UNKNOWN = 0,
            FILE_NOT_FOUND = 2, // The driver files WinDivert32.sys or WinDivert64.sys were not found.
            ACCESS_DENIED = 5, // The calling application does not have Administrator privileges.
            INVALID_PARAMETER = 87, // This indicates an invalid packet filter string, layer, priority, or flags.
            INVALID_IMAGE_HASH = 577, // The WinDivert32.sys or WinDivert64.sys driver does not have a valid digital signature (see the driver signing requirements above).
            DRIVER_FAILED_PRIOR_UNLOAD = 654, //	An incompatible version of the WinDivert driver is currently loaded.
            SERVICE_DOES_NOT_EXIST = 1060, //	The handle was opened with the WINDIVERT_FLAG_NO_INSTALL flag and the WinDivert driver is not already installed.
            DRIVER_BLOCKED = 1275, //	This error occurs for various reasons, including: the WinDivert driver is blocked by security software; or you are using a virtualization environment that does not support drivers.
            EPT_S_NOT_REGISTERED = 1753 //	This error occurs when the Base Filtering Engine service has been disabled.
        }

        static string[] BuildErrorDescriptionsTable()
        {
            string[] table = new string[2048];

            table[0] = "Unknown";
            table[2] = "The driver files WinDivert32.sys or WinDivert64.sys were not found.";
            table[5] = "The calling application does not have Administrator privileges.";
            table[87] = "This indicates an invalid packet filter string, layer, priority, or flags.";
            table[577] = "The WinDivert32.sys or WinDivert64.sys driver does not have a valid digital signature (see the driver signing requirements above).";
            table[654] = "An incompatible version of the WinDivert driver is currently loaded.";
            table[1060] = "The handle was opened with the WINDIVERT_FLAG_NO_INSTALL flag and the WinDivert driver is not already installed.";
            table[1275] = "This error occurs for various reasons, including: the WinDivert driver is blocked by security software; or you are using a virtualization environment that does not support drivers.";
            table[1753] = "This error occurs when the Base Filtering Engine service has been disabled.";

            return table;
        }

        public struct ErrorInfo
        {
            public Error Error;
            public int Value;
            public string Description;

            public ErrorInfo(int value)
            {
                Value = value;
                Error = Enum.IsDefined(typeof(Error), value)
                    ? (Error)value
                    : Error.UNKNOWN;
                Description = ErrorDescriptions[(int)Error];
            }

            public override string ToString()
            {
                return $"{nameof(Error)}: {Error}, {nameof(Value)}: {Value}, {nameof(Description)}: {Description}";
            }
        }
    }
}