using System;
using System.Collections.Generic;
using System.IO;
using Backups.Useful;

namespace Backups.Repo
{
    public class Repository
    {
        public Repository(string directoryPath)
        {
            Properties = new RepositoryProperties(directoryPath);
            ProjectVersions = new List<ProjectVersion>();

            PathCreator.CreatePath(directoryPath);
        }

        public RepositoryProperties Properties { get; }
        public List<ProjectVersion> ProjectVersions { get; }

        public ProjectVersion AddProjectVersion(List<string> relativeFilePaths, string commitMessage = "Untitled Project Version")
        {
            // create version folder
            string projectVersionFolderName =
                $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}";
            string projectVersionFolderPath = $"{Properties.AbsolutePath}/{projectVersionFolderName}";
            File.Create($"{projectVersionFolderPath}/version-info.txt");

            // find absolute file paths (we give them relative to storage folder)
            var absoluteFilePaths = new List<string>();
            foreach (string relativePath in relativeFilePaths)
            {
                absoluteFilePaths.Add($"{projectVersionFolderPath}{relativePath}");
            }

            var projectVersion = new ProjectVersion(absoluteFilePaths, commitMessage);

            ProjectVersions.Add(projectVersion);
            return projectVersion;
        }
    }
}