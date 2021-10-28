namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(LocalRestorePoint localRestorePoint);
    }
}