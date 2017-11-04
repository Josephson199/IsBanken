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
                    Console.Write("> ");
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
                    Console.WriteLine("Ange ett nummer mellan 0-9 eller 'hjälp' för menyn");
                }

                Console.WriteLine();
                Console.Write("> ");
            }


        }

        private static void SaveFile()
        {
            _bank.SaveFile(_bank.GetCustomers(), _bank.GetAccounts());
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
            Console.WriteLine("* Visa kundbild *");
            Console.Write("Ange kundnr: ");
            
            var input = Console.ReadLine();
            Console.WriteLine();

            var inputParsedResult = int.TryParse(input, out int customerId);
            if (inputParsedResult)
            {
                var customer = _bank.GetCustomer(customerId);
                if (customer != null)
                {
                    Console.WriteLine($"Kundnr: {customer.CustomerId}");
                    Console.WriteLine($"Orgnr: {customer.OrganizationId}");
                    Console.WriteLine($"Namn: {customer.CompanyName}");
                    Console.WriteLine($"Adress: {customer.SreetAddress}, {customer.ZipCode} {customer.City}, {customer.Country}");
                    Console.WriteLine();

                    var customerAccounts = _bank.GetCustomerAccounts(customerId);
                    if (customerAccounts != null)
                    {
                        Console.WriteLine("Konton:");
                        foreach (var customerAccount in customerAccounts)
                        {
                            Console.WriteLine($"Kontonr: {customerAccount.AccountId}, Saldo: {customerAccount.Balance}kr");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kunden har inga konton");
                    }
                }
                else
                {
                    Console.WriteLine("Din sökning gav ingen träff");
                }
            }
            else
            {
                Console.WriteLine("Ange ett kundnr");
            }
        }

        private static void SearchCustomer()
        {
            Console.WriteLine("* Sök kund *");
            Console.Write("Ange namn eller postort: ");

            var term = Console.ReadLine();
            var result = _bank.CustomerSearch(term);

            if (result.Count > 0)
            {
                foreach (var customer in result)
                {
                    Console.WriteLine("Kundnr: {0}, Namn: {1}", customer.Key, customer.Value);
                }
            }
            else
            {
                Console.WriteLine("Din sökning gav ingen träff");
            }

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

