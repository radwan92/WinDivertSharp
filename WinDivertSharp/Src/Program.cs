using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WinDivertSharp
{
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

            IntPtr incoming = WinDivert.Open("outbound and tcp.DstPort == 1337", WinDivert.Layer.NETWORK, 0, WinDivert.Flags.READ_WRITE);
            if (incoming == WinDivert.INVALID_HANDLE_VALUE)
            {
                WinDivert.ErrorInfo err = WinDivert.GetLastError();
                Console.WriteLine(err);
                Console.ReadKey();
                return;
            }

            IntPtr packetMem = Marshal.AllocHGlobal(4096);
            try
            {
                while (true)
                {
                    if (WinDivert.Recv(incoming, packetMem, 4096, out uint recvLen, out WinDivert.Address address))
                    {
                        Console.WriteLine($"Got packet: {PrintMemory(packetMem, recvLen)}");
                        WinDivert.Send(incoming, packetMem, recvLen, out uint sent, address);
                    }
                    else
                    {
                        Console.WriteLine("Packet capture failure");
                    }

                    //Thread.Sleep(500);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(packetMem);
            }

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
                int available = client.Available;
                if (available > 0)
                {
                    Span<byte> span = stackalloc byte[available];
                    int read = client.GetStream().Read(span);
                    string text = Encoding.ASCII.GetString(span);

                    Console.WriteLine("Client says: " + text);

                    byte[] data = Encoding.ASCII.GetBytes("server: " + i++);
                    client.GetStream().Write(data, 0, data.Length);
                }
            }
        }

        static void RunClient()
        {
            var client = new TcpClient();
            client.Connect(IPAddress.Loopback, 1337);
            Console.WriteLine("Connected!");

            int i = 0;

            while (true)
            {
                Thread.Sleep(3000);

                byte[] data = Encoding.ASCII.GetBytes("client: " + i++);
                client.GetStream().Write(data, 0, data.Length);

                int available = client.Available;
                if (available > 0)
                {
                    Span<byte> span = stackalloc byte[available];
                    int read = client.GetStream().Read(span);
                    string text = Encoding.ASCII.GetString(span);
                    Console.WriteLine("Server says: " + text);

                }
            }
        }
    }
}
