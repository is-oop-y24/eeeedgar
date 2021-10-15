using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private const int LengthInMinutes = 90;
        public Lesson(DateTime beginTime, uint classroomNumber)
        {
            BeginTime = beginTime;
            ClassroomNumber = classroomNumber;
        }

        public DateTime BeginTime { get; }
        public uint ClassroomNumber { get; }
        public DateTime EndTime => BeginTime + TimeSpan.FromMinutes(LengthInMinutes);

        public bool DoesOverlap(Lesson other)
        {
            return (other.BeginTime > BeginTime && other.BeginTime < EndTime) ||
                   (BeginTime > other.BeginTime && BeginTime < other.EndTime);
        }
    }
}