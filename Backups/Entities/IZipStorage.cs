using System.Collections.Generic;

namespace Backups.Entities
{
    public interface IZipStorage
    {
        void Archive(List<JobObject> jobObjects, string archivePath);
    }
}