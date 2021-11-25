using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void UploadVersion(RestorePoint temporaryRestorePoint)
        {
            DirectoryInfo repositoryRestorePointDirectory = Directory.CreateDirectory(Path.Combine(LocationPath, temporaryRestorePoint.Id.ToString()));
            foreach (Storage localStorage in temporaryRestorePoint.Storages)
                File.Copy(localStorage.Path, Path.Combine(repositoryRestorePointDirectory.FullName, Path.GetFileName(localStorage.Path) ?? throw new BackupsException("wrong archive path")));

            // var storages = new List<Storage>(temporaryRestorePoint.Storages);
            var storages = new List<Storage>();
            foreach (Storage temporaryStorage in temporaryRestorePoint.Storages)
            {
                string filename = Path.GetFileName(temporaryStorage.Path);
                storages.Add(new Storage(filename, temporaryStorage.Id));
            }

            var restorePoint = new RestorePoint(storages, temporaryRestorePoint.DateTime, temporaryRestorePoint.Id);
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