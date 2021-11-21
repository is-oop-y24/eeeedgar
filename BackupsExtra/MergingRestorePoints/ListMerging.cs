using System.Collections.Generic;
using System.Linq;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public class ListMerging
    {
        public RestorePoint SingleStorage(List<RestorePoint> restorePoints)
        {
            RestorePoint result = null;
            for (int p = 0; p < restorePoints.Count - 1; p++)
            {
                if (p == 0)
                {
                    result = new SingleStorageRestorePointsMerging(restorePoints[p], restorePoints[p + 1]).Execute();
                }
                else
                {
                    result = new SingleStorageRestorePointsMerging(result, restorePoints[p + 1]).Execute();
                }
            }

            return result;
        }

        public RestorePoint SplitStorage(List<RestorePoint> restorePoints)
        {
            RestorePoint result = null;
            for (int p = 0; p < restorePoints.Count - 1; p++)
            {
                if (p == 0)
                {
                    result = new SplitStorageRestorePointsMerging(restorePoints[p], restorePoints[p + 1]).Execute();
                }
                else
                {
                    result = new SplitStorageRestorePointsMerging(result, restorePoints[p + 1]).Execute();
                }
            }

            return result;
        }
    }
}