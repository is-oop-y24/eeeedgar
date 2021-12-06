using System;
using System.Collections.Generic;

namespace Backups.Repo
{
    public interface IRepository
    {
        void CreateRestorePoint(List<Storage> temporaryStorages, DateTime datetime);
        List<RestorePoint> GetRestorePoints();
    }
}