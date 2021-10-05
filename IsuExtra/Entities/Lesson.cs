using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        private const int LengthInMinutes = 90;
        public Lesson(DateTime beginTime)
        {
            BeginTime = beginTime;
        }

        public DateTime BeginTime { get; }
        public DateTime EndTime => BeginTime + TimeSpan.FromMinutes(LengthInMinutes);
    }
}