using Backups.Repo;
namespace Backups.Job
{
    public class BackupJob
    {
        public BackupJob(string backupsPath, string localRepoPath)
        {
            CurrentVersion = new BackupJobVersion();
            Backups = new Backup.Backups(backupsPath, true);

            // make just local
            Repository = new LocalRepository(localRepoPath);
        }

        public BackupJobVersion CurrentVersion { get; }
        public Backup.Backups Backups { get; }
        public IRepository Repository { get; }
    }
}