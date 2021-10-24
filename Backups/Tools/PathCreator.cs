using System.IO;

namespace Backups.Tools
{
    public class PathCreator
    {
        public static string GetFreeFileName(string path, string fileBaseName)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileBaseName);
            string extension = Path.GetExtension(fileBaseName);
            string filePath = Path.Combine(path, $"{fileNameWithoutExtension}{extension}");
            if (!File.Exists(filePath))
            {
                return filePath;
            }

            int counter = 2;
            filePath = Path.Combine(path, $"{fileNameWithoutExtension}({counter}){extension}");
            while (File.Exists(filePath))
            {
                counter++;
                filePath = Path.Combine(path, $"{fileNameWithoutExtension}({counter}){extension}");
            }

            return filePath;
        }
    }
}