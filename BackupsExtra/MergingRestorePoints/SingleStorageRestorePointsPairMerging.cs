using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SingleStorageRestorePointsPairMerging : IPairMerging
    {
        private RestorePoint _restorePoint1;
        private RestorePoint _restorePoint2;
        public SingleStorageRestorePointsPairMerging(RestorePoint restorePoint1, RestorePoint restorePoint2)
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
            List<Storage> storages = _restorePoint1.DateTime > _restorePoint2.DateTime ? _restorePoint1.Storages : _restorePoint2.Storages;
            DateTime dateTime = _restorePoint1.DateTime > _restorePoint2.DateTime ? _restorePoint1.DateTime : _restorePoint2.DateTime;
            return new RestorePoint(storages, dateTime, Guid.NewGuid());
        }
    }
}