using System;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(string path)
        {
            DateTime = DateTime.Now;
            Path = path;
        }

        public DateTime DateTime { get; }
        public string Path { get; }
    }
}