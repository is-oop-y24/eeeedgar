using System.Collections.Generic;

namespace Backups.ClientServer
{
    public class ServerFile
    {
        public ServerFile(string relativeName, List<byte> data)
        {
            RelativeName = relativeName;
            Data = data;
        }

        public string RelativeName { get; }
        public List<byte> Data { get; }
    }
}