using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Repo;
using Ionic.Zip;

namespace Backups.Zippers
{
    public class SplitStorageCreator : IStorageCreator
    {
        public SplitStorageCreator(string temporaryFilesDirectoryPath)
        {
            TemporaryFilesDirectoryPath = temporaryFilesDirectoryPath;
        }

        public string TemporaryFilesDirectoryPath { get; }
        public List<Storage> Compress(List<JobObject> jobObjects)
        {
            var bufferStorages = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                string archivePath = Path.Combine(TemporaryFilesDirectoryPath, StorageName());
                var storage = new Storage(archivePath);
                var zip = new ZipFile();
                zip.AddItem(jobObject.Path);
                zip.Save(archivePath);
                storage.JobObjects.Add(jobObject);
                bufferStorages.Add(storage);
            }

            return bufferStorages;
        }

        private string StorageName()
        {
            return $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.zip";
        }
    }
}