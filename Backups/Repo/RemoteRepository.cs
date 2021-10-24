using Backups.Backup;
using Backups.ClientServer;

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
            foreach (Storage storage in restorePoint.Storages)
            {
                Properties.Client.SendFile(storage.Properties.Path, restorePoint.Properties.Name);
                Properties.Server.CreateFiles(Properties.Server.SplitReceivedDataToFiles());
            }
        }
    }
}