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
            var rs1 = new RestorePoint(storages, DateTime.Parse("1/1/2021"), "somebody", Guid.NewGuid());
            var rs2 = new RestorePoint(storages, DateTime.Parse("1/30/2021"), "once", Guid.NewGuid());
            var rs3 = new RestorePoint(storages, DateTime.Parse("2/19/2021"), "told", Guid.NewGuid());
            var rs4 = new RestorePoint(storages, DateTime.Parse("12/29/1924"), "me", Guid.NewGuid());
            _restorePoints.Add(rs1);
            _restorePoints.Add(rs2);
            _restorePoints.Add(rs3);
            _restorePoints.Add(rs4);
        }

        [Test]
        public void SelectOutdatedRestorePoints_CheckOut()
        {
            List<RestorePoint> outdatedRestorePoints =
                new OutdatedRestorePointsSelection(_restorePoints, DateTime.Parse(("12/31/2020")))
                    .Execute();
            Assert.AreEqual(1, outdatedRestorePoints.Count);
            Assert.NotNull(outdatedRestorePoints.Find(p => p.Id.Equals(_restorePoints.Last().Id)));
        }
        
        [Test]
        public void SelectExceededRestorePoints_CheckOut()
        {
            List<RestorePoint> exceededRestorePoints =
                new OverTheNumberLimitRestorePointsSelection(_restorePoints, 2)
                    .Execute();
            Assert.AreEqual(2, exceededRestorePoints.Count);
            Assert.NotNull(exceededRestorePoints.Find(p => p.Id.Equals(_restorePoints.Last().Id)));
            Assert.NotNull(exceededRestorePoints.Find(p => p.Id.Equals(_restorePoints.First().Id)));
        }
        
        
        
    }
}