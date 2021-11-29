using System;
using System.Collections.Generic;
using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public interface IListMerging
    {
        RestorePoint Execute(List<RestorePoint> restorePoints, DateTime time);
        string Log();
    }
}