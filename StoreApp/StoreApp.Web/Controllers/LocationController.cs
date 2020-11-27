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
    public class LocationController : Controller
    {
        private StoreAppRepository _storeRepo;

        public LocationController(StoreAppRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }

        //GET - Locations
        public IActionResult Index()
        {
            var locations = _storeRepo.GetLocations();
            return View(locations);
        }

        //GET - Location Details ( inventory and orders )
        public IActionResult Details(int id)
        {
            if ( id < 0)
            {
                return NotFound();
            }

            var location = _storeRepo.GetLocationById(id);

            TempData["LocationId"] = location.LocationId;

            return View(location);
        }

        //GET - CREATE
        public ActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoreApp.Library.Location location)
        {
            if (ModelState.IsValid)
            {
                _storeRepo.AddLocation(location);
                return RedirectToAction("Index");
            }
            return View(location);
        }
    }
}