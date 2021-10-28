using Backups.TemporaryLocalData;

namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(TemporaryLocalRestorePoint temporaryLocalRestorePoint);
    }
}