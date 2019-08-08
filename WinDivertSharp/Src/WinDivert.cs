using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
#if UNITY_ENGINE
        const string WINDIVERT_DLL = "WinDivert";
#else
        const string WINDIVERT_DLL = "WinDivert.dll";
#endif
        
        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        HANDLE WinDivertOpen(
            __in const char* filter,
            __in WINDIVERT_LAYER layer,
            __in INT16 priority,
            __in UINT64 flags);
        */
        /// <summary> Open a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertOpen", SetLastError = true)]
        public static extern IntPtr Open(
            [MarshalAs(UnmanagedType.LPStr)] string filter,
            Layer layer,
            short priority,
            Flags flags);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        BOOL WinDivertRecv(
            __in HANDLE handle,
            __out_opt PVOID pPacket,
            __in UINT packetLen,
            __out_opt UINT *pRecvLen,
            __out_opt WINDIVERT_ADDRESS *pAddr);
        */
        /// <summary> Receive (read) a packet from a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertRecv", SetLastError = true)]
        public static extern bool Recv(
            IntPtr handle,
            IntPtr pPacket,
            uint packetLen,
            out uint recvLen,
            out Address address);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertRecvEx(
            __in        HANDLE handle,
            __out_opt   VOID *pPacket,
            __in        UINT packetLen,
            __out_opt   UINT *pRecvLen,
            __in        UINT64 flags,
            __out       WINDIVERT_ADDRESS *pAddr,
            __inout_opt UINT *pAddrLen,
            __inout_opt LPOVERLAPPED lpOverlapped);
        */
        /// <summary> Receive (read) a packet from a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertRecvEx", SetLastError = true)]
        public static extern bool RecvEx(
            IntPtr handle,
            IntPtr pPacket,
            uint packetLen,
            out uint recvLen,
            Flags flags,
            out Address address,
            ref uint addrLen,
            ref NativeOverlapped overlapped);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertSend(
            __in        HANDLE handle,
            __in        const VOID *pPacket,
            __in        UINT packetLen,
            __out_opt   UINT *pSendLen,
            __in        const WINDIVERT_ADDRESS *pAddr);
        */
        /// <summary> Send (write/inject) a packet to a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertSend", SetLastError = true)]
        public static extern bool Send(
            IntPtr handle,
            IntPtr pPacket,
            uint packetLen,
            out uint sendLen,
            Address address);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertSendEx(
            __in        HANDLE handle,
            __in        const VOID *pPacket,
            __in        UINT packetLen,
            __out_opt   UINT *pSendLen,
            __in        UINT64 flags,
            __in        const WINDIVERT_ADDRESS *pAddr,
            __in        UINT addrLen,
            __inout_opt LPOVERLAPPED lpOverlapped);
        */
        /// <summary> Send (write/inject) a packet to a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertSendEx", SetLastError = true)]
        public static extern bool SendEx(
            IntPtr handle,
            IntPtr pPacket,
            uint packetLen,
            out uint sendLen,
            Flags flags,
            ref Address address,
            uint addrLen,
            ref NativeOverlapped overlapped);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertShutdown(
            __in HANDLE handle,
            __in WINDIVERT_SHUTDOWN how);
        */
        /// <summary> Shutdown a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertShutdown", SetLastError = true)]
        public static extern bool ShutDown(
            IntPtr handle,
            Shutdown how);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertClose(
            __in HANDLE handle);
        */
        /// <summary> Close a WinDivert handle. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertClose", SetLastError = true)]
        public static extern bool Close(IntPtr handle);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertSetParam(
            __in HANDLE handle,
            __in WINDIVERT_PARAM param,
            __in UINT64 value);
        */
        /// <summary> Set a WinDivert handle parameter. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertSetParam", SetLastError = true)]
        public static extern bool SetParam(
            IntPtr handle,
            Param param,
            ulong value);

        /* ----------------------------------------------------------------------------------------------------------------- */
        /*
        WINDIVERTEXPORT BOOL WinDivertGetParam(
            __in HANDLE handle,
            __in WINDIVERT_PARAM param,
            __out UINT64 *pValue);
        */
        /// <summary> Get a WinDivert handle parameter. </summary>
        [DllImport(WINDIVERT_DLL, EntryPoint = "WinDivertGetParam", SetLastError = true)]
        public static extern bool GetParam(
            IntPtr handle,
            Param param,
            out ulong value);
    }
}