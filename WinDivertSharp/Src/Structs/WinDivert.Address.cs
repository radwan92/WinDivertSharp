using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert address.
         */
        //typedef struct
        //{
        //    INT64 Timestamp;                   /* Packet's timestamp. */
        //    UINT64 Layer:8;                     /* Packet's layer. */
        //    UINT64 Event:8;                     /* Packet event. */
        //    UINT64 Sniffed:1;                   /* Packet was sniffed? */
        //    UINT64 Outbound:1;                  /* Packet is outbound? */
        //    UINT64 Loopback:1;                  /* Packet is loopback? */
        //    UINT64 Impostor:1;                  /* Packet is impostor? */
        //    UINT64 IPv6:1;                      /* Packet is IPv6? */
        //    UINT64 IPChecksum:1;                /* Packet has valid IPv4 checksum? */
        //    UINT64 TCPChecksum:1;               /* Packet has valid TCP checksum? */
        //    UINT64 UDPChecksum:1;               /* Packet has valid UDP checksum? */
        //    UINT64 Reserved1:40;
        //    union
        //    {
        //        WINDIVERT_DATA_NETWORK Network; /* Network layer data. */
        //        WINDIVERT_DATA_FLOW Flow;       /* Flow layer data. */
        //        WINDIVERT_DATA_SOCKET Socket;   /* Socket layer data. */
        //        WINDIVERT_DATA_REFLECT Reflect; /* Reflect layer data. */
        //        UINT8 Reserved2[64];
        //    };
        //} WINDIVERT_ADDRESS, * PWINDIVERT_ADDRESS;

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct Address
        {
            [FieldOffset(0)]        long m_Timestamp;
            [FieldOffset(8)]        byte m_Layer;
            [FieldOffset(9)]        byte m_Event;
            [FieldOffset(10)]       byte m_Flags;
            [FieldOffset(11)] fixed byte m_Reserved[5];
            [FieldOffset(16)] fixed byte m_Reserved2[64];

            [FieldOffset(16)] public Socket  Socket;
            [FieldOffset(16)] public Network Network;
            [FieldOffset(16)] public Flow    Flow;
            [FieldOffset(16)] public Reflect Reflect;

            /// <summary> Packet's timestamp. </summary>
            public long Timestamp
            {
                get => m_Timestamp;
                set => m_Timestamp = value;
            }

            /// <summary> Packet's layer. </summary>
            public Layer Layer
            {
                get => (Layer) m_Layer;
                set => m_Layer = (byte) value;
            }

            /// <summary> Packet event. </summary>
            public byte Event
            {
                get => m_Event;
                set => m_Event = value;
            }

            /// <summary> Packet was sniffed? </summary>
            public bool Sniffed
            {
                get => (m_Flags & (1 << 0)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 0;
                    else
                        m_Flags ^= 1 << 0;
                }
            }

            /// <summary> Packet is outbound? </summary>
            public bool Outbound
            {
                get => (m_Flags & (1 << 1)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 1;
                    else
                        m_Flags ^= 1 << 1;
                }
            }

            /// <summary> Packet is loopback? </summary>
            public bool Loopback
            {
                get => (m_Flags & (1 << 2)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 2;
                    else
                        m_Flags ^= 1 << 2;
                }
            }

            /// <summary> Packet is impostor? </summary>
            public bool Impostor
            {
                get => (m_Flags & (1 << 3)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 3;
                    else
                        m_Flags ^= 1 << 3;
                }
            }

            /// <summary> Packet is IPv6? </summary>
            public bool IPv6
            {
                get => (m_Flags & (1 << 4)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 4;
                    else
                        m_Flags ^= 1 << 4;
                }
            }

            /// <summary> Packet has valid IPv4 checksum? </summary>
            public bool IPChecksum
            {
                get => (m_Flags & (1 << 5)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 5;
                    else
                        m_Flags ^= 1 << 5;
                }
            }

            /// <summary> Packet has valid TCP checksum? </summary>
            public bool TCPChecksum
            {
                get => (m_Flags & (1 << 6)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 6;
                    else
                        m_Flags ^= 1 << 6;
                }
            }

            /// <summary> Packet has valid UDP checksum? </summary>
            public bool UDPChecksum
            {
                get => (m_Flags & (1 << 7)) != 0;
                set
                {
                    if (value)
                        m_Flags |= 1 << 7;
                    else
                        m_Flags ^= 1 << 7;
                }
            }

            /// <summary>
            ///     Flags encoded: Sniffed:1, Outbound:1, Loopback:1, Impostor:1, IPv6:1, IPChecksum:1, TCPChecksum:1,
            ///     UDPChecksum:1
            /// </summary>
            public byte Flags
            {
                get => m_Flags;
                set => m_Flags = value;
            }
        }
    }
}