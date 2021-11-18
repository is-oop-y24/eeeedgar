using System;
using System.Collections.Generic;

namespace Backups.TemporaryLocalData
{
    public class TemporaryLocalRestorePoint
    {
        public TemporaryLocalRestorePoint(List<TemporaryLocalStorage> bufferStorages, DateTime dateTime, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            BufferStorages = bufferStorages;
            DateTime = dateTime;
        }

        public Guid Id { get; }
        public List<TemporaryLocalStorage> BufferStorages { get; }
        public DateTime DateTime { get; }
    }
}