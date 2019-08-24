using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        //typedef struct
        //{
        //    UINT16 SrcPort;
        //    UINT16 DstPort;
        //    UINT32 SeqNum;
        //    UINT32 AckNum;
        //    UINT16 Reserved1:4;
        //    UINT16 HdrLength:4;
        //    UINT16 Fin:1;
        //    UINT16 Syn:1;
        //    UINT16 Rst:1;
        //    UINT16 Psh:1;
        //    UINT16 Ack:1;
        //    UINT16 Urg:1;
        //    UINT16 Reserved2:2;
        //    UINT16 Window;
        //    UINT16 Checksum;
        //    UINT16 UrgPtr;
        //}
        //WINDIVERT_TCPHDR, *PWINDIVERT_TCPHDR;

        [StructLayout(LayoutKind.Sequential)]
        public struct TcpHdr
        {
            public ushort SrcPort;
            public ushort DstPort;
            public uint   SeqNum;
            public uint   AckNum;

            // Reserved1:4, HdrLength:4
            byte m_Reserverd4_HdrLength4;

            // Fin:1, Syn:1, Rst:1, Psh:1, Ack:1, Urg:1, Reserved2:2
            byte m_Flags;

            public ushort Window;
            public ushort Checksum;
            public ushort UrgPtr;

            public ushort Reserved1
            {
                get => (byte) (m_Reserverd4_HdrLength4 & 0x0F);
                set => m_Reserverd4_HdrLength4 = (byte) ((m_Reserverd4_HdrLength4 & 0xF0) | (byte) (value & 0x0F));
            }

            public ushort HdrLength
            {
                get => (byte) (m_Reserverd4_HdrLength4 & 0xF0);
                set => m_Reserverd4_HdrLength4 = (byte) ((m_Reserverd4_HdrLength4 & 0x0F) | (byte) ((value << 4) & 0xF0));
            }

            public bool Fin
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

            public bool Syn
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

            public bool Rst
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

            public bool Psh
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

            public bool Ack
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

            public bool Urg
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

            public ushort Reserved2
            {
                get => (ushort) (m_Flags & 0b_1100_0000);
                set => m_Flags = (byte) ((m_Flags & 0b_0011_1111) | (value << 6));
            }

            /// <summary> Flags encoded: Fin:1, Syn:1, Rst:1, Psh:1, Ack:1, Urg:1, Reserved2:2 </summary>
            public byte Flags
            {
                get => m_Flags;
                set => m_Flags = value;
            }
        }
    }
}