using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public interface IPairMerging
    {
        RestorePoint Execute();
        RestorePoint RestorePoint1();
        RestorePoint RestorePoint2();
    }
}