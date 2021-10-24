using Backups.Job;
using NUnit.Framework;

namespace Backups.Tests
{
    public class SplitStorageLocalRepositoryTests
    {
        private BackupJob _job;

        [SetUp]
        public void Setup()
        {
            const string backupsPath = "D:/OOP/lab-3/BackupJob/backups";
            const string localRepositoryPath = "D:/OOP/lab-3/Repository";
            _job = new BackupJob(backupsPath, localRepositoryPath, true);
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
            Assert.AreEqual(_job.Backups.RestorePoints[^1].Storages.Count, 3);
            
            System.Threading.Thread.Sleep(1000);
            _job.RemoveJobObject(file2Path);
            _job.CreateBackup();
            Assert.AreEqual(_job.Backups.RestorePoints[^1].Storages.Count, 2);
            
            Assert.AreEqual(_job.Backups.RestorePoints.Count, 2);
        }
    }
}