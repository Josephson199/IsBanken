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
            throw new NotImplementedException();
        }

        public void DeleteAccount(int accountId)
        {
            //ta bara bort konto om det inte finns några pengar
            throw new NotImplementedException();
        }

        public List<Account> GetAccounts()
        {
            return Context.Accounts;
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            return Context.Accounts.Where(a => a.CustomerId.Equals(customerId)).ToList();
        }

        public decimal GetTotalAccountBalances()
        {
            throw new NotImplementedException();
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
