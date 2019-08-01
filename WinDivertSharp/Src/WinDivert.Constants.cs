namespace WinDivertSharp
{
    public static partial class WinDivert
    {
        public const int PRIORITY_HIGHEST = 30000;
        public const int PRIORITY_LOWEST = -PRIORITY_HIGHEST;
        public const int PARAM_QUEUE_LENGTH_DEFAULT = 4096;
        public const int PARAM_QUEUE_LENGTH_MIN = 32;
        public const int PARAM_QUEUE_LENGTH_MAX = 16384;
        public const int PARAM_QUEUE_TIME_DEFAULT = 2000; /* 2s */
        public const int PARAM_QUEUE_TIME_MIN = 100; /* 100ms */
        public const int PARAM_QUEUE_TIME_MAX = 16000; /* 16s */
        public const int PARAM_QUEUE_SIZE_DEFAULT = 4194304; /* 4MB */
        public const int PARAM_QUEUE_SIZE_MIN = 65535; /* 64KB */
        public const int PARAM_QUEUE_SIZE_MAX = 33554432; /* 32MB */
        public const int BATCH_MAX = 0xFF; /* 255 */
        public const int MTU_MAX = 40 + 0xFFFF;
    }
}