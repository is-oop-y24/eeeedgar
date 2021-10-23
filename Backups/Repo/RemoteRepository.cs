using Backups.Backup;
using Backups.ClientServer;
using Backups.Job;

namespace Backups.Repo
{
    public class RemoteRepository : IRepository
    {
        public RemoteRepository(Server server)
        {
            Properties = new RemoteRepositoryProperties(server);
        }

        public RemoteRepositoryProperties Properties { get; }

        public void UploadVersion(RestorePoint restorePoint)
        {
            throw new System.NotImplementedException();
        }

        public void UploadVersion(BackupJobProperties backupJobProperties, BackupsProperties backupsProperties, RestorePoint restorePoint)
        {
            throw new System.NotImplementedException();
        }
    }
}