using System.Collections.Generic;
using Backups.ClientServer;

namespace Backups.Entities
{
    public interface IExternalZipStorage
    {
        void Create(List<JobObject> jobObjects, Client client);
    }
}