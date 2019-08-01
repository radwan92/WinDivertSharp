using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;
using WinDivertSharp;

namespace Tests
{
    public class StructsTests
    {
        [DllImport("MarshalDllTest.dll")]
        static extern int SizeOf([MarshalAs(UnmanagedType.LPStr)] string structName);

        [DllImport("MarshalDllTest.dll")]
        static extern int OffsetOf([MarshalAs(UnmanagedType.LPStr)] string structName, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        [Test]
        public void Struct_Size_Matches()
        {
            // Arrange
            string[] structsToTest =
            {
                nameof(WinDivert.Address),
                nameof(WinDivert.Socket),
                nameof(WinDivert.Network),
                nameof(WinDivert.Flow),
                nameof(WinDivert.Reflect),
            };

            int[] sizes = new int[structsToTest.Length];
            int[] expectedSizes = new int[structsToTest.Length];
            Assembly assembly = Assembly.Load("WinDivertSharp");

            // Act
            for (int i = 0; i < sizes.Length; i++)
            {
                string structName = structsToTest[i];
                string typeName = $"{nameof(WinDivertSharp)}.{nameof(WinDivert)}+{structName}";

                sizes[i] = Marshal.SizeOf(assembly.GetType(typeName));
                expectedSizes[i] = SizeOf(structName);
            }

            // Assert
            CollectionAssert.AreEqual(expectedSizes, sizes);
        }

        [Test]
        public void Enum_Size_Matches()
        {
            // Arrange
            string[] enumsToTest =
            {
                nameof(WinDivert.Layer),
                nameof(WinDivert.Event),
                nameof(WinDivert.Param),
                nameof(WinDivert.Shutdown),
            };

            int[] sizes =
            {
                sizeof(WinDivert.Layer),
                sizeof(WinDivert.Event),
                sizeof(WinDivert.Param),
                sizeof(WinDivert.Shutdown),
            };
            int[] expectedSizes = new int[enumsToTest.Length];

            // Act
            for (int i = 0; i < sizes.Length; i++)
            {
                string structName = enumsToTest[i];
                expectedSizes[i] = SizeOf(structName);
            }

            // Assert
            CollectionAssert.AreEqual(expectedSizes, sizes);
        }
    }
}