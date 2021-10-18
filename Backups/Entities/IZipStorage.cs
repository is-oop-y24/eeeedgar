using System.Collections.Generic;

namespace Backups.Entities
{
    public interface IZipStorage
    {
        List<string> Create(List<JobObject> jobObjects, string archivePath);
    }
}