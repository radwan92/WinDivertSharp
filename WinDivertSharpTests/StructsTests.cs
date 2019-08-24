using System;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Framework;
using WinDivertSharp;

namespace Tests
{
    public class StructsTests
    {
        const string TEST_UTILS_DLL = "WinDivertTestUtils.dll";

        [DllImport(TEST_UTILS_DLL)]
        static extern int SizeOf([MarshalAs(UnmanagedType.LPStr)] string structName);

        [DllImport(TEST_UTILS_DLL)]
        static extern int OffsetOf([MarshalAs(UnmanagedType.LPStr)] string structName, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        [DllImport(TEST_UTILS_DLL)]
        static extern long GetAddressValueFrom([In] WinDivert.Address address, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        [DllImport(TEST_UTILS_DLL)]
        static extern long GetIpHdrValueFrom([In] WinDivert.IpHdr header, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        [DllImport(TEST_UTILS_DLL)]
        static extern long GetIpv6HdrValueFrom([In] WinDivert.Ipv6Hdr header, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        [DllImport(TEST_UTILS_DLL)]
        static extern long GetTcpHdrValueFrom([In] WinDivert.TcpHdr header, [MarshalAs(UnmanagedType.LPStr)] string fieldName);

        /* ----------------------------------------------------------------------------------------------------------------- */
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
                nameof(WinDivert.IpHdr),
                nameof(WinDivert.Ipv6Hdr),
                nameof(WinDivert.IcmpHdr),
                nameof(WinDivert.Icmpv6Hdr),
                nameof(WinDivert.TcpHdr),
                nameof(WinDivert.UdpHdr),
            };

            int[]    sizes         = new int[structsToTest.Length];
            int[]    expectedSizes = new int[structsToTest.Length];
            Assembly assembly      = Assembly.Load("WinDivertSharp");

            // Act
            for (int i = 0; i < sizes.Length; i++)
            {
                string structName = structsToTest[i];
                string typeName   = $"{nameof(WinDivertSharp)}.{nameof(WinDivert)}+{structName}";

                sizes[i]         = Marshal.SizeOf(assembly.GetType(typeName));
                expectedSizes[i] = SizeOf(structName);
            }

            // Assert
            CollectionAssert.AreEqual(expectedSizes, sizes);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Enum_Size_Matches()
        {
            // Arrange
            string[] enumsToTest =
            {
                nameof(WinDivert.Layer),
                nameof(WinDivert.Event),
                nameof(WinDivert.Param),
                nameof(WinDivert.Shutdown)
            };

            int[] sizes =
            {
                sizeof(WinDivert.Layer),
                sizeof(WinDivert.Event),
                sizeof(WinDivert.Param),
                sizeof(WinDivert.Shutdown)
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

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Address_Offsets_Match()
        {
            // Arrange & Act
            int[] offsets =
            {
                (int) Marshal.OffsetOf<WinDivert.Address>("m_Timestamp"),
                (int) Marshal.OffsetOf<WinDivert.Address>("Network")
            };
            int[] expectedOffsets =
            {
                OffsetOf(nameof(WinDivert.Address), "Timestamp"),
                OffsetOf(nameof(WinDivert.Address), "Network")
            };


            // Assert
            CollectionAssert.AreEqual(expectedOffsets, offsets);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
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
                nameof(WinDivert.Flow.Protocol)
            };

            // Act
            GetOffsets<WinDivert.Flow>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Network_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Network.IfIdx),
                nameof(WinDivert.Network.SubIfIdx)
            };

            // Act
            GetOffsets<WinDivert.Network>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
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
                nameof(WinDivert.Reflect.Priority)
            };

            // Act
            GetOffsets<WinDivert.Reflect>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
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
                nameof(WinDivert.Socket.Protocol)
            };

            // Act
            GetOffsets<WinDivert.Socket>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void IpHdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.IpHdr.TOS),
                nameof(WinDivert.IpHdr.Length),
                nameof(WinDivert.IpHdr.Id),
                nameof(WinDivert.IpHdr.FragOff0),
                nameof(WinDivert.IpHdr.TTL),
                nameof(WinDivert.IpHdr.Protocol),
                nameof(WinDivert.IpHdr.Checksum),
                nameof(WinDivert.IpHdr.SrcAddr),
                nameof(WinDivert.IpHdr.DstAddr)
            };

            // Act
            GetOffsets<WinDivert.IpHdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public unsafe void Ipv6Hdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Ipv6Hdr.FlowLabel1),
                nameof(WinDivert.Ipv6Hdr.Length),
                nameof(WinDivert.Ipv6Hdr.NextHdr),
                nameof(WinDivert.Ipv6Hdr.HopLimit),
                nameof(WinDivert.Ipv6Hdr.SrcAddr),
                nameof(WinDivert.Ipv6Hdr.DstAddr)
            };

            // Act
            GetOffsets<WinDivert.Ipv6Hdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void IcmpHdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.IcmpHdr.Type),
                nameof(WinDivert.IcmpHdr.Code),
                nameof(WinDivert.IcmpHdr.Checksum),
                nameof(WinDivert.IcmpHdr.Body),
            };

            // Act
            GetOffsets<WinDivert.IcmpHdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Icmpv6Hdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Icmpv6Hdr.Type),
                nameof(WinDivert.Icmpv6Hdr.Code),
                nameof(WinDivert.Icmpv6Hdr.Checksum),
                nameof(WinDivert.Icmpv6Hdr.Body),
            };

            // Act
            GetOffsets<WinDivert.Icmpv6Hdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void TcpHdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.TcpHdr.SrcPort),
                nameof(WinDivert.TcpHdr.DstPort),
                nameof(WinDivert.TcpHdr.SeqNum),
                nameof(WinDivert.TcpHdr.AckNum),
                nameof(WinDivert.TcpHdr.Window),
                nameof(WinDivert.TcpHdr.Checksum),
                nameof(WinDivert.TcpHdr.UrgPtr),
            };

            // Act
            GetOffsets<WinDivert.TcpHdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void UdpHdr_Offsets_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.UdpHdr.SrcPort),
                nameof(WinDivert.UdpHdr.DstPort),
                nameof(WinDivert.UdpHdr.Length),
                nameof(WinDivert.UdpHdr.Checksum),
            };

            // Act
            GetOffsets<WinDivert.UdpHdr>(fields, out int[] expectedOffsets, out int[] offsets);

            // Assert
            for (int i = 0; i < fields.Length; i++)
                Assert.AreEqual(expectedOffsets[i], offsets[i], fields[i]);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Address_Values_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Address.Timestamp),
                nameof(WinDivert.Address.Layer),
                nameof(WinDivert.Address.Event),
                nameof(WinDivert.Address.Sniffed),
                nameof(WinDivert.Address.Outbound),
                nameof(WinDivert.Address.Loopback),
                nameof(WinDivert.Address.Impostor),
                nameof(WinDivert.Address.IPv6),
                nameof(WinDivert.Address.IPChecksum),
                nameof(WinDivert.Address.TCPChecksum),
                nameof(WinDivert.Address.UDPChecksum)
            };

            // Act & Assert
            AssertSingleFieldValueHigh<WinDivert.Address>(fields, GetAddressValueFrom);
            AssertSingleFieldValueLow<WinDivert.Address>(fields, GetAddressValueFrom);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void IpHdr_Values_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.IpHdr.HdrLength),
                nameof(WinDivert.IpHdr.Version),

                nameof(WinDivert.IpHdr.TOS),
                nameof(WinDivert.IpHdr.Length),
                nameof(WinDivert.IpHdr.Id),
                nameof(WinDivert.IpHdr.TTL),
                nameof(WinDivert.IpHdr.Protocol),
                nameof(WinDivert.IpHdr.Checksum),
                nameof(WinDivert.IpHdr.SrcAddr),
                nameof(WinDivert.IpHdr.DstAddr),

                nameof(WinDivert.IpHdr.FragOff),
                nameof(WinDivert.IpHdr.MF),
                nameof(WinDivert.IpHdr.DF),
                nameof(WinDivert.IpHdr.Reserved)
            };

            // Act & Assert
            AssertSingleFieldValueHigh<WinDivert.IpHdr>(fields, GetIpHdrValueFrom);
            AssertSingleFieldValueLow<WinDivert.IpHdr>(fields, GetIpHdrValueFrom);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void Ipv6Hdr_Values_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.Ipv6Hdr.Version),
                nameof(WinDivert.Ipv6Hdr.Length),
                nameof(WinDivert.Ipv6Hdr.NextHdr),
                nameof(WinDivert.Ipv6Hdr.HopLimit),

                // TODO: Workaround for setting fixed buffer values
                //nameof(WinDivert.Ipv6Hdr.SrcAddr),
                //nameof(WinDivert.Ipv6Hdr.DstAddr),

                nameof(WinDivert.Ipv6Hdr.TrafficClass),
                nameof(WinDivert.Ipv6Hdr.FlowLabel),
            };

            // Act & Assert
            AssertSingleFieldValueHigh<WinDivert.Ipv6Hdr>(fields, GetIpv6HdrValueFrom);
            AssertSingleFieldValueLow<WinDivert.Ipv6Hdr>(fields, GetIpv6HdrValueFrom);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        [Test]
        public void TcpHdr_Values_Match()
        {
            // Arrange
            string[] fields =
            {
                nameof(WinDivert.TcpHdr.SrcPort),
                nameof(WinDivert.TcpHdr.DstPort),
                nameof(WinDivert.TcpHdr.SeqNum),
                nameof(WinDivert.TcpHdr.AckNum),
                nameof(WinDivert.TcpHdr.Window),
                nameof(WinDivert.TcpHdr.Checksum),
                nameof(WinDivert.TcpHdr.UrgPtr),
            };

            // Act & Assert
            AssertSingleFieldValueHigh<WinDivert.TcpHdr>(fields, GetTcpHdrValueFrom);
            AssertSingleFieldValueLow<WinDivert.TcpHdr>(fields, GetTcpHdrValueFrom);
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        static void AssertSingleFieldValueHigh<T>(string[] fields, Func<T, string, long> fieldValueGetter) where T : struct
        {
            foreach (string fieldName in fields)
            {
                T      element      = default;
                object boxedElement = element;

                SetMemberValue<T>(boxedElement, fieldName, 1);
                element = (T) boxedElement;
                Assert.AreEqual(1, fieldValueGetter(element, fieldName), fieldName);

                foreach (string otherField in fields)
                {
                    if (fieldName == otherField)
                        continue;

                    Assert.AreEqual(0, fieldValueGetter(element, otherField), $"High: {fieldName}, Low: {otherField}");
                }
            }
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        static void AssertSingleFieldValueLow<T>(string[] fields, Func<T, string, long> fieldValueGetter) where T : struct
        {
            foreach (string fieldName in fields)
            {
                T      element      = default;
                object boxedElement = element;

                foreach (string anyField in fields)
                {
                    SetMemberValue<T>(boxedElement, anyField, 1);
                    Assert.AreEqual(1, fieldValueGetter((T) boxedElement, anyField), anyField);
                }

                SetMemberValue<T>(boxedElement, fieldName, 0);
                element = (T) boxedElement;
                Assert.AreEqual(0, fieldValueGetter(element, fieldName), fieldName);

                foreach (string otherField in fields)
                {
                    if (fieldName == otherField)
                        continue;

                    Assert.AreEqual(1, fieldValueGetter(element, otherField), $"Low: {fieldName}, High: {otherField}");
                }
            }
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        static void GetOffsets<T>(string[] fields, out int[] expectedOffsets, out int[] offsets)
        {
            offsets         = new int[fields.Length];
            expectedOffsets = new int[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                string fieldName = fields[i];
                offsets[i]         = (int) Marshal.OffsetOf<T>(fieldName);
                expectedOffsets[i] = OffsetOf(typeof(T).Name, fieldName);
            }
        }

        /* ----------------------------------------------------------------------------------------------------------------- */
        static void SetMemberValue<T>(object target, string memberName, int value)
        {
            Func<Action, bool> doSafe = (action) =>
            {
                try
                {
                    action();
                    return true;
                }
                catch { }

                return false;
            };

            // HACK: Could use GetMember
            if (doSafe(() => typeof(T).GetProperty(memberName).SetValue(target, (byte) value))) return;
            if (doSafe(() => typeof(T).GetProperty(memberName).SetValue(target, value > 0))) return;
            if (doSafe(() => typeof(T).GetField(memberName).SetValue(target, (byte)value))) return;
            typeof(T).GetField(memberName).SetValue(target, value > 0);
        }
    }
}