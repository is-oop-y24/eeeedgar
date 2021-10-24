using System;
using System.Collections.Generic;
using Backups.Backup;
using Backups.Job;

namespace Backups.Storages
{
    public class SplitZipper : IZipper
    {
        public List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion)
        {
            // todo implement SplitZipper
            throw new NotImplementedException();
/*
            var storages = new List<Storage>();
            Directory.CreateDirectory(restorePointPath);
            foreach (JobObject jobObject in backupJobVersion.JobObjects)
            {
                var zip = new ZipFile();
                zip.AddItem(jobObject.Path);

                // string archiveName =
                //    PathCreator.GetFreeFileName(restorePointPath, $"{jobObject.Path.Split("/")[^1].Split('.')[0]}.zip");
                string archiveName = Path.Combine(restorePointPath, $"{jobObject.Path.Split("/")[^1].Split('.')[0]}.zip");
                Console.WriteLine($"restore point path: {restorePointPath}");
                Console.WriteLine($"arcname: {archiveName}");
                zip.Save(archiveName);
                storages.Add(new Storage(archiveName));
            }

            return storages;
*/
        }
    }
}