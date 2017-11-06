using IsBanken.Buisness.Interfaces;
using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsBanken.Buisness.Infrastructure
{
    public class Bank 
    {        
        private readonly IFileHandler _fileHandler;
        private readonly ICustomerHandler _customerHandler;
        private readonly ITransactionHandler _transactionHandler;
        private readonly IAccountHandler _accountHandler;

        public Bank(IFileHandler fileHandler, 
            ICustomerHandler customerHandler, 
            ITransactionHandler transactionHandler,
            IAccountHandler accountHandler)
        {
            _fileHandler = fileHandler;
            _customerHandler = customerHandler;
            _transactionHandler = transactionHandler;
            _accountHandler = accountHandler;            
        }           

        public List<string> ReadFile()
        {
            return _fileHandler.ReadFile();
        }

        public void ImportCustomers(List<string> fileLines)
        {
            _customerHandler.ImportCustomers(fileLines);
        }

        public void ImportAccounts(List<string> fileLines)
        {
            _accountHandler.ImportAccounts(fileLines);
        }

        public List<Customer> GetCustomers()
        {
            return _customerHandler.GetCustomers();
        }

        public Customer GetCustomer(int customerId)
        {
            return _customerHandler.GetCustomer(customerId);
        }

        public List<Account> GetAccounts()
        {
            return _accountHandler.GetAccounts();
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            return _accountHandler.GetCustomerAccounts(customerId);
        }

        public Customer GetCustomerInformation(int customerId)
        {
            var customer = _customerHandler.GetCustomer(customerId);

            if(customer == null)
            {
                throw new ArgumentNullException();
            }

            var accounts = _accountHandler.GetAccounts().Where(acc => acc.CustomerId == customerId).ToList();

            customer.Accounts = accounts;

            return customer;
        }

        public Customer CreateCustomer()
        {
            //TODO Create customer 
            return new Customer();
        }

        public void CreateAccount(int customerId)
        {
            //TODO Create account for customerid with zero balance, unikt kontonummer(ta högsta accountid som finns + 1)
        }

        public Dictionary<int, string> CustomerSearch(string term)
        {
            return _customerHandler.CustomerSearchByNameOrCity(term);
        }

        public void SaveFile(List<Customer> customers, List<Account> accounts)
        {
            _fileHandler.SaveFile(accounts, customers);
        }

        public decimal GetTotalAccountBalances(int? cusomerId)
        {
            return _accountHandler.GetTotalAccountBalances(cusomerId);
        }
    }


}
