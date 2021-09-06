namespace Isu.Entities
{
    public class GroupNumber
    {
        private string _value;   // aka "05"

        public GroupNumber(string value)
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