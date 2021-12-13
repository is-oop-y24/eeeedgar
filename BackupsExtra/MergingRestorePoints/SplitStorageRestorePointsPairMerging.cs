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

        public RestorePoint RestorePoint1 => _restorePoint1;
        public RestorePoint RestorePoint2 => _restorePoint2;

        public RestorePoint Execute()
        {
            if (_restorePoint1 is null && _restorePoint2 is null)
                return null;
            if (_restorePoint1 is null)
                return new RestorePoint(_restorePoint2.Storages, _restorePoint2.DateTime, Guid.NewGuid());
            if (_restorePoint2 is null)
                return new RestorePoint(_restorePoint1.Storages, _restorePoint1.DateTime, Guid.NewGuid());
            RestorePoint restorePointElder = null;
            RestorePoint restorePointNewer = null;
            if (_restorePoint1.DateTime < _restorePoint2.DateTime)
            {
                restorePointElder = _restorePoint1;
                restorePointNewer = _restorePoint2;
            }
            else
            {
                restorePointElder = _restorePoint2;
                restorePointNewer = _restorePoint1;
            }

            var storages = new List<Storage>(restorePointNewer.Storages);
            foreach (Storage storage in restorePointElder.Storages)
            {
                if (storages.Find(s => s.JobObjects.First().Id.Equals(storage.JobObjects.First().Id)) == null)
                    storages.Add(storage);
            }

            return new RestorePoint(storages, restorePointNewer.DateTime, Guid.NewGuid());
        }
    }
}