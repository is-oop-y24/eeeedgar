﻿using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Services;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        private const uint LastCourseNumber = 4;
        public MegaFaculty(string megaFacultyName)
        {
            Name = megaFacultyName;
            IsuService = new IsuService();
            ExtraDisciplineService = new ExtraDisciplineService(4);
            AssociatedPrefixes = new List<char>();
        }

        public IsuService IsuService { get; }
        public ExtraDisciplineService ExtraDisciplineService { get; }
        public string Name { get; }

        public List<char> AssociatedPrefixes { get; }

        public ExtraDisciplineGroup AddExtraDisciplineGroup(string extraDisciplineGroupName)
        {
            var extraDisciplineGroup = new ExtraDisciplineGroup(extraDisciplineGroupName);
            ExtraDisciplineService.AddExtraDisciplineGroup(extraDisciplineGroupName);
            return extraDisciplineGroup;
        }

        public void AddStudentToExtraDisciplineGroup(Student student, ExtraDisciplineGroup extraDisciplineGroup)
        {
            extraDisciplineGroup.AddStudent(student);
        }
    }
}