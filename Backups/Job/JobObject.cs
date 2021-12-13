using System;

namespace Backups.Job
{
    public class JobObject
    {
        public JobObject(Guid id, string path)
        {
            Id = id;
            Path = path;
        }

        public JobObject(string path)
        {
            Id = Guid.NewGuid();
            Path = path;
        }

        public Guid Id { get; }
        public string Path { get; }
    }
}