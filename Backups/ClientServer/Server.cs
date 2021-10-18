using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Backups.ClientServer
{
    public class Server
    {
        private string _expectedFileName = string.Empty;
        private long _expectedFileSize = 0;
        private byte[] _fileData;
        public Server(IPAddress ipAddress, int port, string location)
        {
            IpAddress = ipAddress;
            Port = port;
            Location = location;
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

        public void StartListening()
        {
            ListenSocket.Listen(10);
        }

        public void StopListening()
        {
            ListenSocket.Close();
        }

        public string GetFilePath()
        {
            // var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // var ipEndPoint = new IPEndPoint(IpAddress, Port);
            // listenSocket.Bind(ipEndPoint);
            // listenSocket.Listen(10);
            Socket handler = ListenSocket.Accept();
            const int bufferSize = 512;
            byte[] buffer = new byte[bufferSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            string filePath = Encoding.Unicode.GetString(buffer, 0, bufferRealSize);
            Console.WriteLine($"server: filepath received: {filePath}");
            _expectedFileName = filePath;

            // handler.Close();
            // listenSocket.Close();
            return filePath;
        }

        public int GetFileSize()
        {
            // var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // var ipEndPoint = new IPEndPoint(IpAddress, Port);
            // listenSocket.Bind(ipEndPoint);
            Socket handler = ListenSocket.Accept();
            const int bufferSize = 512;
            byte[] buffer = new byte[bufferSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            int fileSize = int.Parse(Encoding.Unicode.GetString(buffer, 0, bufferRealSize));
            Console.WriteLine($"server: filepath received: {fileSize}");
            _expectedFileSize = fileSize;

            // handler.Close();
            // listenSocket.Close();
            return fileSize;
        }

        public byte[] GetFileData()
        {
            // var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // var ipEndPoint = new IPEndPoint(IpAddress, Port);
            // listenSocket.Bind(ipEndPoint);
            // listenSocket.Listen(10);
            Socket handler = ListenSocket.Accept();
            byte[] buffer = new byte[_expectedFileSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            Console.WriteLine($"server: file received, file size: {bufferRealSize}");
            _fileData = buffer;
            WriteFile();

            // handler.Close();
            // listenSocket.Close();
            return buffer;
        }

        private void WriteFile()
        {
            string directoryName = $"{Location}/{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            File.WriteAllBytes($"{directoryName}/{_expectedFileName.Split('/')[^1]}", _fileData);
        }
    }
}