using System;
using System.Collections.Generic;
using System.Text;

namespace IsBanken.Buisness.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
