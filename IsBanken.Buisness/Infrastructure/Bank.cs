﻿using IsBanken.Buisness.Interfaces;
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

        public Customer CreateCustomer(Customer customer)
        {
            var createdCustomer = _customerHandler.CreateCustomer(customer);

            return createdCustomer;

        }

        public void CreateAccount(int customerId)
        {

            _accountHandler.CreateAccount(customerId);
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

        public bool DeleteCustomer(int customerId)
        {
            return _customerHandler.DeleteCustomer(customerId);
        }

        public bool DeleteCustomerAccount(int accountId)
        {
            return _accountHandler.DeleteAccount(accountId);
        }

        public TransactionResult AddTransaction(int fromAccountId, int toAccountId, decimal amount)
        {
            return _transactionHandler.AddTransaction(toAccountId, fromAccountId, amount);
        }

        public TransactionResult Deposit(int accoundId, decimal amount)
        {
            return _transactionHandler.Deposit(accoundId, amount);
        }

        public TransactionResult Withdraw(int accountId, decimal amount)
        {
            return _transactionHandler.Withdraw(accountId, amount);
        }
    }


}
