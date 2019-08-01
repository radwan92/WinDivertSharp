namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
         * WinDivert parameters.
         */
        //typedef enum
        //{
        //    WINDIVERT_PARAM_QUEUE_LENGTH = 0,   /* Packet queue length. */
        //    WINDIVERT_PARAM_QUEUE_TIME = 1,     /* Packet queue time. */
        //    WINDIVERT_PARAM_QUEUE_SIZE = 2,     /* Packet queue size. */
        //    WINDIVERT_PARAM_VERSION_MAJOR = 3,  /* Driver version (major). */
        //    WINDIVERT_PARAM_VERSION_MINOR = 4,  /* Driver version (minor). */
        //}
        //WINDIVERT_PARAM, *PWINDIVERT_PARAM;
        //#define WINDIVERT_PARAM_MAX             WINDIVERT_PARAM_VERSION_MINOR

        public enum Param
        {
            /// <summary> Packet queue length. </summary>
            QUEUE_LENGTH,

            /// <summary> Packet queue time. </summary>
            QUEUE_TIME,

            /// <summary> Packet queue size. </summary>
            QUEUE_SIZE,

            /// <summary> Driver version (major). </summary>
            VERSION_MAJOR,

            /// <summary> Driver version (minor). </summary>
            VERSION_MINOR,

            MAX = VERSION_MINOR
        }
    }
}