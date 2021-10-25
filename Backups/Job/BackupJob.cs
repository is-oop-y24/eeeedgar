using Backups.ClientServer;
using Backups.Repo;

namespace Backups.Job
{
    public class BackupJob
    {
        public BackupJob(string backupsPath, string localRepoPath, bool isSplitCompression, Server server = null, bool isItTest = false)
        {
            CurrentVersion = new BackupJobVersion();
            Backups = new Backup.Backups(backupsPath, isSplitCompression, isItTest);

            if (server is null)
                Repository = new LocalRepository(localRepoPath);
            else
                Repository = new RemoteRepository(server);
        }

        public BackupJobVersion CurrentVersion { get; }
        public Backup.Backups Backups { get; }
        public IRepository Repository { get; }

        public void AddJobObject(JobObject jobObject)
        {
            CurrentVersion.JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            CurrentVersion.JobObjects.Remove(jobObject);
        }

        public JobObject FindJobObject(string path)
        {
            return CurrentVersion.JobObjects.Find(o => o.Properties.Path.Equals(path));
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