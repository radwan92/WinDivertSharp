using System;
using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * IPv4/IPv6/ICMP/ICMPv6/TCP/UDP header definitions.
         */
        //typedef struct
        //{
        //    UINT8 HdrLength:4;
        //    UINT8 Version:4;
        //    UINT8  TOS;
        //    UINT16 Length;
        //    UINT16 Id;
        //    UINT16 FragOff0;
        //    UINT8  TTL;
        //    UINT8  Protocol;
        //    UINT16 Checksum;
        //    UINT32 SrcAddr;
        //    UINT32 DstAddr;
        //}
        //WINDIVERT_IPHDR, *PWINDIVERT_IPHDR;

        [StructLayout(LayoutKind.Sequential)]
        public struct IpHdr
        {
            byte m_HdrLength4_Version4;

            public byte   TOS;
            public ushort Length;
            public ushort Id;
            public ushort FragOff0;
            public byte   TTL;
            public byte   Protocol;
            public ushort Checksum;
            public uint   SrcAddr;
            public uint   DstAddr;

            public byte HdrLength
            {
                get => (byte) (m_HdrLength4_Version4 & 0x0F);
                set => m_HdrLength4_Version4 = (byte)((m_HdrLength4_Version4 & 0xF0) | ((byte) (value & 0x0F)));
            }

            public byte Version
            {
                get => (byte) (m_HdrLength4_Version4 & 0xF0);
                set => m_HdrLength4_Version4 = (byte)((m_HdrLength4_Version4 & 0x0F) | ((byte) ((value << 4) & 0xF0)));
            }

            public ushort FragOff
            {
                get => (ushort) (FragOff0 & 0xFF1F);
                set => FragOff0 = (ushort)((FragOff0 & 0x00E0) | (value & 0xFF1F));
            }

            public bool MF
            {
                get => (FragOff0 & 0x0020) != 0;
                set => FragOff0 = (ushort) ((FragOff0 & 0xFFDF) | (Convert.ToInt32(value) << 5));
            }

            public bool DF
            {
                get => (FragOff0 & 0x0040) != 0;
                set => FragOff0 = (ushort)((FragOff0 & 0xFFBF) | (Convert.ToInt32(value) << 6));
            }

            public bool Reserved
            {
                get => (FragOff0 & 0x0080) != 0;
                set => FragOff0 = (ushort)((FragOff0 & 0xFF7F) | (Convert.ToInt32(value) << 7));
            }
        }
    }
}