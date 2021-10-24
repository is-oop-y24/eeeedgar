using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
            ReceivedData.Add(package);
            return package;
        }

        public List<ServerFile> SplitReceivedDataToFiles()
        {
            var files = new List<ServerFile>();
            int packageCount = 0; // sets to the data start point

            while (packageCount < ReceivedData.Count)
            {
                // packages number in this file
                byte[] package = ReceivedData[packageCount];
                int filePackageNumber = int.Parse(System.Text.Encoding.Default.GetString(package));
                packageCount++;

                // file path
                package = ReceivedData[packageCount];
                string filePath = System.Text.Encoding.Default.GetString(package);
                packageCount++;

                // directory
                package = ReceivedData[packageCount];
                string directory = System.Text.Encoding.Default.GetString(package);
                packageCount++;

                // file data
                var fileData = new List<byte>();
                for (int p = packageCount; p < packageCount + filePackageNumber; p++)
                {
                    package = ReceivedData[p];
                    fileData.AddRange(package);
                }

                directory = directory.Replace("\0", string.Empty);
                string fileName = Path.GetFileName(filePath).Replace("\0", string.Empty);
                string fileServerPath = Path.Combine(directory, fileName);
                files.Add(new ServerFile(fileServerPath, fileData.ToArray()));
                if (!Directory.Exists(Path.Combine(Location, directory)))
                    Directory.CreateDirectory(Path.Combine(Location, directory));
                packageCount += filePackageNumber;
            }

            return files;
        }

        public void CreateFiles(List<ServerFile> files)
        {
            foreach (ServerFile file in files)
            {
                File.WriteAllBytes(Path.Combine(Location, file.RelativeName), file.Data);
            }
        }
    }
}