using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Backups.Server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234);
            string location = @"D:\oop\lab-3\server";
            {
                server.Start();

                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    NetworkStream stream = client.GetStream();

                    int fileNameLength;
                    while ((fileNameLength = ReadInt(stream)) != 0)
                    {
                        ServerFile serverFile = ReceiveFile(fileNameLength, stream);
                        string directoryPath = Path.GetDirectoryName(serverFile.Name);
                        if (!Directory.Exists(directoryPath))
                        {
                            if (directoryPath != null)
                                Directory.CreateDirectory(Path.Combine(location, directoryPath));
                        }

                        File.WriteAllBytes(Path.Combine(location, serverFile.Name), serverFile.Data);
                    }
                }
            }
        }

        public static int ReadInt(NetworkStream stream)
        {
            byte[] bytes = new byte[sizeof(int)];
            stream.Read(bytes, 0, sizeof(int));
            return BitConverter.ToInt32(bytes);
        }

        public static ServerFile ReceiveFile(int nameLength, NetworkStream stream)
        {
            byte[] namePackage = new byte[nameLength];
            stream.Read(namePackage, 0, namePackage.Length);
            int fileDataSize = ReadInt(stream);
            byte[] data = new byte[fileDataSize];
            stream.Read(data, 0, data.Length);
            var serverFile = new ServerFile(System.Text.Encoding.ASCII.GetString(namePackage), data);
            return serverFile;
        }
    }
}