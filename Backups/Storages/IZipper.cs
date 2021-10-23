using System.Collections.Generic;
using Backups.Job;

namespace Backups.Storages
{
    public interface IZipper
    {
        List<string> Compress(string archivePath, BackupJobProperties backupJobProperties, BackupJobVersion backupJobVersion);
    }
}