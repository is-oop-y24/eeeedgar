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

        public bool RemoveStudent(Student student)
        {
            return Students.Remove(student);
        }

        public Student FindStudent(int id)
        {
            return Students.Find(student => student.Id == id);
        }
    }
}