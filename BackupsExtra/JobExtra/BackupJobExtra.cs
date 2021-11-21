using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;
using BackupsExtra.MergingRestorePoints;

namespace BackupsExtra.JobExtra
{
    public class BackupJobExtra
    {
        public BackupJobExtra(BackupJob job, StorageConditions storageConditions, IListMerging merging)
        {
            Job = job;
            StorageConditions = storageConditions;
            Merging = merging;
        }

        public BackupJob Job { get; }
        public StorageConditions StorageConditions { get; }
        public IListMerging Merging { get; }

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            Job.CreateBackup(date);
            List<RestorePoint> restorePoints = Job.Repository.GetRestorePoints();

            List<RestorePoint> exceededRestorePoints = GetExceededRestorePoints(restorePoints);
            if (exceededRestorePoints.Count > 0)
            {
                RestorePoint exceededRestorePointsMerge = Merging.Execute(exceededRestorePoints);

                foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
                {
                    restorePoints.Remove(exceededRestorePoint);
                }

                RestorePoint oldestNotExceededRestorePoint = OldestRestorePointFromTheList(restorePoints);

                var exceededAndLast = new List<RestorePoint>
                    { exceededRestorePointsMerge, oldestNotExceededRestorePoint };
                restorePoints.Remove(oldestNotExceededRestorePoint);
                RestorePoint merged = Merging.Execute(exceededAndLast);
                Console.WriteLine($"merged: {merged.Storages.Count}");
                restorePoints.Add(merged);
            }

            SortRestorePointsByDate(restorePoints);
        }

        private RestorePoint OldestRestorePointFromTheList(List<RestorePoint> restorePoints)
        {
            RestorePoint oldestNotExceededRestorePoint = restorePoints.First();
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (oldestNotExceededRestorePoint.DateTime > restorePoint.DateTime)
                    oldestNotExceededRestorePoint = restorePoint;
            }

            return oldestNotExceededRestorePoint;
        }

        private List<RestorePoint> GetExceededRestorePoints(List<RestorePoint> restorePoints)
        {
            var exceededRestorePoints = new List<RestorePoint>();
            if (StorageConditions.HasDeadline)
            {
                var selection =
                    new OutdatedRestorePointsSelection(restorePoints, StorageConditions.Deadline);
                var command = new SelectExceededCommand(selection, DateTime.Now);
                List<RestorePoint> outdatedRestorePoints = command.Execute();
                Console.WriteLine(command.Log());
                exceededRestorePoints.AddRange(outdatedRestorePoints);
            }

            if (StorageConditions.HasNumberLimit)
            {
                var selection =
                    new OverTheNumberLimitRestorePointsSelection(restorePoints, StorageConditions.NumberLimit);
                var command = new SelectExceededCommand(selection, DateTime.Now);
                List<RestorePoint> overNumberLimitRestorePoints = command.Execute();
                Console.WriteLine(command.Log());
                foreach (RestorePoint overNumberLimitRestorePoint in overNumberLimitRestorePoints)
                {
                    if (!exceededRestorePoints.Contains(overNumberLimitRestorePoint))
                        exceededRestorePoints.Add(overNumberLimitRestorePoint);
                }
            }

            return exceededRestorePoints;
        }

        private void SortRestorePointsByDate(List<RestorePoint> restorePoints)
        {
            restorePoints.Sort((p1, p2) => p1.DateTime.CompareTo(p2.DateTime));
        }
    }
}