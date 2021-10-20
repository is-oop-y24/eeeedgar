using System.Collections.Generic;

namespace Backups.Repo
{
    public class ProjectVersion
    {
        public ProjectVersion(List<string> filePaths, string relativePath)
        {
            Storage = new Storage(filePaths);
            Properties = new ProjectVersionProperties(relativePath);
        }

        public Storage Storage { get; }
        public ProjectVersionProperties Properties { get; }
    }
}