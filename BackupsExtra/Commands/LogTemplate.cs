using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.Commands
{
    public class LogTemplate
    {
        public string Merge(string storageType, RestorePoint restorePoint1, RestorePoint restorePoint2, RestorePoint resultRestorePoint, DateTime time)
        {
            return
                $"{time}\n{storageType}-Storage-Restore-Points Merge:\n{RestorePointLog(restorePoint1)}{RestorePointLog(restorePoint2)}{RestorePointLog(resultRestorePoint)}";
        }

        public string ExceededSelection(List<RestorePoint> exceededRestorePoints, DateTime date)
        {
            if (exceededRestorePoints.Count == 0)
            {
                return "Restore Points are under the Limit\n";
            }

            string answer = $"{date}\nExceeded Restore Points:\n";
            foreach (RestorePoint restorePoint in exceededRestorePoints)
            {
                answer +=
                    $"Restore Point:\t{restorePoint.Id}\t{restorePoint.DateTime}\n";
            }

            return answer;
        }

        private string RestorePointLog(RestorePoint restorePoint)
        {
            return restorePoint != null
                ? $"Restore Point 1:\t{restorePoint.Id}\t{restorePoint.DateTime}\n"
                : "undefined\n";
        }
    }
}