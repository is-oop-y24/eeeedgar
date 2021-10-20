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
            ClientServerSendData();
        }

        private static void ServerClientPackageSendAndEncode()
        {
            Server server = CreateServer();
            server.StartListening();
            var client = new Client(server);

            const string path = "D:/OOP/lab-3/files/m.txt";
            byte[] data = File.ReadAllBytes(path);
            client.SendPackage(data);

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
            Console.WriteLine("data length: " + data.Length);
            client.SendByteData(data);
            server.StopListening();
            Console.WriteLine("rdata[0] length: " + server.EncodePackage(server.ReceivedData[0]).Length);
            Console.WriteLine("rdata[1] length: " + server.EncodePackage(server.ReceivedData[1]).Length);
            foreach (byte[] package in server.ReceivedData)
            {
                Console.WriteLine(Encoding.Default.GetString(package));
            }
        }

        private static void ClientServerSendValue()
        {
            Server server = CreateServer();
            var client = new Client(server);
            server.StartListening();
            client.SendValue(5);
            server.StopListening();
            Console.Write(server.EncodePackage(server.ReceivedData[0]));
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
