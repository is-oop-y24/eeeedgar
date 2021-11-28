using System;

namespace Backups.Job
{
    public class JobObject
    {
        public JobObject(string path, Guid id = default)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Path = path;
        }

        public Guid Id { get; }
        public string Path { get; }
    }
}