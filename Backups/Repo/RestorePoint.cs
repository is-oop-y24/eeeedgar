using System;
using System.Collections.Generic;
using Backups.Zippers;

namespace Backups.Repo
{
    public class RestorePoint
    {
        public RestorePoint(List<Storage> storages, DateTime dateTime, int id)
        {
            Storages = storages;
            DateTime = dateTime;
            Id = id;
        }

        public int Id { get; }
        public DateTime DateTime { get; }
        public List<Storage> Storages { get; }
    }
}