using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsBanken.Buisness.Infrastructure
{
    public class AccountHandler
    {
        private const int AccountDividerCount = 3;

        public void CreateAccounts(List<string> fileLines)
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
                        Balance = Convert.ToDecimal(splittedLine[2])
                    };

                    Bank.Accounts.Add(account);
                }
            }
        }
    }
}
