using System.Drawing;
using Backups.Job;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupJobTests
    {
        private BackupJob _job;

        [SetUp]
        public void Setup()
        {
            const string backupsPath = @"D:\\OOP\\lab-3\\Repository";
            const string localRepositoryPath = @"D:\\OOP\\lab-3\\server";
            _job = new BackupJob(backupsPath, localRepositoryPath, false, null, true);
        }

        [Test]
        public void CreateTestBackup_CheckRestorePoints()
        {
            const string file1Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt";
            const string file2Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\utorrent.exe";
            const string file3Path = @"D:\OOP\lab-3\BackupJob\CurrentVersion\folder\3.txt";
            _job.AddJobObject(file1Path);
            _job.AddJobObject(file2Path);
            _job.AddJobObject(file3Path);
            _job.CreateBackup();
            
            Assert.AreEqual(_job.Backups.RestorePoints.Count, 1);
        }
    }
}