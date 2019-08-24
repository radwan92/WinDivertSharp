using System;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * Flags for WinDivertHelperCalcChecksums()
         */
        //#define WINDIVERT_HELPER_NO_IP_CHECKSUM                     1
        //#define WINDIVERT_HELPER_NO_ICMP_CHECKSUM                   2
        //#define WINDIVERT_HELPER_NO_ICMPV6_CHECKSUM                 4
        //#define WINDIVERT_HELPER_NO_TCP_CHECKSUM                    8
        //#define WINDIVERT_HELPER_NO_UDP_CHECKSUM                    16

        [Flags]
        public enum CalcChecksum : ulong
        {
            DEFAULT            = 0,
            NO_IP_CHECKSUM     = 1,
            NO_ICMP_CHECKSUM   = 2,
            NO_ICMPV6_CHECKSUM = 4,
            NO_TCP_CHECKSUM    = 8,
            NO_UDP_CHECKSUM    = 16
        }
    }
}