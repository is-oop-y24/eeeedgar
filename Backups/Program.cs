using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Backups.ClientServer;
using Backups.Job;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            SendData();
        }

        private static void MakeSingleStorageLocalBackup()
        {
            var backupJob = new BackupJob("D:/OOP/lab-3/BackupJob/backups", "D:/OOP/lab-3/Repository", false);
            backupJob.CurrentVersion.JobObjects.Add(new JobObject(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt"));
            backupJob.Repository.UploadVersion(backupJob.Backups.CreateRestorePoint(backupJob.CurrentVersion));
        }

        private static void SendData()
        {
            Server server = CreateServer();
            server.StartListening();
            var client = new Client(server);

            // client.SendFile(@"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt", DateTime.Now.Second.ToString());
            client.SendFile(@"D:\OOP\lab-3\BackupJob\CurrentVersion\utorrent.exe", "hello");
            server.StopListening();
            List<ServerFile> files = server.SplitReceivedDataToFiles();
            foreach (ServerFile file in files)
            {
                Console.WriteLine("server location: " + server.Location + "\n");

                Console.WriteLine("file relative path: " + file.RelativeName + "\n");
                Console.WriteLine("file full path: " + Path.Combine(server.Location, file.RelativeName) + "\n");

                File.WriteAllBytes(Path.Combine(server.Location, file.RelativeName), file.Data);
            }
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
