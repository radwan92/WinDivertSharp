using System;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert flags.
         */
        //#define WINDIVERT_FLAG_SNIFF            0x0001
        //#define WINDIVERT_FLAG_DROP             0x0002
        //#define WINDIVERT_FLAG_RECV_ONLY        0x0004
        //#define WINDIVERT_FLAG_READ_ONLY        WINDIVERT_FLAG_RECV_ONLY
        //#define WINDIVERT_FLAG_SEND_ONLY        0x0008
        //#define WINDIVERT_FLAG_WRITE_ONLY       WINDIVERT_FLAG_SEND_ONLY
        //#define WINDIVERT_FLAG_NO_INSTALL       0x0010

        [Flags]
        public enum Flags : ulong
        {
            READ_WRITE = 0x0000,
            SNIFF      = 0x0001,
            DROP       = 0x0002,
            RECV_ONLY  = 0x0004,
            READ_ONLY  = RECV_ONLY,
            SEND_ONLY  = 0x0008,
            WRITE_ONLY = SEND_ONLY,
            NO_INSTALL = 0x0010
        }
    }
}