using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        //typedef struct
        //{
        //    UINT16 SrcPort;
        //    UINT16 DstPort;
        //    UINT16 Length;
        //    UINT16 Checksum;
        //}
        //WINDIVERT_UDPHDR, * PWINDIVERT_UDPHDR;

        [StructLayout(LayoutKind.Sequential)]
        public struct UdpHdr
        {
            public ushort SrcPort;
            public ushort DstPort;
            public ushort Length;
            public ushort Checksum;
        }
    }
}