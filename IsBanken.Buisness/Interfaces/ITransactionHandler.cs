using IsBanken.Buisness.Infrastructure;

namespace IsBanken.Buisness.Interfaces
{
    public interface ITransactionHandler
    {
        TransactionResult AddTransaction(int toAccountId, int fromAccountId, decimal amount);
        TransactionResult Withdraw(int accountId, decimal amount);
        TransactionResult Deposit(int accoundId, decimal amount);

    }
}
