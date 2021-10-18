using System.Collections.Generic;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SingleZipStorage : IZipStorage
    {
        public List<string> Create(List<JobObject> jobObjects, string archivePath)
        {
            // const string backupInfoFileName = "backupInfo.txt";
            // Directory.CreateDirectory(archivePath);
            // File.Create($"{archivePath}/{backupInfoFileName}");
            // File.WriteAllText($"{archivePath}/{backupInfoFileName}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            using var zip = new ZipFile();
            foreach (JobObject jobObject in jobObjects)
            {
                zip.AddItem(jobObject.FilePath);
            }

            zip.Save($"{archivePath}.zip");
            var paths = new List<string> { $"{archivePath}.zip" };
            return paths;
        }
    }
}