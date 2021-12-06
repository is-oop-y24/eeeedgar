using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Backups.Repo;
using Backups.Tools;
using Backups.Zippers;

namespace Backups.Job
{
    public class BackupJob
    {
        public BackupJob(Guid id, IRepository repository, IStorageCreator storageCreator)
        {
            Id = id;
            JobObjects = new List<JobObject>();
            Repository = repository;
            StorageCreator = storageCreator;
        }

        public BackupJob(IRepository repository, IStorageCreator storageCreator)
        {
            Id = Guid.NewGuid();
            JobObjects = new List<JobObject>();
            Repository = repository;
            StorageCreator = storageCreator;
        }

        [JsonConstructor]
        private BackupJob(Guid id, List<JobObject> jobObjects, IStorageCreator storageCreator, IRepository repository)
        {
            Id = id;
            JobObjects = jobObjects;
            StorageCreator = storageCreator;
            Repository = repository;
        }

        public Guid Id { get; }
        public List<JobObject> JobObjects { get; }
        public IStorageCreator StorageCreator { get; }
        public IRepository Repository { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (JobObjects.Find(o => o.Path == jobObject.Path) != null)
                throw new BackupsException("job object is already added");
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            JobObjects.Remove(jobObject);
        }

        public void FindJobObject(string path)
        {
            JobObjects.Find(o => o.Path.Equals(path));
        }

        public void CreateBackup(DateTime backupDate = default)
        {
            DateTime date = backupDate == default ? DateTime.Now : backupDate;
            List<Storage> temporaryStorages = StorageCreator.Compress(JobObjects);
            Repository.UploadVersion(temporaryStorages, date);
        }
    }
}