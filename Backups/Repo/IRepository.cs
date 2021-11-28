using System;
using System.Collections.Generic;

namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(List<Storage> temporaryStorages, DateTime datetime);
        List<RestorePoint> GetRestorePoints();
    }
}