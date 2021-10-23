using System;
using System.IO;
using Backups.Backup;
using Backups.Job;
using Backups.Useful;

namespace Backups.Repo
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(string path)
        {
            Properties = new LocalRepositoryProperties(path);
        }

        public LocalRepositoryProperties Properties { get; }
        public void UploadVersion(BackupJobProperties backupJobProperties, BackupsProperties backupsProperties, RestorePoint restorePoint)
        {
            string restorePointPath = PathCreator.CreateUniqueDirectory(Properties.Path, restorePoint.Properties.RelativePath);
            foreach (Storage storage in restorePoint.Storages)
            {
                Console.WriteLine(restorePointPath);
                Console.WriteLine(Path.Combine(backupJobProperties.Path, backupsProperties.RelativePath, restorePoint.Properties.RelativePath, storage.Properties.RelativePath));
                File.WriteAllBytes(restorePointPath, File.ReadAllBytes(Path.Combine(backupJobProperties.Path, backupsProperties.RelativePath, restorePoint.Properties.RelativePath, storage.Properties.RelativePath)));
            }
        }
    }
}