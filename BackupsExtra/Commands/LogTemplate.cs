using System;
using Backups.Repo;

namespace BackupsExtra.Commands
{
    public static class LogTemplate
    {
        public static string Log(string storageType, RestorePoint restorePoint1, RestorePoint restorePoint2, RestorePoint resultRestorePoint, DateTime time)
        {
            return
            $"{time}\n{storageType}-Storage-Restore-Points Merge:\n" +
                $"Restore Point 1:\t{restorePoint1.Id}\t{restorePoint1.DateTime}\n" +
                $"Restore Point 2:\t{restorePoint2.Id}\t{restorePoint2.DateTime}\n" +
                $"Result Restore Point:\t{resultRestorePoint.Id}\t{resultRestorePoint.DateTime}\n";
        }
    }
}