using System.Collections.Generic;

namespace Isu.Entities
{
    public class Group
    {
        public Group(string name)
        {
            GroupName = new GroupName(name);
            Students = new List<Student>();
        }

        public GroupName GroupName { get; }
        public List<Student> Students { get; }

        public string Name()
        {
            return GroupName.Name;
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }
}