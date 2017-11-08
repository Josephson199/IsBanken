using IsBanken.Buisness.Interfaces;
using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IsBanken.Buisness.Infrastructure
{
    public class AccountHandler : IAccountHandler
    {
        private const int AccountDividerCount = 3;

        public void CreateAccount(int customerId)
        {

            var accountWithHighestId = Context.Accounts.OrderBy(x => x.AccountId).LastOrDefault();

            int accountId;

            if (accountWithHighestId == null)
            {
                accountId = 1;
            }
            else
            {
                accountId = accountWithHighestId.AccountId + 1;
            }

            var account = new Account()
            {
                AccountId = accountId,
                CustomerId = customerId,
                Balance = 0
            };


            Context.Accounts.Add(account);
        }

        public bool DeleteAccount(int accountId)
        {
            var account = Context.Accounts.FirstOrDefault(a => a.AccountId.Equals(accountId));

            if (account == null || account.Balance != 0) return false;

            Context.Accounts.Remove(account);
            return true;
        }


        public List<Account> GetAccounts()
        {
            return Context.Accounts;
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            return Context.Accounts.Where(a => a.CustomerId.Equals(customerId)).ToList();
        }

        public decimal GetTotalAccountBalances(int? customerId)
        {
            decimal totalBalance = 0;

            if (customerId.HasValue)
            {
                var customerAccounts = Context.Accounts.Where(a => a.CustomerId.Equals(customerId));
                totalBalance += customerAccounts.Sum(customerAccount => customerAccount.Balance);

                return totalBalance;
            }

            totalBalance = Context.Accounts.Sum(a => a.Balance);

            return totalBalance;
        }

        public void ImportAccounts(List<string> fileLines)
        {
            foreach (var line in fileLines)
            {
                if (line == null)
                    continue;

                var splittedLine = line.Split(';');

                if (splittedLine.Length == AccountDividerCount)
                {
                    var account = new Account
                    {
                        AccountId = Convert.ToInt32(splittedLine[0]),
                        CustomerId = Convert.ToInt32(splittedLine[1]),
                        Balance = Convert.ToDecimal(splittedLine[2], CultureInfo.InvariantCulture)
                    };

                    Context.Accounts.Add(account);
                }
            }           
        }
    }
}
