using System;
using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups.Repo
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(string locationPath)
        {
            LocationPath = locationPath;
            RestorePoints = new List<RestorePoint>();
        }

        public string LocationPath { get; }
        public List<RestorePoint> RestorePoints { get; }
        public void UploadVersion(List<Storage> temporaryStorages, DateTime datetime)
        {
            SaveRestorePointFiles(temporaryStorages);

            var storages = new List<Storage>();
            foreach (Storage temporaryStorage in temporaryStorages)
            {
                string filename = Path.GetFileName(temporaryStorage.Path);
                var storage = new Storage(filename, temporaryStorage.Id);
                storage.JobObjects.AddRange(temporaryStorage.JobObjects);
                storages.Add(storage);
            }

            var restorePoint = new RestorePoint(storages, datetime, Guid.NewGuid());
            RestorePoints.Add(restorePoint);
            DeleteTemporaryStorages(temporaryStorages);
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return RestorePoints;
        }

        private string RestorePointName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }

        private void SaveRestorePointFiles(List<Storage> temporaryStorages)
        {
            foreach (Storage localStorage in temporaryStorages)
                File.Copy(localStorage.Path, Path.Combine(LocationPath, Path.GetFileName(localStorage.Path) ?? throw new BackupsException("wrong archive path")));
        }

        private void DeleteTemporaryStorages(List<Storage> storages)
        {
            foreach (Storage storage in storages)
            {
                File.Delete(storage.Path);
            }
        }
    }
}