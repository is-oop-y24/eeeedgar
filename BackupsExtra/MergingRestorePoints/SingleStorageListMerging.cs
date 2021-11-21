using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SingleStorageListMerging : IListMerging
    {
        public RestorePoint Execute(List<RestorePoint> restorePoints)
        {
            RestorePoint result = restorePoints[0];
            for (int p = 1; p < restorePoints.Count; p++)
                result = new SingleStorageRestorePointsPairMerging(result, restorePoints[p]).Execute();

            return result;
        }
    }
}