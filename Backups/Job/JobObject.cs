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

        public override bool Equals(object obj) => this.Equals(obj as JobObject);

        public bool Equals(JobObject jobObject)
        {
            if (jobObject is null)
            {
                return false;
            }

            return Path.Equals(jobObject.Path);
        }

        public override int GetHashCode() => Path.GetHashCode();
    }
}