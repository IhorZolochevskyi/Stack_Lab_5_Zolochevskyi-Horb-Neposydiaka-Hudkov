using Microsoft.AspNetCore.Mvc;
using lab5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace lab5.Controllers
{
    public class carRentController : Controller
    {
        carRentContext _db;
        public carRentController(carRentContext ctx)
        {
            _db = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllClients()
        {
            var clients = _db.Clients.ToList();
            return Json(clients);
        }

        [HttpGet]
        public JsonResult GetAllCars()
        {
            var cars = _db.Cars.ToList();
            
            return Json(cars);
        }

        [HttpGet]
        public JsonResult GetAllDocuments()
        {
            var documents = _db.Documents.ToList();
            return Json(documents);
        }
    }
}
