using System.Net.Sockets;
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

        [Test]
        public void Address_Offsets_Match()
        {
            // Arrange & Act
            int[] offsets =
            {
                (int)Marshal.OffsetOf<WinDivert.Address>("m_Timestamp"),
                (int)Marshal.OffsetOf<WinDivert.Address>("Network"),

            };
            int[] expectedOffsets =
            {
                OffsetOf(nameof(WinDivert.Address), "Timestamp"),
                OffsetOf(nameof(WinDivert.Address), "Network"),
            };

            
            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        [Test]
        public unsafe void Flow_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Flow.EndpointId),
                nameof(WinDivert.Flow.ParentEndpointId),
                nameof(WinDivert.Flow.ProcessId),
                nameof(WinDivert.Flow.LocalAddr),
                nameof(WinDivert.Flow.LocalPort),
                nameof(WinDivert.Flow.RemoteAddr),
                nameof(WinDivert.Flow.RemotePort),
                nameof(WinDivert.Flow.Protocol),
            };

            // Act
            GetOffsets<WinDivert.Flow>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        [Test]
        public void Network_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Network.IfIdx),
                nameof(WinDivert.Network.SubIfIdx),
            };

            // Act
            GetOffsets<WinDivert.Network>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        [Test]
        public void Reflect_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Reflect.Timestamp),
                nameof(WinDivert.Reflect.ProcessId),
                nameof(WinDivert.Reflect.Layer),
                nameof(WinDivert.Reflect.Flags),
                nameof(WinDivert.Reflect.Priority),
            };

            // Act
            GetOffsets<WinDivert.Reflect>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        [Test]
        public unsafe void Socket_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Socket.EndpointId),
                nameof(WinDivert.Socket.ParentEndpointId),
                nameof(WinDivert.Socket.ProcessId),
                nameof(WinDivert.Socket.LocalAddr),
                nameof(WinDivert.Socket.LocalPort),
                nameof(WinDivert.Socket.RemoteAddr),
                nameof(WinDivert.Socket.RemotePort),
                nameof(WinDivert.Socket.Protocol),
            };

            // Act
            GetOffsets<WinDivert.Socket>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        static void GetOffsets<T>(string[] fields, out int[] expectedOffsets, out int[] offsets)
        {
            offsets = new int[fields.Length];
            expectedOffsets = new int[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                string fieldName = fields[i];
                offsets[i] = (int)Marshal.OffsetOf<T>(fieldName);
                expectedOffsets[i] = OffsetOf(typeof(T).Name, fieldName);
            }
        }
    }
}