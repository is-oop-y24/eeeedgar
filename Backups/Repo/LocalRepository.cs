using System;
using System.IO;
using Backups.Backup;

namespace Backups.Repo
{
    public class LocalRepository : IRepository
    {
        public LocalRepository(string path)
        {
            Properties = new LocalRepositoryProperties(path);
        }

        public LocalRepositoryProperties Properties { get; }
        public void UploadVersion(RestorePoint restorePoint)
        {
            string resDirectoryPath = Path.Combine(Properties.Path, restorePoint.Properties.Name);
            Directory.CreateDirectory(resDirectoryPath);
            foreach (Storage storage in restorePoint.Storages)
            {
                File.Copy(
                    storage.Properties.Path ?? throw new InvalidOperationException(),
                    Path.Combine(
                        resDirectoryPath,
                        Path.GetFileName(storage.Properties.Path) ?? throw new InvalidOperationException()));
            }
        }
    }
}