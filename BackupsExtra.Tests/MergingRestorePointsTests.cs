using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using BackupsExtra.MergingRestorePoints;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class MergingRestorePointsTests
    {
        private List<JobObject> _jobObjects;

        [SetUp]
        public void Setup()
        {
            var jobObject1 = new JobObject(@"path1");
            var jobObject2 = new JobObject(@"path2");
            var jobObject3 = new JobObject(@"path3");
            _jobObjects = new List<JobObject>
            {
                jobObject1,
                jobObject2,
                jobObject3
            };
        }
        
        [Test]
        public void CreateSingleStorageRestorePointsAndMergeThem_CheckResult()
        {
            var storage1 = new Storage();
            var storage2 = new Storage();
            
            storage1.JobObjects.Add(_jobObjects[0]);
            storage1.JobObjects.Add(_jobObjects[1]);
            
            storage2.JobObjects.Add(_jobObjects[1]);
            storage2.JobObjects.Add(_jobObjects[2]);
            
            var storages1 = new List<Storage> { storage1 };
            var storages2 = new List<Storage> { storage2 };
            
            var dateTime1 = DateTime.Parse("7/30/2002");
            var dateTime2 = DateTime.Parse("8/22/1998");

            var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
            var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

            RestorePoint resultRestorePoint = new SingleStorageRestorePointsMerging(restorePoint1, restorePoint2).Execute();
            
            Assert.AreEqual(1, resultRestorePoint.Storages.Count);
            Assert.AreEqual(2, resultRestorePoint.Storages.First().JobObjects.Count);
            foreach (JobObject jobObject in resultRestorePoint.Storages.First().JobObjects)
            {
                Assert.NotNull(restorePoint1.Storages.First().JobObjects.Find(o => o.Id.Equals(jobObject.Id)));
            }
        }
        
        [Test]
        public void CreateSplitStorageRestorePointsAndMergeThem_CheckResult()
        {
            var storage1 = new Storage();
            var storage2 = new Storage();
            var storage3 = new Storage();
            
            storage1.JobObjects.Add(_jobObjects[0]);
            storage2.JobObjects.Add(_jobObjects[1]);
            storage3.JobObjects.Add(_jobObjects[2]);

            var storages = new List<Storage> { storage1, storage2, storage3 };
            var storages1 = new List<Storage> { storage1, storage2 };
            var storages2 = new List<Storage> { storage2, storage3 };
            
            var dateTime1 = DateTime.Parse("7/30/2002");
            var dateTime2 = DateTime.Parse("8/22/1998");

            var restorePoint1 = new RestorePoint(storages1, dateTime1, "okay", Guid.NewGuid());
            var restorePoint2 = new RestorePoint(storages2, dateTime2, "okay", Guid.NewGuid());

            RestorePoint resultRestorePoint = new SplitStorageRestorePointsMerging(restorePoint1, restorePoint2).Execute();
            
            Assert.AreEqual(_jobObjects.Count, resultRestorePoint.Storages.Count);
            foreach (Storage storage in storages)
            {
                Assert.Contains(storage, resultRestorePoint.Storages);
            }

            foreach (Storage storage in resultRestorePoint.Storages)
            {
                Assert.NotNull(_jobObjects.Find(o => o.Id.Equals(storage.JobObjects.First().Id)));
            }
        }
    }
}