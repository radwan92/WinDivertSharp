using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        //typedef struct
        //{
        //    UINT8  Type;
        //    UINT8  Code;
        //    UINT16 Checksum;
        //    UINT32 Body;
        //}
        //WINDIVERT_ICMPHDR, * PWINDIVERT_ICMPHDR;

        [StructLayout(LayoutKind.Sequential)]
        public struct Icmpv6Hdr
        {
            public byte   Type;
            public byte   Code;
            public ushort Checksum;
            public uint   Body;
        }
    }
}