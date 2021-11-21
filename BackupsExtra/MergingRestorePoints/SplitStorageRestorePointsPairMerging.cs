using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SplitStorageRestorePointsPairMerging : IPairMerging
    {
        private readonly RestorePoint _restorePoint1;
        private readonly RestorePoint _restorePoint2;

        public SplitStorageRestorePointsPairMerging(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            _restorePoint1 = restorePoint1;
            _restorePoint2 = restorePoint2;
        }

        public RestorePoint RestorePoint1() => _restorePoint1;
        public RestorePoint RestorePoint2() => _restorePoint2;

        public RestorePoint Execute()
        {
            RestorePoint restorePointElder =
                _restorePoint1.DateTime < _restorePoint2.DateTime ? _restorePoint1 : _restorePoint2;
            DateTime dateTime = _restorePoint1.DateTime > _restorePoint2.DateTime ? _restorePoint1.DateTime : _restorePoint2.DateTime;
            RestorePoint restorePointNewer = _restorePoint1.DateTime < _restorePoint2.DateTime ? _restorePoint2 : _restorePoint1;
            var storages = new List<Storage>();
            storages.AddRange(restorePointNewer.Storages);
            foreach (Storage storage in restorePointElder.Storages)
            {
                if (storages.Find(s => s.JobObjects.First().Id.Equals(storage.JobObjects.First().Id)) == null)
                    storages.Add(storage);
            }

            return new RestorePoint(storages, dateTime, "unnamed", Guid.NewGuid());
        }
    }
}