using IsBanken.Buisness.Infrastructure;
using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;

namespace IsBanken.ConsoleApp
{
    class Program
    {
        private static void Initialize()
        {
            var fileHandler = new FileHandler();
            var fileLines = fileHandler.ReadFile();

            var customerHandler = new CustomerHandler();
            customerHandler.CreateCustomers(fileLines);

            var accountHandler = new AccountHandler();
            accountHandler.CreateAccounts(fileLines);

        }
        static void Main(string[] args)
        {

            Initialize();

            Console.WriteLine(Bank.Customers.Count);
            Console.WriteLine(Bank.Accounts.Count);
            Console.ReadLine();
        }

       
    }
}
