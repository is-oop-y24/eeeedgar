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

        public void SendFile(string path, string pathOnServer, NetworkStream stream)
        {
            string name = Path.GetFileName(path);
            byte[] data = File.ReadAllBytes(path);
            // SendString(Path.Combine(directoryName, name), stream);
            SendString(pathOnServer, stream);
            SendByteData(data, stream);
        }
        private void SendInt(int nextPackageSize, NetworkStream stream)
        {
            stream.Write(BitConverter.GetBytes(nextPackageSize), 0, sizeof(int));
        }

        private void SendString(string value, NetworkStream stream)
        {
            byte[] package = Encoding.Default.GetBytes(value);
            SendInt(package.Length, stream);
            stream.Write(package, 0, package.Length);
        }

        private void SendByteData(byte[] data, NetworkStream stream)
        {
            SendInt(data.Length, stream);
            stream.Write(data, 0, data.Length);
        }
    }
}