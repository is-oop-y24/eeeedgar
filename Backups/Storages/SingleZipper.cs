using System;
using System.Collections.Generic;
using System.IO;
using Backups.Backup;
using Backups.Job;
using Ionic.Zip;

namespace Backups.Storages
{
    public class SingleZipper : IZipper
    {
        public SingleZipper()
        {
        }

        public List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion)
        {
            var zip = new ZipFile();
            Directory.CreateDirectory(restorePointPath);
            foreach (JobObject jobObject in backupJobVersion.JobObjects)
                zip.AddItem(jobObject.Path);
            string archivePath = Path.Combine(restorePointPath, "archive.zip");
            zip.Save(archivePath);
            var storages = new List<Storage> { new (archivePath) };
            return storages;
        }
    }
}