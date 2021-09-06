namespace Isu.Entities
{
    public class CourseNumber
    {
        private string _value;    // aka "1"

        public CourseNumber(string value)
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