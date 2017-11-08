﻿using IsBanken.Buisness.Infrastructure;
using System;
using System.Linq;
using IsBanken.Buisness.Models;
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
            Console.WriteLine("Antal kunder: " + _bank.GetCustomers().Count + "st");
            Console.WriteLine("Antal konton: " + _bank.GetAccounts().Count + "st");
            Console.WriteLine("Totalt saldo: " + _bank.GetTotalAccountBalances(null) + "kr");
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
            Console.WriteLine("* Sparar.. *");
            Console.WriteLine();

            _bank.SaveFile(_bank.GetCustomers(), _bank.GetAccounts());

            Console.WriteLine();

            Console.WriteLine("Antal kunder: " + _bank.GetCustomers().Count + "st");
            Console.WriteLine("Antal konton: " + _bank.GetAccounts().Count + "st");
            Console.WriteLine("Totalt saldo: " + _bank.GetTotalAccountBalances(null) + "kr");

            Console.WriteLine();
            
            Console.WriteLine("Klicka på valfri knapp för att avsluta.");
            Console.ReadLine();
        }

        private static void AddTransaction()
        {
            Console.WriteLine("* Lägg till en transaktion *");
            int fromAccountId;

            while (true)
            {
                Console.WriteLine("Ange från kontonummer: ");
                var inputFromAccountId = Console.ReadLine();
                
                var parsedSucced = int.TryParse(inputFromAccountId, out fromAccountId);

                if (parsedSucced)
                {
                    if (_bank.GetAccounts().FirstOrDefault(x => x.AccountId == fromAccountId) != null)
                    {
                        break;
                    }
                    
                }
            }

            int toAccountId;

            while (true)
            {
                Console.WriteLine("Ange till kontonummer: ");
                var inputToAccountId = Console.ReadLine();

                var parsedSucced = int.TryParse(inputToAccountId, out toAccountId);

                if (parsedSucced)
                {
                    if (_bank.GetAccounts().FirstOrDefault(x => x.AccountId == toAccountId) != null)
                    {
                        break;
                    }

                }
            }

            decimal amount;

            while (true)
            {
                Console.WriteLine("Ange belopp: ");
                var inputAmount = Console.ReadLine();
                var amountReplace = inputAmount.Replace(".", ",");

                var parsedSucced = decimal.TryParse(amountReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out amount);

                if (parsedSucced)
                {
                    break;
                }
            }

            var result = _bank.AddTransaction(fromAccountId, toAccountId, amount);

            Console.WriteLine(result.Success ? "Transaktion genomförd!" : result.ErrorMessage);
        }

        private static void Withdraw()
        {
            Console.WriteLine("* Uttag *");
            int accountId;

            while (true)
            {
                Console.WriteLine("Ange kontonummer: ");
                var accountInput = Console.ReadLine();

                var parsedSucced = int.TryParse(accountInput, out accountId);

                if (parsedSucced)
                {
                    if (_bank.GetAccounts().FirstOrDefault(x => x.AccountId == accountId) != null)
                    {
                        break;
                    }

                }
            }

            decimal amount;

            while (true)
            {
                Console.WriteLine("Ange belopp: ");
                var amountInput = Console.ReadLine();
                var amountReplace = amountInput.Replace(".", ",");

                var parsedSucced = decimal.TryParse(amountReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out amount);

                if (parsedSucced)
                {
                    break;
                }
            }

            var withdrawResult = _bank.Withdraw(accountId, amount);

            Console.WriteLine(withdrawResult.Success ? "Uttag genomförd!" : withdrawResult.ErrorMessage);
        }

        private static void Deposit()
        {
            Console.WriteLine("* Insättning *");
            int accountId;

            while (true)
            {
                Console.WriteLine("Ange kontonummer: ");
                var accountInput = Console.ReadLine();                

                var parsedSucced = int.TryParse(accountInput, out accountId);

                if (parsedSucced)
                {
                    if (_bank.GetAccounts().FirstOrDefault(x => x.AccountId == accountId) != null)
                    {
                        break;
                    }
                   
                }
            }

            decimal amount;

            while (true)
            {
                Console.WriteLine("Ange belopp: ");
                var amountInput = Console.ReadLine();

                var amountReplace = amountInput.Replace(".", ",");

                var parsedSucced = decimal.TryParse(amountReplace, NumberStyles.Currency, new CultureInfo("sv-SE"), out amount);

                if (parsedSucced)
                {
                    break;
                }
            }

            var depositResult = _bank.Deposit(accountId, amount);

            if (depositResult.Success)
            {
                Console.WriteLine("Insättning genomförd!");
            }
            else
            {
                Console.WriteLine(depositResult.ErrorMessage);
            }
        }

        private static void DeleteAccount()
        {
            Console.WriteLine("* Ta bort konto *");
            Console.Write("Ange kontonr: ");

            var input = Console.ReadLine();
            Console.WriteLine();

            var inputParsedResult = int.TryParse(input, out int accountId);
            if (inputParsedResult)
            {
                var result = _bank.DeleteCustomerAccount(accountId);

                Console.WriteLine(result ? $"Konto {accountId} borttaget" : "Kontot kan inte tas bort");
            }
            else
            {
                Console.WriteLine("Ange ett kontonr");
            }
        }

        private static void CreateAccount()
        {
            Console.WriteLine("* Skapa Konto *");
            int customerId;

            while (true)
            {
                Console.WriteLine("Ange kundnummer: ");

                var parsedSucced = int.TryParse(Console.ReadLine(), out customerId);

                if (parsedSucced)
                {
                    if (_bank.GetCustomer(customerId) != null)
                    {
                        break;
                    }
                }
            }

            _bank.CreateAccount(customerId);

            Console.WriteLine("Konto skapat");

        }

        private static void DeleteCustomer()
        {
            Console.WriteLine("* Ta bort kund *");
            Console.Write("Ange kundnr: ");

            var input = Console.ReadLine();
            Console.WriteLine();

            var inputParsedResult = int.TryParse(input, out int customerId);
            if (inputParsedResult)
            {
                var result = _bank.DeleteCustomer(customerId);

                Console.WriteLine(result ? $"Kund {customerId} borttagen" : "Kunden kan inte tas bort");
            }
            else
            {
                Console.WriteLine("Ange kundnr");
            }
        }

        private static void CreateCustomer()
        {
            Console.WriteLine("* Skapa kund *");
            string inputName;
            while (true)
            {
                Console.WriteLine("Ange namn:");
                inputName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputName))
                {
                    break;
                }
            }

            string inputAddress;
            while (true)
            {
                Console.WriteLine("Ange adress:");
                inputAddress = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inputAddress))
                {
                    break;
                }
            }

            string inputZipCode;
            while (true)
            {
                Console.WriteLine("Ange postnummer:");
                inputZipCode = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inputZipCode))
                {
                    break;
                }
            }

            string inputCity;
            while (true)
            {
                Console.WriteLine("Ange postort:");
                inputCity = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inputCity))
                {
                    break;
                }
            }

            Console.WriteLine("Ange telefonnummer:");
            var inputPhone = Console.ReadLine().Trim();

            Console.WriteLine("Ange land:");
            var inputCountry = Console.ReadLine().Trim();

            string inputOrgNumber;
            while (true)
            {
                Console.WriteLine("Ange organisationsnummer:");
                inputOrgNumber = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(inputOrgNumber))
                {
                    break;
                }

            }

            var customer = new Customer()
            {
                CompanyName = inputName,
                SreetAddress = inputAddress,
                ZipCode = inputZipCode,
                City = inputCity,
                Phonenumber = inputPhone,
                Country = inputCountry,
                OrganizationId = inputOrgNumber
            };

            var createdCustomer = _bank.CreateCustomer(customer);

            _bank.CreateAccount(createdCustomer.CustomerId);

            Console.WriteLine("Kund skapad" + " " + createdCustomer.CompanyName);
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
                    Console.WriteLine($"Telefon: {customer.Phonenumber}");
                    Console.WriteLine();

                    var customerAccounts = _bank.GetCustomerAccounts(customerId);
                    if (customerAccounts != null)
                    {
                        Console.WriteLine("Konton:");
                        foreach (var customerAccount in customerAccounts)
                        {
                            Console.WriteLine($"Kontonr: {customerAccount.AccountId}, Saldo: {customerAccount.Balance}kr");
                        }
                        var totalBalance = _bank.GetTotalAccountBalances(customerId);
                        Console.WriteLine($"Totalt saldo: {totalBalance}kr");

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
                Console.WriteLine("Ange ett korrekt kundnummer");
            }
        }

        private static void SearchCustomer()
        {
            Console.WriteLine("* Sök kund *");
            Console.Write("Ange namn eller postort: ");

            var term = Console.ReadLine();
            Console.WriteLine();

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

