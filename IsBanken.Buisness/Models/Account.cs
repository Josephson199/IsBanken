using System.Collections.Generic;

namespace IsBanken.Buisness.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }        
        public List<Transaction> Transactions { get; set; }
        public decimal Balance { get; set; }

    }
}
