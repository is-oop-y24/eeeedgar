using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public class BackupJob
    {
        private bool _isZipZipStorageSplit; // false - single, true - split

        public BackupJob(string backupPath, bool makeZipStorageSplit)
        {
            BackupPath = backupPath;
            _isZipZipStorageSplit = makeZipStorageSplit;
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
            if (!Directory.Exists(BackupPath))
                Directory.CreateDirectory(backupPath);
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }
        public string BackupPath { get; }

        public void AddJobObject(string filePath)
        {
            var jobObject = new JobObject(filePath);
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(string filePath)
        {
            JobObjects.Remove(JobObjects.Find(o => o.FilePath.Equals(filePath)));
        }

        public void MakeBackup()
        {
            var restorePoint = new RestorePoint(_isZipZipStorageSplit);
            restorePoint.ZipStorage.Archive(JobObjects, $"{BackupPath}/{ArchiveName()}");
        }

        private string ArchiveName()
        {
            DateTime dateTime = DateTime.Now;
            return
                $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}-{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}";
        }
    }
}