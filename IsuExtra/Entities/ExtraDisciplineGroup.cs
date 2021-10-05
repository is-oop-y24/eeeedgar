using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class ExtraDisciplineGroup // потоки
    {
        public ExtraDisciplineGroup(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        public string Name { get; }

        public List<Student> Students { get; }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }
    }
}