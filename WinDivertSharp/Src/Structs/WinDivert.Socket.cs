using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
     * WinDivert SOCKET layer data.
     */
        //typedef struct
        //{
        //    UINT64 EndpointId;                  /* Endpoint ID. */
        //    UINT64 ParentEndpointId;            /* Parent Endpoint ID. */
        //    UINT32 ProcessId;                   /* Process ID. */
        //    UINT32 LocalAddr[4];                /* Local address. */
        //    UINT32 RemoteAddr[4];               /* Remote address. */
        //    UINT16 LocalPort;                   /* Local port. */
        //    UINT16 RemotePort;                  /* Remote port. */
        //    UINT8 Protocol;                    /* Protocol. */
        //}
        //WINDIVERT_DATA_SOCKET, * PWINDIVERT_DATA_SOCKET;

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct Socket
        {
            /// <summary> Endpoint ID. </summary>
            [FieldOffset(0)]
            public ulong EndpointId;

            /// <summary> Parent Endpoint ID. </summary>
            [FieldOffset(8)]
            public ulong ParentEndpointId;

            /// <summary> Process ID. </summary>
            [FieldOffset(16)]
            public uint ProcessId;

            /// <summary> Local address. </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            [FieldOffset(24)]
            public uint[] LocalAddr;

            /// <summary> Remote address.  </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            [FieldOffset(40)]
            public uint[] RemoteAddr;

            /// <summary> Local port. </summary>
            [FieldOffset(56)]
            public ushort LocalPort;

            /// <summary> Remote port. </summary>
            [FieldOffset(58)]
            public ushort RemotePort;

            /// <summary> Protocol. </summary>
            [FieldOffset(60)]
            public byte Protocol;
        }
    }
}