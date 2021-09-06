namespace Isu.Entities
{
    public class Specialty
    {
        private string _value;  // aka "M31"

        public Specialty(string value)
        {
            _value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }
    }
}