using System;
using System.Collections.Generic;
using Backups.Job;

namespace Backups.Repo
{
    public class Storage
    {
        public Storage(Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            JobObjects = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<JobObject> JobObjects { get; }
    }
}