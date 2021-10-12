using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        public Schedule()
        {
            Lessons = new List<Lesson>();
        }

        private List<Lesson> Lessons { get; }

        public void PlanLesson(Lesson lesson)
        {
            if (!CanAddLesson(lesson)) throw new IsuException("INVALID SCHEDULE");
            Lessons.Add(lesson);
        }

        public bool DoesOverlap(Schedule other)
        {
            return Lessons.Any(lesson => other.Lessons.Any(lesson.DoesOverlap));
        }

        private bool CanAddLesson(Lesson lesson)
        {
            return Lessons.All(existingLesson => !lesson.DoesOverlap(existingLesson));
        }
    }
}