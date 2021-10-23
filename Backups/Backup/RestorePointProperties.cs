using System;

namespace Backups.Backup
{
    public class RestorePointProperties
    {
        public RestorePointProperties(string name)
        {
            RelativePath = name;
        }

        public string RelativePath { get; }
    }
}