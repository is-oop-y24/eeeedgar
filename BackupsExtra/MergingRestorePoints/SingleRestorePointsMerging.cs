using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SingleRestorePointsMerging
    {
        private readonly RestorePoint _restorePoint1;
        private readonly RestorePoint _restorePoint2;

        public SingleRestorePointsMerging(RestorePoint restorePoint1, RestorePoint restorePoint2)
        {
            _restorePoint1 = restorePoint1;
            _restorePoint2 = restorePoint2;
        }

        public RestorePoint Execute()
        {
            List<Storage> storages = _restorePoint1.DateTime > _restorePoint2.DateTime ? _restorePoint1.Storages : _restorePoint2.Storages;
            return new RestorePoint(storages, DateTime.Now, "unnamed", Guid.NewGuid());
        }
    }
}