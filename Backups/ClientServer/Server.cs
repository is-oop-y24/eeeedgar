using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Backups.ClientServer
{
    public class Server
    {
        public Server(IPAddress ipAddress, int port, string location)
        {
            IpAddress = ipAddress;
            Port = port;
            Location = location;
            ReceivedData = new List<byte[]>();
            if (!Directory.Exists(Location))
                Directory.CreateDirectory(Location);
            Console.WriteLine($"server location: {Location}");
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IpEndPoint = new IPEndPoint(IpAddress, Port);
            ListenSocket.Bind(IpEndPoint);
        }

        public IPAddress IpAddress { get; }

        public int Port { get; }

        public string Location { get; }

        public Socket ListenSocket { get; }
        public IPEndPoint IpEndPoint { get; }
        public List<byte[]> ReceivedData { get; }

        public void StartListening()
        {
            ListenSocket.Listen(10);
        }

        public void StopListening()
        {
            ListenSocket.Close();
        }

        public byte[] ReceivePackage()
        {
            Socket handler = ListenSocket.Accept();
            byte[] package = new byte[Package.ByteSize];
            handler.Receive(package);

            Console.WriteLine("server: package received");
            ReceivedData.Add(package);
            return package;
        }
    }
}