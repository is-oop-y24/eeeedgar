using System.Linq;
using Backups.ClientServer;
using Backups.Repo;
using Backups.Tools;

namespace Backups.Job
{
    public class BackupJob
    {
        public BackupJob(string backupsPath, string localRepoPath, bool isSplitCompression, Server server = null)
        {
            CurrentVersion = new BackupJobVersion();
            Backups = new Backup.Backups(backupsPath, isSplitCompression);

            if (server is null)
                Repository = new LocalRepository(localRepoPath);
            else
                Repository = new RemoteRepository(server);
        }

        public BackupJobVersion CurrentVersion { get; }
        public Backup.Backups Backups { get; }
        public IRepository Repository { get; }

        public void AddJobObject(string path)
        {
            if (CurrentVersion.JobObjects.Find(o => o.Path.Equals(path)) != null)
                throw new BackupsException("file is already added");
            CurrentVersion.JobObjects.Add(new JobObject(path));
        }

        public void RemoveJobObject(string path)
        {
            CurrentVersion.JobObjects.Remove(CurrentVersion.JobObjects.Find(o => o.Path.Equals(path)));
        }

        public void CreateBackup()
        {
            Repository.UploadVersion(Backups.CreateRestorePoint(CurrentVersion));
        }

        public void ChangeStorageMode(bool isSplitCompression)
        {
            Backups.Properties.IsSplitCompression = isSplitCompression;
        }
    }
}