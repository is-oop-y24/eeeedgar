using Backups.Backup;
using Backups.Job;

namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(BackupJobProperties backupJobProperties, BackupsProperties backupsProperties, RestorePoint restorePoint);
    }
}