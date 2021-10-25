using System.Collections.Generic;
using Backups.Backup;
using Backups.Job;

namespace Backups.Zippers
{
    public class TestZipper : IZipper
    {
        public List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion)
        {
            return new List<Storage>();
        }
    }
}