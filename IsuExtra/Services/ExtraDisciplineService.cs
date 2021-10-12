using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class ExtraDisciplineService
    {
        public ExtraDisciplineService()
        {
            Groups = new List<ExtraDisciplineGroup>();
        }

        public List<ExtraDisciplineGroup> Groups { get; }

        public ExtraDisciplineGroup AddExtraDisciplineGroup(string name)
        {
            var extraDisciplineGroup = new ExtraDisciplineGroup(name);
            Groups.Add(extraDisciplineGroup);
            return extraDisciplineGroup;
        }

        public ExtraDisciplineGroup FindGroup(string name)
        {
            return Groups.Find(group => group.Name.Value.Equals(name));
        }
    }
}