using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuServiceExtra
    {
        private List<MegaFaculty> _megaFaculties;
        private Dictionary<Group, Schedule> _standartGroupSchedule;
        private Dictionary<ExtraDisciplineGroup, Schedule> _extraDGroupSchedule;
        public IsuServiceExtra()
        {
            _megaFaculties = new List<MegaFaculty>();
            _standartGroupSchedule = new Dictionary<Group, Schedule>();
            _extraDGroupSchedule = new Dictionary<ExtraDisciplineGroup, Schedule>();
        }

        public MegaFaculty AddMegaFaculty(string megaFacultyName)
        {
            var megaFaculty = new MegaFaculty(megaFacultyName);
            _megaFaculties.Add(megaFaculty);
            return megaFaculty;
        }

        public void AddPrefixToMegaFaculty(MegaFaculty megaFaculty, char prefix)
        {
            if (_megaFaculties.Any(faculty => faculty.AssociatedPrefixes.Contains(prefix)))
                throw new IsuException("PREFIX IS ALREADY USED");
            megaFaculty.AssociatedPrefixes.Add(prefix);
        }

        public MegaFaculty FindMegaFacultyByName(string megaFacultyName)
        {
            return _megaFaculties.Find(megaFaculty => megaFaculty.Name == megaFacultyName);
        }

        public Group AddGroup(string groupName)
        {
            if (groupName.Length < 1) throw new IsuException("INVALID_GROUP_NAME");
            char associatedPrefix = Convert.ToChar(groupName.Substring(0, 1));
            MegaFaculty megaFaculty = _megaFaculties.Find(mf => mf.AssociatedPrefixes.Contains(associatedPrefix)) ?? throw new IsuException("this group doesn't belong to any mega faculty");

            Group group = megaFaculty.IsuService.AddGroup(groupName);
            if (_standartGroupSchedule.ContainsKey(group)) throw new IsuException("SCHEDULE FOR GROUP IS ALREADY EXISTS LOL");
            _standartGroupSchedule.Add(group, new Schedule());
            return group;
        }

        public ExtraDisciplineGroup AddExtraDisciplineGroup(MegaFaculty megaFaculty, int courseNumber, string groupName)
        {
            ExtraDisciplineGroup edGroup = megaFaculty.ExtraDisciplineService.AddExtraDisciplineGroup(groupName);
            if (_extraDGroupSchedule.ContainsKey(edGroup)) throw new IsuException("SCHEDULE FOR GROUP IS ALREADY EXISTS LOL");
            _extraDGroupSchedule.Add(edGroup, new Schedule());
            return edGroup;
        }

        public void AddStudentToExtraDisciplineGroup(ExtraDisciplineGroup extraDisciplineGroup, Student student)
        {
            if (_standartGroupSchedule[FindGroupByStudent(student)].DoesOverlap(_extraDGroupSchedule[extraDisciplineGroup]))
                throw new IsuException("BASE AND EXTRA DISCIPLINE GROUPS SCHEDULES OVERLAP");
            extraDisciplineGroup.AddStudent(student);
        }

        public Student FindStudent(string name)
        {
            return
                _megaFaculties
                .Select(megaFaculty => megaFaculty.IsuService.FindStudent(name))
                .FirstOrDefault(student => student != null);
        }

        public Group FindGroupByStudent(Student student)
        {
            return student.Group;
        }

        public ExtraDisciplineGroup FindExtraDisciplineGroup(string name)
        {
            return _megaFaculties
                .Select(megaFaculty => megaFaculty.ExtraDisciplineService.FindGroup(name))
                .FirstOrDefault(group => group != null);
        }

        public Group FindGroup(string name)
        {
            return _megaFaculties
                .Select(megaFaculty => megaFaculty.IsuService.FindGroup(name))
                .FirstOrDefault(group => group != null);
        }

        public void AddLesson(Group group, Lesson lesson)
        {
            _standartGroupSchedule[group].PlanLesson(lesson);
        }

        public void AddLesson(ExtraDisciplineGroup group, Lesson lesson)
        {
            _extraDGroupSchedule[group].PlanLesson(lesson);
        }
    }
}