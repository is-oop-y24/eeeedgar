using Backups.ClientServer;

namespace Backups.Repo
{
    public class RemoteRepositoryProperties
    {
        public RemoteRepositoryProperties(Server server)
        {
            Client = new Client(server);
        }

        public Client Client { get; }
    }
}