using IsBanken.Buisness.Models;
using System.Collections.Generic;

namespace IsBanken.Buisness.Interfaces
{
    public interface IAccountHandler
    {
        void ImportAccounts(List<string> fileLines);
        List<Account> GetAccounts();
        decimal GetTotalAccountBalances();
        void CreateAccount(int customerId);
        void DeleteAccount(int accountId);
    }
}
