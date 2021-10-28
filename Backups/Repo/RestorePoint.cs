using System;
using System.Collections.Generic;

namespace Backups.Repo
{
    public class RestorePoint
    {
        public RestorePoint(List<Storage> storages, DateTime dateTime, int id, string name)
        {
            Storages = storages;
            DateTime = dateTime;
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public DateTime DateTime { get; }
        public List<Storage> Storages { get; }
        public string Name { get; }
    }
}