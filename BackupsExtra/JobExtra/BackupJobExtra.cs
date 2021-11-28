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
        public BackupJobExtra(BackupJob job, StorageConditions storageConditions, IListMerging merging, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Job = job;
            StorageConditions = storageConditions;
            Merging = merging;
        }

        public Guid Id { get; }
        public BackupJob Job { get; }
        public StorageConditions StorageConditions { get; }
        public IListMerging Merging { get; }

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            Job.CreateBackup(date);
            ClearRestorePoints();
        }

        public void ClearRestorePoints()
        {
            List<RestorePoint> restorePoints = Job.Repository.GetRestorePoints();
            List<RestorePoint> exceededRestorePoints =
                GetExceededRestorePoints(restorePoints);
            List<RestorePoint> activeRestorePoints =
                GetActiveRestorePoints(restorePoints);

            var restorePointsToMerge = new List<RestorePoint>(exceededRestorePoints);
            if (activeRestorePoints.Count > 0)
            {
                RestorePoint oldestActiveRestorePoint = OldestRestorePoint(activeRestorePoints);
                restorePointsToMerge.Add(oldestActiveRestorePoint);
                restorePoints.Remove(oldestActiveRestorePoint);
            }

            RestorePoint mergeResult = Merging.Execute(restorePointsToMerge);

            restorePoints.Add(mergeResult);
            foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
            {
                restorePoints.Remove(exceededRestorePoint);
            }

            SortRestorePointsByDate(restorePoints);
        }

        private RestorePoint OldestRestorePoint(List<RestorePoint> restorePoints)
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

        private List<RestorePoint> GetActiveRestorePoints(List<RestorePoint> restorePoints)
        {
            List<RestorePoint> exceededRestorePoints = GetExceededRestorePoints(restorePoints);
            var activeRestorePoints = new List<RestorePoint>(restorePoints);
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (exceededRestorePoints.Contains(restorePoint))
                    activeRestorePoints.Remove(restorePoint);
            }

            return activeRestorePoints;
        }

        private void SortRestorePointsByDate(List<RestorePoint> restorePoints)
        {
            restorePoints.Sort((p1, p2) => p1.DateTime.CompareTo(p2.DateTime));
        }
    }
}