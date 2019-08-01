using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WinDivertSharp
{
    class Test
    {
        //typedef struct
        //{
        //    INT32 Foo;
        //    const char* Bar;
        //    byte Bytes[4];
        //}
        //TestStruct;

        const string DLL = "MarshalDllTest.dll";

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public unsafe struct TS
        {
            public int Foo;

            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            //[MarshalAs(UnmanagedType.LPStr)]
            //public string Bar;
            public char* Bar;

            public fixed byte Bytes[4];

            public override string ToString()
            {
                //return $":: {nameof(Foo)}: {Foo}, {nameof(Bar)}: {Bar}, {nameof(Bytes)}: {Bytes[0]} {Bytes[1]} {Bytes[2]} {Bytes[3]}";
                return $":: {nameof(Foo)}: {Foo}, {nameof(Bar)}: {(Bar != null ? Marshal.PtrToStringBSTR((IntPtr)Bar) : "")}, {nameof(Bytes)}: {Bytes[0]} {Bytes[1]} {Bytes[2]} {Bytes[3]}";
            }
        }

        public static unsafe void RunTest()
        {
            Console.WriteLine($"C# size: {Marshal.SizeOf<TS>()} C Size: {GetSize()}");

            //char * str = (char *)Marshal.StringToHGlobalAnsi("xx!");

            TS ts = GetData("woof!");
            Console.WriteLine(ts);
            TestInOut(ref ts);
            //TS ts = default;
            //TestOut(ref ts);

            Console.WriteLine(ts);
            Console.ReadKey();

            //Marshal.FreeHGlobal((IntPtr)str);
        }

        static unsafe TS GetData(string str)
        {
            TS ts = new TS();
            ts.Foo = 999;
            //ts.Bar = str;
            //ts.Bytes = new byte[4];
            ts.Bytes[0] = 99;
            ts.Bytes[1] = 98;
            ts.Bytes[2] = 97;
            ts.Bytes[3] = 96;
            return ts;
        }

        [DllImport(DLL)]
        public static extern void TestIn(TS test);

        [DllImport(DLL)]
        public static extern int GetSize();

        [DllImport(DLL)]
        public static extern void TestInRef(TS test);

        [DllImport(DLL)]
        public static extern void TestInRefMod([In, Out] ref TS test);

        [DllImport(DLL)]
        public static extern void TestOut([In, Out] ref TS test);

        [DllImport(DLL)]
        public static extern void TestInOut([In, Out] ref TS test);

        [DllImport(DLL)]
        public static extern void TestOutNoStr([In, Out] ref TS test);

        [DllImport(DLL)]
        public static extern void Strout([Out] [MarshalAs(UnmanagedType.LPStr)] out StringBuilder s);
    }
}
