using System;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(bool isSplit)
        {
            DateTime = DateTime.Now;
            if (isSplit)
                ZipStorage = new SplitZipStorage();
            else
                ZipStorage = new SingleZipStorage();
        }

        public IZipStorage ZipStorage { get; }
        public DateTime DateTime { get; }
    }
}