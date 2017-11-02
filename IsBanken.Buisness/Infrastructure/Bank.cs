using IsBanken.Buisness.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsBanken.Buisness.Infrastructure
{
    public class Bank
    {
        public static List<Customer> Customers { get; } = new List<Customer>();
        public static List<Account> Accounts { get; } = new List<Account>();
        public static List<Transaction> Transactions { get; } = new List<Transaction>();
    }
}
