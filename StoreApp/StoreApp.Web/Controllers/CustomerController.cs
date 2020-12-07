using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataModel.Repositories;
using Microsoft.Extensions.Logging;
using StoreApp.Library.Interfaces;

namespace StoreApplication.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private IStoreAppRepository _storeRepo;

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IStoreAppRepository storeRepo, ILogger<CustomerController> logger)
        {
            _storeRepo = storeRepo;

            _logger = logger;
        }

        //GET - Customer
        public IActionResult Index(string searchString)
        {
            var customers= _storeRepo.GetCustomers();
            if (!String.IsNullOrEmpty(searchString))
            {
                string[] nameList= searchString.Split(' '); ///[firstname, lastname]
                string firstName = nameList[0];
                string lastName = nameList[1];
               customers = _storeRepo.GetCustomerByName(firstName, lastName);

                return View(customers);

            }
            return View(customers);
        }

        //GET: /<controller>/
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var orders = _storeRepo.GetCustomerOrders(id);

            return View(orders);
        }


        //GET - CREATE
        public ActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreApp.Library.Customer customer)
        {
            if (!ModelState.IsValid)
            {
            return View(customer);
            }
            try
            {

                _storeRepo.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While adding customer");
                ModelState.AddModelError("", "There was some Error, try again");

                return View();
            }
        }
    }
}