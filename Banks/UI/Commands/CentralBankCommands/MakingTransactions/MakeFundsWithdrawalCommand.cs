using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.UI.Commands.CentralBankCommands.Selecting;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.CentralBankCommands.MakingTransactions
{
    public class MakeFundsWithdrawalCommand : ICommand
    {
        public Context Execute(Context context)
        {
            Bank accountBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(accountBank.BankAccounts);
            int accountId = (int)Clarifier.AskDecimal("account id");
            IBankAccount account = accountBank.BankAccounts[accountId];

            decimal money = Clarifier.AskDecimal("money transfer value");
            var fundsWithdrawal = new FundsWithdrawal(account, money);
            context.CentralBank.MakeTransaction(fundsWithdrawal);

            return context;
        }
    }
}