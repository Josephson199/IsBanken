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
                transactionResult.ErrorMessage = $"Till konto existerade ej. Kontonummer: {toAccountId}";
                return transactionResult;
            }              

            var fromAccount = Context.Accounts.FirstOrDefault(acc => acc.AccountId == fromAccountId);

            if (fromAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"Från konto existerade ej. Kontonummer: {fromAccountId}";
                return transactionResult;
            }               

            if (fromAccount.Balance < amount)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"Summan överstrider saldot. Kontonummer: {fromAccountId}, nuvarande saldo: {fromAccount.Balance}";
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
            var transactionResult = new TransactionResult();

            var toAccount = Context.Accounts.FirstOrDefault(acc => acc.AccountId == accoundId);

            if (toAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"Till konto existerade inte. Kontonummer: {accoundId}";
                return transactionResult;
            }

            toAccount.Balance += amount;
            transactionResult.Success = true;

            return transactionResult;
        }

        public TransactionResult Withdraw(int accountId, decimal amount)
        {
            var transactionResult = new TransactionResult();

            var toAccount = Context.Accounts.FirstOrDefault(acc => acc.AccountId == accountId);

            if (toAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"Från konto existerade inte. Kontonummer: {accountId}";
                return transactionResult;
            }

            if(toAccount.Balance < amount)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"Överstiger saldo. Kontonummer: {accountId}, Nuvarande saldo: {toAccount.Balance}";
                return transactionResult;
            }


            toAccount.Balance -= amount;
            transactionResult.Success = true;

            return transactionResult;
        }

        }
    }

    public class TransactionResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

