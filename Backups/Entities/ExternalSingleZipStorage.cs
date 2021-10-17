using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Ionic.Zip;

namespace Backups.Entities
{
    public class ExternalSingleZipStorage : IExternalZipStorage
    {
        public void Create(List<JobObject> jobObjects, TcpListener server)
        {
            string temporaryArchivePath = MakeTemporaryArchive(jobObjects);
            SendFile(temporaryArchivePath, server);
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

        private void SendFile(string fileName, TcpListener server)
        {
            const int bufferSize = 512;
            TcpClient client = server.AcceptTcpClient();
            NetworkStream netStream = client.GetStream();
            long fileSize = new FileInfo(fileName).Length;
            long totalRead = 0;
            using var fs = new FileStream(fileName, FileMode.Open);
            while (totalRead < fileSize)
            {
                byte[] buffer = new byte[fileSize - totalRead < bufferSize ? fileSize - totalRead : bufferSize];
                totalRead += fs.Read(buffer, 0, buffer.Length);
                netStream.Read(buffer, 0, bufferSize);
            }
        }
    }
}