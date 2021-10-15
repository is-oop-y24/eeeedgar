using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        private const int LowestId = 100000;
        private static int _id;
        private Student(int id, string name, Group group)
        {
            Id = id;
            if (string.IsNullOrWhiteSpace(name))
                throw new IsuException("INVALID_STUDENT_NAME");
            Name = name;
            Group = group;
        }

        public int Id { get; }
        public string Name { get; }

        public Group Group { get; internal set; }

        public static Student CreateInstance(string name, Group group)
        {
            return new Student(LowestId + _id++, name, group);
        }
    }
}