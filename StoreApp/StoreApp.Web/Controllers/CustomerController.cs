using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataModel.Repositories;

namespace StoreApplication.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private StoreAppRepository _storeRepo;

        public CustomerController(StoreAppRepository storeRepo)
        {
            _storeRepo = storeRepo;
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
            if (ModelState.IsValid)
            {
                _storeRepo.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}