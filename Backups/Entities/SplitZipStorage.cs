using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SplitZipStorage : IZipStorage
    {
        public void Archive(List<JobObject> jobObjects, string archivePath)
        {
            Directory.CreateDirectory(archivePath);
            using var zip = new ZipFile();
            foreach (JobObject jobObject in jobObjects)
            {
                zip.AddItem(jobObject.FilePath);
                zip.Save($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
            }
        }
    }
}