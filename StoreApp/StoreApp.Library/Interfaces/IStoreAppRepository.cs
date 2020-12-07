using System.Collections.Generic;


namespace StoreApp.Library.Interfaces
{


    public interface IStoreAppRepository
    {

        List<Location> GetLocations();
        void AddOrderByCustomerId(int customerId, int locationId);
        void AddCustomer(Customer customer);
        List<Customer> GetCustomers();
        List<Customer> GetCustomerByName(string firstName, string lastName);
        List<Order> GetCustomerOrders(int id);

    }
}