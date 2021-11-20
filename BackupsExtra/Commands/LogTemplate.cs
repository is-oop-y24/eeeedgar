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
            $"{time}\n{storageType}-Storage-Restore-Points Merge:\n" +
                $"Restore Point 1:\t{restorePoint1.Id}\t{restorePoint1.DateTime}\n" +
                $"Restore Point 2:\t{restorePoint2.Id}\t{restorePoint2.DateTime}\n" +
                $"New Restore Point:\t{resultRestorePoint.Id}\t{resultRestorePoint.DateTime}\n";
        }

        public string ExceededSelection(List<RestorePoint> exceededRestorePoints, DateTime date)
        {
            string answer = $"{date}\nExceeded Restore Points:\n";
            foreach (RestorePoint restorePoint in exceededRestorePoints)
            {
                answer +=
                    $"Restore Point:\t{restorePoint.Id}\t{restorePoint.DateTime}\n";
            }

            return answer;
        }
    }
}