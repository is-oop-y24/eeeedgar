using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class ExtraDisciplineGroup
    {
        public ExtraDisciplineGroup(string name)
        {
            Students = new List<Student>();
            Name = new ExtraDisciplineGroupName(name);
        }

        public ExtraDisciplineGroupName Name { get; }
        public List<Student> Students { get; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }
    }
}