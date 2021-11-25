using System.Collections.Generic;

namespace Backups.Repo
{
    public interface IRepository
    {
        void UploadVersion(RestorePoint temporaryRestorePoint);
        List<RestorePoint> GetRestorePoints();
    }
}