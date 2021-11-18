using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.TemporaryLocalData;
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
        public void UploadVersion(TemporaryLocalRestorePoint temporaryLocalRestorePoint)
        {
            string restorePointName = RestorePointName();
            DirectoryInfo repositoryRestorePointDirectory = Directory.CreateDirectory(Path.Combine(LocationPath, restorePointName));
            foreach (TemporaryLocalStorage localStorage in temporaryLocalRestorePoint.BufferStorages)
                File.Copy(localStorage.TemporaryPath, Path.Combine(repositoryRestorePointDirectory.FullName, Path.GetFileName(localStorage.TemporaryPath) ?? throw new BackupsException("wrong archive path")));
            var storages = temporaryLocalRestorePoint.BufferStorages.Select(bufferStorage => bufferStorage.Storage).ToList();
            var restorePoint = new RestorePoint(storages, temporaryLocalRestorePoint.DateTime, restorePointName);
            RestorePoints.Add(restorePoint);
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return RestorePoints;
        }

        private string RestorePointName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }
    }
}