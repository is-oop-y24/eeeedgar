using System.Collections.Generic;
using System.IO;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class ExternalSingleZipStorage : IExternalZipStorage
    {
        public void Create(List<JobObject> jobObjects, Client client)
        {
            SendFile(MakeTemporaryArchive(jobObjects), client);
        }

        private string AvailableTemporaryDirectoryName()
        {
            string directoryBaseName = "temporary_directory_";
            int additionalNumber = 0;
            while (Directory.Exists(directoryBaseName + additionalNumber))
                additionalNumber++;
            return directoryBaseName + additionalNumber;
        }

        private string MakeTemporaryArchive(List<JobObject> jobObjects)
        {
            string temporaryDirectoryName = AvailableTemporaryDirectoryName();
            Directory.CreateDirectory(temporaryDirectoryName);
            var singleZipStorage = new SingleZipStorage();
            const string temporaryArchiveName = "temporaryArchive";
            string temporaryArchivePath = $"{temporaryDirectoryName}/{temporaryArchiveName}";
            singleZipStorage.Create(jobObjects, temporaryArchivePath);
            return temporaryDirectoryName;
        }

        private void SendFile(string temporaryDirectoryName, Client client)
        {
            client.SendFile($"{temporaryDirectoryName}/temporaryArchive.zip");

            File.Delete($"{temporaryDirectoryName}/temporaryArchive.zip");
            Directory.Delete(temporaryDirectoryName);
        }
    }
}