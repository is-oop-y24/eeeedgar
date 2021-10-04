using System;
using System.Collections.Generic;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class IsuServiceExtra
    {
        private List<MegaFaculty> _megaFaculties;
        public IsuServiceExtra()
        {
            _megaFaculties = new List<MegaFaculty>();
        }

        public MegaFaculty AddMegaFaculty(string megaFacultyName)
        {
            var megaFaculty = new MegaFaculty(megaFacultyName);
            _megaFaculties.Add(megaFaculty);
            return megaFaculty;
        }

        public MegaFaculty FindMegaFacultyByName(string megaFacultyName)
        {
            return _megaFaculties.Find(megaFaculty => megaFaculty.Name == megaFacultyName);
        }

        public void AddGroup(string groupName)
        {
            if (groupName.Length < 1) throw new IsuException("INVALID_GROUP_NAME");
            char associatedPrefix = Convert.ToChar(groupName.Substring(0, 1));
            MegaFaculty megaFaculty = _megaFaculties.Find(mf => mf.IsAssociatesWithPrefix(associatedPrefix)) ?? throw new IsuException("this group doesn't belong to any mega faculty");
            megaFaculty.IsuService.AddGroup(groupName);
        }
    }
}