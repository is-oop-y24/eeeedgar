using System.Collections.Generic;

namespace Backups.Repo
{
    public class Storage
    {
        public Storage(List<string> filePaths)
        {
            FilePaths = filePaths;
        }

        public List<string> FilePaths { get; }
    }
}