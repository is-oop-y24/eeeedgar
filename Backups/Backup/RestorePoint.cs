using System.Collections.Generic;

namespace Backups.Backup
{
    public class RestorePoint
    {
        public RestorePoint(string name, List<Storage> storages)
        {
            Properties = new RestorePointProperties(name);
            Storages = storages;
        }

        public RestorePointProperties Properties { get; }
        public List<Storage> Storages { get; }
    }
}