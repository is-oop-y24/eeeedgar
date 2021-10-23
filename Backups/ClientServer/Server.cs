using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Backups.Useful;

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

        /// <summary>
        /// Receives package from the Client and writes it to the byte[] ReceivedData.
        /// </summary>
        /// <returns>Received package.</returns>
        public byte[] ReceivePackage()
        {
            Socket handler = ListenSocket.Accept();
            byte[] package = new byte[Package.ByteSize];
            handler.Receive(package);

            // Console.WriteLine("server: package received");
            ReceivedData.Add(package);
            return package;
        }

        public List<ServerFile> SplitReceivedDataToFiles()
        {
            Console.WriteLine("ReceivedData.Count: " + ReceivedData.Count);
            var files = new List<ServerFile>();
            byte[] package;
            int packageCount = 0; // sets to the data start point

            while (packageCount < ReceivedData.Count)
            {
                // packages number in this file
                package = ReceivedData[packageCount];
                int filePackageNumber = int.Parse(PathCreator.RemoveWhitespace(System.Text.Encoding.Default.GetString(package)));
                Console.WriteLine($"{packageCount}  filePackageNumber: {filePackageNumber}");
                packageCount++;

                // file path
                package = ReceivedData[packageCount];
                string filePath = PathCreator.RemoveWhitespace(System.Text.Encoding.Default.GetString(package));
                Console.WriteLine($"{packageCount}  filePath: {filePath}");
                packageCount++;

                // file data
                var fileData = new List<byte>();

                // for (int p = packageCount; p < packageCount + filePackageNumber; p++)
                // {
                //    package = ReceivedData[p];
                //    Console.WriteLine(System.Text.Encoding.Default.GetString(package));
                //    fileData.AddRange(package);
                // }
                // files.Add(new ServerFile(filePath, fileData));
                // Console.WriteLine($"{filePath}  {fileData}");
                // packageCount += filePackageNumber;
                packageCount += 1000;
            }

            return files;
        }
    }
}