using System;
using System.Collections.Generic;
using System.IO;
using Backups.Repo;
using Backups.Tools;
using Backups.Zippers;

namespace Backups.Job
{
    public class BackupJob
    {
        private int _restorePointId;
        public BackupJob(IRepository repository, IStorageCreator storageCreator)
        {
            JobObjects = new List<JobObject>();
            Repository = repository;
            StorageCreator = storageCreator;
        }

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

        public void CreateBackup()
        {
            List<LocalStorage> localStorages = StorageCreator.Compress(JobObjects);
            var localRestorePoint = new LocalRestorePoint(localStorages, DateTime.Now, _restorePointId++);
            Repository.UploadVersion(localRestorePoint);
            foreach (LocalStorage localStorage in localStorages)
            {
                File.Delete(localStorage.TemporaryPath);
            }
        }
    }
}