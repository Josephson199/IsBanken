using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsBanken.Buisness.Infrastructure
{
    public class Context
    {
        public static List<Customer> Customers { get; set; } = new List<Customer>();
        public static List<Account> Accounts { get; set; } = new List<Account>();
        public static List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
