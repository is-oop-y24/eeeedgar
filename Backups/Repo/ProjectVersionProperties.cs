using System;

namespace Backups.Repo
{
    public class ProjectVersionProperties
    {
        public ProjectVersionProperties(string relativePath)
        {
            DateTime = DateTime.Now;
            RelativePath = relativePath;
        }

        public DateTime DateTime { get; }
        public string RelativePath { get; }
    }
}