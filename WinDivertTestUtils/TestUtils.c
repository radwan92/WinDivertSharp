#include <stdio.h>
#include "windivert.h"

#define EXPORT __declspec(dllexport)

#define TryGetOffset(structName, field, fieldName) if (!strcmp(field, #fieldName)) return offsetof(WINDIVERT_##structName, fieldName)
#define TryGetValue(object, field, fieldName) if (!strcmp(field, #fieldName)) return (object)->fieldName

EXPORT int SizeOf(char* name)
{
	if (!strcmp(name, "Layer")) return sizeof(WINDIVERT_LAYER);
	if (!strcmp(name, "Event")) return sizeof(WINDIVERT_EVENT);
	if (!strcmp(name, "Param")) return sizeof(WINDIVERT_PARAM);
	if (!strcmp(name, "Shutdown")) return sizeof(WINDIVERT_SHUTDOWN);

	if (!strcmp(name, "Address")) return sizeof(WINDIVERT_ADDRESS);
	if (!strcmp(name, "Socket")) return sizeof(WINDIVERT_DATA_SOCKET);
	if (!strcmp(name, "Network")) return sizeof(WINDIVERT_DATA_NETWORK);
	if (!strcmp(name, "Flow")) return sizeof(WINDIVERT_DATA_FLOW);
	if (!strcmp(name, "Reflect")) return sizeof(WINDIVERT_DATA_REFLECT);
	if (!strcmp(name, "IpHdr")) return sizeof(WINDIVERT_IPHDR);
	if (!strcmp(name, "Ipv6Hdr")) return sizeof(WINDIVERT_IPV6HDR);
	if (!strcmp(name, "IcmpHdr")) return sizeof(WINDIVERT_ICMPHDR);
	if (!strcmp(name, "Icmpv6Hdr")) return sizeof(WINDIVERT_ICMPV6HDR);
	if (!strcmp(name, "TcpHdr")) return sizeof(WINDIVERT_TCPHDR);
	if (!strcmp(name, "UdpHdr")) return sizeof(WINDIVERT_UDPHDR);

	return -1;
}

EXPORT int OffsetOf(char* object, char* field)
{
	if (!strcmp(object, "Address"))
	{
		TryGetOffset(ADDRESS, field, Timestamp);
		TryGetOffset(ADDRESS, field, Network);
		TryGetOffset(ADDRESS, field, Reserved2);
	}
	if (!strcmp(object, "Socket"))
	{
		TryGetOffset(DATA_SOCKET, field, EndpointId);
		TryGetOffset(DATA_SOCKET, field, ParentEndpointId);
		TryGetOffset(DATA_SOCKET, field, ProcessId);
		TryGetOffset(DATA_SOCKET, field, LocalAddr);
		TryGetOffset(DATA_SOCKET, field, RemoteAddr);
		TryGetOffset(DATA_SOCKET, field, LocalPort);
		TryGetOffset(DATA_SOCKET, field, RemotePort);
		TryGetOffset(DATA_SOCKET, field, Protocol);
	}
	if (!strcmp(object, "Network"))
	{
		TryGetOffset(DATA_NETWORK, field, IfIdx);
		TryGetOffset(DATA_NETWORK, field, SubIfIdx);
	}
	if (!strcmp(object, "Flow"))
	{
		TryGetOffset(DATA_SOCKET, field, EndpointId);
		TryGetOffset(DATA_SOCKET, field, ParentEndpointId);
		TryGetOffset(DATA_SOCKET, field, ProcessId);
		TryGetOffset(DATA_SOCKET, field, LocalAddr);
		TryGetOffset(DATA_SOCKET, field, RemoteAddr);
		TryGetOffset(DATA_SOCKET, field, LocalPort);
		TryGetOffset(DATA_SOCKET, field, RemotePort);
		TryGetOffset(DATA_SOCKET, field, Protocol);
	}
	if (!strcmp(object, "Reflect"))
	{
		TryGetOffset(DATA_REFLECT, field, Timestamp);
		TryGetOffset(DATA_REFLECT, field, ProcessId);
		TryGetOffset(DATA_REFLECT, field, Layer);
		TryGetOffset(DATA_REFLECT, field, Flags);
		TryGetOffset(DATA_REFLECT, field, Priority);
	}
	if (!strcmp(object, "IpHdr"))
	{
		TryGetOffset(IPHDR, field, TOS);
		TryGetOffset(IPHDR, field, Length);
		TryGetOffset(IPHDR, field, Id);
		TryGetOffset(IPHDR, field, FragOff0);
		TryGetOffset(IPHDR, field, TTL);
		TryGetOffset(IPHDR, field, Protocol);
		TryGetOffset(IPHDR, field, Checksum);
		TryGetOffset(IPHDR, field, SrcAddr);
		TryGetOffset(IPHDR, field, DstAddr);
	}
	if (!strcmp(object, "Ipv6Hdr"))
	{
		TryGetOffset(IPV6HDR, field, FlowLabel1);
		TryGetOffset(IPV6HDR, field, NextHdr);
		TryGetOffset(IPV6HDR, field, Length);
		TryGetOffset(IPV6HDR, field, HopLimit);
		TryGetOffset(IPV6HDR, field, SrcAddr);
		TryGetOffset(IPV6HDR, field, DstAddr);
	}
	if (!strcmp(object, "IcmpHdr"))
	{
		TryGetOffset(ICMPHDR, field, Type);
		TryGetOffset(ICMPHDR, field, Code);
		TryGetOffset(ICMPHDR, field, Checksum);
		TryGetOffset(ICMPHDR, field, Body);
	}
	if (!strcmp(object, "Icmpv6Hdr"))
	{
		TryGetOffset(ICMPHDR, field, Type);
		TryGetOffset(ICMPHDR, field, Code);
		TryGetOffset(ICMPHDR, field, Checksum);
		TryGetOffset(ICMPHDR, field, Body);
	}
	if (!strcmp(object, "TcpHdr"))
	{
		TryGetOffset(TCPHDR, field, SrcPort);
		TryGetOffset(TCPHDR, field, DstPort);
		TryGetOffset(TCPHDR, field, SeqNum);
		TryGetOffset(TCPHDR, field, AckNum);
		TryGetOffset(TCPHDR, field, Window);
		TryGetOffset(TCPHDR, field, Checksum);
		TryGetOffset(TCPHDR, field, UrgPtr);
	}
	if (!strcmp(object, "UdpHdr"))
	{
		TryGetOffset(UDPHDR, field, SrcPort);
		TryGetOffset(UDPHDR, field, DstPort);
		TryGetOffset(UDPHDR, field, Length);
		TryGetOffset(UDPHDR, field, Checksum);
	}

	return -1;
}

EXPORT long GetAddressValueFrom(PWINDIVERT_ADDRESS address, char* field)
{
	TryGetValue(address, field, Timestamp);
	TryGetValue(address, field, Layer);
	TryGetValue(address, field, Event);
	TryGetValue(address, field, Sniffed);
	TryGetValue(address, field, Outbound);
	TryGetValue(address, field, Loopback);
	TryGetValue(address, field, Impostor);
	TryGetValue(address, field, IPv6);
	TryGetValue(address, field, IPChecksum);
	TryGetValue(address, field, TCPChecksum);
	TryGetValue(address, field, UDPChecksum);

	return -1;
}

EXPORT long GetIpHdrValueFrom(PWINDIVERT_IPHDR header, char* field)
{
	TryGetValue(header, field, HdrLength);
	TryGetValue(header, field, Version);
	TryGetValue(header, field, TOS);
	TryGetValue(header, field, Length);
	TryGetValue(header, field, Id);
	TryGetValue(header, field, FragOff0);
	TryGetValue(header, field, TTL);
	TryGetValue(header, field, Protocol);
	TryGetValue(header, field, Checksum);
	TryGetValue(header, field, SrcAddr);
	TryGetValue(header, field, DstAddr);

	if (!strcmp(field, "FragOff")) return WINDIVERT_IPHDR_GET_FRAGOFF(header);
	if (!strcmp(field, "MF")) return WINDIVERT_IPHDR_GET_MF(header);
	if (!strcmp(field, "DF")) return WINDIVERT_IPHDR_GET_DF(header);
	if (!strcmp(field, "Reserved")) return WINDIVERT_IPHDR_GET_RESERVED(header);

	return -1;
}

EXPORT long GetIpv6HdrValueFrom(PWINDIVERT_IPV6HDR header, char* field)
{
	TryGetValue(header, field, TrafficClass0);
	TryGetValue(header, field, Version);
	TryGetValue(header, field, FlowLabel0);
	TryGetValue(header, field, TrafficClass1);
	TryGetValue(header, field, FlowLabel1);
	TryGetValue(header, field, Length);
	TryGetValue(header, field, NextHdr);
	TryGetValue(header, field, HopLimit);
	TryGetValue(header, field, SrcAddr)[0];
	TryGetValue(header, field, DstAddr)[0];

	if (!strcmp(field, "TrafficClass")) return WINDIVERT_IPV6HDR_GET_TRAFFICCLASS(header);
	if (!strcmp(field, "FlowLabel")) return WINDIVERT_IPV6HDR_GET_FLOWLABEL(header);

	return -1;
}

EXPORT long GetTcpHdrValueFrom(PWINDIVERT_TCPHDR header, char* field)
{
	TryGetValue(header, field, SrcPort);
	TryGetValue(header, field, DstPort);
	TryGetValue(header, field, SeqNum);
	TryGetValue(header, field, AckNum);
	TryGetValue(header, field, Reserved1);
	TryGetValue(header, field, HdrLength);
	TryGetValue(header, field, Fin);
	TryGetValue(header, field, Syn);
	TryGetValue(header, field, Rst);
	TryGetValue(header, field, Psh);
	TryGetValue(header, field, Ack);
	TryGetValue(header, field, Urg);
	TryGetValue(header, field, Reserved2);
	TryGetValue(header, field, Window);
	TryGetValue(header, field, Checksum);
	TryGetValue(header, field, UrgPtr);

	return -1;
}