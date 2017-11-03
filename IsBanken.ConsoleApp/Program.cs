using IsBanken.Buisness.Infrastructure;
using System;

namespace IsBanken.ConsoleApp
{
    class Program
    {
        private static Bank _bank;

        private static void Initialize()
        {
            var fileHandler = new FileHandler();
            var customerHandler = new CustomerHandler();
            var accountHandler = new AccountHandler();
            var transactionHandler = new TransactionHandler();

            _bank = new Bank(fileHandler, customerHandler, transactionHandler, accountHandler);

            var fileLines = _bank.ReadFile();

            _bank.ImportCustomers(fileLines);
            _bank.ImportAccounts(fileLines);

        }

        static void Main(string[] args)
        {
            Initialize();            

            Console.WriteLine(_bank.GetCustomers().Count);
            Console.WriteLine(_bank.GetAccounts().Count);
            
            Console.ReadLine();
        }

       
    }
}
