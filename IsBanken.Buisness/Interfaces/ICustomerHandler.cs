using IsBanken.Buisness.Models;
using System.Collections.Generic;

namespace IsBanken.Buisness.Interfaces
{
    public interface ICustomerHandler
    {
        void ImportCustomers(List<string> fileLines);
        List<Customer> GetCustomers();
        Customer GetCustomer(int customerId);
        Dictionary<int, string> CustomerSearchByNameOrCity(string term);
        Customer CreateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
    }
}
