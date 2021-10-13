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
        private Dictionary<Group, Schedule> _regularGroupSchedule;
        private Dictionary<ExtraDisciplineGroup, Schedule> _extraDGroupSchedule;

        public IsuServiceExtra()
        {
            _megaFaculties = new List<MegaFaculty>();
            _regularGroupSchedule = new Dictionary<Group, Schedule>();
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
            char associatedPrefix = GroupName.Prefix(groupName);
            MegaFaculty megaFaculty = _megaFaculties.Find(mf => mf.AssociatedPrefixes.Contains(associatedPrefix)) ??
                                      throw new IsuException("this group doesn't belong to any mega faculty");

            Group group = megaFaculty.IsuService.AddGroup(groupName);
            if (_regularGroupSchedule.ContainsKey(group))
                throw new IsuException("GROUP WITH THAT NAME ALREADY EXISTS");
            _regularGroupSchedule.Add(group, new Schedule());
            return group;
        }

        public ExtraDisciplineGroup AddExtraDisciplineGroup(MegaFaculty megaFaculty, string groupName)
        {
            ExtraDisciplineGroup edGroup = megaFaculty.ExtraDisciplineService.AddExtraDisciplineGroup(groupName);
            if (_extraDGroupSchedule.ContainsKey(edGroup))
                throw new IsuException("EXTRA DISCIPLINE GROUP WITH THAT NAME ALREADY EXISTS");
            _extraDGroupSchedule.Add(edGroup, new Schedule());
            return edGroup;
        }

        public void AddStudentToExtraDisciplineGroup(ExtraDisciplineGroup extraDisciplineGroup, Student student)
        {
            if (FindExtraDisciplineGroupByStudent(student) != null)
                throw new IsuException("STUDENT ALREADY HAS EXTRA DISCIPLINE GROUP");
            if (_regularGroupSchedule[FindGroupByStudent(student)]
                .DoesOverlap(_extraDGroupSchedule[extraDisciplineGroup]))
                throw new IsuException("BASE AND EXTRA DISCIPLINE GROUPS SCHEDULES OVERLAP");
            if (GetMegaFacultyByGroup(student.Group) == GetMegaFacultyByExtraDisciplineGroup(extraDisciplineGroup))
                throw new IsuException("ATTEMPT TO ADD STUDENT TO EXTRA DISCIPLINE BELONGS TO HIS MEGA FACULTY");
            extraDisciplineGroup.AddStudent(student);
        }

        public Student FindStudent(string name)
        {
            return _megaFaculties.Select(megaFaculty => megaFaculty.IsuService.FindStudent(name))
                .FirstOrDefault(student => student != null);
        }

        public Group FindGroupByStudent(Student student)
        {
            return student.Group;
        }

        public ExtraDisciplineGroup FindExtraDisciplineGroupByStudent(Student student)
        {
            return _megaFaculties.SelectMany(megaFaculty => megaFaculty.ExtraDisciplineService.Groups)
                .FirstOrDefault(edGroup => edGroup.Students.Contains(student));
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
            _regularGroupSchedule[group].PlanLesson(lesson);
        }

        public void AddLesson(ExtraDisciplineGroup group, Lesson lesson)
        {
            _extraDGroupSchedule[group].PlanLesson(lesson);
        }

        public void RejectOfExtraDiscipline(Student student)
        {
            ExtraDisciplineGroup edGroup = FindExtraDisciplineGroupByStudent(student);
            if (edGroup == null)
                throw new IsuException("STUDENT IS NOT SUBSCRIBED TO ANY EXTRA DISCIPLINE");
            edGroup.Students.Remove(student);
        }

        private MegaFaculty GetMegaFacultyByExtraDisciplineGroup(ExtraDisciplineGroup edGroup)
        {
            return _megaFaculties.FirstOrDefault(faculty => faculty.ExtraDisciplineService.Groups.Contains(edGroup)) ??
                   throw new IsuException("EXTRA DISCIPLINE DOES NOT BELONG TO ANY MEGA FACULTY");
        }

        private MegaFaculty GetMegaFacultyByGroup(Group group)
        {
            foreach (MegaFaculty megaFaculty in from megaFaculty in _megaFaculties let gr = megaFaculty.IsuService.FindGroup(@group.Name.Value) where gr != null select megaFaculty)
            {
                return megaFaculty;
            }

            throw new IsuException("EXTRA DISCIPLINE DOES NOT BELONG TO ANY MEGA FACULTY");
        }
    }
}