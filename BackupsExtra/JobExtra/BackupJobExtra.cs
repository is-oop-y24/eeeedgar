using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Job;
using Backups.Repo;
using Backups.Zippers;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;
using BackupsExtra.MergingRestorePoints;
using BackupsExtra.RepoExtra;

namespace BackupsExtra.JobExtra
{
    public class BackupJobExtra
    {
        public BackupJobExtra(StorageConditions storageConditions, IListMerging merging, IRepositoryExtra repositoryExtra, IStorageCreator storageCreator, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            RepositoryExtra = repositoryExtra;
            Job = new BackupJob(repositoryExtra.Repository(), storageCreator, Id);
            StorageConditions = storageConditions;
            Merging = merging;
        }

        public Guid Id { get; }
        public BackupJob Job { get; }
        public StorageConditions StorageConditions { get; }
        public IListMerging Merging { get; }
        public IRepositoryExtra RepositoryExtra { get; }

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            Job.CreateBackup(date);
            List<RestorePoint> restorePoints = Job.Repository.GetRestorePoints();

            List<RestorePoint> exceededRestorePoints = GetExceededRestorePoints(restorePoints);
            foreach (RestorePoint exceededRestorePoint in exceededRestorePoints)
            {
                RepositoryExtra.DeleteRestorePoint(exceededRestorePoint.Id);
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