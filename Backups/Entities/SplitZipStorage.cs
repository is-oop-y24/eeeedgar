using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SplitZipStorage : IZipStorage
    {
        public void Create(List<JobObject> jobObjects, string archivePath)
        {
            Directory.CreateDirectory(archivePath);
            foreach (JobObject jobObject in jobObjects)
            {
                using var zip = new ZipFile();
                zip.AddItem(jobObject.FilePath);
                zip.Save($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
            }
        }
    }
}