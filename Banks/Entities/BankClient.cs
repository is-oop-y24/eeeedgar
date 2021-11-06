using Banks.Tools;

namespace Banks.Entities
{
    public class BankClient
    {
        private string _address;
        private string _passportData;
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Address
        {
            get => _address;
            set
            {
                if (_address != null)
                    throw new BanksException("illegal attempt to change client's address");
                _address = value;
            }
        }

        public string PassportData
        {
            get => _passportData;
            set
            {
                if (_passportData != null)
                    throw new BanksException("illegal attempt to change client's passport data");
                _passportData = value;
            }
        }
    }
}