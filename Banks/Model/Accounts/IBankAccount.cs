namespace Banks.Model.Accounts
{
    public interface IBankAccount
    {
        decimal Balance();

        void SendMoney(decimal money);

        void ScheduleRenew(decimal t);

        void ReceiveMoney(decimal money);
        string StringType();
    }
}