using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.ClearingRestorePoints
{
    public class OutdatedRestorePointsSelection : IExceededRestorePointsSelection
    {
        // private readonly List<RestorePoint> _restorePoints;
        public OutdatedRestorePointsSelection(DateTime deadline)
        {
            // _restorePoints = restorePoints;
            Deadline = deadline;
        }

        public DateTime Deadline { get; }

        public List<RestorePoint> Execute(List<RestorePoint> restorePoints)
        {
            var outdatedRestorePoints = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (restorePoint.DateTime < Deadline)
                    outdatedRestorePoints.Add(restorePoint);
            }

            return outdatedRestorePoints;
        }
    }
}