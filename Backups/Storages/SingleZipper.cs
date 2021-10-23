using System;
using System.Collections.Generic;
using System.IO;
using Backups.Backup;
using Backups.Job;
using Backups.Useful;
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
            foreach (JobObject jobObject in backupJobVersion.JobObjects)
            {
                Console.WriteLine(jobObject.Path);
                if (!Directory.Exists(restorePointPath))
                    Directory.CreateDirectory(restorePointPath);
                zip.AddItem(jobObject.Path);
            }

            string archivePath = Path.Combine(restorePointPath, "archive.zip");
            zip.Save(archivePath);
            var storages = new List<Storage> { new (archivePath) };
            return storages;
        }
    }
}