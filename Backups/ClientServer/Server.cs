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
            IpEndPoint = new IPEndPoint(IpAddress, Port);
            Location = location;
            if (!Directory.Exists(Location))
                Directory.CreateDirectory(Location);
            Console.WriteLine($"server location: {Location}");
        }

        public IPAddress IpAddress { get; }

        public int Port { get; }

        public IPEndPoint IpEndPoint { get; }

        public Socket ListenSocket
        {
            get;
            set;
        }

        public string Location { get; }

        public void Run()
        {
            ListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenSocket.Bind(IpEndPoint);
            ListenSocket.Listen(10);
        }

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
            Socket handler = ListenSocket.Accept();
            const int bufferSize = 512;
            byte[] buffer = new byte[bufferSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            string filePath = Encoding.Unicode.GetString(buffer, 0, bufferRealSize);
            Console.WriteLine($"server: filepath received: {filePath}");
            _expectedFileName = filePath;
            return filePath;
        }

        public int GetFileSize()
        {
            Socket handler = ListenSocket.Accept();
            const int bufferSize = 512;
            byte[] buffer = new byte[bufferSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            int fileSize = int.Parse(Encoding.Unicode.GetString(buffer, 0, bufferRealSize));
            Console.WriteLine($"server: filepath received: {fileSize}");
            _expectedFileSize = fileSize;
            return fileSize;
        }

        public byte[] GetFileData()
        {
            Socket handler = ListenSocket.Accept();
            byte[] buffer = new byte[_expectedFileSize];
            if (handler.Available <= 0)
                throw new Exception("UNDEFINED PATH");
            int bufferRealSize = handler.Receive(buffer);
            Console.WriteLine($"server: file received, file size: {bufferRealSize}");
            _fileData = buffer;
            WriteFile();
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