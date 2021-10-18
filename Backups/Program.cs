using System.Net;
using Backups.ClientServer;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            Lab();
        }

        private static void ServerClient()
        {
            const string filePath1 = "D:/OOP/lab-3/cur-version/1.txt";
            const string filePath2 = "D:/OOP/lab-3/cur-version/2.txt";
            const string serverPath = "D:/OOP/lab-3/server";
            const string localIpAddress = "127.0.0.1";
            var ipAddress = IPAddress.Parse(localIpAddress);
            const int port = 1234;
            var server = new Server(ipAddress, port, serverPath);
            server.StartListening();
            var client = new Client(server);
            client.SendFile(filePath1);
            client.SendFile(filePath2);
            server.StopListening();
        }

        private static void Lab()
        {
            const string backupsPath = "D:/OOP/lab-3/backups";
            const string serverPath = "D:/OOP/lab-3/server";
            const string localIpAddress = "127.0.0.1";
            var ipAddress = IPAddress.Parse(localIpAddress);
            const int port = 1234;
            var server = new Server(ipAddress, port, serverPath);
            server.StartListening();
            var client = new Client(server);
            var backupJob = new BackupJob(backupsPath, true, client);
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/1.txt");
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/2.txt");
            backupJob.MakeExternalBackup();
            server.StopListening();
        }
    }
}
