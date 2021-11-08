using Banks.Model.Accounts;
using Banks.Model.Entities;
using Banks.Model.Transactions;
using Banks.UI.Commands.CentralBankCommands.Selecting;
using Banks.UI.EntitiesUI;
using Banks.UI.Tools;

namespace Banks.UI.Commands.CentralBankCommands.MakingTransactions
{
    public class MakeMoneyTransferCommand : ICommand
    {
        public Context Execute(Context context)
        {
            Bank senderBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(senderBank.BankAccounts);
            int senderAccountId = (int)Clarifier.AskDecimal("sender account id");
            IBankAccount senderAccount = senderBank.BankAccounts[senderAccountId];

            Bank receiverBank = new SelectBankCommand().Execute(context).Bank;
            BankUi.DisplayAccounts(receiverBank.BankAccounts);
            int receiverAccountId = (int)Clarifier.AskDecimal("receiver account id");
            IBankAccount receiverAccount = receiverBank.BankAccounts[receiverAccountId];

            decimal money = Clarifier.AskDecimal("money transfer value");
            var moneyTransfer = new MoneyTransfer(senderAccount, receiverAccount, money);
            context.CentralBank.MakeTransaction(moneyTransfer);

            return context;
        }
    }
}