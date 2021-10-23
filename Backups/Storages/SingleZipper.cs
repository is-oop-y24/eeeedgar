using System;
using System.Collections.Generic;
using System.IO;
using Backups.Job;
using Backups.Useful;
using Ionic.Zip;

namespace Backups.Storages
{
    public class SingleZipper // : IZipper
    {
        public SingleZipper()
        {
        }

        public string ArchiveName => "archive.zip";
    }
}