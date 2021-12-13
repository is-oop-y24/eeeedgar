using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class ClearingSavePointsTests
    {
        private List<RestorePoint> _restorePoints;
        [SetUp]
        public void Setup()
        {
            _restorePoints = new List<RestorePoint>();
            var storages = new List<Storage>();
            var rs1 = new RestorePoint(storages, DateTime.Parse("1/1/2021"), Guid.NewGuid());
            var rs2 = new RestorePoint(storages, DateTime.Parse("1/30/2021"), Guid.NewGuid());
            var rs3 = new RestorePoint(storages, DateTime.Parse("2/19/2021"), Guid.NewGuid());
            var rs4 = new RestorePoint(storages, DateTime.Parse("12/29/1924"), Guid.NewGuid());
            _restorePoints.Add(rs1);
            _restorePoints.Add(rs2);
            _restorePoints.Add(rs3);
            _restorePoints.Add(rs4);
        }

        [Test]
        public void SelectOutdatedRestorePoints_CheckOut()
        {
            List<RestorePoint> outdatedRestorePoints =
                new OutdatedRestorePointsSelection(DateTime.Parse(("12/31/2020")))
                    .Execute(_restorePoints);
            Assert.AreEqual(1, outdatedRestorePoints.Count);
            Assert.NotNull(outdatedRestorePoints.Find(p => p.Id.Equals(_restorePoints.Last().Id)));
            foreach (RestorePoint outdatedRestorePoint in outdatedRestorePoints)
            {
                Assert.Contains(outdatedRestorePoint, _restorePoints);
            }
        }
        
        [Test]
        public void SelectExceededRestorePoints_CheckOut()
        {
            List<RestorePoint> exceededRestorePoints =
                new OverTheNumberLimitRestorePointsSelection(2)
                    .Execute(_restorePoints);
            Assert.AreEqual(2, exceededRestorePoints.Count);
            Assert.NotNull(exceededRestorePoints.Find(p => p.Id.Equals(_restorePoints.Last().Id)));
            Assert.NotNull(exceededRestorePoints.Find(p => p.Id.Equals(_restorePoints.First().Id)));
            foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
            {
                Assert.Contains(exceededRestorePoint, _restorePoints);
            }
        }

        [Test]
        public void ClearOutdatedRestorePoints_CheckOut()
        {
            List<RestorePoint> outdatedRestorePoints =
                new OutdatedRestorePointsSelection(DateTime.Parse(("12/31/2020")))
                    .Execute(_restorePoints);
            foreach (RestorePoint outdatedRestorePoint in outdatedRestorePoints)
            {
                _restorePoints.Remove(outdatedRestorePoint);
            }
            Assert.AreEqual(3, _restorePoints.Count);
            foreach (RestorePoint outdatedRestorePoint in outdatedRestorePoints)
            {
                CollectionAssert.DoesNotContain(_restorePoints, outdatedRestorePoint);
            }
        }
        [Test]
        public void ClearExceededRestorePoints_CheckOut()
        {
            List<RestorePoint> exceededRestorePoints =
                new OverTheNumberLimitRestorePointsSelection(2)
                    .Execute(_restorePoints);
            foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
            {
                _restorePoints.Remove(exceededRestorePoint);
            }
            Assert.AreEqual(2, _restorePoints.Count);
            foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
            {
                CollectionAssert.DoesNotContain(_restorePoints, exceededRestorePoint);
            }
        }
    }
}