using System;
using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Zippers;

namespace Backups.Backup
{
    public class Backups
    {
        public Backups(string path, bool isSplitCompression = false, bool isItTest = false)
        {
            Properties = new BackupsProperties(path, isSplitCompression, isItTest);
            RestorePoints = new List<RestorePoint>();
        }

        public BackupsProperties Properties { get; }
        public List<RestorePoint> RestorePoints { get; }

        public RestorePoint CreateRestorePoint(BackupJobVersion version)
        {
            IZipper zip;
            if (Properties.IsSplitCompression)
            {
                zip = new SplitZipper();
            }
            else
            {
                zip = new SingleZipper();
            }

            if (Properties.IsItTest)
                zip = new TestZipper();

            string restorePointName = $"{TimeToString()}";
            string restorePointPath = Path.Combine(Properties.Path, restorePointName);
            var restorePoint = new RestorePoint(restorePointName, zip.Compress(restorePointPath, version));
            RestorePoints.Add(restorePoint);
            return restorePoint;
        }

        private string TimeToString()
        {
            return $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        }
    }
}