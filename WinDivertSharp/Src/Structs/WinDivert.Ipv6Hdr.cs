using System;
using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        //typedef struct
        //{
        //    UINT8 TrafficClass0:4;
        //    UINT8 Version:4;
        //    UINT8 FlowLabel0:4;
        //    UINT8 TrafficClass1:4;
        //    UINT16 FlowLabel1;
        //    UINT16 Length;
        //    UINT8  NextHdr;
        //    UINT8  HopLimit;
        //    UINT32 SrcAddr[4];
        //    UINT32 DstAddr[4];
        //}
        //WINDIVERT_IPV6HDR, * PWINDIVERT_IPV6HDR;

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Ipv6Hdr
        {
            // 4 bits of TrafficClass0, 4 bits of Version
            byte m_TrafficClass4_Version4;
            // 4 bits of FlowLabel0, 4 bits of TrafficClass1
            byte m_FlowLabel4_TrafficClass4;

            public ushort FlowLabel1;
            public ushort Length;
            public byte NextHdr;
            public byte HopLimit;
            public fixed uint SrcAddr[4];
            public fixed uint DstAddr[4];

            public byte TrafficClass0
            {
                get => (byte)(m_TrafficClass4_Version4 & 0x0F);
                set => m_TrafficClass4_Version4 = (byte)((m_TrafficClass4_Version4 & 0xF0) | (byte)(value & 0x0F));
            }

            public byte Version
            {
                get => (byte)(m_TrafficClass4_Version4 & 0xF0);
                set => m_TrafficClass4_Version4 = (byte)((m_TrafficClass4_Version4 & 0x0F) | (byte)((value << 4) & 0xF0));
            }

            public byte FlowLabel0
            {
                get => (byte)(m_FlowLabel4_TrafficClass4 & 0x0F);
                set => m_FlowLabel4_TrafficClass4 = (byte)((m_FlowLabel4_TrafficClass4 & 0xF0) | (byte)(value & 0x0F));
            }

            public byte TrafficClass1
            {
                get => (byte)(m_FlowLabel4_TrafficClass4 & 0xF0);
                set => m_FlowLabel4_TrafficClass4 = (byte)((m_FlowLabel4_TrafficClass4 & 0x0F) | (byte)((value << 4) & 0xF0));
            }

            public byte TrafficClass
            {
                get => (byte)((TrafficClass0 << 4) | (TrafficClass1));
                set
                {
                    TrafficClass0 = (byte)(value >> 4);
                    TrafficClass1 = value;
                }
            }

            public uint FlowLabel
            {
                get => (uint)((FlowLabel0 << 16) | (FlowLabel1));
                set
                {
                    FlowLabel0 = (byte)(value >> 16);
                    FlowLabel1 = (ushort)value;
                }
            }
        }
    }
}