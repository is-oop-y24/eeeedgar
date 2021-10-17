using System.Net;
using System.Net.Sockets;
using Backups.Entities;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var server = new TcpListener(IPAddress.Parse("127.0.0.1"), 1234);
            server.Start();

            var backupJob = new BackupJob("D:/OOP/lab-3/backups", false, server);
            backupJob.AddJobObject("D:/OOP/lab-3/cur-version/1.txt");
            backupJob.AddJobObject("C:/Users/edgar/Downloads/Telegram Desktop/lgd.pdf");
            backupJob.MakeBackup();

            server.Stop();
        }
    }
}
