using Backups.Repo;

namespace BackupsExtra.MergingRestorePoints
{
    public interface IPairMerging
    {
        RestorePoint RestorePoint1 { get; }
        RestorePoint RestorePoint2 { get; }
        RestorePoint Execute();
    }
}