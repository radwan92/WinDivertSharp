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

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 64)]
        public unsafe struct Socket
        {
            /// <summary> Endpoint ID. </summary>
            public ulong EndpointId;

            /// <summary> Parent Endpoint ID. </summary>
            public ulong ParentEndpointId;

            /// <summary> Process ID. </summary>
            public uint ProcessId;

            /// <summary> Local address. </summary>
            public fixed uint LocalAddr[4];

            /// <summary> Remote address. </summary>
            public fixed uint RemoteAddr[4];

            /// <summary> Local port. </summary>
            public ushort LocalPort;

            /// <summary> Remote port. </summary>
            public ushort RemotePort;

            /// <summary> Protocol. </summary>
            public byte Protocol;
        }
    }
}