using IsBanken.Buisness.Infrastructure;
using IsBanken.Buisness.Interfaces;
using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace IsBanken.Tests
{
    public class UnitTests
    {
        private readonly Bank _bank;

        public UnitTests()
        {
            var accountHandler = new AccountHandler();
            var customerHandler = new CustomerHandler();
            var transactionHandler = new TransactionHandler();
            var fakeFileHandler = new FakeFileHandler();

            _bank = new Bank(new FakeFileHandler(), customerHandler, transactionHandler, accountHandler);
            Seed();
        }
       

        [Fact]
        public void Test_get_customers_returns_list_of_customers()
        {
            var customers = _bank.GetCustomers();

            Assert.NotEmpty(customers);
            Assert.IsType(typeof(List<Customer>), customers);
        }

        [Fact]
        public void Test_get_customer_with_id_1_should_return_customer_with_id_1()
        {
            var customer = _bank.GetCustomer(1);

            Assert.Equal(1, customer.CustomerId);
        }


        private void Seed()
        {
            Context.Customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    City = "Stockholm",
                    CompanyName = "Ice inc.",
                    Country = "Ice",
                    OrganizationId = "0101-121-211",
                    Phonenumber = "2221212",
                    SreetAddress = "Vägen 1",
                    ZipCode = "122112"
                },
                 new Customer
                {
                    CustomerId = 2,
                    City = "Stockholm",
                    CompanyName = "Mattias inc.",
                    Country = "Sweden",
                    OrganizationId = "018901-1211-29",
                    Phonenumber = "9239808",
                    SreetAddress = "Gatan 1",
                    ZipCode = "29292"
                },
                  new Customer
                {
                    CustomerId = 3,
                    City = "Stockholm",
                    CompanyName = "Patric inc.",
                    Country = "Sweden",
                    OrganizationId = "999-121-211",
                    Phonenumber = "121455",
                    SreetAddress = "Vägen 515",
                    ZipCode = "135111"
                }
            };

            Context.Accounts = new List<Account>
            {
                //Ice konto
                new Account
                {
                    AccountId = 1,
                    Balance = 942049.00M,
                    CustomerId = 1
                },
                //Ice konto
                new Account
                {
                    AccountId = 2,
                    Balance = 9129.00M,
                    CustomerId = 1
                },
                //Mattias konto
                new Account
                {
                    AccountId = 3,
                    Balance = 9422.00M,
                    CustomerId = 2
                },
                //Mattias konto
                new Account
                {
                    AccountId = 4,
                    Balance = 919.00M,
                    CustomerId = 2
                },
                //Patric konto
                new Account
                {
                    AccountId = 5,
                    Balance = 94249.00M,
                    CustomerId = 3
                },
                 //Patric konto
                new Account
                {
                    AccountId = 6,
                    Balance = 942049.00M,
                    CustomerId = 3
                }
            };
        }
    }

    internal class FakeFileHandler : IFileHandler
    {
        public List<string> ReadFile()
        {
            throw new NotImplementedException();
        }

        public void SaveFile(List<Account> accounts, List<Customer> customers)
        {
            throw new NotImplementedException();
        }
    }
}
