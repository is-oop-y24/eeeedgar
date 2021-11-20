using System;
using System.Collections.Generic;
using System.IO;
using Backups.Repo;
using Backups.TemporaryLocalData;
using Backups.Tools;
using Backups.Zippers;

namespace Backups.Job
{
    public class BackupJob
    {
        public BackupJob(IRepository repository, IStorageCreator storageCreator, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            JobObjects = new List<JobObject>();
            Repository = repository;
            StorageCreator = storageCreator;
        }

        public Guid Id { get; }
        public List<JobObject> JobObjects { get; }
        public IStorageCreator StorageCreator { get; }
        public IRepository Repository { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (JobObjects.Find(o => o.Equals(jobObject)) != null)
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

        public void CreateBackup(DateTime backupDateTime = default)
        {
            DateTime date = backupDateTime == default ? DateTime.Now : backupDateTime;
            List<TemporaryLocalStorage> temporaryLocalStorages = StorageCreator.Compress(JobObjects);
            var temporaryLocalRestorePoint = new TemporaryLocalRestorePoint(temporaryLocalStorages, date);
            Repository.UploadVersion(temporaryLocalRestorePoint);
            foreach (TemporaryLocalStorage localStorage in temporaryLocalStorages)
            {
                File.Delete(localStorage.TemporaryPath);
            }
        }
    }
}