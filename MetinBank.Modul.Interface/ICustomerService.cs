using System.Collections.Generic;
using MetinBank.Entities;

namespace MetinBank.Modul.Interface
{
    /// <summary>
    /// Müşteri işlemleri için interface
    /// </summary>
    public interface ICustomerService
    {
        string? GetAllCustomers(out List<Customer>? customers);
        string? GetCustomerById(int customerId, out Customer? customer);
        string? SaveCustomer(Customer customer);
        string? DeleteCustomer(int customerId);
    }
}
