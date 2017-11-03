using IsBanken.Buisness.Models;
using System.Collections.Generic;

namespace IsBanken.Buisness.Interfaces
{
    public interface ICustomerHandler
    {
        void ImportCustomers(List<string> fileLines);
        List<Customer> GetCustomers();
        Customer GetCustomer(int CustomerId);
        Dictionary<int, string> CustomerSearchByNameOrCity(string term);
        void CreateCustomer();
        void DeleteCustomer(int customerId);
    }
}
