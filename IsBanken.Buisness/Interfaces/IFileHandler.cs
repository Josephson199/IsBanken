using IsBanken.Buisness.Models;
using System.Collections.Generic;

namespace IsBanken.Buisness.Interfaces
{
    public interface IFileHandler
    {
        List<string> ReadFile();
        void SaveFile(List<Account> accounts, List<Customer> customers);
    }
}
