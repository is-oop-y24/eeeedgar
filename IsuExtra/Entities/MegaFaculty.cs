using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(string megaFacultyName)
        {
            Name = megaFacultyName;
            IsuService = new IsuService();
            AssociatedPrefixes = new List<char>();
            ExtraDisciplineGroups = new List<ExtraDisciplineGroup>();
        }

        public IsuService IsuService { get; }
        public string Name { get; }

        public ExtraDiscipline ExtraDiscipline { get; private set; }

        public List<ExtraDisciplineGroup> ExtraDisciplineGroups { get; }

        public List<char> AssociatedPrefixes { get; }

        public void AddAssociatedPrefix(char prefix)
        {
            if (!AssociatedPrefixes.Contains(prefix))
                AssociatedPrefixes.Add(prefix);
        }

        public bool IsAssociatesWithPrefix(char prefix)
        {
            return AssociatedPrefixes.Contains(prefix);
        }

        public void SetExtraDiscipline(string disciplineName)
        {
            ExtraDiscipline = new ExtraDiscipline(disciplineName);
        }

        public ExtraDisciplineGroup SetExtraDisciplineGroup(string extraDisciplineGroupName)
        {
            var extraDisciplineGroup = new ExtraDisciplineGroup(extraDisciplineGroupName);
            ExtraDisciplineGroups.Add(extraDisciplineGroup);
            return extraDisciplineGroup;
        }

        public void AddStudentToExtraDisciplineGroup(Student student, ExtraDisciplineGroup extraDisciplineGroup)
        {
            extraDisciplineGroup.AddStudent(student);
        }
    }
}