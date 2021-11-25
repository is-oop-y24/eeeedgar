using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Backups.Client;

namespace Backups.Repo
{
    public class RemoteRepository : IRepository
    {
        public RemoteRepository(string hostname, int port)
        {
            Sender = new Sender(hostname, port);
            RestorePoints = new List<RestorePoint>();
        }

        public Sender Sender { get; }
        public List<RestorePoint> RestorePoints { get; }

        public void UploadVersion(RestorePoint temporaryRestorePoint)
        {
            using NetworkStream stream = Sender.Client.GetStream();
            foreach (Storage localStorage in temporaryRestorePoint.Storages)
            {
                Sender.SendFile(localStorage.Path, temporaryRestorePoint.Id.ToString(), stream);
            }

            var storages = new List<Storage>();
            foreach (Storage temporaryStorage in temporaryRestorePoint.Storages)
            {
                string filename = Path.GetFileName(temporaryStorage.Path);
                storages.Add(new Storage(filename, temporaryStorage.Id));
            }

            var restorePoint = new RestorePoint(storages, temporaryRestorePoint.DateTime, temporaryRestorePoint.Id);
            RestorePoints.Add(restorePoint);
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return RestorePoints;
        }
    }
}