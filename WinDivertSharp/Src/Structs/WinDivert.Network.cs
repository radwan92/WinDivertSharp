using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
     * WinDivert NETWORK and NETWORK_FORWARD layer data.
     */
        //typedef struct
        //{
        //    UINT32 IfIdx;                       /* Packet's interface index. */
        //    UINT32 SubIfIdx;                    /* Packet's sub-interface index. */
        //}
        //WINDIVERT_DATA_NETWORK, * PWINDIVERT_DATA_NETWORK;

        [StructLayout(LayoutKind.Sequential)]
        public struct Network
        {
            /// <summary> Packet's interface index. </summary>
            public uint IfIdx;

            /// <summary> Packet's sub-interface index. </summary>
            public uint SubIfIdx;
        }
    }
}