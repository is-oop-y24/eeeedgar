using System.Collections.Generic;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using Backups.Tests;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.JobExtra;
using BackupsExtra.MergingRestorePoints;
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

            _backupJobExtra = new BackupJobExtra(job, new SingleStorageListMerging(), new List<IExceededRestorePointsSelection>());
        }

        [Test]
        public void SetRestorePointsNumberLimitAndExceedIt_CheckRestorePointsNumber()
        {
            _backupJobExtra.Rules.Add(new OverTheNumberLimitRestorePointsSelection(3));
            for (int i = 0; i < 9; i++)
            {
                _backupJobExtra.CreateBackup();
            }

            Assert.AreEqual(3, _backupJobExtra.Job.Repository.GetRestorePoints().Count);
        }
    }
}