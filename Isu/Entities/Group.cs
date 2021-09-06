using System.Collections.Generic;

namespace Isu.Entities
{
    public class Group
    {
        private GroupProperties _properties;
        private List<Student> _students;

        public Group(string name)
        {
            _properties = new GroupProperties(name);
            _students = new List<Student>();
        }

        public GroupProperties Properties
        {
            get
            {
                return _properties;
            }
        }

        public List<Student> Students
        {
            get
            {
                return _students;
            }
        }

        public void AddStudent(Student student)
        {
            _students.Add(student); // todo copy-constructor
        }

        public bool RemoveStudent(Student student)
        {
            foreach (Student st in Students)
            {
                if (st.Id == student.Id)
                {
                    _students.Remove(st);
                    return true;
                }
            }

            return false;
        }

        public Student FindStudent(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                    return student;
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in Students)
            {
                if (student.Name == name)
                    return student;
            }

            return null;
        }

        public Student FindStudent(Student student)
        {
            foreach (Student st in Students)
            {
                if (st.Equals(student))
                    return st;
            }

            return null;
        }
    }
}