using System.Collections.Generic;
using Backups.Backup;
using Backups.Job;

namespace Backups.Zippers
{
    public interface IZipper
    {
        List<Storage> Compress(string restorePointPath, BackupJobVersion backupJobVersion);
    }
}