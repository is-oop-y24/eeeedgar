using Isu.Tools;

namespace IsuExtra.Entities
{
    public class ExtraDisciplineGroupName
    {
        public ExtraDisciplineGroupName(string name)
        {
            CheckNameValidity(name);
            Value = name;
        }

        public string Value { get; }

        private void CheckNameValidity(string name)
        {
            if (name == string.Empty) throw new IsuException("INVALID EXTRA DISCIPLINE GROUP NAME");
        }
    }
}