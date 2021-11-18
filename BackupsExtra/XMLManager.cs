using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Backups.Job;

namespace BackupsExtra
{
    public static class XmlManager
    {
        public static void CreateBackupJobsXml(List<BackupJob> backupJobs, string path)
        {
            var xDoc = new XDocument();
            var xBackupJobs = new XElement("backup_jobs");
            foreach (BackupJob job in backupJobs)
            {
                var xBackupJob = new XElement("backup_job");
                var xBackupJobId = new XAttribute("id", job.Id);
                xBackupJob.Add(xBackupJobId);

                var xJobObjects = new XElement("job_objects");
                foreach (JobObject o in job.JobObjects)
                {
                    var xJobObject = new XElement("job_object");
                    var xJobObjectId = new XAttribute("id", o.Id);
                    xJobObject.Add(xJobObjectId);
                    var xJobObjectPath = new XAttribute("path", o.Path);
                    xJobObject.Add(xJobObjectPath);
                    xJobObjects.Add(xJobObject);
                }

                xBackupJob.Add(xJobObjects);
                xBackupJobs.Add(xBackupJob);
            }

            xDoc.Add(xBackupJobs);
            xDoc.Save(path);
        }

        public static List<BackupJob> CreateBackupJobsFromXml(string path)
        {
            var xDoc = new XmlDocument();
            xDoc.Load(path);

            XmlElement xBackupJobs = xDoc.DocumentElement;
            var backupJobs = new List<BackupJob>();

            if (xBackupJobs != null)
            {
                foreach (XmlNode xBackupJob in xBackupJobs.ChildNodes)
                {
                    if (xBackupJob.Attributes != null)
                    {
                        var jobId = Guid.Parse((ReadOnlySpan<char>)xBackupJob.Attributes.GetNamedItem("id")?.Value);
                        Console.WriteLine($"backup job id: {jobId.ToString()}");
                        var job = new BackupJob(null, null, jobId);

                        foreach (XmlNode childnode in xBackupJob.ChildNodes)
                        {
                            if (childnode.Name == "job_objects")
                            {
                                foreach (XmlNode xJobObject in childnode.ChildNodes)
                                {
                                    if (xJobObject.Attributes != null)
                                    {
                                        var objId = Guid.Parse(
                                            (ReadOnlySpan<char>)xJobObject.Attributes.GetNamedItem("id")?.Value);
                                        var objPath = (ReadOnlySpan<char>)xJobObject.Attributes.GetNamedItem("path")?.Value;
                                        Console.WriteLine(
                                            $"job object id: {objId.ToString()}");
                                        Console.WriteLine(
                                            $"job object path: {objPath.ToString()}");
                                        var obj = new JobObject(objPath.ToString(), objId);
                                        job.JobObjects.Add(obj);
                                    }
                                }
                            }
                        }

                        backupJobs.Add(job);
                    }
                }
            }

            return backupJobs;
        }
    }
}