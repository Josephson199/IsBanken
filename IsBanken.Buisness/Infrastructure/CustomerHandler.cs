using IsBanken.Buisness.Interfaces;
using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsBanken.Buisness.Infrastructure
{
    public class CustomerHandler : ICustomerHandler
    {
        private const int CustomerDividerCount = 9;

        public Customer CreateCustomer(Customer customer)
        {
            //create customer and add to context.customer
            //make sure to create an account in bank class for this customer.
            var customerWithHighestId = Context.Customers.OrderBy(x => x.CustomerId).LastOrDefault();

            int customerId;

            if (customerWithHighestId == null)
            {
                customerId = 1;
            }
            else
            {
                customerId = customerWithHighestId.CustomerId + 1;
            }

            customer.CustomerId = customerId;


            Context.Customers.Add(customer);

            return customer;
        }

        public Dictionary<int, string> CustomerSearchByNameOrCity(string term)
        {
            var result = Context.Customers.Where(c => c.CompanyName.ToLower().Contains(term) || c.City.ToLower().Contains(term)).ToList();

            var dictionary = result.ToDictionary(c => c.CustomerId, c => c.CompanyName);

            return dictionary;
        }

        public bool DeleteCustomer(int customerId)
        {
            var customerAccounts = Context.Accounts.Where(c => c.CustomerId.Equals(customerId)).ToList();

            if (customerAccounts.Count != 0) return false;

            var customer = Context.Customers.FirstOrDefault(c => c.CustomerId.Equals(customerId));
            Context.Customers.Remove(customer);

            return true;
        }

        public Customer GetCustomer(int customerId)
        {
            return Context.Customers.FirstOrDefault(c => c.CustomerId.Equals(customerId));
        }

        public List<Customer> GetCustomers()
        {
            return Context.Customers;
        }

        public void ImportCustomers(List<string> fileLines)
        {            
            foreach (var line in fileLines)
            {
                if (line == null)
                    continue;

                var splittedLine = line.Split(';');

                if (splittedLine.Length == CustomerDividerCount)
                {
                    var customer = new Customer
                    {
                        CustomerId = Convert.ToInt32(splittedLine[0]),
                        OrganizationId = splittedLine[1],
                        CompanyName = splittedLine[2],
                        SreetAddress = splittedLine[3],
                        City = splittedLine[4],
                        Region = splittedLine[5],
                        ZipCode = splittedLine[6],
                        Country = splittedLine[7],
                        Phonenumber = splittedLine[8],
                    };

                    Context.Customers.Add(customer);
                }
            }            
        }
    }
}
