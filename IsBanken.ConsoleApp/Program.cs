using IsBanken.Buisness.Infrastructure;
using System;
using System.Globalization;

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


            Console.WriteLine("\n>>>>>>>>>>Välkommen till IsBanken<<<<<<<<<<\n\n");
            Console.WriteLine("Antal kunder: " + _bank.GetCustomers().Count);
            Console.WriteLine("Antal konton: " + _bank.GetAccounts().Count);
            //todo saldo
            Console.WriteLine("Totalt saldo:");
            Console.WriteLine();
            Console.WriteLine();

            DisplayMenu();
            Console.Write("> ");
            var run = true;
            while (run)
            {
                var input = Console.ReadLine();
                if (input.ToLower().Trim() == "hjälp")
                {
                    DisplayMenu();
                    Console.WriteLine();
                    continue;
                }
                var inputParsedResult = int.TryParse(input, out int result);
                if (inputParsedResult)
                {
                    switch (result)
                    {
                        case 1:
                            SearchCustomer();
                            break;
                        case 2:
                            ShowCustomer();
                            break;
                        case 3:
                            CreateCustomer();
                            break;
                        case 4:
                            DeleteCustomer();
                            break;
                        case 5:
                            CreateAccount();
                            break;
                        case 6:
                            DeleteAccount();
                            break;
                        case 7:
                            Deposit();
                            break;
                        case 8:
                            Withdraw();
                            break;
                        case 9:
                            AddTransaction();
                            break;
                        case 0:
                            SaveFile();
                            run = false;
                            break;
                        default:
                            Console.WriteLine();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Ange ett nummer mellan 0-9");
                }
            }


        }

        private static void SaveFile()
        {
            throw new NotImplementedException();
        }

        private static void AddTransaction()
        {
            throw new NotImplementedException();
        }

        private static void Withdraw()
        {
            throw new NotImplementedException();
        }

        private static void Deposit()
        {
            throw new NotImplementedException();
        }

        private static void DeleteAccount()
        {
            throw new NotImplementedException();
        }

        private static void CreateAccount()
        {
            throw new NotImplementedException();
        }

        private static void DeleteCustomer()
        {
            throw new NotImplementedException();
        }

        private static void CreateCustomer()
        {
            throw new NotImplementedException();
        }

        private static void ShowCustomer()
        {
            throw new NotImplementedException();
        }

        private static void SearchCustomer()
        {
            throw new NotImplementedException();
        }

        static void DisplayMenu()
        {
            Console.WriteLine("HUVUDMENY");
            Console.WriteLine();
            Console.WriteLine("0. Avsluta och spara");
            Console.WriteLine("1. Sök kund");
            Console.WriteLine("2. Visa kundbild");
            Console.WriteLine("3. Skapa kund");
            Console.WriteLine("4. Ta bort kund");
            Console.WriteLine("5. Skapa konto");
            Console.WriteLine("6. Ta bort konto");
            Console.WriteLine("7. Insättning");
            Console.WriteLine("8. Uttag");
            Console.WriteLine("9. Överföring");
            Console.WriteLine();
        }
    }
}
