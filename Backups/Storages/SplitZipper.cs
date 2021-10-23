using System;
using System.Collections.Generic;
using System.IO;
using Backups.Backup;
using Backups.Job;
using Ionic.Zip;

namespace Backups.Storages
{
    public class SplitZipper : IZipper
    {
        public List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion)
        {
            var storages = new List<Storage>();
            int id = 0;
            Directory.CreateDirectory(restorePointPath);
            foreach (JobObject jobObject in backupJobVersion.JobObjects)
            {
                var zip = new ZipFile();
                Console.WriteLine(jobObject.Path);

                zip.AddItem(jobObject.Path);
                string archiveName = Path.Combine(restorePointPath, $"{id++}.zip");
                zip.Save(archiveName);
                storages.Add(new Storage(archiveName));
            }

            return storages;
        }
    }
}