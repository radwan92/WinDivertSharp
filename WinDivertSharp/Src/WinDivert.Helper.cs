using System;
using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        UINT64 WinDivertHelperHashPacket(
            __in const VOID* pPacket,
            __in UINT packetLen,
            __in UINT64 seed);
        */
        /// <summary> Hash a packet. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertHelperHashPacket", SetLastError = true)]
        public static extern ulong HashPacket(
            IntPtr pPacket,
            uint packetLen,
            ulong seed = 0);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        BOOL WinDivertHelperParsePacket(
            __in        const VOID* pPacket,
            __in        UINT packetLen,
            __out_opt   PWINDIVERT_IPHDR* ppIpHdr,
            __out_opt   PWINDIVERT_IPV6HDR* ppIpv6Hdr,
            __out_opt   UINT8* pProtocol,
            __out_opt   PWINDIVERT_ICMPHDR* ppIcmpHdr,
            __out_opt   PWINDIVERT_ICMPV6HDR* ppIcmpv6Hdr,
            __out_opt   PWINDIVERT_TCPHDR* ppTcpHdr,
            __out_opt   PWINDIVERT_UDPHDR* ppUdpHdr,
            __out_opt   PVOID* ppData,
            __out_opt   UINT* pDataLen,
            __out_opt   PVOID* ppNext,
            __out_opt   UINT* pNextLen);
        */
        /// <summary> Parse IPv4/IPv6/ICMP/ICMPv6/TCP/UDP headers from a raw packet. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertHelperParsePacket", SetLastError = true)]
        public static extern bool ParsePacket(
            IntPtr pPacket,
            uint packetLen,
            out IpHdr ipHdr,
            out Ipv6Hdr ipv6Hdr,
            out byte protocol,
            out IcmpHdr icmpHdr,
            out Icmpv6Hdr icmpv6Hdr,
            out TcpHdr tcpHdr,
            out UdpHdr udpHdr,
            out IntPtr pData,
            out uint dataLen,
            out IntPtr pNext,
            out ushort nextLen);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        BOOL WinDivertHelperCalcChecksums(
            __inout VOID *pPacket,
            __in      UINT packetLen,
            __out_opt WINDIVERT_ADDRESS *pAddr,
            __in      UINT64 flags);
        */
        /// <summary> Calculate IPv4/IPv6/ICMP/ICMPv6/TCP/UDP checksums. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertHelperCalcChecksums", SetLastError = true)]
        public static extern bool CalcChecksums(
            IntPtr pPacket,
            uint packetLen,
            out Address address,
            CalcChecksum calcChecksumFlags);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        BOOL WinDivertHelperDecrementTTL(
            __inout VOID *pPacket,
            __in UINT packetLen);
        */
        /// <summary> Decrement the TTL/HopLimit. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertHelperDecrementTTL", SetLastError = true)]
        public static extern bool DecrementTTL(
            IntPtr pPacket,
            uint packetLen);
    }
}