using System;
using System.Collections.Generic;

namespace Backups.Repo
{
    public class RestorePoint
    {
        public RestorePoint(List<Storage> storages, DateTime dateTime, string name, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Storages = storages;
            DateTime = dateTime;
            Name = name;
        }

        public Guid Id { get; }
        public DateTime DateTime { get; }
        public List<Storage> Storages { get; }
        public string Name { get; }
    }
}