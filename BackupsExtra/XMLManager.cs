using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Backups.Job;
using BackupsExtra.JobExtra;

namespace BackupsExtra
{
    public static class XmlManager
    {
        public static void CreateBackupJobsXml(List<BackupJobExtra> backupJobsExtra, string path = @"D:\\OOP\\lab-5\\backups_extra.xml")
        {
            var xDoc = new XDocument();
            var xBackupJobsExtra = new XElement("backup_jobs_extra");
            foreach (BackupJobExtra jobExtra in backupJobsExtra)
            {
                var xBackupJobExtra = new XElement("backup_job_extra");
                var xBackupJobExtraId = new XAttribute("id", jobExtra.Id);
                var xBackupJobExtraMergingType = new XAttribute("merging_type", jobExtra.Merging.GetType());
                xBackupJobExtra.Add(xBackupJobExtraId);
                xBackupJobExtra.Add(xBackupJobExtraMergingType);

                var xStorageConditions = new XElement("storage_conditions");

                var xDeadline = new XElement("deadline");
                var xHasDeadline = new XAttribute("has_deadline", jobExtra.StorageConditions.HasDeadline);
                var xDeadlineDate = new XAttribute("date", jobExtra.StorageConditions.Deadline);
                xDeadline.Add(xHasDeadline);
                xDeadline.Add(xDeadlineDate);

                var xLimit = new XElement("limit");
                var xHasLimit = new XAttribute("has_limit", jobExtra.StorageConditions.HasNumberLimit);
                var xLimitNumber = new XAttribute("number", jobExtra.StorageConditions.NumberLimit);
                xLimit.Add(xHasLimit);
                xLimit.Add(xLimitNumber);

                xStorageConditions.Add(xDeadline);
                xStorageConditions.Add(xLimit);

                xBackupJobExtra.Add(xStorageConditions);

                var xJob = new XElement("backup_job");
                var xJobObjects = new XElement("job_objects");
                foreach (JobObject jobObject in jobExtra.Job.JobObjects)
                {
                    var xJobObject = new XElement("job_object");
                    var xJobObjectId = new XAttribute("id", jobObject.Id);
                    var xJobObjectPath = new XAttribute("path", jobObject.Path);

                    xJobObject.Add(xJobObjectId);
                    xJobObject.Add(xJobObjectPath);

                    xJobObjects.Add(xJobObject);
                }

                xJob.Add(xJobObjects);
                xBackupJobExtra.Add(xJob);

                xBackupJobsExtra.Add(xBackupJobExtra);
            }

            xDoc.Add(xBackupJobsExtra);
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