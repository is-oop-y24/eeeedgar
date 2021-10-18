using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Backups.ClientServer;

namespace Backups.Entities
{
    public class BackupJob
    {
        private Server _server;
        private Client _client;
        public BackupJob(string backupPath, bool makeZipStorageSplit, Server server = null, Client client = null)
        {
            BackupPath = backupPath;
            if (server != null)
            {
                _server = server;
                _client = client;
                if (makeZipStorageSplit)
                    throw new NotImplementedException();
                else
                    ExternalZipStorage = new ExternalSingleZipStorage();
            }
            else
            {
                if (!Directory.Exists(BackupPath))
                    Directory.CreateDirectory(backupPath);
                if (makeZipStorageSplit)
                    LocalZipStorage = new SplitZipStorage();
                else
                    LocalZipStorage = new SingleZipStorage();
            }

            JobObjects = new List<JobObject>();
        }

        public List<JobObject> JobObjects { get; }
        public string BackupPath { get; }
        public IZipStorage LocalZipStorage { get; }
        public IExternalZipStorage ExternalZipStorage { get; }

        public void AddJobObject(string filePath)
        {
            var jobObject = new JobObject(filePath);
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(string filePath)
        {
            JobObjects.Remove(JobObjects.Find(o => o.FilePath.Equals(filePath)));
        }

        public void MakeBackup()
        {
            string backupPath = $"{BackupPath}/{ArchiveName()}";
            LocalZipStorage.Create(JobObjects, backupPath);
        }

        public void MakeExternalBackup()
        {
            ExternalZipStorage.Create(JobObjects, _server, _client);
        }

        private string ArchiveName()
        {
            DateTime dateTime = DateTime.Now;
            return
                $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}-{dateTime.Hour}-{dateTime.Minute}-{dateTime.Second}";
        }
    }
}