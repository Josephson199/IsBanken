using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IsBanken.Buisness.Infrastructure
{
    public class FileHandler
    {
        private readonly List<string> _fileLines = new List<string>();
        private const int CustomerEnum = 4;
        private const int AccountEnum = 3;

        public void ReadFile()
        {
            using (var sr = new StreamReader(@"\IsBanken.Buisness\Files\bankdata-small.txt"))
            {
                while (sr.ReadLine() != null)
                {
                    _fileLines.Add(sr.ReadLine());
                }
            }
        }

        public void CreateCustomers()
        {
            foreach (var line in _fileLines)
            {
                var array = line.Split(';').ToArray();

                if (array.Length > CustomerEnum)
                {
                    var customer = new Customer
                    {
                        CustomerId = Convert.ToInt32(array[0]),
                        OrganizationId = array[1],
                        CompanyName = array[2],
                        SreetAddress = array[3],
                        City = array[4],
                        Region = array[5],
                        ZipCode = array[6],
                        Country = array[7],
                        Phonenumber = array[8],
                    };

                    //_bank.Customers.Add(customer);
                }
            }
        }

        public void CreateAccounts()
        {
            foreach (var line in _fileLines)
            {
                var array = line.Split(';').ToArray();

                if (array.Length > AccountEnum)
                {
                    var account = new Account
                    {
                        AccountId = Convert.ToInt32(array[0]),
                        CustomerId = Convert.ToInt32(array[1]),
                        //Balance = Convert.ToDecimal(array[2])
                    };
                    
                    //_bank.Accounts.Add(account);
                }
            }
        }

        //Save new file
    }
}
