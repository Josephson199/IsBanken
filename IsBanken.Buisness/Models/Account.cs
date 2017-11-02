using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsBanken.Buisness.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }        
        public List<Transaction> Transactions { get; set; }
        public decimal Balance {
            get { return Balance; }
            set
            {
                if (value + Balance < 0)
                    throw new ArgumentOutOfRangeException("Only positive values are allowed");

                Balance = value;
            }
        }
    }
}
