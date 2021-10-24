using System.Net;
using Backups.ClientServer;
using Backups.Job;
using NUnit.Framework;

namespace Backups.Tests
{
    public class SingleStorageRemoteRepositoryTests
    {
        private BackupJob _job;
        private Server _server;

        [SetUp]
        public void Setup()
        {
            _server = CreateServer();
            _server.StartListening();
            const string backupsPath = "D:/OOP/lab-3/BackupJob/backups";
            string localRepositoryPath = string.Empty;
            _job = new BackupJob(backupsPath, localRepositoryPath, false, _server);
        }
        
        [Test]
        public void CreateBackup_CheckRestorePoints()
        {
            const string file1Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt";
            const string file2Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\utorrent.exe";
            const string file3Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\folder\3.txt";
            _job.AddJobObject(file1Path);
            _job.AddJobObject(file2Path);
            _job.AddJobObject(file3Path);
            _job.CreateBackup();
            Assert.AreEqual(_job.Backups.RestorePoints[^1].Storages.Count, 1);
            
            System.Threading.Thread.Sleep(1000);
            _job.RemoveJobObject(file2Path);
            _job.CreateBackup();
            Assert.AreEqual(_job.Backups.RestorePoints[^1].Storages.Count, 1);
            
            Assert.AreEqual(_job.Backups.RestorePoints.Count, 2);
            _server.StopListening();
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