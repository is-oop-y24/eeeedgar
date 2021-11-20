using System;
using System.Collections.Generic;
using Backups.Job;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;

namespace BackupsExtra.JobExtra
{
    public class BackupJobExtra
    {
        public BackupJobExtra(BackupJob job, StorageConditions storageConditions)
        {
            Job = job;
            StorageConditions = storageConditions;
        }

        public BackupJob Job { get; }
        public StorageConditions StorageConditions { get; }

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            Job.CreateBackup(date);
            List<RestorePoint> restorePoints = Job.Repository.GetRestorePoints();
            if (StorageConditions.HasDeadline)
            {
                var selection =
                    new OutdatedRestorePointsSelection(restorePoints, StorageConditions.Deadline);
                var command = new SelectExceededCommand(selection, DateTime.Now);
                List<RestorePoint> exceededRestorePoints = command.Execute();
                Console.WriteLine(command.Log());
                foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
                {
                    restorePoints.Remove(exceededRestorePoint);
                }
            }

            if (StorageConditions.HasNumberLimit)
            {
                var selection =
                    new OverTheNumberLimitRestorePointsSelection(restorePoints, StorageConditions.NumberLimit);
                var command = new SelectExceededCommand(selection, DateTime.Now);
                List<RestorePoint> exceededRestorePoints = command.Execute();
                Console.WriteLine(command.Log());
                foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
                {
                    restorePoints.Remove(exceededRestorePoint);
                }
            }
        }
    }
}