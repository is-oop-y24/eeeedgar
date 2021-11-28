using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.ClearingRestorePoints
{
    public class OverTheNumberLimitRestorePointsSelection : IExceededRestorePointsSelection
    {
        private readonly List<RestorePoint> _restorePoints;
        public OverTheNumberLimitRestorePointsSelection(List<RestorePoint> restorePoints, int limit)
        {
            _restorePoints = restorePoints;
            Limit = limit;
        }

        public int Limit { get; }

        public List<RestorePoint> Execute()
        {
            var points = new List<RestorePoint>(_restorePoints);
            points.Sort((p1, p2) => p1.DateTime.CompareTo(p2.DateTime));
            int exceededNumber = _restorePoints.Count - Limit;
            if (exceededNumber < 0)
                exceededNumber = 0;
            return points.GetRange(0, exceededNumber);
        }
    }
}