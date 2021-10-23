using System;
using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Storages;
using Backups.Useful;

namespace Backups.Backup
{
    public class Backups
    {
        public Backups(string path, bool isSplitCompression = false)
        {
            Properties = new BackupsProperties(path, isSplitCompression);
            RestorePoints = new List<RestorePoint>();
        }

        public BackupsProperties Properties { get; }
        public List<RestorePoint> RestorePoints { get; }

        public void CreateRestorePoint(BackupJobVersion version)
        {
            IZipper zip;
            if (Properties.IsSplitCompression)
            {
                Console.WriteLine("split");
                zip = new SplitZipper();
            }
            else
            {
                Console.WriteLine("single");
                zip = new SingleZipper();
            }

            string archivePath = Path.Combine(Properties.Path, $"{TimeToString()}");
            RestorePoints.Add(new RestorePoint(zip.Compress(archivePath, version)));
        }

        private string TimeToString()
        {
            return $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}_{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
        }
    }
}