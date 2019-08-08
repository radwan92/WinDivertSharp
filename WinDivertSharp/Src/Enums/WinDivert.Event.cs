namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert events.
         */
        //typedef enum
        //{
        //    WINDIVERT_EVENT_NETWORK_PACKET = 0, /* Network packet. */
        //    WINDIVERT_EVENT_FLOW_ESTABLISHED = 1,
        //    /* Flow established. */
        //    WINDIVERT_EVENT_FLOW_DELETED = 2,   /* Flow deleted. */
        //    WINDIVERT_EVENT_SOCKET_BIND = 3,    /* Socket bind. */
        //    WINDIVERT_EVENT_SOCKET_CONNECT = 4, /* Socket connect. */
        //    WINDIVERT_EVENT_SOCKET_LISTEN = 5,  /* Socket listen. */
        //    WINDIVERT_EVENT_SOCKET_ACCEPT = 6,  /* Socket accept. */
        //    WINDIVERT_EVENT_SOCKET_CLOSE = 7,   /* Socket close. */
        //    WINDIVERT_EVENT_REFLECT_OPEN = 8,   /* WinDivert handle opened. */
        //    WINDIVERT_EVENT_REFLECT_CLOSE = 9,  /* WinDivert handle closed. */
        //}
        //WINDIVERT_EVENT, *PWINDIVERT_EVENT;

        public enum Event
        {
            /// <summary> Network packet. </summary>
            NETWORK_PACKET,

            /// <summary> Flow established. </summary>
            FLOW_ESTABLISHED,

            /// <summary> Flow deleted. </summary>
            FLOW_DELETED,

            /// <summary> Socket bind. </summary>
            SOCKET_BIND,

            /// <summary> Socket connect. </summary>
            SOCKET_CONNECT,

            /// <summary> Socket listen. </summary>
            SOCKET_LISTEN,

            /// <summary> Socket accept. </summary>
            SOCKET_ACCEPT,

            /// <summary> Socket close. </summary>
            SOCKET_CLOSE,

            /// <summary> WinDivert handle opened. </summary>
            REFLECT_OPEN,

            /// <summary> WinDivert handle closed. </summary>
            REFLECT_CLOSE
        }
    }
}