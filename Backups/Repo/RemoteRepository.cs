using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Backups.Client;
using Backups.TemporaryLocalData;

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

        public void UploadVersion(TemporaryLocalRestorePoint temporaryLocalRestorePoint)
        {
            string restorePointName = RestorePointName();
            using NetworkStream stream = Sender.Client.GetStream();
            foreach (TemporaryLocalStorage localStorage in temporaryLocalRestorePoint.BufferStorages)
            {
                Sender.SendFile(localStorage.TemporaryPath, restorePointName, stream);
            }

            var storages = temporaryLocalRestorePoint.BufferStorages.Select(bufferStorage => bufferStorage.Storage).ToList();
            var restorePoint = new RestorePoint(storages, temporaryLocalRestorePoint.DateTime, restorePointName);
            RestorePoints.Add(restorePoint);
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return RestorePoints;
        }

        private string RestorePointName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }
    }
}