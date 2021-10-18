using System.Collections.Generic;
using System.Net;
using Backups.ClientServer;

namespace Backups.Entities
{
    public interface IExternalZipStorage
    {
        void Create(List<JobObject> jobObjects, Server server);
    }
}