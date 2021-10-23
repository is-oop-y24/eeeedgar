using System.Collections.Generic;

namespace Backups.Backup
{
    public class RestorePoint
    {
        public RestorePoint(List<Storage> storages)
        {
            Storages = storages;
        }

        public List<Storage> Storages { get; }
    }
}