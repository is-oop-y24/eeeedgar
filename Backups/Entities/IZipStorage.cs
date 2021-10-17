using System.Collections.Generic;

namespace Backups.Entities
{
    public interface IZipStorage
    {
        void Create(List<JobObject> jobObjects, string archivePath);
    }
}