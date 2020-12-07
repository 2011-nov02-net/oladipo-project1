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
               var newOrder =  _storeRepo.AddOrder(order);
                return RedirectToAction(nameof(Details), new { id = newOrder.OrderId });
            }

            //if (ModelState.IsValid)
            //{
            //    var newOrder = new Library.Order()
            //    {
            //        CustomerId = order.CustomerId,
            //        LocationId = order.LocationId
            //    };
            //    var addNewOrder = _storeRepo.AddOrder(newOrder);
            //    return RedirectToAction(nameof(Details), new { id = addNewOrder.OrderId });
            //}
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
            var orderItem = new OrderDetailViewModel
            {
                OrderId = id,
                Quantity = 1
            };
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
                var order = _storeRepo.GetOrderById(item.OrderId);
                var inventory = _storeRepo.GetInventoryByLocationId(order.LocationId);
                var product = inventory.Find(o => o.ProductId == item.ProductId);
                if ( product.Quantity - item.Quantity < 0) 
                {
                    return RedirectToAction(nameof(Details), new { id = item.OrderId });
                }
                if ( order.OrderDetails.Any(o => o.ProductId == item.ProductId))
                {
                    foreach( var entry in order.OrderDetails)
                         {
                            if (entry.ProductId == item.ProductId)
                            {
                                entry.Quantity += item.Quantity;
                                _storeRepo.UpdateOrderDetail(entry);
                                return RedirectToAction(nameof(Details), new { id = item.OrderId });
                            }
                         };

                } else
                {
                        _storeRepo.AddOrderItem(item);

                 }

                    product.Quantity -= item.Quantity;

                    _storeRepo.UpdateInventory(product.LocationId, product.ProductId, product.Quantity);
                    return RedirectToAction(nameof(Details), new { id = item.OrderId });
               
            }
            return View("Index");
        }
    }
}