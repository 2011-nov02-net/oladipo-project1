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
            var allLocations = _storeRepo.GetLocations();

            var allCustomer = _storeRepo.GetCustomers();

            var newOrder = new OrderViewModel();

            foreach (var customer in allCustomer)
            {
                newOrder.Customers.Add(customer);
            }
            foreach (var location in allLocations)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _storeRepo.AddOrderByCustomerId(order.CustomerId, order.LocationId);
                return RedirectToAction("Details");
            }
            return View();
        }
    }
}
