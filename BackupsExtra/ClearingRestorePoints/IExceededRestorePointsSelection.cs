using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.ClearingRestorePoints
{
    public interface IExceededRestorePointsSelection
    {
        List<RestorePoint> Execute(List<RestorePoint> restorePoints);
    }
}