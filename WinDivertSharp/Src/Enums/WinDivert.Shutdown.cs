namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert shutdown parameter.
         */
        //typedef enum
        //{
        //    WINDIVERT_SHUTDOWN_RECV = 0x1,      /* Shutdown recv. */
        //    WINDIVERT_SHUTDOWN_SEND = 0x2,      /* Shutdown send. */
        //    WINDIVERT_SHUTDOWN_BOTH = 0x3,      /* Shutdown recv and send. */
        //}
        //WINDIVERT_SHUTDOWN, *PWINDIVERT_SHUTDOWN;
        //#define WINDIVERT_SHUTDOWN_MAX          WINDIVERT_SHUTDOWN_BOTH

        public enum Shutdown
        {
            /// <summary> Shutdown recv. </summary>
            RECV = 1,

            /// <summary> Shutdown send. </summary>
            SEND = 2,

            /// <summary> Shutdown recv and send. </summary>
            BOTH = 3,

            MAX = BOTH
        }
    }
}