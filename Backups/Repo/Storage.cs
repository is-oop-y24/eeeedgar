using System;
using System.Collections.Generic;
using Backups.Job;

namespace Backups.Repo
{
    public class Storage
    {
        public Storage(string path, Guid id = default)
        {
            Path = path;
            Id = id == default ? Guid.NewGuid() : id;
            JobObjects = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<JobObject> JobObjects { get; }
        public string Path { get; }
    }
}