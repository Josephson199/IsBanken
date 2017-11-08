using System.Collections.Generic;

namespace IsBanken.Buisness.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string OrganizationId { get; set; }
        public string CompanyName { get; set; }
        public string SreetAddress { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phonenumber { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
