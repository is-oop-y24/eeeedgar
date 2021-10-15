using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private List<Lesson> _lessons;

        public Schedule()
        {
            _lessons = new List<Lesson>();
        }

        public void PlanLesson(Lesson lesson)
        {
            if (!CanAddLesson(lesson)) throw new IsuException("INVALID SCHEDULE");
            _lessons.Add(lesson);
        }

        public bool DoesOverlap(Schedule other)
        {
            return _lessons.Any(lesson => other._lessons.Any(lesson.DoesOverlap));
        }

        private bool CanAddLesson(Lesson lesson)
        {
            return _lessons.All(existingLesson => !lesson.DoesOverlap(existingLesson));
        }
    }
}