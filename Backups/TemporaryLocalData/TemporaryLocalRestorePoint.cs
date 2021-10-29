using System;
using System.Collections.Generic;

namespace Backups.TemporaryLocalData
{
    public class TemporaryLocalRestorePoint
    {
        public TemporaryLocalRestorePoint(List<TemporaryLocalStorage> bufferStorages, DateTime dateTime, int id)
        {
            BufferStorages = bufferStorages;
            DateTime = dateTime;
            Id = id;
        }

        public List<TemporaryLocalStorage> BufferStorages { get; }
        public DateTime DateTime { get; }
        public int Id { get; }
    }
}