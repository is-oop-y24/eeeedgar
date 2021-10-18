using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class ExternalSingleZipStorage : IExternalZipStorage
    {
        public void Create(List<JobObject> jobObjects, Server server)
        {
            string temporaryArchivePath = MakeTemporaryArchive(jobObjects);
            SendFile(jobObjects, server);
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
            return temporaryArchivePath;
        }

        private void SendFile(List<JobObject> jobObjects, Server server)
        {
            var client = new Client(server);
            string temporaryArchiveName = MakeTemporaryArchive(jobObjects);
            client.SendFile(temporaryArchiveName);
            File.Delete(temporaryArchiveName);
        }
    }
}