using System.Collections.Generic;
using System.IO;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class ExternalSplitZipStorage : IExternalZipStorage
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
            string temporaryDirectoryName = AvailableTemporaryDirectoryName();
            Directory.CreateDirectory(temporaryDirectoryName);
            var splitZipStorage = new SplitZipStorage();
            return splitZipStorage.Create(jobObjects, temporaryDirectoryName);
        }

        private void SendFiles(List<string> fileNames, Client client)
        {
            foreach (string fileName in fileNames)
            {
                // client.SendFile(fileName);
            }
        }
    }
}