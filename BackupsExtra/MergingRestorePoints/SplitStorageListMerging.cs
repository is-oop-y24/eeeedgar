using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class SplitStorageListMerging : IListMerging
    {
        public RestorePoint Execute(List<RestorePoint> restorePoints)
        {
            RestorePoint result = restorePoints[0];
            for (int p = 1; p < restorePoints.Count; p++)
                result = new SplitStorageRestorePointsPairMerging(result, restorePoints[p]).Execute();

            return result;
        }
    }
}