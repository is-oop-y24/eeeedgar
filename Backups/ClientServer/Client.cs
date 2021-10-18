using System;
using System.Data;
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

        public void SendFile(string filePath)
        {
            SendFilePath(filePath);
            SendFileSize(filePath);
            SendFileData(filePath);
        }

        public void SendFilePath(string filePath)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] package = Encoding.Unicode.GetBytes(filePath);
            socket.Connect(_ipEndPoint);
            socket.Send(package);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            Console.WriteLine("client: file path sent");
            _server.GetFilePath();
        }

        public void SendFileSize(string filePath)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] package = Encoding.Unicode.GetBytes(new FileInfo(filePath).Length.ToString());
            socket.Connect(_ipEndPoint);
            socket.Send(package);
            Console.WriteLine("client: file size sent");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            _server.GetFileSize();
        }

        public void SendFileData(string filePath)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(_ipEndPoint);
            socket.SendFile(filePath);
            Console.WriteLine("client: file sent");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            _server.GetFileData();
        }
    }
}