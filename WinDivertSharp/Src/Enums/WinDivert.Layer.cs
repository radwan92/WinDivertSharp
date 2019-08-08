namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert layers.
         */
        //typedef enum
        //{
        //    WINDIVERT_LAYER_NETWORK = 0,        /* Network layer. */
        //    WINDIVERT_LAYER_NETWORK_FORWARD = 1,/* Network layer (forwarded packets) */
        //    WINDIVERT_LAYER_FLOW = 2,           /* Flow layer. */
        //    WINDIVERT_LAYER_SOCKET = 3,         /* Socket layer. */
        //    WINDIVERT_LAYER_REFLECT = 4,        /* Reflect layer. */
        //}
        //WINDIVERT_LAYER, *PWINDIVERT_LAYER;

        public enum Layer
        {
            /// <summary> Network layer. </summary>
            NETWORK,

            /// <summary> Network layer (forwarded packets) </summary>
            NETWORK_FORWARD,

            /// <summary> Flow layer. </summary>
            FLOW,

            /// <summary> Socket layer. </summary>
            SOCKET,

            /// <summary> Reflect layer. </summary>
            REFLECT
        }
    }
}