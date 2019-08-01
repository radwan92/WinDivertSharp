using System.Runtime.InteropServices;

namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        /*
     * WinDivert REFLECTION layer data.
     */
        //typedef struct
        //{
        //    INT64 Timestamp;                   /* Handle open time. */
        //    UINT32 ProcessId;                   /* Handle process ID. */
        //    WINDIVERT_LAYER Layer;              /* Handle layer. */
        //    UINT64 Flags;                       /* Handle flags. */
        //    INT16 Priority;                    /* Handle priority. */
        //}
        //WINDIVERT_DATA_REFLECT, * PWINDIVERT_DATA_REFLECT;

        [StructLayout(LayoutKind.Sequential)]
        public struct Reflect
        {
            /// <summary> Handle open time. </summary>
            public long Timestamp;

            /// <summary> Handle process ID. </summary>
            public uint ProcessId;

            /// <summary> Handle layer. </summary>
            public Layer Layer;

            /// <summary> Handle flags. </summary>
            public ulong Flags;

            /// <summary> Handle priority. </summary>
            public short Priority;
        }
    }
}