using System.Collections.Generic;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            Number = courseNumber;
            Groups = new List<Group>();
        }

        public int Number { get; }

        public List<Group> Groups { get; }

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