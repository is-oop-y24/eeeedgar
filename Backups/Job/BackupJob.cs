using Backups.ClientServer;
using Backups.Repo;
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
            CurrentVersion.JobObjects.Add(new JobObject(path));
        }

        public void CreateBackup()
        {
            Repository.UploadVersion(Backups.CreateRestorePoint(CurrentVersion));
        }
    }
}