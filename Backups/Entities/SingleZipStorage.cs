using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SingleZipStorage : IZipStorage
    {
        public List<string> Create(List<JobObject> jobObjects, string archivePath)
        {
            if (!Directory.Exists(archivePath))
                Directory.CreateDirectory(archivePath);
            foreach (JobObject jobObject in jobObjects)
            {
                using var zip = new ZipFile();
                zip.AddItem(jobObject.FilePath);
                zip.Save($"{archivePath}/archive.zip");
            }

            var paths = new List<string> { $"{archivePath}.zip" };
            paths.Add(CreateInfoFile(archivePath));
            return paths;
        }

        private string CreateInfoFile(string archivePath)
        {
            const string backupInfoFileName = "backupInfo.txt";
            StreamWriter streamWriter = File.CreateText($"{archivePath}/{backupInfoFileName}");
            streamWriter.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture));
            streamWriter.Close();
            return $"{archivePath}/{backupInfoFileName}";
        }
    }
}