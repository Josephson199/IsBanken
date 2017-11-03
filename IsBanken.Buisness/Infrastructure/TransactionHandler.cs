using IsBanken.Buisness.Interfaces;
using IsBanken.Buisness.Models;
using System.Linq;

namespace IsBanken.Buisness.Infrastructure
{
    public class TransactionHandler : ITransactionHandler
    {
        public TransactionResult AddTransaction(int toAccountId, int fromAccountId, decimal amount)
        {
            var transactionResult = new TransactionResult();
            
            var toAccount = Context.Accounts.FirstOrDefault(acc => acc.AccountId == toAccountId);

            if (toAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"ToAccount did not exist. AccountId: {toAccountId}";
                return transactionResult;
            }              

            var fromAccount = Context.Accounts.FirstOrDefault(acc => acc.AccountId == fromAccountId);

            if (fromAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"FromAccount did not exist. AccountId: {fromAccountId}";
                return transactionResult;
            }               

            if (fromAccount.Balance < amount)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"FromAccount balance is beneath the limit. AccountId: {fromAccountId}";
                return transactionResult;
            }               

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                FromAccount = fromAccount,
                ToAccount = toAccount
            };

            Context.Transactions.Add(transaction);

            transactionResult.Success = true;

            return transactionResult;
        }

        public TransactionResult Deposit(int accoundId, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public TransactionResult Withdraw(int accountId, decimal amount)
        {
            //kan inte ta ut om amount är större än saldot.
            throw new System.NotImplementedException();
        }
    }

    public class TransactionResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
