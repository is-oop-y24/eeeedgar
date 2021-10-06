using Isu.Entities;

namespace IsuExtra.Entities
{
    public class ExtraDisciplineGroup : Group // потоки
    {
        public ExtraDisciplineGroup(string name)
            : base(name)
        {
            Name = new ExtraDisciplineGroupName(name);
        }

        public new ExtraDisciplineGroupName Name { get; }
    }
}