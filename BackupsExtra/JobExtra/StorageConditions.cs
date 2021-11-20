using System;

namespace BackupsExtra.JobExtra
{
    public class StorageConditions
    {
        public StorageConditions()
        {
            HasNumberLimit = false;
            HasDeadline = false;
        }

        public int NumberLimit { get; private set; }
        public bool HasNumberLimit { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool HasDeadline { get; private set; }

        public StorageConditions SetNumberLimit(int number)
        {
            HasNumberLimit = true;
            NumberLimit = number;
            return this;
        }

        public StorageConditions SetDeadline(DateTime deadline)
        {
            HasDeadline = true;
            Deadline = deadline;
            return this;
        }
    }
}