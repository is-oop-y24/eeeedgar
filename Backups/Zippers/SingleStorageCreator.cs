using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Repo;
using Backups.TemporaryLocalData;
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
        public List<TemporaryLocalStorage> Compress(List<JobObject> jobObjects)
        {
            var bufferStorages = new List<TemporaryLocalStorage>();
            var zip = new ZipFile();
            string temporaryArchivePath = Path.Combine(TemporaryFilesDirectoryPath, ArchiveName);
            var storage = new Storage();
            foreach (JobObject jobObject in jobObjects)
            {
                zip.AddItem(jobObject.Path);
                storage.JobObjects.Add(jobObject);
            }

            zip.Save(temporaryArchivePath);
            var temporaryStorage = new TemporaryLocalStorage(storage, temporaryArchivePath);
            bufferStorages.Add(temporaryStorage);
            return bufferStorages;
        }
    }
}