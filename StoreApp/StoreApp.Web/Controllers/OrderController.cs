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

     
        //CREATE

        public ActionResult Create()

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Library.Order order)
        {
          
            if (ModelState.IsValid)
            {
                _storeRepo.AddOrder(order);
                return RedirectToAction(nameof(Index), new { id = order.OrderId });
            }
            return View("Index");
        }

        public ActionResult Details(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(Library.OrderDetail item)
        {

            if (ModelState.IsValid)
            {
                _storeRepo.AddOrderItem(item);
                return RedirectToAction(nameof(Details), new { id = item.OrderId });
            }
            return View("Index");
        }
    }
}