namespace Isu.Entities
{
    public class GroupProperties
    {
        private Specialty _specialty;       // M3_1_05
        private CourseNumber _courseNumber;
        private GroupNumber _groupNumber;

        public GroupProperties(string name)
        {
            _specialty = new Specialty(name[0].ToString() + name[1].ToString());
            _courseNumber = new CourseNumber(name[2].ToString());
            _groupNumber = new GroupNumber(name[3].ToString() + name[4].ToString());
        }

        public Specialty Specialty
        {
            get
            {
                return _specialty;
            }
        }

        public CourseNumber CourseNumber
        {
            get
            {
                return _courseNumber;
            }
        }

        public string Name
        {
            get
            {
                return _specialty.Value + _courseNumber.Value + _groupNumber.Value;
            }
        }

        public GroupNumber GroupNumber
        {
            get
            {
                return _groupNumber;
            }
        }
    }
}