using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Repo;
using Ionic.Zip;

namespace Backups.Zippers
{
    public class SingleStorageCreator : IStorageCreator
    {
        public SingleStorageCreator(string temporaryFilesDirectoryPath)
        {
            TemporaryFilesDirectoryPath = temporaryFilesDirectoryPath;
        }

        public string TemporaryFilesDirectoryPath { get; }
        public string ArchiveName => "archive.zip";
        public List<Storage> Compress(List<JobObject> jobObjects)
        {
            var bufferStorages = new List<Storage>();
            var zip = new ZipFile();
            string temporaryArchivePath = Path.Combine(TemporaryFilesDirectoryPath, ArchiveName);
            var storage = new Storage(temporaryArchivePath);
            foreach (JobObject jobObject in jobObjects)
            {
                zip.AddItem(jobObject.Path);
                storage.JobObjects.Add(jobObject);
            }

            zip.Save(temporaryArchivePath);
            bufferStorages.Add(storage);
            return bufferStorages;
        }
    }
}