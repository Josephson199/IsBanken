using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsBanken.Buisness.Infrastructure
{
    public class TransactionHandler
    {
        public TransactionResult AddTransaction(int toAccountId, int fromAccountId, decimal amount)
        {
            var transactionResult = new TransactionResult();
            
            var toAccount = Bank.Accounts.FirstOrDefault(acc => acc.AccountId == toAccountId);

            if (toAccount == null)
            {
                transactionResult.Success = false;
                transactionResult.ErrorMessage = $"ToAccount did not exist. AccountId: {toAccountId}";
                return transactionResult;
            }              

            var fromAccount = Bank.Accounts.FirstOrDefault(acc => acc.AccountId == fromAccountId);

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

            Bank.Transactions.Add(transaction);

            transactionResult.Success = true;

            return transactionResult;
        }
    }

    public class TransactionResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
