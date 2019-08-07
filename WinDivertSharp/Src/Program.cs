using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WinDivertSharp
{
    // Lags - packet delay, bandwidth intact
    // Bandwidth - bytes per second throttling
    // Duplication - inserting 1 to x duplicates of a packet
    // Corruption - changing data inside packet
    // Drop - just randomly drop packets
    // Reorder - change packet ordering

    public class SocketManipulator : IDisposable
    {
        const int DISPOSE_TIMEOUT_MS = 500;
        const int BUFFER_SIZE = short.MaxValue;

        readonly Socket m_Socket;
        readonly IntPtr m_WinDivertHandle;
        readonly IntPtr m_PacketBuffer;

        readonly Thread m_ReadThread;
        readonly Thread m_WriteThread;

        volatile bool m_Shutdown;

        struct Packet
        {
            public byte[] Payload;
            public WinDivert.Address Address;

            public Packet(byte[] payload, WinDivert.Address address)
            {
                Payload = payload;
                Address = address;
            }
        }

        Queue<Packet> m_Packets = new Queue<Packet>();
        object m_Locker = new object();

        public SocketManipulator(Socket socket)
        {
            if (socket.ProtocolType != ProtocolType.Tcp) throw new ArgumentException("Not a TCP socket");
            if (!socket.IsBound) throw new ArgumentException("Socket not bound");
            if (!socket.Connected) throw new ArgumentException("Socket not connected");

            m_Socket = socket;

            int srcPort = ((IPEndPoint) m_Socket.LocalEndPoint).Port;
            int dstPort = ((IPEndPoint) m_Socket.RemoteEndPoint).Port;
            string socketFilter = $"(inbound or outbound) and tcp.SrcPort == {srcPort} and tcp.DstPort == {dstPort}";

            m_WinDivertHandle = WinDivert.Open(socketFilter, WinDivert.Layer.NETWORK, 0, WinDivert.Flags.READ_WRITE);
            if (m_WinDivertHandle == WinDivert.INVALID_HANDLE_VALUE)
            {
                WinDivert.ErrorInfo err = WinDivert.GetLastError();
                throw new Exception(err.Description);
            }

            m_PacketBuffer = Marshal.AllocHGlobal(BUFFER_SIZE);

            // Kick off the loops
            m_ReadThread = new Thread(ReadPackets)
            {
                Name = $"PRdr-{srcPort}-{dstPort}",
                IsBackground = true,
            };

            m_WriteThread = new Thread(WritePackets)
            {
                Name = $"PWrt-{srcPort}-{dstPort}",
                IsBackground = true,
            };

            m_ReadThread.Start();
            m_WriteThread.Start();
        }

        public void Dispose()
        {
            m_Shutdown = true;

            m_ReadThread.Join(DISPOSE_TIMEOUT_MS / 2);

            // Unlock write thread
            lock (m_Locker)
            {
                Monitor.Pulse(m_Locker);
            }

            m_WriteThread.Join(DISPOSE_TIMEOUT_MS / 2);
        }

        void ReadPackets()
        {
            try
            {
                while (!m_Shutdown)
                {
                    if (!WinDivert.Recv(m_WinDivertHandle, m_PacketBuffer, BUFFER_SIZE, out uint received, out WinDivert.Address address))
                    {
                        WinDivert.ErrorInfo error = WinDivert.GetLastError();
                        throw new Exception($"{nameof(WinDivert.Recv)} failure: {error}");
                    }

                    Console.WriteLine($"Recv: {received}");

                    byte[] payload = new byte[received];
                    Marshal.Copy(m_PacketBuffer, payload, 0, payload.Length);
                    var packet = new Packet(payload, address);

                    lock (m_Locker)
                    {
                        m_Packets.Enqueue(packet);
                        Monitor.Pulse(m_Locker);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{nameof(ReadPackets)} failure: {exception}");
                m_Shutdown = true;
            }
        }

        unsafe void WritePackets()
        {
            try
            {
                while (!m_Shutdown)
                {
                    lock (m_Locker)
                    {
                        while (m_Packets.Count <= 0 && !m_Shutdown)
                        {
                            Monitor.Wait(m_Locker);
                        }

                        if (m_Shutdown)
                        {
                            return;
                        }

                        while (m_Packets.TryDequeue(out Packet packet))
                        {
                            fixed (byte * pPayload = packet.Payload)
                            {
                                if (!WinDivert.Send(m_WinDivertHandle, (IntPtr) pPayload, (uint) packet.Payload.Length, out uint _, packet.Address))
                                {
                                    WinDivert.ErrorInfo error = WinDivert.GetLastError();
                                    throw new Exception($"{nameof(WinDivert.Send)} failure: {error}");
                                }

                                Console.WriteLine($"Send: {packet.Payload.Length}");
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{nameof(WritePackets)} failure: {exception}");
                m_Shutdown = true;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Thread host = new Thread(RunHost);
            Thread client = new Thread(RunClient);
            host.IsBackground = true;
            client.IsBackground = true;

            host.Start();
            client.Start();

            //IntPtr incoming = WinDivert.Open("(inbound or outbound) and tcp.DstPort == 1337", WinDivert.Layer.NETWORK, 0, WinDivert.Flags.READ_WRITE);
            //if (incoming == WinDivert.INVALID_HANDLE_VALUE)
            //{
            //    WinDivert.ErrorInfo err = WinDivert.GetLastError();
            //    Console.WriteLine(err);
            //    Console.ReadKey();
            //    return;
            //}

            //if (WinDivert.GetParam(incoming, WinDivert.Param.QUEUE_LENGTH, out ulong value))
            //{
            //    Console.WriteLine($"QueueLength: {value}");
            //}
            //if (WinDivert.GetParam(incoming, WinDivert.Param.QUEUE_SIZE, out value))
            //{
            //    Console.WriteLine($"QueueSize: {value}");
            //}
            //if (WinDivert.GetParam(incoming, WinDivert.Param.QUEUE_TIME, out value))
            //{
            //    Console.WriteLine($"QueueTime: {value}");
            //}

            //IntPtr packetMem = Marshal.AllocHGlobal(4096);
            //Random rnd = new Random();
            //try
            //{
            //    while (true)
            //    {
            //        if (WinDivert.Recv(incoming, packetMem, 4096, out uint recvLen, out WinDivert.Address address))
            //        {
            //            //Console.WriteLine($"Got packet [{(address.Outbound ? "OUT" : "IN")}]: {PrintMemory(packetMem, recvLen)}");

            //            bool drop = rnd.Next(0, 4) == 1;
            //            if (drop)
            //            {
            //                Console.WriteLine("Dropped");
            //            }
            //            else
            //            {
            //                Console.WriteLine("Sent");
            //                WinDivert.Send(incoming, packetMem, recvLen, out uint sent, address);
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Packet capture failure");
            //        }

            //        //Thread.Sleep(500);
            //    }
            //}
            //finally
            //{
            //    Marshal.FreeHGlobal(packetMem);
            //}

            Console.ReadKey();
        }

        static unsafe string PrintMemory(IntPtr address, uint length)
        {
            var sb = new StringBuilder();
            sb.Append($"[{length}] ");

            if (length >= 40)
            {
                length -= 40;
                address += 40;
            }

            byte* ptr = (byte*)address;

            for (int i = 0; i < length; i++)
            {
                sb.Append(Convert.ToString(*ptr, 16).ToUpperInvariant().PadLeft(2, '0'));
                sb.Append(" ");
                ptr++;
            }

            sb.AppendLine();

            ptr = (byte*)address;
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)*ptr);
                ptr++;
            }

            return sb.ToString();
        }

        static void RunHost()
        {
            Thread.Sleep(1000);

            var listener = new TcpListener(IPAddress.Loopback, 1337);
            listener.Start();
            var client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected!");

            int i = 0;

            while (true)
            {
                Thread.Sleep(3000);

                // Read
                int available = client.Available;
                if (available > 0)
                {
                    Span<byte> span = stackalloc byte[available];
                    int read = client.GetStream().Read(span);
                    string text = Encoding.ASCII.GetString(span);

                    Console.WriteLine("Client says: " + text);
                }

                // Write
                byte[] data = Encoding.ASCII.GetBytes("server: " + i++);
                client.GetStream().Write(data, 0, data.Length);
            }
        }

        static void RunClient()
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, 1337);
            Console.WriteLine("Connected!");

            SocketManipulator sm = new SocketManipulator(client.Client);

            int i = 0;

            while (true)
            {
                Thread.Sleep(3000);

                // Read
                int available = client.Available;
                if (available > 0)
                {
                    Span<byte> span = stackalloc byte[available];
                    int read = client.GetStream().Read(span);
                    string text = Encoding.ASCII.GetString(span);

                    Console.WriteLine("Server says: " + text);
                }

                // Write
                byte[] data = Encoding.ASCII.GetBytes("client: " + i++);
                client.GetStream().Write(data, 0, data.Length);

                GC.KeepAlive(sm);
            }
        }
    }
}
