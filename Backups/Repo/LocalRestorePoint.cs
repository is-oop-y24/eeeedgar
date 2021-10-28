using System;
using System.Collections.Generic;
using Backups.Zippers;

namespace Backups.Repo
{
    public class LocalRestorePoint
    {
        public LocalRestorePoint(List<LocalStorage> bufferStorages, DateTime dateTime, int id)
        {
            BufferStorages = bufferStorages;
            DateTime = dateTime;
            Id = id;
        }

        public List<LocalStorage> BufferStorages { get; }
        public DateTime DateTime { get; }
        public int Id { get; }
    }
}