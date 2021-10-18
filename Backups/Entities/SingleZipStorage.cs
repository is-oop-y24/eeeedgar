using System.Collections.Generic;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SingleZipStorage : IZipStorage
    {
        public void Create(List<JobObject> jobObjects, string archivePath)
        {
            using var zip = new ZipFile();
            foreach (JobObject jobObject in jobObjects)
            {
                zip.AddItem(jobObject.FilePath);
            }

            zip.Save($"{archivePath}.zip");
        }
    }
}