using Backups.ClientServer;

namespace Backups.Repo
{
    public class RemoteRepositoryProperties
    {
        public RemoteRepositoryProperties(Server server)
        {
            Server = server;
            Client = new Client(server);
        }

        public Server Server { get; }

        public Client Client { get; }
    }
}