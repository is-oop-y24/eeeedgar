using System.Collections.Generic;

namespace Isu.Entities
{
    public class CourseNumber
    {
        private CourseNumber(int courseNumber)
        {
            Number = courseNumber;
            Groups = new List<Group>();
        }

        public int Number { get; }

        public List<Group> Groups { get; }

        public static CourseNumber CreateInstance(int courseNumber)
        {
            return new CourseNumber(courseNumber);
        }

        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }

        public void RemoveGroup(Group group)
        {
            Groups.Remove(group);
        }
    }
}