using System;
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

        public void UploadVersion(List<Storage> temporaryStorages, DateTime datetime)
        {
            using NetworkStream stream = Sender.Client.GetStream();
            foreach (Storage localStorage in temporaryStorages)
            {
                Sender.SendFile(localStorage.Path, Path.GetFileName(localStorage.Path), stream);
            }

            var storages = new List<Storage>();
            foreach (Storage temporaryStorage in temporaryStorages)
            {
                string filename = Path.GetFileName(temporaryStorage.Path);
                storages.Add(new Storage(filename, temporaryStorage.Id));
            }

            var restorePoint = new RestorePoint(storages, datetime, Guid.NewGuid());
            RestorePoints.Add(restorePoint);
            DeleteTemporaryStorages(temporaryStorages);
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return RestorePoints;
        }

        private void DeleteTemporaryStorages(List<Storage> storages)
        {
            foreach (Storage storage in storages)
            {
                File.Delete(storage.Path);
            }
        }
    }
}