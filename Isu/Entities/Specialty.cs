using Isu.Tools;

namespace Isu.Entities
{
    public class Specialty
    {
        public Specialty(string value)
        {
            if (!value.Equals("M3"))
            {
                throw new IsuException("Exception: wrong speciality");
            }

            Value = value;
        }

        public string Value { get; }
    }
}
