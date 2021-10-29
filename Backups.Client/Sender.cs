using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Backups.Client
{
    public class Sender
    {
        public Sender(string hostname, int port)
        {
            Client = new TcpClient(hostname, port);
        }

        public TcpClient Client { get; }

        public static void SendFile(string path, string directoryName, NetworkStream stream)
        {
            string name = Path.GetFileName(path);
            byte[] data = File.ReadAllBytes(path);
            Console.WriteLine(Path.Combine(directoryName, name));
            SendString(Path.Combine(directoryName, name), stream);
            SendByteData(data, stream);
        }
        private static void SendInt(int nextPackageSize, NetworkStream stream)
        {
            stream.Write(BitConverter.GetBytes(nextPackageSize), 0, sizeof(int));
        }

        private static void SendString(string value, NetworkStream stream)
        {
            byte[] package = Encoding.Default.GetBytes(value);
            SendInt(package.Length, stream);
            stream.Write(package, 0, package.Length);
        }

        private static void SendByteData(byte[] data, NetworkStream stream)
        {
            SendInt(data.Length, stream);
            stream.Write(data, 0, data.Length);
        }
    }
}