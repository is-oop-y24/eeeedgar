using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.ClearingRestorePoints
{
    public class AlgorithmByDate
    {
        private readonly List<RestorePoint> _restorePoints;
        public AlgorithmByDate(List<RestorePoint> restorePoints, DateTime deadline)
        {
            _restorePoints = restorePoints;
            Deadline = deadline;
        }

        public DateTime Deadline { get; }

        public List<RestorePoint> SelectOutdatedRestorePoints()
        {
            var outdatedRestorePoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in _restorePoints)
            {
                if (restorePoint.DateTime < Deadline)
                    outdatedRestorePoints.Add(restorePoint);
            }

            return outdatedRestorePoints;
        }
    }
}