using Backups.Job;
using Backups.Repo;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupJobTests
    {
        private BackupJob _job;

        [SetUp]
        public void Setup()
        {
            const string localRepositoryPath = @"D:\\OOP\\lab-3\\server";
            var repository = new LocalRepository(localRepositoryPath);
            var zipper = new TestStorageCreator();
            _job = new BackupJob(repository, zipper);
        }

        [Test]
        public void CreateTestBackup_CheckRestorePoints()
        {
            const string filePath = @"D:\OOP\lab-3\BackupJob\CurrentVersion\1.txt";
            var jobObject = new JobObject(filePath);
            _job.AddJobObject(jobObject);
            _job.CreateBackup();
            
            Assert.AreEqual(_job.Repository.GetRestorePoints().Count, 1);
        }
    }
}