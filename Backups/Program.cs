using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Backups.ClientServer;
using Backups.Entities;
using Backups.Repo;
using Backups.Useful;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            ServerClientPackageSendAndEncode();
        }

        private static void ServerClientPackageSendAndEncode()
        {
            Server server = CreateServer();
            server.StartListening();
            var client = new Client(server);

            byte[] package = new byte[PackageManager.ByteSize];
            package[0] = (byte)'h';
            package[1] = (byte)'e';
            package[2] = (byte)'l';
            package[3] = (byte)'l';
            package[4] = (byte)'o';
            client.SendPackage(package);

            server.StopListening();

            byte[] receivedPackage = server.ReceivedData[0];
            string encodedPackage = Encoding.Default.GetString(receivedPackage);
            Console.WriteLine(encodedPackage);
        }

        private static void ClientServerSendData()
        {
            Server server = CreateServer();
            var client = new Client(server);
            server.StartListening();

            string path = "D:/OOP/lab-3/files/1.txt";
            byte[] data = File.ReadAllBytes(path);
            client.SendByteData(data);
            server.StopListening();
            foreach (byte[] package in server.ReceivedData)
            {
                Console.Write(Encoding.Default.GetString(package));
            }
        }

        private static Server CreateServer()
        {
            const string serverPath = "D:/OOP/lab-3/server";
            const string localIpAddress = "127.0.0.1";
            var ipAddress = IPAddress.Parse(localIpAddress);
            const int port = 1234;
            return new Server(ipAddress, port, serverPath);
        }
    }
}
