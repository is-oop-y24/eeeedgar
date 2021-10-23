using Backups.Repo;
namespace Backups.Job
{
    public class BackupJob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackupJob"/> class.
        /// </summary>
        /// <param name="path">Path to project directory.</param>
        /// <param name="localRepoPath">Path to local repo.</param>
        public BackupJob(string path, string localRepoPath)
        {
            Properties = new BackupJobProperties(path);
            CurrentVersion = new BackupJobVersion(Properties.CurrentVersionDirectoryName);
            Backups = new Backup.Backups(Properties.BackupsDirectoryName);

            // make just local
            Repository = new LocalRepository(localRepoPath);
        }

        public BackupJobProperties Properties { get; }
        public BackupJobVersion CurrentVersion { get; }
        public Backup.Backups Backups { get; }
        public IRepository Repository { get; }
    }
}