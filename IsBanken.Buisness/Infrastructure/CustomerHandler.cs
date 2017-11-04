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

        public void CreateCustomer()
        {
            //create customer and add to context.customer
            //make sure to create an account in bank class for this customer.
            throw new NotImplementedException();
        }

        public Dictionary<int, string> CustomerSearchByNameOrCity(string term)
        {
            var result = Context.Customers.Where(c => c.CompanyName.Contains(term) || c.City.Contains(term)).ToList();

            var dictionary = result.ToDictionary(c => c.CustomerId, c => c.CompanyName);

            return dictionary;
        }

        public void DeleteCustomer(int customerId)
        {
            //Ta bara bort customer om customer inte har några konton.
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int CustomerId)
        {
            throw new NotImplementedException();
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
