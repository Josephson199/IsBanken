using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;

namespace IsBanken.Buisness.Infrastructure
{
    public class CustomerHandler
    {
        private const int CustomerDividerCount = 9;

        public void CreateCustomers(List<string> fileLines)
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

                    Bank.Customers.Add(customer);
                }
            }
        }
    }
}
