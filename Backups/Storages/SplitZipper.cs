using System.Collections.Generic;
using Backups.Job;

namespace Backups.Storages
{
    public class SplitZipper : IZipper
    {
        public void AddFile(string absolutePath)
        {
            throw new System.NotImplementedException();
        }

        public List<string> Compress()
        {
            throw new System.NotImplementedException();
        }

        public List<string> Compress(string archivePath, BackupJobProperties backupJobProperties, BackupJobVersion backupJobVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}