using System;
using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Storages;

namespace Backups.Backup
{
    public class Backups
    {
        public Backups(string relativePath)
        {
            Properties = new BackupsProperties(relativePath);
            RestorePoints = new List<RestorePoint>();
        }

        public BackupsProperties Properties { get; }
        public List<RestorePoint> RestorePoints { get; }

        public RestorePoint CreateRestorePoint(BackupJobProperties backupJobProperties, BackupJobVersion version)
        {
            string restorePointName = TimeToString();
            string restorePointPath = Path.Combine(backupJobProperties.Path, Properties.RelativePath, restorePointName);
            var zipper = new SingleZipper();
            return null;
        }

        private string TimeToString()
        {
            return $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        }
    }
}