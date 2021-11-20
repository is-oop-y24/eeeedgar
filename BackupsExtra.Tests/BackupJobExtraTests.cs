using System;
using Backups.Job;
using Backups.Repo;
using Backups.Tests;
using Backups.Zippers;
using BackupsExtra.JobExtra;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupJobExtraTests
    {
        private BackupJobExtra _backupJobExtra;

        [SetUp]
        public void Setup()
        {
            const string localRepositoryPath = @"D:\\OOP\\lab-5\\repo";
            var repository = new LocalRepository(localRepositoryPath);
            var zipper = new TestStorageCreator();
            var job = new BackupJob(repository, zipper);
            
            _backupJobExtra = new BackupJobExtra(job, new StorageConditions());
        }

        [Test]
        public void SetRestorePointsNumberLimitAndExceedIt_CheckRestorePointsNumber()
        {
            _backupJobExtra.StorageConditions.SetNumberLimit(3);
            _backupJobExtra.CreateBackup(DateTime.Parse("7/30/2002"));
            _backupJobExtra.CreateBackup(DateTime.Parse("8/30/2002"));
            _backupJobExtra.CreateBackup(DateTime.Parse("9/30/2002"));
            _backupJobExtra.CreateBackup(DateTime.Parse("10/30/2002"));
            _backupJobExtra.CreateBackup(DateTime.Parse("11/30/2002"));
            Assert.AreEqual(_backupJobExtra.StorageConditions.NumberLimit, _backupJobExtra.Job.Repository.GetRestorePoints().Count);
        }
    }
}