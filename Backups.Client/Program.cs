using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Backups;

namespace Backups.Client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using var client = new TcpClient("127.0.0.1", 1234);
            byte[] filePath = Encoding.Default.GetBytes("D:/oop/lab-3/backupjob/currentversion/1.txt");
            byte[] fileData = File.ReadAllBytes("D:/oop/lab-3/backupjob/currentversion/1.txt");
            using NetworkStream stream = client.GetStream();
            SendFile(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt", stream);
        }

        public static void SendInt(int nextPackageSize, NetworkStream stream)
        {
            stream.Write(BitConverter.GetBytes(nextPackageSize), 0, sizeof(int));
        }
        
        public static void SendString(string value, NetworkStream stream)
        {
            byte[] package = Encoding.Default.GetBytes(value);
            SendInt(package.Length, stream);
            stream.Write(package, 0, package.Length);
        }

        public static void SendByteData(byte[] data, NetworkStream stream)
        {
            SendInt(data.Length, stream);
            stream.Write(data, 0, data.Length);
        }

        public static void SendFile(string path, NetworkStream stream)
        {
            string name = Path.GetFileName(path);
            byte[] data = File.ReadAllBytes(path);
            SendString(name, stream);
            SendByteData(data, stream);
        }
    }
}