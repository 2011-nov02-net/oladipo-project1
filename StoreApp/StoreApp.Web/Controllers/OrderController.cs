   using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataModel.Repositories;
using StoreApp.Web.Models;
using System.Dynamic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApp.Web.Controllers
{
    public class OrderController : Controller
    {

        private StoreAppRepository _storeRepo;

        public OrderController(StoreAppRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var inventory = _storeRepo.GetOrdersByLocationId(id);

            return View(inventory);
        }

        public IActionResult Create()

        {
            var locations = _storeRepo.GetLocations();

            var customers = _storeRepo.GetCustomers();

            var newOrder = new OrderViewModel();

            foreach (var customer in customers)
            {
                newOrder.Customers.Add(customer);
            }
            foreach (var location in locations)
            {
                newOrder.Locations.Add(location);
            };

            return View(newOrder);
        }



        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            };

            dynamic mymodel = new ExpandoObject();

            mymodel.Order = _storeRepo.GetOrderById(id);

            mymodel.OrderDetails = _storeRepo.GetOrderDetails(id);


            return View(mymodel);
        }

        //POST - CREATE
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(OrderViewModel order)
        //{
        //    var customer = _storeRepo.GetCustomerById(order.CustomerId);
        //    var location = _storeRepo.GetLocationById(order.LocationId);

        //    var newOrder = new DataModel.Order
        //    {
        //        Customer = customer,
        //        Location = location,
        //        Date = DateTime.Now
        //    };

        //    _storeRepo.InsertOrder(newOrder);

        //    return RedirectToAction("Index");

        //}
        public ActionResult AddItem(int id)
        {
            var orderItem = new OrderDetailViewModel();

            orderItem.OrderId = id;
            orderItem.Quantity = 1;
            var products = _storeRepo.GetProducts();
            foreach (var product in products)
            {
                orderItem.Products.Add(product);
            }
            return View(orderItem);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddItem(OrderDetailViewModel orderDetail)
        //{
        //    t
        //}
    }
}