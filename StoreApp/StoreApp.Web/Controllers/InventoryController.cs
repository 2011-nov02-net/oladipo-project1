using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreApp.DataModel.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreApp.Web.Controllers
{
    public class InventoryController : Controller
    {

        private StoreAppRepository _storeRepo;

        public InventoryController(StoreAppRepository storeRepo)
        {
            _storeRepo = storeRepo;
        }
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            if ( id < 0 )
            {
                return NotFound();
            }

            var inventory = _storeRepo.GetInventoryByLocationId(id);

            return View(inventory);
        }
    }
}
