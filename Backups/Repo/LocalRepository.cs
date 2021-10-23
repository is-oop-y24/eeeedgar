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
        }
    }
}