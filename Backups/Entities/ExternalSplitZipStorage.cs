using System.Collections.Generic;
using System.IO;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class ExternalSplitZipStorage : IExternalZipStorage
    {
        public void Create(List<JobObject> jobObjects, Server server, Client client)
        {
            throw new System.NotImplementedException();
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
            var splitZipStorage = new SplitZipStorage();
            splitZipStorage.Create(jobObjects, temporaryDirectoryName);
            return temporaryDirectoryName;
        }

        private void SendFiles(string temporaryDirectoryName, Server server, Client client)
        {
            // client.SendFile();
        }
    }
}