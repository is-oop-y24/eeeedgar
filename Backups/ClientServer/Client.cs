using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Backups.ClientServer
{
    public class Client
    {
        private Server _server;
        private IPEndPoint _ipEndPoint;
        public Client(Server server)
        {
            _server = server;
            _ipEndPoint = new IPEndPoint(server.IpAddress, server.Port);
        }

        /// <summary>
        /// Sends local file by path, puts it on server in directory.
        /// </summary>
        /// <param name="path">Local file path.</param>
        /// <param name="directory">Server directory.</param>
        public void SendFile(string path, string directory)
        {
            byte[] data = File.ReadAllBytes(path);
            int packagesNumber = PackagesNumber(data.Length);

            SendValue(packagesNumber);
            SendValue(path);
            SendValue(directory);
            SendByteData(data);
        }

        private static int PackagesNumber(int dataLenght)
        {
            int i = 0;
            int packagesNumber = 0;
            while (i++ < dataLenght)
            {
                if (i % Package.ByteSize == 1)
                    packagesNumber++;
            }

            return packagesNumber;
        }

        private void SendByteData(byte[] data)
        {
            int packagesNumber = PackagesNumber(data.Length);
            byte[] package = new byte[Package.ByteSize];
            int packageNumber = 1;
            while (packageNumber < packagesNumber)
            {
                Array.Copy(data, Package.ByteSize * (packageNumber - 1), package, 0, Package.ByteSize);
                SendPackage(package);
                packageNumber++;
            }

            Array.Copy(data, Package.ByteSize * (packageNumber - 1), package, 0, data.Length - (Package.ByteSize * (packageNumber - 1)));
            SendPackage(package);
        }

        private void SendPackage(byte[] package)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(_ipEndPoint);
            socket.Send(package);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            _server.ReceivePackage();
        }

        private void SendValue<T>(T data)
        {
            string dataString = data.ToString();
            if (dataString is null)
                throw new Exception("Client error: can't send empty package");
            byte[] packageWithWhitespaces = Encoding.Unicode.GetBytes(dataString);
            byte[] package = new byte[Package.ByteSize];
            for (int i = 0; i < packageWithWhitespaces.Length; i += 2)
                package[i / 2] = packageWithWhitespaces[i];
            if (package.Length > Package.ByteSize)
                throw new Exception("Client error: too big data for one package");
            SendPackage(package);
        }
    }
}