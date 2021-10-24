using System;
using System.IO;
using System.Linq;

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

        public static string CreateUniqueFile(string path, string fileBaseName)
        {
            string fileNameWithoutExtension = fileBaseName.Split('.')[0];
            string extension = fileBaseName.Split('.')[1];
            CreatePath(path);
            string filePath = Path.Combine(path, $"{fileNameWithoutExtension}.{extension}");
            if (!File.Exists(filePath))
            {
                CreateEmptyFile(filePath);
                return filePath;
            }

            int counter = 2;
            filePath = Path.Combine(path, $"{fileNameWithoutExtension} ({counter}).{extension}");
            while (File.Exists(filePath))
            {
                counter++;
                filePath = Path.Combine(path, $"{fileNameWithoutExtension} ({counter}).{extension}");
            }

            CreateEmptyFile(filePath);
            return filePath;
        }

        public static string GetFreeFileName(string path, string fileBaseName)
        {
            string fileNameWithoutExtension = fileBaseName.Split('.')[0];
            string extension = fileBaseName.Split('.')[1];
            string filePath = Path.Combine(path, $"{fileNameWithoutExtension}.{extension}");
            if (!File.Exists(filePath))
            {
                return filePath;
            }

            int counter = 2;
            filePath = Path.Combine(path, $"{fileNameWithoutExtension}({counter}).{extension}");
            while (File.Exists(filePath))
            {
                counter++;
                filePath = Path.Combine(path, $"{fileNameWithoutExtension}({counter}).{extension}");
            }

            return filePath;
        }

        public static string CreateUniqueDirectory(string path, string directoryBaseName)
        {
            CreatePath(path);
            string directoryPath = Path.Combine(path, $"{directoryBaseName}");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return directoryPath;
            }

            int counter = 2;
            directoryPath = Path.Combine(path, $"{directoryBaseName} ({counter})");
            while (Directory.Exists(directoryPath))
            {
                counter++;
                directoryPath = Path.Combine(path, $"{directoryBaseName} ({counter})");
            }

            Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !char.IsWhiteSpace(c))
                .ToArray());
        }
    }
}