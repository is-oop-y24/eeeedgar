using System.Collections.Generic;
using System.IO;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class ExternalSingleZipStorage : IExternalZipStorage
    {
        public void Create(List<JobObject> jobObjects, Client client)
        {
            SendFiles(MakeTemporaryArchive(jobObjects), client);
        }

        private string AvailableTemporaryDirectoryName()
        {
            string directoryBaseName = "temporary_directory_";
            int additionalNumber = 0;
            while (Directory.Exists(directoryBaseName + additionalNumber))
                additionalNumber++;
            return directoryBaseName + additionalNumber;
        }

        private List<string> MakeTemporaryArchive(List<JobObject> jobObjects)
        {
            var singleZipStorage = new SingleZipStorage();
            const string temporaryDirectoryPath = "D:/OOP/lab-3/temporaryBackups";
            if (!Directory.Exists(temporaryDirectoryPath))
                Directory.CreateDirectory(temporaryDirectoryPath);

            string temporaryDirectoryName = AvailableTemporaryDirectoryName();
            if (!Directory.Exists($"{temporaryDirectoryPath}/{temporaryDirectoryName}"))
                Directory.CreateDirectory(AvailableTemporaryDirectoryName());

            string temporaryArchivePath = $"{temporaryDirectoryPath}/{temporaryDirectoryName}";
            List<string> paths = singleZipStorage.Create(jobObjects, temporaryArchivePath);
            return paths;
        }

        private void SendFiles(List<string> filePaths, Client client)
        {
            foreach (string filePath in filePaths)
            {
                // client.SendFile(filePath);
            }
        }
    }
}