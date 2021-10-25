using System.Collections.Generic;
using System.IO;
using Backups.Backup;
using Backups.Job;
using Backups.Tools;
using Ionic.Zip;

namespace Backups.Zippers
{
    public class SplitZipper : IZipper
    {
        public List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion)
        {
            var storages = new List<Storage>();
            Directory.CreateDirectory(restorePointPath);
            foreach (JobObject jobObject in backupJobVersion.JobObjects)
            {
                var zip = new ZipFile();
                zip.AddItem(jobObject.Path);
                string archiveName = $"{Path.GetFileNameWithoutExtension(jobObject.Path)}.zip";
                archiveName = PathCreator.GetFreeFileName(restorePointPath, archiveName);
                string archivePath = Path.Combine(restorePointPath, archiveName);
                zip.Save(archivePath);
                storages.Add(new Storage(archivePath));
            }

            return storages;
        }
    }
}