#include <stdio.h>
#include "windivert.h"

#define EXPORT __declspec(dllexport)
#define TryGetOffset(structName, field, fieldName) if (!strcmp(field, #fieldName)) return offsetof(WINDIVERT_##structName, fieldName)
#define TryGetValue(object, field, fieldName) if (!strcmp(field, #fieldName)) return object->fieldName

EXPORT int SizeOf(char * name) 
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

	return -1;
}

EXPORT int OffsetOf(char * object, char * field)
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

	return -1;
}

EXPORT long GetAddressValueFrom(PWINDIVERT_ADDRESS address, char * field)
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
}