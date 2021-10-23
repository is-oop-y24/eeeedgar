using System.Net;
using Backups.ClientServer;
using Backups.Job;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var backupJob = new BackupJob("D:/OOP/lab-3/BackupJob/backups", "D:/OOP/lab-3/Repository");
            backupJob.CurrentVersion.JobObjects.Add(new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt"));
            backupJob.CurrentVersion.JobObjects.Add(new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\2.txt"));
            backupJob.CurrentVersion.JobObjects.Add(new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\folder\3.txt"));
            backupJob.Backups.CreateRestorePoint(backupJob.CurrentVersion);
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
