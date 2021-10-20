using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Ionic.Zip;

namespace Backups.Entities
{
    public class SplitZipStorage : IZipStorage
    {
        public List<string> Create(List<JobObject> jobObjects, string archivePath)
        {
            var paths = new List<string>();
            if (!Directory.Exists(archivePath))
                Directory.CreateDirectory(archivePath);
            foreach (JobObject jobObject in jobObjects)
            {
                using var zip = new ZipFile();
                zip.AddItem(jobObject.FilePath);
                zip.Save($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
                paths.Add($"{archivePath}/{jobObject.FileNameWithoutExtension}_{jobObject.FileExtension}.zip");
            }

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