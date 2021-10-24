using System.Net;
using Backups.ClientServer;
using Backups.Job;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            SingleServerBackup();
            SingleLocalBackup();
        }

        private static void SingleServerBackup()
        {
            Server server = CreateServer();
            server.StartListening();
            var job = new BackupJob("D:/OOP/lab-3/BackupJob/backups", "D:/OOP/lab-3/Repository", false, server);
            job.AddJobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt");
            job.AddJobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\utorrent.exe");
            job.CreateBackup();
            server.StopListening();
        }

        private static void SingleLocalBackup()
        {
            var job = new BackupJob("D:/OOP/lab-3/BackupJob/backups", "D:/OOP/lab-3/Repository", false);
            job.AddJobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\utorrent.exe");
            job.AddJobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt");
            job.CreateBackup();
        }

        private static Server CreateServer()
        {
            const string serverPath = "D:/OOP/lab-3/server";
            const string localIpAddress = "127.0.0.1";
            var ipAddress = IPAddress.Parse(localIpAddress);
            const int port = 1234;
            return new Server(ipAddress, port, serverPath);
        }
    }
}
