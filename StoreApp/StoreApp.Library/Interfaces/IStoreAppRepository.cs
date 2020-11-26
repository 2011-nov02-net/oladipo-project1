using System.Collections.Generic;


namespace StoreApp.Library.Interfaces
{


    public interface IStoreAppRepository
    {

        List<Library.Location> GetLocations();
        void AddOrderByCustomerId(int customerId, int locationId);
        void AddCustomer(Library.Customer customer);
        List<Library.Customer> GetCustomers();
        Library.Customer GetCustomerByName(string firstName, string lastName);

    }
}