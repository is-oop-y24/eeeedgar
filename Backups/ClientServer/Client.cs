using System;
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

        public void SendByteData(byte[] data)
        {
            int packagesNumber = PackagesNumber(data.Length);
            SendValue(packagesNumber);

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

        public void SendPackage(byte[] package)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(_ipEndPoint);
            socket.Send(package);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            Console.WriteLine("Client: package sent");
            _server.ReceivePackage();
        }

        public void SendValue<T>(T data)
        {
            string dataString = data.ToString();
            if (dataString is null)
                throw new Exception("Client error: can't send empty package");
            byte[] package = Encoding.Unicode.GetBytes(dataString);
            if (package.Length > Package.ByteSize)
                throw new Exception("Client error: too big data for one package");
            SendPackage(package);
        }

        private int PackagesNumber(int dataLenght)
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
    }
}