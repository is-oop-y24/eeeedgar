using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Backups.Job;
using Backups.Repo;
using BackupsExtra.ClearingRestorePoints;
using BackupsExtra.Commands;
using BackupsExtra.MergingRestorePoints;
using Newtonsoft.Json;

namespace BackupsExtra.JobExtra
{
    public class BackupJobExtra
    {
        private Stream _logStream;

        public BackupJobExtra(Guid id, BackupJob job, IListMerging merging, List<IExceededRestorePointsSelection> rules, Stream logStream = null)
        {
            Id = id;
            Job = job;
            Merging = merging;
            Rules = rules;
            _logStream = logStream;
        }

        public BackupJobExtra(BackupJob job, IListMerging merging, List<IExceededRestorePointsSelection> rules, Stream logStream = null)
        {
            Id = Guid.NewGuid();
            Job = job;
            Merging = merging;
            Rules = rules;
            _logStream = logStream;
        }

        public Guid Id { get; }
        public BackupJob Job { get; }
        public IListMerging Merging { get; }
        public List<IExceededRestorePointsSelection> Rules { get; }

        public static BackupJobExtra Deserialize(string s)
        {
            return JsonConvert.DeserializeObject<BackupJobExtra>(s, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            });
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(
                this, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                    Formatting = Formatting.Indented,
                });
        }

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            Job.CreateBackup(date);
            ClearRestorePoints(backupDateTime);
        }

        public void ClearRestorePoints(DateTime now)
        {
            List<RestorePoint> restorePoints = Job.Repository.GetRestorePoints();
            List<RestorePoint> exceededRestorePoints =
                GetExceededRestorePoints(restorePoints, now);
            List<RestorePoint> activeRestorePoints =
                GetActiveRestorePoints(restorePoints, now);

            var restorePointsToMerge = new List<RestorePoint>(exceededRestorePoints);
            if (activeRestorePoints.Count > 0)
            {
                RestorePoint oldestActiveRestorePoint = OldestRestorePoint(activeRestorePoints);
                restorePointsToMerge.Add(oldestActiveRestorePoint);
                restorePoints.Remove(oldestActiveRestorePoint);
            }

            RestorePoint mergeResult = Merging.Execute(restorePointsToMerge, DateTime.Now);
            string log = Merging.Log();
            _logStream?.Write(Encoding.Unicode.GetBytes(log));

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

        private List<RestorePoint> GetExceededByRule(List<RestorePoint> restorePoints, IExceededRestorePointsSelection rule, DateTime now)
        {
            var command = new SelectExceededCommand(rule, now, restorePoints);
            List<RestorePoint> overNumberLimitRestorePoints = command.Execute();
            string log = command.Log();
            _logStream?.Write(Encoding.Unicode.GetBytes(log));

            return overNumberLimitRestorePoints;
        }

        private List<RestorePoint> GetExceededRestorePoints(List<RestorePoint> restorePoints, DateTime now)
        {
            var exceededRestorePoints = new List<RestorePoint>();
            foreach (IExceededRestorePointsSelection rule in Rules)
            {
                List<RestorePoint> exceededByRule = GetExceededByRule(restorePoints, rule, now);
                foreach (RestorePoint exceeded in exceededByRule)
                {
                    if (!exceededRestorePoints.Contains(exceeded))
                        exceededRestorePoints.Add(exceeded);
                }
            }

            return exceededRestorePoints;
        }

        private List<RestorePoint> GetActiveRestorePoints(List<RestorePoint> restorePoints, DateTime now)
        {
            List<RestorePoint> exceededRestorePoints = GetExceededRestorePoints(restorePoints, now);
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