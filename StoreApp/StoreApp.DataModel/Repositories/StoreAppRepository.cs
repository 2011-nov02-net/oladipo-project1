using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoreApp.Library.Interfaces;

namespace StoreApp.DataModel.Repositories
{
    /// <summary>
    /// A repository managing data access for a store objects and their members,
    /// using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This class ought to have better exception handling and logging.
    /// </remarks>
    public class StoreAppRepository : IStoreAppRepository
    {
        private readonly DbContextOptions<project0Context> _dbContext;

        /// <summary>
        /// Initializes a new StoreApp Repository given a suitable store data source.
        /// </summary>
        /// <param name="dbContext">The data source</param>
        public StoreAppRepository(DbContextOptions<project0Context> dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get all locations with deferred execution,
        /// including associated reviews.
        /// </summary>
        /// <returns>The collection of Locations</returns>
        public List<Library.Location> GetLocations()
        {

            using var context = new project0Context(_dbContext);

            var dbLocations = context.Locations.ToList();

            var appLocations = dbLocations.Select(l => new Library.Location(l.LocationId, l.Name, l.Address, l.City, l.State)).ToList();


            return appLocations;
        }
        /// <summary>
        /// Get a Location by ID.
        /// </summary>
        /// <returns>The Location</returns>
        public Library.Location GetLocationById(int locationId)
        {
            using var context = new project0Context(_dbContext);

            var dbLocation = context.Locations
                .Where(o => o.LocationId == locationId)
                .FirstOrDefault();
            var location = new Library.Location()
            {
                LocationId = dbLocation.LocationId,
                Name = dbLocation.Name,
                Address = dbLocation.Address,
                City = dbLocation.City,
                State = dbLocation.State
            };
            var locationInventory = GetInventoryByLocation(location);
            foreach (var product in locationInventory)
            {
                location.Inventory.Add(product);
            }
            return location;
        }

        public List<Library.Inventory> GetInventoryByLocation(Library.Location location)
        {
            using var context = new project0Context(_dbContext);

            var dbInventroy = context.Inventories
                .Where(o => o.LocationId == location.LocationId)
                .Include(o => o.Product)
                .ToList();

            var inventory = new List<Library.Inventory>();
            foreach (var inv in dbInventroy)
            {
                var newProduct = new Library.Inventory(inv.LocationId, inv.Product.ProductId, inv.Quantity);
                newProduct.ProductId = inv.ProductId;
                inventory.Add(newProduct);

            }
            return inventory;
        }

        public List<OrderDetail> GetOrderDetails(int id)
         {
            using var context = new project0Context(_dbContext);

            var dbOrders = context.OrderDetails
            .Include(o => o.Product)
            .Include(o => o.Order)
            .Where(o => o.OrderId == id);

            var products= new List<OrderDetail>();
            foreach (var product in dbOrders)
            { 
                products.Add(product);
            }
            return products;

        }

         /// <summary>
        /// Update an Inventory
        /// </summary>
        /// <returns>The Inventory with changed values</return>
        public void  UpdateInventory(int locationId, int productId, int quantity)
        {
            using var context = new project0Context(_dbContext);

            var dbInventory = context.Inventories
                .Where(o => o.LocationId == locationId && o.ProductId == productId)
                .FirstOrDefault();

            dbInventory.Quantity = quantity;

            context.SaveChanges();
        
        }

        /// <summary>
        /// Gets all customers from database
        /// </summary>
        /// <returns>The Customers</returns>
        public List<Library.Customer> GetCustomers()
        {
            using var context = new project0Context(_dbContext);

            var dbCustomers = context.Customers.ToList();

            var appCustomer = dbCustomers.Select(c => new Library.Customer(c.CustomerId, c.FirstName, c.LastName, c.Email)).ToList();

            var customers = new List<Library.Customer>();
            foreach (var customer in appCustomer)
            {
                customers.Add(customer);
            };
            return appCustomer;
        }

        ///<summary>
        ///Gets alll products from database
        ///</summary>
        ///<returns>List of Products</returns>
        public List<Library.Product> GetProducts()
        {
            using var context = new project0Context(_dbContext);

            var dbProducts = context.Products.ToList();

            var appProduct= dbProducts.Select(o => new Library.Product(o.ProductId,o.Name, o.Price)).ToList();

            var prodcuts = new List<Library.Product>();
            foreach( var product in appProduct)
            {
                prodcuts.Add(product);
            };

            return appProduct;
        }


        /// <summary>
        /// Gets all others for a particular customer
        /// </summary>
        /// <returns>Customer's Orders</param>

        public List<Order> GetCustomerOrders(int customerId)
        {
            using var context = new project0Context(_dbContext);
            var dbOrders = context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Location)
            .Where(o => o.CustomerId == customerId);

            var orders = new List<Order>();
            foreach (var order in dbOrders)
            {
                orders.Add(order);
            }
            return orders;
        }



        /// <summary>
        /// Gets Orders for a particular location
        /// </summary>
        /// <returns>Location's Orders</returns>
        public List<Order> GetOrdersByLocationId(int locationId)
        {
            using var context = new project0Context(_dbContext);
            var dbOrders = context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Location)
            .Where(o => o.LocationId == locationId);

            var orders = new List<Order>();
            foreach (var order in dbOrders)
            {
                orders.Add(order);
            }

            return orders;

        }
        /// <summary>
        /// Adds a customer
        /// </summary>
        /// <param name="customer">Added customer</param>
        public void AddCustomer(Library.Customer customer)
        {
            using var context = new project0Context(_dbContext);

            var dbCustomer = new Customer()
            {

                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            context.Add(dbCustomer);
            context.SaveChanges();

        }

        public void AddOrderItem(Library.OrderDetail orderItem)
        {
            using var context = new project0Context(_dbContext);

            var dbItem = new OrderDetail()
            {
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity
            };

            context.Add(dbItem);
            context.SaveChanges();
        }
        public Order AddOrder(Library.Order order)
        {
            using var context = new project0Context(_dbContext);

            var dbOrder = new Order()
            {
                CustomerId = order.CustomerId,
                LocationId = order.LocationId,
                Date       = order.Date

            };

            //var appOrder = new Library.Order()
            //{
            //    CustomerId = dbOrder.CustomerId,
            //    LocationId = dbOrder.LocationId,
            //    Date = dbOrder.Date
            //};

            context.Add(dbOrder);
            context.SaveChanges();

            return GetOrderById(dbOrder.OrderId);

        }


        public void AddProduct(Library.Product product)
        {
            using var context = new project0Context(_dbContext);

            var dbProduct = new Product()
            {
                Name = product.Name,
                Price = product.Price
            };

            context.Add(dbProduct);
            context.SaveChanges();
        }

        public void AddLocation(Library.Location location)
        {
            using var context = new project0Context(_dbContext);

            var dbLocation = new Library.Location()
            {
                Name = location.Name,
                Address = location.Address,
                City = location.City,
                State = location.State
        };
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            using var context = new project0Context(_dbContext);

            var dbOrderDetail = context.OrderDetails
                .Where(o => o.OrderId == orderDetail.OrderId)
                .FirstOrDefault();

            dbOrderDetail.Quantity = orderDetail.Quantity;

            context.SaveChanges();    
        }



        /// <summary>
        /// Adds order to a particular customer
        /// </summary>
        /// <param name="order">Order Added</param>
        public void AddOrderByCustomerId(int customerId, int locationId)
        {
            using var context = new project0Context(_dbContext);
            var appOrders = new Library.Order()
            {
                CustomerId = customerId,
                LocationId = locationId,

            };
            var dbOrders = new DataModel.Order()
            {
                CustomerId = appOrders.CustomerId,
                LocationId = appOrders.LocationId,
                // OrderDetails.Quantity = quantity,
            };

            context.Add(dbOrders);
            context.SaveChanges();
        }

     
        

        /// <summary>
        /// Gets Inventory of a Location
        /// </summary>
        /// <returns>Location's inventory</returns>
        public List<Inventory> GetInventoryByLocationId(int locationId)
        {
            using var context = new project0Context(_dbContext);

            var dbInventory = context.Inventories
            .Include(i => i.Product)
            .Include(i => i.Location)
            .Where(i => i.LocationId == locationId);

            List<Inventory> Inventories = new List<Inventory>();
            foreach (var inventory in dbInventory)
            {
                Inventories.Add(inventory);
            }
            return Inventories;
        }

        /// <summary>
        /// Gets Orders By Id
        /// </summary>
        /// <returns> Order</returns>
        public Order GetOrderById(int orderId)
        {
            using var context = new project0Context(_dbContext);
            var dbOrders = context.Orders
            .Where(o => o.OrderId == orderId)
            .Include(o => o.Customer)
            .Include(o => o.Location)
            .FirstOrDefault();

            var order = new Order()
            {
                OrderId = dbOrders.OrderId,
                Date    = dbOrders.Date,
                CustomerId = dbOrders.CustomerId,
                LocationId = dbOrders.LocationId

            };
            return dbOrders;
        }

        public Library.Customer GetCustomerById( int customerId)
        {
            using var context = new project0Context(_dbContext);
            var dbCustomer = context.Customers.FirstOrDefault(o => o.CustomerId == customerId);
            var appCustomer = new Library.Customer(dbCustomer.CustomerId,dbCustomer.FirstName, dbCustomer.LastName, dbCustomer.Email);
            return appCustomer;
        }

        /// <summary>
        /// Gets Customer By Name
        /// </summary>
        /// <returns>Customer</returns>
        public List<Library.Customer> GetCustomerByName(string firstName, string lastName)
        {
            using var context = new project0Context(_dbContext);
            var dbCustomers = context.Customers.First(c => c.FirstName.ToLower() == firstName.ToLower() && c.LastName.ToLower() == lastName.ToLower());

            var customer = new Library.Customer(dbCustomers.CustomerId, dbCustomers.FirstName, dbCustomers.LastName, dbCustomers.Email);

            var appCustomer = new List<Library.Customer>();
            appCustomer.Add(customer);

            return appCustomer;
        }


    }
}


