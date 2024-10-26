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
        [HttpGet]
        public JsonResult GetAvailableCarModels()
        {
            var availableCarModels = _db.Cars
                .GroupBy(car => new { car.brand, car.model, car.pricePerDay })
                .Select(group => new
                {
                    brand = group.Key.brand,   // Изменено на нижний регистр
                    model = group.Key.model,   // Изменено на нижний регистр
                    pricePerDay = group.Key.pricePerDay  // Изменено на нижний регистр
                })
                .ToList();

            return Json(availableCarModels);
        }
        [HttpPost]
        public IActionResult AddClient(string Name, int age)
        {
            if (string.IsNullOrEmpty(Name) || age <= 0)
            {
                return BadRequest("Неверные данные клиента.");
            }

            Client client = new Client
            {
                name = Name,
                age = age
            };

            _db.Clients.Add(client);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddCar(string brand, string model, string carNumber, decimal pricePerDay)
        {
            if (string.IsNullOrEmpty(brand) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(carNumber) || pricePerDay <= 0)
            {
                return BadRequest("Неверные данные машины.");
            }

            Car car = new Car
            {
                brand = brand,
                model = model,
                carNumber = carNumber,
                pricePerDay = pricePerDay
            };

            _db.Cars.Add(car);
            _db.SaveChanges();

            return Ok();
        }
    }
}
