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
    public class ProductController : Controller
    {
        private StoreAppRepository _storeRepo;

        public ProductController(StoreAppRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }

        //GET - Prodcut
        public IActionResult Index()
        {
            var products = _storeRepo.GetProducts();
            return View(products);
        }

        //GET - CREATE
        public ActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreApp.Library.Product product)
        {
            if (ModelState.IsValid)
            {
                _storeRepo.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}