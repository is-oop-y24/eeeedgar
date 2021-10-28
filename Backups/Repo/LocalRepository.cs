using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Tools;
using Backups.Zippers;

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
        public void UploadVersion(LocalRestorePoint localRestorePoint)
        {
            DirectoryInfo repositoryRestorePointDirectory = Directory.CreateDirectory(Path.Combine(LocationPath, CommitName()));
            foreach (LocalStorage localStorage in localRestorePoint.BufferStorages)
                File.Copy(localStorage.TemporaryPath, Path.Combine(repositoryRestorePointDirectory.FullName, Path.GetFileName(localStorage.TemporaryPath) ?? throw new BackupsException("wrong archive path")));
            var storages = localRestorePoint.BufferStorages.Select(bufferStorage => bufferStorage.Storage).ToList();
            var restorePoint = new RestorePoint(storages, localRestorePoint.DateTime, localRestorePoint.Id);
            RestorePoints.Add(restorePoint);
        }

        private string CommitName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }
    }
}