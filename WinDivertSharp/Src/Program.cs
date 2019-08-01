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

            Console.WriteLine($"Sock: {Marshal.SizeOf<WinDivert.Socket>()}, " +
                              $"Network: {Marshal.SizeOf<WinDivert.Network>()}, " +
                              $"Flow: {Marshal.SizeOf<WinDivert.Flow>()}");

            IntPtr winDivert = WinDivert.Open("outbound and tcp.DstPort == 1337", WinDivert.Layer.NETWORK, 0, WinDivert.Flags.SNIFF);
            if (winDivert == WinDivert.INVALID_HANDLE_VALUE)
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
                    if (WinDivert.Recv(winDivert, packetMem, 4096, out uint recvLen, out WinDivert.Address address))
                    {
                        Console.WriteLine("Got packet!");
                    }
                    else
                    {
                        Console.WriteLine("Packet capture failure");
                    }

                    Thread.Sleep(500);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(packetMem);
            }

            Console.ReadKey();
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
                    string text = Encoding.UTF8.GetString(span);

                    Console.WriteLine("Client says: " + text);

                    byte[] data = Encoding.UTF8.GetBytes("server: " + i++);
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

                byte[] data = Encoding.UTF8.GetBytes("client: " + i++);
                client.GetStream().Write(data, 0, data.Length);

                int available = client.Available;
                if (available > 0)
                {
                    Span<byte> span = stackalloc byte[available];
                    int read = client.GetStream().Read(span);
                    string text = Encoding.UTF8.GetString(span);
                    Console.WriteLine("Server says: " + text);

                }
            }
        }
    }
}
