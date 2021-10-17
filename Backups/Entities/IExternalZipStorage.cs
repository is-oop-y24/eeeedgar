using System.Collections.Generic;
using System.Net.Sockets;

namespace Backups.Entities
{
    public interface IExternalZipStorage
    {
        void Create(List<JobObject> jobObjects, TcpListener server);
    }
}