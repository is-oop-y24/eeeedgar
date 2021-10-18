using System.Net;
using Backups.ClientServer;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            const string backupsPath = "D:/OOP/lab-3/backups";
            const string serverPath = "D:/OOP/lab-3/server";
            const string localIpAddress = "127.0.0.1";
            var ipAddress = IPAddress.Parse(localIpAddress);
            const int port = 1234;
            var server = new Server(ipAddress, port, serverPath);
            server.Run();
            var backupJob = new BackupJob(backupsPath, false, ipAddress, port);
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/1.txt");
            backupJob.MakeExternalBackup();
            server.StopListening();
        }

        private void Other()
        {
            /*
            var server = new TcpListener(ipAddress, port);
            server.Start();

            var backupJob = new BackupJob("D:/OOP/lab-3/backups", false, ipAddress, port);
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/1.txt");
            backupJob.AddJobObject("C:/Users/edgar/Downloads/Telegram Desktop/lgd.pdf");
            backupJob.MakeBackup();

            server.Stop();
            */
        }
    }
}
