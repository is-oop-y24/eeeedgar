using Backups.Backup;

namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(RestorePoint restorePoint);
    }
}