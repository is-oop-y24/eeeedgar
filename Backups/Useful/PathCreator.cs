using System;
using System.IO;

namespace Backups.Useful
{
    public class PathCreator
    {
        public static void CreatePath(string path)
        {
            string[] followingDirectories = path.Split('/');
            string p = string.Empty;
            foreach (string directory in followingDirectories)
            {
                p += $"{directory}/";
                if (!Directory.Exists(p))
                    Directory.CreateDirectory(p);
            }
        }

        public static void CreateEmptyFile(string path)
        {
            WriteAll(path).Close();
        }

        public static void CreateEmptyFile(string directoryPath, string fileName)
        {
            WriteAll(directoryPath, fileName).Close();
        }

        public static FileStream WriteAll(string directoryPath, string fileName)
        {
            if (File.Exists($"{directoryPath}/{fileName}"))
                throw new Exception("file already exists");

            CreatePath(directoryPath);
            return File.Create($"{directoryPath}/{fileName}");
        }

        public static FileStream WriteAll(string path)
        {
            if (File.Exists(path))
                throw new Exception("file already exists");

            string[] followingDirectoriesWithFileName = path.Split('/');
            string directoryPath = string.Empty;
            for (int d = 0; d < followingDirectoriesWithFileName.Length - 1; d++)
            {
                directoryPath += $"{followingDirectoriesWithFileName[d]}/";
            }

            CreatePath(directoryPath);
            return File.Create(path);
        }
    }
}