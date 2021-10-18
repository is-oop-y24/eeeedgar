using System.Collections.Generic;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SplitZipStorage : IZipStorage
    {
        public List<string> Create(List<JobObject> jobObjects, string archivePath)
        {
            // const string backupInfoFileName = "backupInfo.txt";
            // Directory.CreateDirectory(archivePath);
            // File.Create($"{archivePath}/{backupInfoFileName}");
            // File.WriteAllText($"{archivePath}/{backupInfoFileName}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            var paths = new List<string>();
            foreach (JobObject jobObject in jobObjects)
            {
                using var zip = new ZipFile();
                zip.AddItem(jobObject.FilePath);
                zip.Save($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
                paths.Add($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
            }

            return paths;
        }
    }
}